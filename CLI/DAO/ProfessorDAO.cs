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
    public Professor AddProfessor(Professor professor)
    {
        professor.Address.ProfessorId = professor.Idnumber;
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
        return _professors.Find(p => p.Idnumber == id);
    }

    public Professor? RemoveProfessor(int id)
    {
        Professor? professor = GetProfessorByIdNumber(id);
        if (professor == null) return null;

        _professors.Remove(professor);
        _professorStorage.Save(_professors);
        return professor;
    }

    public List<Professor> GetAllProfessors()
    {
        return _professors;
    }
}