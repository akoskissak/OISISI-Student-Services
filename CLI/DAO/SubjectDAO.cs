using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class SubjectDAO
{
    private readonly List<Subject> _subjects;
    private readonly Storage<Subject> _subjectStorage;
    public List<Subject> getSubjects()
    {
        return _subjects;
    }
    public SubjectDAO()
    {
        _subjectStorage = new Storage<Subject>("subjects.txt");
        _subjects = _subjectStorage.Load();
    }
    private int GenerateSubjectId()
    {
        if (_subjects.Count == 0) 
            return 0;
        return _subjects[^1].SubjectCode + 1;
    }
    public Subject AddSubject(Subject subject)
    {
        subject.SubjectCode = GenerateSubjectId();
        _subjects.Add(subject);
        _subjectStorage.Save(_subjects);
        return subject;
    }

    public Subject? UpdateSubject(Subject subject)
    {
        Subject? oldSubject = GetSubjectById(subject.SubjectCode);
        if (oldSubject is null) return null;

        oldSubject.SubjectCode = subject.SubjectCode;
        oldSubject.Name = subject.Name;
        oldSubject.ProfessorId = subject.ProfessorId;
        oldSubject.Semestar = subject.Semestar;
        oldSubject.YearOfStudy = subject.YearOfStudy;
        oldSubject.Espb = subject.Espb;
        
        _subjectStorage.Save(_subjects);
        return oldSubject;
    }

    public Subject? RemoveSubject(int id)
    {
        Subject? subject = GetSubjectById(id);
        if (subject == null)
            return null;

        _subjects.Remove(subject);
        _subjectStorage.Save(_subjects);
        return subject;
    }

    public Subject? GetSubjectById(int id)
    {
        return _subjects.Find(s => s.SubjectCode == id);
    }

    public List<Subject> GetAllSubjects()
    {
        return _subjects;
    }
}