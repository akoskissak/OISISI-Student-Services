using CLI.Model;
using CLI.Storage;
using Index = CLI.Model.Index;

namespace CLI.DAO;

public class StudentDAO
{
    private readonly List<Student> _students;
    // private readonly List<Address> _addresses;
    // private readonly List<Index> _indexes;
    // private readonly List<StudentSubject> _studentSubjects;
    
    private readonly Storage<Student> _studentStorage;
    // private readonly Storage<Address> _addressStorage;
    // private readonly Storage<Index> _indexStorage;
    // private readonly Storage<StudentSubject> _studentSubjectStorage;

    private AddressDAO _addressDao;
    private IndexDAO _indexDao;
    private StudentSubjectDAO _studentSubjectDao;

    // public StudentDAO(List<Address> _addrs,Storage<Address> _addrStorage, 
    //     List<Index> _idxs, Storage<Index> _idxStorage, List<StudentSubject> _studSubjs, Storage<StudentSubject> _studSubjStorage)
    public StudentDAO(AddressDAO addrDAO, IndexDAO idxDAO)
    {
        _studentStorage = new Storage<Student>("students.txt");
        _students = _studentStorage.Load();

        // foreach (Student s in _students)
        // {
        //     
        // }
        // studSubjDAO.GetAllSubjectIds();
        
        _addressDao = addrDAO;
        _indexDao = idxDAO;
        // StudentSubjectDAO _studentSubjectDao = studSubjDAO;
        
        // _addressStorage = _addrStorage;
        // _addresses = _addrs;
        //
        // _indexes = _idxs;
        // _indexStorage = _idxStorage;
        //
        // _studentSubjects = _studSubjs;
        // _studentSubjectStorage = _studSubjStorage;
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
        // student.Address.StudentId = student.Id;
        _students.Add(student);
        _studentStorage.Save(_students);

        _addressDao.AddAddress(student.Address);
        // _addresses.Add(student.Address);
        // _addressStorage.Save(_addresses);
        _indexDao.AddIndex(student.Index);
        return student;
    }

    public void AddExamGradeForStudent(ExamGrade examGrade)
    {
        Student s = _students.Find(student => student.Id == examGrade.StudentId)!;
        s.Grades.Add(examGrade);
        s.UnsubmittedSubjects.Remove(examGrade.Subject);
        foreach (ExamGrade eg in s.Grades)
        {
            s.AverageGrade += eg.Grade;
        }

        s.AverageGrade /= s.Grades.Count;
        
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
            return null;
        }
        
        _students.Remove(student);
        _addressDao.RemoveAddress(student.Address.Id);
        _indexDao.RemoveIndex(student.Index.Id);
        _studentStorage.Save(_students);
        return student;
    }

    public void AddSubjectsForStudent(List<Subject> subjects, int Id)
    {
        _students.Find(student => student.Id == Id)!.UnsubmittedSubjects = subjects;
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