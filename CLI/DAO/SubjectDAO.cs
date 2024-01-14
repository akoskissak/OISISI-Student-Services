using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO;

public class SubjectDAO
{
    private readonly List<Subject> _subjects;
    private readonly Storage<Subject> _subjectStorage;

    public Observable SubjectObservable;

    public SubjectDAO()
    {
        _subjectStorage = new Storage<Subject>("subjects.txt");
        _subjects = _subjectStorage.Load();
        SubjectObservable = new Observable();
    }
    private int GenerateSubjectId()
    {
        if (_subjects.Count == 0) 
            return 0;
        return _subjects[^1].Id + 1;
    }
    public Subject AddSubject(Subject subject)
    {
        subject.Id = GenerateSubjectId();
        _subjects.Add(subject);
        _subjectStorage.Save(_subjects);
        SubjectObservable.NotifyObservers();

        return subject;
    }

    public void AddStudentPassedForSubject(ExamGrade examGrade)
    {
        Subject subject = _subjects.Find(s => s.Id == examGrade.SubjectId)!;
        subject.StudentsPassed.Add(examGrade.Student);
        subject.StudentsDidNotPass.Remove(examGrade.Student);
        _subjectStorage.Save(_subjects);
        SubjectObservable.NotifyObservers();
    }

    public void RemoveStudentPassedForSubject(ExamGrade examGrade)
    {
        Subject subject = _subjects.Find(s => s.Id == examGrade.SubjectId)!;
        subject.StudentsPassed.Remove(examGrade.Student);
        subject.StudentsDidNotPass.Add(examGrade.Student);
        _subjectStorage.Save(_subjects);
        SubjectObservable.NotifyObservers();
    }

    public void RemoveStudentDidNotPass(Student student, int subjectId)
    {
        Subject subject = _subjects.Find(s => s.Id == subjectId)!;
        subject.StudentsDidNotPass.Remove(student);
        _subjectStorage.Save(_subjects);
        SubjectObservable.NotifyObservers();
    }

    public Subject GetSubjectAddStudent(Student student)
    {
        Subject subject = _subjects.First();
        subject.StudentsDidNotPass.Add(student);
        _subjectStorage.Save(_subjects);
        SubjectObservable.NotifyObservers();
        return subject;
    }

    public Subject? UpdateSubject(Subject subject)
    {
        Subject? oldSubject = GetSubjectById(subject.Id);
        if (oldSubject is null) return null;

        oldSubject.SubjectCode = subject.SubjectCode;
        oldSubject.Name = subject.Name;
        oldSubject.ProfessorId = subject.ProfessorId;
        oldSubject.Semester = subject.Semester;
        oldSubject.YearOfStudy = subject.YearOfStudy;
        oldSubject.Espb = subject.Espb;
        oldSubject.Professor = subject.Professor;
        
        _subjectStorage.Save(_subjects);
        SubjectObservable.NotifyObservers();
        return oldSubject;
    }

    public Subject? RemoveSubject(int id)
    {
        Subject? subject = GetSubjectById(id);
        if (subject == null)
            return null;

        if (subject.StudentsDidNotPass.Count != 0 || subject.StudentsPassed.Count != 0 || subject.ProfessorId != -1)
        {
            System.Console.WriteLine("Cannot remove subject that has students!\nRemove them first and then you can remove subject.");
            Subject subj = new Subject();
            subj.Id = -1;
            return subj;
        }

        _subjects.Remove(subject);
        _subjectStorage.Save(_subjects);
        SubjectObservable.NotifyObservers();
        return subject;
    }

    public void AddStudentsForSubject(List<Student> students, int id)
    {
        _subjects.Find(subject => subject.Id == id)!.StudentsDidNotPass = students;
        _subjectStorage.Save(_subjects);
        SubjectObservable.NotifyObservers();
    }

    public void AddStudentForSubject(Student student, int subjectId)
    {
        _subjects.Find(subject => subject.Id == subjectId)!.StudentsDidNotPass.Add(student);
        _subjectStorage.Save(_subjects);
        SubjectObservable.NotifyObservers();
    }

    public void SaveSubjects()
    {
        _subjectStorage.Save(_subjects);
        SubjectObservable.NotifyObservers();
    }

    public Subject? GetSubjectById(int id)
    {
        return _subjects.Find(s => s.Id == id);
    }

    public List<Subject> GetAllSubjects()
    {
        return _subjects;
    }
    public void NotifyObservers()
    {
        SubjectObservable.NotifyObservers();
    }
    
    public List<Subject>? FindSubjectsByText(string text)
    {
        string[] inputs = text.Split(',');
        if (inputs.Length == 1)
        {
            string subjectName = inputs[0];
            return _subjects.FindAll(subject => subject.Name == subjectName);
        }
        else if (inputs.Length == 2)
        {
            string subjectName = inputs[0];
            string subjectCode = inputs[1];
            return _subjects.FindAll(subject => subject.Name == subjectName && subject.SubjectCode == subjectCode);
        }

        return null;
    }

    public void SetSubjectProfessor(int subjectId, Professor professor)
    {
        Subject? subject = GetSubjectById(subjectId);
        subject.Professor = professor;
        subject.ProfessorId = professor.Id;

        _subjectStorage.Save(_subjects);
        SubjectObservable.NotifyObservers();

    }

    public List<Subject>? FindSubjectsByProfessorId(int professorId)
    {
        return _subjects.FindAll(subject => subject.ProfessorId == professorId);
    }
}