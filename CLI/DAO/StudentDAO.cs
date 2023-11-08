using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class StudentDAO
{
    private readonly List<Student> _students;
    private readonly List<Address> _addresses;
    private readonly Storage<Student> _studentStorage;
    private readonly Storage<Address> _addressStorage;

    public StudentDAO()
    {
        _studentStorage = new Storage<Student>("students.txt");
        _students = _studentStorage.Load();
        _addressStorage = new Storage<Address>("addresses.txt");
        _addresses = _addressStorage.Load();
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
        student.Address.StudentId = student.Id;
        _students.Add(student);
        _studentStorage.Save(_students);
        
        _addresses.Add(student.Address);
        _addressStorage.Save(_addresses);
        return student;
    }
    
    
}