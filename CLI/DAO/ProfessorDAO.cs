using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO;

public class ProfessorDAO
{
    private List<Professor> _professors;
    private readonly Storage<Professor> _professorStorage;

    private readonly List<ProfessorDepartment> _professorDepartments;
    private readonly Storage<ProfessorDepartment> _professorDepartmentStorage;

    public Observable ProfessorObservable;

    public ProfessorDAO()
    {
        _professorStorage = new Storage<Professor>("professors.txt");
        _professors = _professorStorage.Load();

        _professorDepartmentStorage = new Storage<ProfessorDepartment>("professorDepartment.txt");
        _professorDepartments = _professorDepartmentStorage.Load();
        ProfessorObservable = new Observable();
    }

    private int GenerateProfessorId()
    {
        if (_professors.Count == 0)
            return 0;
        return _professors.Max(p => p.Id) + 1;
    }
    public Professor AddProfessor(Professor professor)
    {
        professor.Id = GenerateProfessorId();
        _professors.Add(professor);
        _professorStorage.Save(_professors);
        ProfessorObservable.NotifyObservers();
        
        return professor;
    }

    public Professor? UpdateProfessor(Professor professor)
    {
        Professor? oldProfessor = GetProfessorById(professor.Id);
        if (oldProfessor is null)
            return null;

        oldProfessor.Lastname = professor.Lastname;
        oldProfessor.Name = professor.Name;
        oldProfessor.DateOfBirth = professor.DateOfBirth;
        oldProfessor.Address = professor.Address;
        oldProfessor.PhoneNumber = professor.PhoneNumber;
        oldProfessor.Email = professor.Email;
        oldProfessor.Idnumber = professor.Idnumber;
        oldProfessor.Title = professor.Title;
        oldProfessor.YearsOfService = professor.YearsOfService;
        oldProfessor.IdOfChiefDepartment = professor.IdOfChiefDepartment;

        _professorStorage.Save(_professors);
        ProfessorObservable.NotifyObservers();
        return oldProfessor;
    }

    public Professor? GetProfessorById(int id)
    {
        return _professors.Find(p => p.Id == id);
    }

    public Professor? RemoveProfessor(int id)
    {
        Professor? professor = GetProfessorById(id);
        if (professor == null) return null;

        if (professor.Subjects.Count != 0 || professor.IdOfChiefDepartment != -1)
        {
            System.Console.WriteLine("Cannot remove professor that has subject/s or is chief!");
            Professor prof = new Professor();
            prof.Id = -1;
            return prof;
        }

        List<ProfessorDepartment> listpd = _professorDepartments.FindAll(p => p.ProfessorId == id);
        if (listpd.Count > 0)
        {
            foreach (ProfessorDepartment pd in listpd)
            {
                _professorDepartments.Remove(pd);
            }
            _professorDepartmentStorage.Save(_professorDepartments);
        }

        _professors.Remove(professor);
        _professorStorage.Save(_professors);
        ProfessorObservable.NotifyObservers();
        return professor;
    }

    public void AddSubjectsForProfessor(List<Subject> subjects, int Id)
    {
        _professors.Find(professor => professor.Id == Id)!.Subjects = subjects;
        _professorStorage.Save(_professors);
    }

    public List<Professor> GetAllProfessors()
    {
        return _professors;
    }
    public void SaveProfessors()
    {
        _professorStorage.Save(_professors);
        ProfessorObservable.NotifyObservers();
    }

    public void NotifyObservers()
    {
        ProfessorObservable.NotifyObservers();
    }

    public List<Professor>? FindProfessorsByText(string text)
    {
        string[] inputs = text.Split(',');
        if (inputs.Length == 1)
        {
            string lastname = inputs[0];
            return _professors.FindAll(professor => professor.Lastname == lastname);
        }
        else if (inputs.Length == 2)
        {
            string lastname = inputs[0];
            string name = inputs[1].Trim();
            return _professors.FindAll(professor => professor.Lastname == lastname && professor.Name == name);
        }

        return null;
    }
    
    public Professor? FindProfessorById(int professorId)
    {
        return _professors.Find(p => p.Id == professorId);
    }

    public bool CanProfessorBeAChief(int professorId)
    {
        Professor professor = GetProfessorById(professorId);
        if(string.Equals(professor.Title, "vanredni profesor", StringComparison.OrdinalIgnoreCase) || string.Equals(professor.Title, "redovni profesor", StringComparison.OrdinalIgnoreCase))
        {
            if(professor.YearsOfService >= 5)
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public void SortProfessors(string columnName, int sortDirection)
    {
        switch (columnName)
        {
            case "Name":
                _professors = _professors.OrderBy(p => p.Name).ThenBy(p=>p.Lastname).ToList();
                break;
            case "Lastname":
                _professors = _professors.OrderBy(p => p.Lastname).ThenBy(p => p.Name).ToList();
                break;
            case "Title":
                _professors = _professors.OrderBy(p => p.Title).ThenBy(p => p.Name).ToList();
                break;
            case "E-mail":
                _professors = _professors.OrderBy(p => p.Email).ToList();
                break;
        }
        
        if (sortDirection == 1)
        {
            _professors.Reverse();
        }

        _professorStorage.Save(_professors);
        ProfessorObservable.NotifyObservers();
    }

    public List<Professor> GetSortedSearchedProfessors(List<int> ids)
    {
        List<Professor> retVal = new List<Professor>();
        foreach (int id in ids)
        {
            Professor? professor = _professors.Find(p => p.Id == id);
            if (professor != null)
            {
                retVal.Add(professor);
            }
        }

        return retVal;
    }
}