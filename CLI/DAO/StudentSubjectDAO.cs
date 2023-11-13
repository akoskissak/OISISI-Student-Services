using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class StudentSubjectDAO
{
    private readonly List<StudentSubject> _studentSubject;
    private readonly Storage<StudentSubject> _studentSubjectStorage;

    private StudentDAO _studentDao;
    private SubjectDAO _subjectDao;

    public StudentSubjectDAO(StudentDAO studentDao, SubjectDAO subjectDao)
    {
        _studentSubjectStorage = new Storage<StudentSubject>("studentSubject.txt");
        _studentSubject = _studentSubjectStorage.Load();

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

    public StudentSubject AddStudentSubject(StudentSubject studentSubject)
    {
        studentSubject.Id = GenerateId();
        _studentSubject.Add(studentSubject);
        _studentSubjectStorage.Save(_studentSubject);
        return studentSubject;
    }

    // public List<int>? GetAllStudentIds()
    // {
    //     List<int> studentIds = new List<int>();
    //     foreach (StudentSubject ss in _studentSubject)
    //     {
    //         studentIds.Add(ss.StudentId);
    //     }
    //     if (studentIds.Count == 0)
    //         return null;
    //
    //     return studentIds;
    // }
    //
    // public List<int>? GetAllSubjectsForStudent(Student student)
    // {
    //     List<Subject> subjects = new List<Subject>();
    //     foreach (StudentSubject ss in _studentSubject)
    //     {
    //         if (ss.StudentId == student.Id)
    //             subjects.Add(ss.);
    //     }
    //
    //     if (subjectIds.Count == 0)
    //         return null;
    //     return subjectIds;
    // }
    //
    // public List<int>? GetAllSubjectIds()
    // {
    //     List<int> subjectIds = new List<int>();
    //     foreach (StudentSubject ss in _studentSubject)
    //     {
    //         subjectIds.Add(ss.SubjectId);
    //     }
    //
    //     if (subjectIds.Count == 0)
    //         return null;
    //     
    //     return subjectIds;
    // }
}