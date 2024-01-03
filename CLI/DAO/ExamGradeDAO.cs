using CLI.Console;
using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO;

public class ExamGradeDAO
{
    private readonly List<ExamGrade> _examGrades;
    private readonly Storage<ExamGrade> _examGradeStorage;

    private StudentDAO _studentDao;
    private SubjectDAO _subjectDao;

    public Observable ExamGradeObservable;

    public ExamGradeDAO(StudentDAO studentDao, SubjectDAO subjectDao)
    {
        _examGradeStorage = new Storage<ExamGrade>("examGrades.txt");
        _examGrades = _examGradeStorage.Load();

        ExamGradeObservable = new Observable();

        _studentDao = studentDao;
        _subjectDao = subjectDao;

        foreach (Student student in _studentDao.GetAllStudents())
        {
            foreach (Subject subject in _subjectDao.GetAllSubjects())
            {
                foreach (ExamGrade eg in _examGrades)
                {
                    if (subject.Id == eg.SubjectId && student.Id == eg.StudentId)
                    {
                        student.UnsubmittedSubjects.Remove(subject);
                        student.Grades.Add(eg);
                        student.SetAverageGrade();
                        subject.StudentsPassed.Add(student);
                        eg.Student = student;
                        eg.Subject = subject;
                        _studentDao.SaveStudents();
                        _subjectDao.SaveSubjects();
                        ExamGradeObservable.NotifyObservers();
                        break;
                    }
                }
            }
        }
    }

    private int GenerateId()
    {
        if (_examGrades.Count == 0)
            return 0;
        return _examGrades[^1].Id + 1;
    }

    public ExamGrade AddExamGrade(ExamGrade examGrade)
    {
        examGrade.Id = GenerateId();
        examGrade.Student = _studentDao.GetStudentById(examGrade.StudentId)!;
        examGrade.Subject = _subjectDao.GetSubjectById(examGrade.SubjectId)!;
        _examGrades.Add(examGrade);
        _examGradeStorage.Save(_examGrades);
        ExamGradeObservable.NotifyObservers();
        
        _studentDao.AddExamGradeForStudent(examGrade);
        _subjectDao.AddStudentPassedForSubject(examGrade);
        ExamGradeObservable.NotifyObservers();
        return examGrade;
    }

    public ExamGrade? UpdateExamGrade(ExamGrade examGrade)
    {
        ExamGrade? oldExamGrade = GetExamByIds(examGrade.StudentId, examGrade.SubjectId);
        if (oldExamGrade is null)
            return null;
        oldExamGrade.Grade = examGrade.Grade;
        oldExamGrade.Date = examGrade.Date;
        
        _examGradeStorage.Save(_examGrades);
        ExamGradeObservable.NotifyObservers();
        return oldExamGrade;
    }

    private ExamGrade? GetExamByIds(int studId, int subjId)
    {
        return _examGrades.Find(exam => exam.StudentId == studId
                                         && exam.SubjectId == subjId);
    }

    public ExamGrade? RemoveExamGrade(int studId, int subjId)
    {
        ExamGrade? examGrade = GetExamByIds(studId, subjId);
        if (examGrade == null) return null;

        _examGrades.Remove(examGrade);
        _examGradeStorage.Save(_examGrades);
        ExamGradeObservable.NotifyObservers();

        _studentDao.RemoveExamGradeForStudent(examGrade);
        _subjectDao.RemoveStudentPassedForSubject(examGrade);
        ExamGradeObservable.NotifyObservers();
        return examGrade;
    }

    public List<ExamGrade> GetAllExamGrades()
    {
        return _examGrades;
    }

    public void NotifyObservers()
    {
        ExamGradeObservable.NotifyObservers();
    }
}