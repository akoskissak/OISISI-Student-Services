using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class ProfessorDAO
{
    private readonly List<Professor> _professors;
    private readonly List<Address> _addresses;
    private readonly Storage<Professor> _professorStorage;
    private readonly Storage<Address> _addressStorage;

    public ProfessorDAO()
    {
        _professorStorage = new Storage<Professor>("professors.txt");
        _professors = _professorStorage.Load();
        _addressStorage = new Storage<Address>("addresses.txt");
        _addresses = _addressStorage.Load();
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
        
        _addresses.Add(professor.Address);
        _addressStorage.Save(_addresses);
        return professor;
    }

    public Professor? UpdateProfessor(Professor professor)
    {
        Professor? oldProfessor = GetProfessorByIdNumber(professor.Idnumber);
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
        return oldProfessor;
    }

    private Professor? GetProfessorByIdNumber(int id)
    {
        return _professors.Find(p => p.Id == id);
    }

    public Professor? RemoveProfessor(int id)
    {
        Professor? professor = GetProfessorByIdNumber(id);
        if (professor == null) return null;

        if (professor.Subjects.Count != 0 || professor.IdOfChiefDepartment != -1)
        {
            System.Console.WriteLine("Cannot remove professor that has subject/s or is chief!");
            return null;
        }

        _professors.Remove(professor);
        _professorStorage.Save(_professors);
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
}