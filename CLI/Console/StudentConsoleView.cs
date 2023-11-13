using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class StudentConsoleView
{
    private readonly StudentDAO _studentDao;
    private readonly StudentSubjectDAO _studentSubjectDao;
    public StudentConsoleView(StudentDAO studentDao, StudentSubjectDAO studentSubjectDao)
    {
        _studentDao = studentDao;
        _studentSubjectDao = studentSubjectDao;
    }

    private Student InputStudent(SubjectDAO subjectDao)
    {
        System.Console.WriteLine("STUDENT\nEnter last name: ");
        string lastname = ConsoleViewUtils.SafeInputString("last name");

        System.Console.WriteLine("Enter name: ");
        string name = ConsoleViewUtils.SafeInputString("name");
        
        System.Console.WriteLine("Enter birth date (e.g. mm/dd/yy): ");
        DateOnly date = ConsoleViewUtils.SafeInputDateOnly();

        System.Console.WriteLine("Enter address street: ");
        string street = ConsoleViewUtils.SafeInputString("street");

        System.Console.WriteLine("Enter address number: ");
        int number = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter address city: ");
        string city = ConsoleViewUtils.SafeInputString("city");

        System.Console.WriteLine("Enter address country: ");
        string country = ConsoleViewUtils.SafeInputString("country");

        System.Console.WriteLine("Enter phone number: ");
        string phone = ConsoleViewUtils.SafeInputString("phone number");

        System.Console.WriteLine("Enter e-mail: ");
        string mail = ConsoleViewUtils.SafeInputString("e-mail");

        System.Console.WriteLine("Enter study programme mark: ");
        string programme = ConsoleViewUtils.SafeInputString("study programme mark");

        System.Console.WriteLine("Enter enrollment number: ");
        int enrollNum = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter enrollment year: ");
        int enrollYear = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter current year of study: ");
        int year = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter status(B or S): ");
        Status status = ConsoleViewUtils.SafeInputStatus();

        return new Student(lastname, name, date, street, number, city, country, phone,
            mail, programme, enrollNum, enrollYear, year, status);
    }
    
    private void AddStudent(SubjectDAO subjectDao)
    {
        if (subjectDao.GetAllSubjects().Count == 0)
        {
            System.Console.WriteLine("Student cannot be added! There are no subjects.");
            return;
        }
        Student student = InputStudent(subjectDao);
        Subject subject = subjectDao.GetSubjectAddStudent(student);
        student.UnsubmittedSubjects.Add(subject);
        _studentDao.AddStudent(student);
        StudentSubject studentSubject = new StudentSubject(student.Id, subject.Id);
        _studentSubjectDao.AddStudentSubject(studentSubject);
        System.Console.WriteLine("Student added");
    }

    private int InputId()
    {
        System.Console.WriteLine("Enter student id: ");
        return ConsoleViewUtils.SafeInputInt();
    }
    private void UpdateStudent(SubjectDAO subjectDao)
    {
        int id = InputId();
        Student student = InputStudent(subjectDao)!;
        student.Id = id;
        Student? updateStudent = _studentDao.UpdateStudent(student);
        if (updateStudent == null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }
        
        System.Console.WriteLine("Student updated");
    }

    private void RemoveStudent()
    {
        int id = InputId();
        Student? removedStudent = _studentDao.RemoveStudent(id);
        if (removedStudent != null && removedStudent.Id == -1)
            return;
        if (removedStudent == null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }
        
        System.Console.WriteLine("Student removed");
    }

    private void RemoveSubjectForStudent(SubjectDAO subjectDao, StudentSubjectDAO studentSubjectDao)
    {
        int studentId = InputId();
        Student? student = _studentDao.GetStudentById(studentId);
        if (student == null)
        {
            System.Console.WriteLine("Student does not exists.");
            return;
        }
        System.Console.WriteLine("Enter subject id you want to remove: ");
        int subjectId = ConsoleViewUtils.SafeInputInt();
        Subject? removedStudentSubject = _studentDao.RemoveSubjectForStudent(studentId, subjectId, studentSubjectDao);
        if (removedStudentSubject == null)
            System.Console.WriteLine("Could not find requested subject");
        else
        {
            subjectDao.RemoveStudentDidNotPass(student, subjectId);
            System.Console.WriteLine("Student's subject removed.");
        }
    }
    
    public void RunMenu(SubjectDAO subjectDao)
    {
        while (true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            HandleMenuInput(userInput, subjectDao);
        }
    }
    
    private void HandleMenuInput(string input, SubjectDAO subjectDao)
    {
        switch (input)
        {
            case "1":
                ShowAllStudents();
                break;
            case "2":
                AddStudent(subjectDao);
                break;
            case "3": 
                UpdateStudent(subjectDao);
                break;
            case "4":
                RemoveStudent();
                break;
            case "5":
                ShowStudentUnsubmittedSubjects();
                break;
            case "6":
                ShowStudentGrades();
                break;
            case "7":
                RemoveSubjectForStudent(subjectDao, _studentSubjectDao);
                break;
            // case "7":
            //     ShowAndSortStudents();
            //     break;
        }
    }
    private void PrintStudents(List<Student> students)
    {
        System.Console.WriteLine("Students: ");
        string header = $"Id {"", 5} | LastName {"", 8} | Name {"", 8} | DateOfBirth {"", 15} | Street {"", 10} | Number {"", 2} | City {"", 6} | Country {"", 6} | StudyProgrammeMark {"", 3} | EnrollmentNumber {"", 2} | EnrollmentYear {"", 4} | PhoneNumber {"", 12} | Email {"", 12} | CurrentYearOfStudy {"", 1} | Status {"", 1} | AverageGrade {"", 4}";
        System.Console.WriteLine(header);
        foreach (Student s in students)
        {
            System.Console.WriteLine(s);
        }
    }
    private void ShowAllStudents()
    {
        PrintStudents(_studentDao.GetAllStudents());
    }

    private void ShowStudentUnsubmittedSubjects()
    {
        System.Console.WriteLine("Students: ");
        string header = $"Id {"",5} | LastName {"",8} | Name {"",8} | Subjects";
        System.Console.WriteLine(header);
        foreach (Student student in _studentDao.GetAllStudents())
        {
            System.Console.Write($"Id: {student.Id,5} | LastName: {student.Lastname, 8} | Name: {student.Name,8}");
            foreach (Subject subject in student.UnsubmittedSubjects)
            {
                System.Console.Write($" | {subject.Id} | {subject.Name}");
            }
            System.Console.WriteLine();
        }
    }

    private void ShowStudentGrades()
    {
        System.Console.WriteLine("Students: ");
        string header = $"Id {"",5} | LastName {"",8} | Name {"",8} | Grades";
        System.Console.WriteLine(header);
        foreach (Student student in _studentDao.GetAllStudents())
        {
            System.Console.Write($"Id: {student.Id,5} | LastName: {student.Lastname,8} | Name: {student.Name,8}");
            foreach (ExamGrade examGrade in student.Grades)
            {
                System.Console.Write($" | SubjectId: {examGrade.SubjectId} | SubjectName: {examGrade.Subject.Name} | Grade: {examGrade.Grade}");
            }
            System.Console.WriteLine();
        }
    }
    
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All Students");
        System.Console.WriteLine("2: Add Student");
        System.Console.WriteLine("3: Update Student");
        System.Console.WriteLine("4: Remove Student");
        System.Console.WriteLine("5: Show Students' Unsubmitted Subjects");
        System.Console.WriteLine("6: Show Students' Grades");
        System.Console.WriteLine("7: Remove Student's subject");
        System.Console.WriteLine("0: Back");
    }
}