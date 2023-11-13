using CLI.Model;
using CLI.Storage;
using Index = CLI.Model.Index;

namespace CLI.DAO;

public class StudentDAO
{
    private readonly List<Student> _students;
    private readonly Storage<Student> _studentStorage;

    private AddressDAO _addressDao;
    private IndexDAO _indexDao;

    public StudentDAO(AddressDAO addrDAO, IndexDAO idxDAO)
    {
        _studentStorage = new Storage<Student>("students.txt");
        _students = _studentStorage.Load();
        
        _addressDao = addrDAO;
        _indexDao = idxDAO;
    }

    private int GenerateStudentId()
    {
        if (_students.Count == 0) 
            return 0;
        return _students[^1].Id + 1;
    }
    
    public Student AddStudent(Student student)
    {
        student.Id = GenerateStudentId();
        _students.Add(student);
        _studentStorage.Save(_students);

        _addressDao.AddAddress(student.Address);
        _indexDao.AddIndex(student.Index);
        return student;
    }

    public void AddExamGradeForStudent(ExamGrade examGrade)
    {
        Student s = _students.Find(student => student.Id == examGrade.StudentId)!;
        s.Grades.Add(examGrade);
        s.UnsubmittedSubjects.Remove(examGrade.Subject);
        s.SetAverageGrade();
        
        _studentStorage.Save(_students);
    }
    
    public void RemoveExamGradeForStudent(ExamGrade examGrade)
    {
        Student s = _students.Find(student => student.Id == examGrade.StudentId)!;
        s.Grades.Remove(examGrade);
        s.UnsubmittedSubjects.Add(examGrade.Subject);
        s.SetAverageGrade();
        
        _studentStorage.Save(_students);
    }

    public Student? UpdateStudent(Student student)
    {
        Student? oldStudent = GetStudentById(student.Id);
        if (oldStudent is null) return null;

        oldStudent.Lastname = student.Lastname;
        oldStudent.Name = student.Name;
        oldStudent.DateOfBirth = student.DateOfBirth;
        oldStudent.Address = student.Address;
        oldStudent.Index = student.Index;
        oldStudent.PhoneNumber = student.PhoneNumber;
        oldStudent.Email = student.Email;
        oldStudent.CurrentYearOfStudy = student.CurrentYearOfStudy;
        oldStudent.Status = student.Status;
        oldStudent.AverageGrade = student.AverageGrade;
        
        _studentStorage.Save(_students);
        _addressDao.UpdateAddress(student.Address);
        _indexDao.UpdateIndex(student.Index);
        return oldStudent;
    }

    public Student? RemoveStudent(int id)
    {
        Student? student = GetStudentById(id);
        if (student == null)
            return null;
        
        if (student.UnsubmittedSubjects.Count != 0 || student.Grades.Count != 0)
        {
            System.Console.WriteLine("Cannot remove student that has subject/s or grade/s!\nRemove them first and then you can remove student.");
            Student stud = new Student();
            stud.Id = -1;
            return stud;
        }
        
        _students.Remove(student);
        _addressDao.RemoveAddress(student.Address.Id);
        _indexDao.RemoveIndex(student.Index.Id);
        _studentStorage.Save(_students);
        return student;
    }

    public Subject? RemoveSubjectForStudent(int studentId, int subjectId, StudentSubjectDAO studentSubjectDao)
    {
        Student? student = GetStudentById(studentId);
        if (student == null)
            return null;
        Subject? subject = student.UnsubmittedSubjects.Find(subject => subject.Id == subjectId);
        if (subject == null)
            return null;
        student.UnsubmittedSubjects.Remove(subject);
        _studentStorage.Save(_students);
        
        studentSubjectDao.RemoveStudentSubject(studentId, subjectId);
        return subject;
    }

    public void AddSubjectsForStudent(List<Subject> subjects, int id)
    {
        _students.Find(student => student.Id == id)!.UnsubmittedSubjects = subjects;
        _studentStorage.Save(_students);
    }

    public void SaveStudents()
    {
        _studentStorage.Save(_students);
    }

    public Student? GetStudentById(int id)
    {
        return _students.Find(s => s.Id == id);
    }
    public List<Student> GetAllStudents()
    {
        return _students;
    }
}