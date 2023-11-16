using CLI.DAO;

namespace CLI.Console;

public class ConsoleView
{
    //private readonly AddressDAO _addressDao;
    private readonly IndexDAO _indexDao;
    private readonly StudentDAO _studentDao;

    private readonly ProfessorDAO _professorDao;
    private readonly SubjectDAO _subjectDao;
    private readonly StudentSubjectDAO _studentSubjectDao;

    private readonly DepartmentDAO _departmentDao;
    private readonly ProfessorDepartmentDAO _professorDepartmentDao;
    private readonly ProfessorSubjectDAO _professorSubjectDao;

    private readonly ExamGradeDAO _examGradeDao;
    public ConsoleView()
    {
        //_addressDao = new AddressDAO();
        _indexDao = new IndexDAO();
        _studentDao = new StudentDAO(/*_addressDao, */_indexDao);
        
        _professorDao = new ProfessorDAO();
        _subjectDao = new SubjectDAO();
        _studentSubjectDao = new StudentSubjectDAO(_studentDao, _subjectDao);
        
        _departmentDao = new DepartmentDAO();
        _professorDepartmentDao = new ProfessorDepartmentDAO(_professorDao, _departmentDao);
        _professorSubjectDao = new ProfessorSubjectDAO(_professorDao, _subjectDao);
        _examGradeDao = new ExamGradeDAO(_studentDao, _subjectDao);
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
            case "1":
                StudentConsoleView studView = new StudentConsoleView(_studentDao, _studentSubjectDao);
                studView.RunMenu(_subjectDao);
                break;
            case "2":
                ProfessorConsoleView profView = new ProfessorConsoleView(_professorDao);
                profView.RunMenu();
                break;
            case "3":
                SubjectConsoleView subjView = new SubjectConsoleView(_subjectDao, _professorDao);
                subjView.RunMenu();
                break;
            case "4":
                DepartmentConsoleView depView = new DepartmentConsoleView(_departmentDao);
                depView.RunMenu(_professorDao);
                break;
            case "5":
                ExamGradeConsoleView examGrView = new ExamGradeConsoleView(_examGradeDao, _studentDao, _subjectDao);
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