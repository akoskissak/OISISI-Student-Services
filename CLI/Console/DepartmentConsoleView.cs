using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class DepartmentConsoleView
{
    private readonly DepartmentDAO _departmentDao;

    public DepartmentConsoleView(DepartmentDAO departmentDao)
    {
        _departmentDao = departmentDao;
    }

    private Department InputDepartment()
    {
        System.Console.WriteLine("DEPARTMENT\nEnter code: ");
        int code = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter name: ");
        string name = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter chief: ");
        string chief = System.Console.ReadLine() ?? string.Empty;

        return new Department(code, name, chief);
    }

    private void AddDepartment()
    {
        Department department = InputDepartment();
        _departmentDao.AddDepartment(department);
        System.Console.WriteLine("Department added");
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
                AddDepartment();
                break;
            case "3":
                UpdateDepartment();
                break;
            // case "4":
            //     RemoveStudent();
            //     break;
            // case "5":
            //     ShowAndSortStudents();
            //     break;
        }
    }

    private void UpdateDepartment()
    {
        Department department = InputDepartment();
        Department? updatedDepartment = _departmentDao.UpdateDepartment(department);
        if (updatedDepartment == null)
        {
            System.Console.WriteLine("Department not found");
            return;
        }
        
        System.Console.WriteLine("Department updated");
    }
    
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All Departments");
        System.Console.WriteLine("2: Add Department");
        System.Console.WriteLine("3: Update Department");
        System.Console.WriteLine("4: Remove Department");
        System.Console.WriteLine("5: Show and sort Departments");
        System.Console.WriteLine("0: Close");
    }
}