using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class SubjectDAO
{
    private readonly List<Subject> _subjects;
    private readonly Storage<Subject> _subjectStorage;


    public SubjectDAO()
    {
        _subjectStorage = new Storage<Subject>("subjects.txt");
        _subjects = _subjectStorage.Load();
    }

    public Subject AddSubject(Subject subject)
    {
        _subjects.Add(subject);
        _subjectStorage.Save(_subjects);
        return subject;
    }
}