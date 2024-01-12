using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO;

public class StudentSubjectDAO
{
    private readonly List<StudentSubject> _studentSubject;
    private readonly Storage<StudentSubject> _studentSubjectStorage;

    private StudentDAO _studentDao;
    private SubjectDAO _subjectDao;

    public Observable StudentSubjectObservable;
    public StudentSubjectDAO(StudentDAO studentDao, SubjectDAO subjectDao)
    {
        _studentSubjectStorage = new Storage<StudentSubject>("studentSubject.txt");
        _studentSubject = _studentSubjectStorage.Load();
        StudentSubjectObservable = new Observable();

        _studentDao = studentDao;
        _subjectDao = subjectDao;

        HashSet<Student> studentsForSubject = new HashSet<Student>();
        HashSet<Subject> subjectsForStudent = new HashSet<Subject>();

        foreach (Student student in studentDao.GetAllStudents())
        {
            foreach (Subject subject in subjectDao.GetAllSubjects())
            {
                foreach (StudentSubject ss in _studentSubject)
                {
                    if (subject.Id == ss.SubjectId && student.Id == ss.StudentId)
                    {
                        subjectsForStudent.Add(subject);
                        break;
                    }
                }
            }

            if (subjectsForStudent.Count > 0)
                studentDao.AddSubjectsForStudent(subjectsForStudent.ToList(), student.Id);
            subjectsForStudent.Clear();
        }

        foreach (Subject subject in subjectDao.GetAllSubjects())
        {
            foreach (Student student in studentDao.GetAllStudents())
            {
                foreach (StudentSubject ss in _studentSubject)
                {
                    if (student.Id == ss.StudentId && subject.Id == ss.SubjectId)
                    {
                        studentsForSubject.Add(student);
                        break;
                    }
                }
                
            }
            if (studentsForSubject.Count > 0)
                subjectDao.AddStudentsForSubject(studentsForSubject.ToList(), subject.Id);
            studentsForSubject.Clear();
        }

    }

    private int GenerateId()
    {
        if (_studentSubject.Count == 0)
            return 0;
        return _studentSubject[^1].Id + 1;
    }

    public void AddStudentSubject(StudentSubject studentSubject)
    {
        studentSubject.Id = GenerateId();
        _studentSubject.Add(studentSubject);
        _studentSubjectStorage.Save(_studentSubject);
        StudentSubjectObservable.NotifyObservers();
    }

    public void RemoveStudentSubject(int studentId, int subjectId)
    {
        StudentSubject studentSubject = _studentSubject.Find(ss => ss.StudentId == studentId && ss.SubjectId == subjectId)!;
        _studentSubject.Remove(studentSubject);
        _studentSubjectStorage.Save(_studentSubject);
        StudentSubjectObservable.NotifyObservers();
    }

    public List<Student>? FindAllStudentsForSubjects(List<Subject> subjects)
    {
        List<Student> students = new List<Student>();
        foreach(Subject subject in subjects)
        {
            Student? s = GetStudentBySubjectId(subject.Id);
            if(s != null)
            {
                students.Add(s);
            }
        }
        if (students.Count > 0)
        {
                return students.Distinct().ToList();
        }
        return null;

    }

    public Student? GetStudentBySubjectId(int subjectId)
    {
        StudentSubject? studentSubject = _studentSubject.Find(studentSubject => studentSubject.SubjectId == subjectId);
        if(studentSubject != null)
        {
            return _studentDao.GetStudentById(studentSubject.StudentId);
        }
        return null;
    }

    public List<Subject> GetAllSubjectsNotByStudentId(int studentId)
    {
        List<Subject> subjectsForStudent = new List<Subject>();
        List<Subject> allsubjects = _subjectDao.GetAllSubjects();

        List<StudentSubject> listss = _studentSubject.FindAll(ss => ss.StudentId == studentId);
        if (listss.Count > 0)
        {
            foreach (Subject subject in allsubjects)
            {
                bool inside = false;
                foreach (StudentSubject studentSubject in listss)
                {
                    if (subject.Id == studentSubject.SubjectId && studentId == studentSubject.StudentId)
                    {
                        inside = true;
                    }
                }
                if (!inside)
                {
                    subjectsForStudent.Add(subject);
                }
            }
        }

        return subjectsForStudent;
    }

    public void NotifyObservers()
    {
        StudentSubjectObservable.NotifyObservers();
    }
}