using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO;

public class ProfessorDAO
{
    private readonly List<Professor> _professors;
    private readonly Storage<Professor> _professorStorage;

    public Observable ProfessorObservable;

    public ProfessorDAO()
    {
        _professorStorage = new Storage<Professor>("professors.txt");
        _professors = _professorStorage.Load();
        ProfessorObservable = new Observable();
    }

    private int GenerateProfessorId()
    {
        if (_professors.Count == 0)
            return 0;
        return _professors[^1].Id + 1;
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
        
        _professorStorage.Save(_professors);
        ProfessorObservable.NotifyObservers();
        return oldProfessor;
    }

    private Professor? GetProfessorById(int id)
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
}