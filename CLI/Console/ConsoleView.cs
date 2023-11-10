using CLI.DAO;

namespace CLI.Console;

public class ConsoleView
{
    public ConsoleView()
    {
        
    }

    public void RunMenu(StudentDAO studDAO, ProfessorDAO profDAO,
        SubjectDAO subjDAO, DepartmentDAO depDAO, ExamGradeDAO examGrDAO,
        IndexDAO indDAO, AddressDAO addrDAO)
    {
        while (true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            HandleMenuInput(userInput, studDAO, profDAO, subjDAO, depDAO, examGrDAO, indDAO, addrDAO);
        }
    }
    private void HandleMenuInput(string input, StudentDAO studDAO, ProfessorDAO profDAO,
        SubjectDAO subjDAO, DepartmentDAO depDAO, ExamGradeDAO examGrDAO,
        IndexDAO indDAO, AddressDAO addrDAO)
    {
        switch (input)
        {
            case "1":
                StudentConsoleView studView = new StudentConsoleView(studDAO);
                studView.RunMenu(subjDAO);
                break;
            case "2":
                ProfessorConsoleView profView = new ProfessorConsoleView(profDAO);
                profView.RunMenu();
                break;
            case "3":
                SubjectConsoleView subjView = new SubjectConsoleView(subjDAO);
                subjView.RunMenu();
                break;
            case "4":
                DepartmentConsoleView depView = new DepartmentConsoleView(depDAO);
                depView.RunMenu();
                break;
            case "5":
                ExamGradeConsoleView examGrView = new ExamGradeConsoleView(examGrDAO);
                examGrView.RunMenu();
                break;
        }
    }
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Students");
        System.Console.WriteLine("2: Professors");
        System.Console.WriteLine("3: Subjects");
        System.Console.WriteLine("4: Departments");
        System.Console.WriteLine("5: ExamGrades");
        System.Console.WriteLine("0: Close");
    }
}