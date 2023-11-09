using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class StudentConsoleView
{
    private readonly StudentDAO _studentDao;
    public StudentConsoleView(StudentDAO studentDao)
    {
        _studentDao = studentDao;
    }
    private Student InputStudent(SubjectDAO subjectDao)
    {
        System.Console.WriteLine("STUDENT\nEnter last name: ");
        string lastname = System.Console.ReadLine() ?? String.Empty;

        System.Console.WriteLine("Enter name: ");
        string name = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter birth date (e.g. mm/dd/yy): ");
        DateTime date = ConsoleViewUtils.SafeInputDateTime();

        System.Console.WriteLine("Enter address street: ");
        string street = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter address number: ");
        int number = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter address city: ");
        string city = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter address country: ");
        string country = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter phone number: ");
        string phone = System.Console.ReadLine();

        System.Console.WriteLine("Enter e-mail: ");
        string mail = System.Console.ReadLine();

        System.Console.WriteLine("Enter study programme mark: ");
        string programme = System.Console.ReadLine();

        System.Console.WriteLine("Enter enrollment number: ");
        int enrollNum = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter enrollment year: ");
        int enrollYear = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter current year of study: ");
        int year = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter status(B or S): ");
        Status status = ConsoleViewUtils.SafeInputStatus();

        System.Console.WriteLine("Enter average grade: ");
        double grade = ConsoleViewUtils.SafeInputDouble();
        
        System.Console.WriteLine("Enter unsubmitted subject code: ");
        int subjCode = ConsoleViewUtils.SafeInputInt();
        
        Student student = new Student(lastname, name, date, street, number, city, country, phone,
            mail, programme, enrollNum, enrollYear, year, status, grade);

        Subject? subj = subjectDao.GetSubjectById(subjCode);
        if (subj != null)
        {
            student.UnsubmittedSubjects.Add(subj);
        }
        else
        {
            System.Console.WriteLine($"Subject with subject code {subjCode} does not exist.");
        }
        
        System.Console.WriteLine("Enter submitted subject code: ");
        subjCode = ConsoleViewUtils.SafeInputInt();
        
        subj = subjectDao.GetSubjectById(subjCode);
        if (subj != null)
        {
            student.UnsubmittedSubjects.Add(subj);
        }
        else
        {
            System.Console.WriteLine($"Subject with subject code {subjCode} does not exist.");
        }

        return student;
    }
    
    private void AddStudent(SubjectDAO subjectDao)
    {
        Student student = InputStudent(subjectDao);
        _studentDao.AddStudent(student);
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
        Student student = InputStudent(subjectDao);
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
        if (removedStudent == null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }
        
        System.Console.WriteLine("Student removed");
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
            // case "5":
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
    
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All Students");
        System.Console.WriteLine("2: Add Student");
        System.Console.WriteLine("3: Update Student");
        System.Console.WriteLine("4: Remove Student");
        System.Console.WriteLine("5: Show and sort Students");
        System.Console.WriteLine("0: Back");
    }
}