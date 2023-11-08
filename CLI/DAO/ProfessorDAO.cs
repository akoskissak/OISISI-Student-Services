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
}