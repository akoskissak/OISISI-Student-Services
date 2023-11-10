using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class StudentSubjectDAO
{
    private readonly List<StudentSubject> _studentSubject;
    private readonly Storage<StudentSubject> _studentSubjectStorage;

    public StudentSubjectDAO()
    {
        _studentSubjectStorage = new Storage<StudentSubject>("studentSubject.txt");
        _studentSubject = _studentSubjectStorage.Load();
    }
}