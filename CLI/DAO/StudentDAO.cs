using CLI.Model;
using CLI.Observer;
using CLI.Storage;
using Index = CLI.Model.Index;

namespace CLI.DAO;

public class StudentDAO
{
    private readonly List<Student> _students;
    private readonly List<Subject> _subjects;

    private readonly Storage<Student> _studentStorage;
    private readonly Storage<Subject> _subjectStorage;

    public Observable StudentObservable;
    public StudentDAO()
    {
        _studentStorage = new Storage<Student>("students.txt");
        _students = _studentStorage.Load();
        StudentObservable = new Observable();

        _subjectStorage = new Storage<Subject>("subjects.txt");
        _subjects = _subjectStorage.Load();
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
        StudentObservable.NotifyObservers();
        return student;
    }

    public void AddExamGradeForStudent(ExamGrade examGrade)
    {
        Student s = _students.Find(student => student.Id == examGrade.StudentId)!;
        s.Grades.Add(examGrade);
        s.UnsubmittedSubjects.Remove(examGrade.Subject);
        s.SetAverageGrade();

        _studentStorage.Save(_students);
        StudentObservable.NotifyObservers();
    }

    public void RemoveExamGradeForStudent(ExamGrade examGrade)
    {
        Student s = _students.Find(student => student.Id == examGrade.StudentId)!;
        s.Grades.Remove(examGrade);
        s.UnsubmittedSubjects.Add(examGrade.Subject);
        s.SetAverageGrade();

        _studentStorage.Save(_students);
        StudentObservable.NotifyObservers();

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
        StudentObservable.NotifyObservers();
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
        _studentStorage.Save(_students);
        StudentObservable.NotifyObservers();
        return student;
    }

    public Subject? RemoveSubjectForStudent(int studentId, int subjectId, StudentSubjectDAO studentSubjectDao)
    {
        Student? student = GetStudentById(studentId);
        if (student == null)
            return null;
        Subject? subject = student.UnsubmittedSubjects.Find(s => s.Id == subjectId);
        if (subject == null)
            return null;
        student.UnsubmittedSubjects.Remove(subject);
        studentSubjectDao.RemoveStudentSubject(studentId, subjectId);
        _studentStorage.Save(_students);
        StudentObservable.NotifyObservers();
        return subject;
    }

    public void AddSubjectsForStudent(List<Subject> subjects, int id)
    {
        _students.Find(student => student.Id == id)!.UnsubmittedSubjects = subjects;
        _studentStorage.Save(_students);
        StudentObservable.NotifyObservers();
    }

    public void AddSubjectForStudent(int subjectId, int studentId)
    {
        Subject subject = _subjects.Find(subject => subject.Id == subjectId);
        _students.Find(student => student.Id == studentId)!.UnsubmittedSubjects.Add(subject);
        _studentStorage.Save(_students);
        StudentObservable.NotifyObservers();
    }
    public void SaveStudents()
    {
        _studentStorage.Save(_students);
        StudentObservable.NotifyObservers();
    }

    public Student? GetStudentById(int id)
    {
        return _students.Find(s => s.Id == id);
    }
    public List<Student> GetAllStudents()
    {
        return _students;
    }
    public void NotifyObservers()
    {
        StudentObservable.NotifyObservers();
    }

    public List<Student>? FindStudentByText(string text)
    {
        string[] inputs = text.Split(',');
        if(inputs.Length == 1)
        {
            string lastname = inputs[0];
            return _students.FindAll(student => student.Lastname.Equals(lastname, StringComparison.OrdinalIgnoreCase));
        }
        else if (inputs.Length == 2)
        {
            string lastname = inputs[0];
            string name = inputs[1].Trim();
            return _students.FindAll(student => student.Lastname.Equals(lastname, StringComparison.OrdinalIgnoreCase) && student.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        else if (inputs.Length == 3)
        {
            string[] indexes = inputs[0].Split("-");
            if (indexes.Length != 3) return null;
            string smark = indexes[0];
            int num;
            int year;

            string name = inputs[1].Trim();
            string lastname = inputs[2].Trim();

            if (int.TryParse(indexes[1], out num) && int.TryParse(indexes[2], out year))
                return _students.FindAll(student => student.Index.EnrollmentYear == year && student.Index.EnrollmentNumber == num && student.Index.StudyProgrammeMark.Equals(smark, StringComparison.OrdinalIgnoreCase) && student.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && student.Lastname.Equals(lastname, StringComparison.OrdinalIgnoreCase));
        }
        return null;
    }

}