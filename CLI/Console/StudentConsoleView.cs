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

    private Student InputStudent()
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
        int enrollyear = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter current year of study: ");
        int year = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter status(B or S): ");
        Status status = ConsoleViewUtils.SafeInputStatus();

        System.Console.WriteLine("Enter average grade: ");
        double grade = ConsoleViewUtils.SafeInputDouble();


        return new Student(lastname, name, date, street, number, city, country, phone,
            mail, programme, enrollNum, enrollyear, year, status, grade);
    }
    
    private void AddStudent()
    {
        Student student = InputStudent();
        _studentDao.AddStudent(student);
        System.Console.WriteLine("Student added");
    }
    
    public void RunMenu()
    {
        while (true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            HandleMenuInput(userInput);
        }
    }
    
    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            // case "1":
            //     ShowAllStudents();
            //     break;
            case "2":
                AddStudent();
                break;
            // case "3":
            //     UpdateStudent();
            //     break;
            // case "4":
            //     RemoveStudent();
            //     break;
            // case "5":
            //     ShowAndSortStudents();
            //     break;
        }
    }
    
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All Students");
        System.Console.WriteLine("2: Add Student");
        System.Console.WriteLine("3: Update Student");
        System.Console.WriteLine("4: Remove Student");
        System.Console.WriteLine("5: Show and sort Students");
        System.Console.WriteLine("0: Close");
    }
}