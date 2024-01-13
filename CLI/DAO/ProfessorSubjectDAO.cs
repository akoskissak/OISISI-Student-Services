using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO;

public class ProfessorSubjectDAO
{
    private readonly List<ProfessorSubject> _professorSubject;
    private readonly List<Subject> _subject;
    private readonly Storage<ProfessorSubject> _professorSubjectStorage;
    private readonly Storage<Subject> _subjectStorage;

    private ProfessorDAO _professorDao;
    private SubjectDAO _subjectDao;

    public Observable ProfessorSubjectObservable;
    public ProfessorSubjectDAO(ProfessorDAO professorDao, SubjectDAO subjectDao)
    {
        _professorSubjectStorage = new Storage<ProfessorSubject>("professorSubject.txt");
        _professorSubject = _professorSubjectStorage.Load();

        _subjectStorage = new Storage<Subject>("subjects.txt");
        _subject = _subjectStorage.Load();

        _professorDao = professorDao;
        _subjectDao = subjectDao;

        HashSet<Subject> subjectsForProfessor = new HashSet<Subject>();

        foreach (Professor professor in professorDao.GetAllProfessors())
        {
            foreach (Subject subject in subjectDao.GetAllSubjects())
            {
                foreach (ProfessorSubject ps in _professorSubject)
                {
                    if (subject.Id == ps.SubjectId && professor.Id == ps.ProfessorId)
                    {
                        subjectsForProfessor.Add(subject);
                        subject.ProfessorId = professor.Id;
                        subject.Professor = professor;
                        break;
                    }
                }
            }

            if (subjectsForProfessor.Count > 0)
                professorDao.AddSubjectsForProfessor(subjectsForProfessor.ToList(), professor.Id);
            subjectsForProfessor.Clear();
        }

    }

    public List<Professor>? FindAllProfessorsForSubjects(List<Subject> unsubmittedSubjects)
    {
        List<Professor> professors = new List<Professor>();
        foreach (Subject subject in unsubmittedSubjects)
        {
            Professor p = GetProfessorBySubjectId(subject.Id);
            if (p != null)
                professors.Add(p);
        }

        if (professors.Count > 0)
            return professors.Distinct().ToList();
        return null;
    }

    public Professor? GetProfessorBySubjectId(int subjectId)
    {
        ProfessorSubject? ps = _professorSubject.Find(ps => ps.SubjectId == subjectId);
        if (ps != null)
            return _professorDao.FindProfessorById(ps.ProfessorId);
        return null;
    }

    private int GenerateProfessorSubjectId()
    {
        if (_professorSubject.Count == 0)
            return 0;
        return _professorSubject[^1].Id + 1;
    }

    public void SetProfessorSubject(int professorId, int subjectId)
    {
        if(professorId != -1){
            ProfessorSubject ps = new ProfessorSubject(professorId, subjectId);
            ps.Id = GenerateProfessorSubjectId();
            _professorSubject.Add(ps);
            _professorSubjectStorage.Save(_professorSubject);
        }
    }
    public void SetProfessorForSubject(int subjectId, Professor professor)
    {
        Subject? subject = _subjectDao.GetSubjectById(subjectId);
        subject.Professor = professor;
        subject.ProfessorId = professor.Id;
        professor.Subjects.Add(subject);
        ProfessorSubject? ps = _professorSubject.Find(ps => ps.SubjectId == subjectId);
        if (ps != null)
        {
            ps.ProfessorId = subject.ProfessorId;

        }
        else
        {
            ProfessorSubject profs = new ProfessorSubject(subject.ProfessorId, subjectId);
            profs.Id = GenerateProfessorSubjectId();
            _professorSubject.Add(profs);
        }

        _professorSubjectStorage.Save(_professorSubject);
    }

    public bool RemoveProfessorFromSubject(int subjectId)
    {
        Subject? subject = _subjectDao.GetSubjectById(subjectId);
        Professor? professor = _professorDao.GetProfessorById(subject.ProfessorId);
        if (professor != null && subject != null)
        {
            ProfessorSubject? ps = _professorSubject.Find(ps => ps.SubjectId == subjectId && ps.ProfessorId == subject.ProfessorId);

            subject.Professor = null;
            subject.ProfessorId = -1;

            professor.Subjects.Remove(subject);

            _professorSubject.Remove(ps);

            _professorSubjectStorage.Save(_professorSubject);
            _subjectDao.UpdateSubject(subject);
            return true;
        }
        else
            return false;

    }

    public List<Subject> GetAllSubjectsByProfessorId(int professorId)
    {
        List<Subject> subjectsForProfessor = new List<Subject>();

        List<ProfessorSubject> allps = _professorSubject.FindAll(ps => ps.ProfessorId == professorId);
        if (allps.Count > 0)
        {
            foreach (ProfessorSubject ps in allps)
            {
                Subject? s = _subjectDao.GetSubjectById(ps.SubjectId);
                subjectsForProfessor.Add(s);
            }
        }

        return subjectsForProfessor;
    }

    public List<Subject> GetAllSubjectsNotByProfessorId(int professorId)
    {
        List<Subject> subjectsForProfessor = _subject.FindAll(s => s.ProfessorId == -1);
        return subjectsForProfessor;
    }
    public void NotifyObservers()
    {
        ProfessorSubjectObservable.NotifyObservers();
    }

}