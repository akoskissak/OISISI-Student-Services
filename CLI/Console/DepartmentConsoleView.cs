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

    private Department InputDepartment(ProfessorDAO professorDao)
    {
        System.Console.WriteLine("DEPARTMENT\nEnter department code: ");
        string code = ConsoleViewUtils.SafeInputString("department code");

        System.Console.WriteLine("Enter name: ");
        string name = ConsoleViewUtils.SafeInputString("name");

        System.Console.Write("Enter chief id (-1 = noChief");
        List<int> ids = new List<int>();
        foreach (Professor p in professorDao.GetAllProfessors())
        {
            if (p.IdOfChiefDepartment == -1)
            {
                ids.Add(p.Id);
            }
        }
        
        if (ids.Count > 0)
            System.Console.Write(" or ");
        for (int i = 0; i < ids.Count - 1; i++)
        {
            System.Console.Write($"{ids[i]}, ");
        }
        System.Console.Write($"{ids[^1]}): ");
        
        int chiefId = ConsoleViewUtils.SafeInputInt();
        if (chiefId != -1)
        {
            while (professorDao.GetAllProfessors().Find(p => p.Id == chiefId) == null || ids.Contains(chiefId) == false)
            {
                System.Console.WriteLine("Not a valid chief id. Try again: ");
                chiefId = ConsoleViewUtils.SafeInputInt();
                if (chiefId == -1)
                    return new Department(code, name, chiefId);
            }
            
            Professor prof = professorDao.GetAllProfessors().Find(p => p.Id == chiefId)!;
            Department dep = new Department(code, name, chiefId);
            prof.IdOfChiefDepartment = dep.Id;
            dep.Chief = prof;
            return dep;
        }
        return new Department(code, name, chiefId);
    }

    private void AddDepartment(ProfessorDAO professorDao)
    {
        Department department = InputDepartment(professorDao);
        _departmentDao.AddDepartment(department);
        System.Console.WriteLine("Department added");
    }
    
    private void UpdateDepartment(ProfessorDAO professorDao)
    {
        int id = InputId();
        Department department = InputDepartment(professorDao);
        department.Id = id;
        Department? updatedDepartment = _departmentDao.UpdateDepartment(department);
        if (updatedDepartment == null)
        {
            System.Console.WriteLine("Department not found");
            return;
        }
        
        System.Console.WriteLine("Department updated");
    }

    private int InputId()
    {
        System.Console.WriteLine("Enter department id: ");
        return ConsoleViewUtils.SafeInputInt();
    }
    private void RemoveDepartment()
    {
        int id = InputId();
        Department? removedDepartment = _departmentDao.RemoveDepartment(id);
        if (removedDepartment != null && removedDepartment.Id == -1)
            return;
        if (removedDepartment is null)
        {
            System.Console.WriteLine("Department not found");
            return;
        }
        
        System.Console.WriteLine("Department removed");
    }

    private void ShowAllDepartments()
    {
        PrintDepartments(_departmentDao.GetAllDepartments());
    }

    private void PrintDepartments(List<Department> departments)
    {
        System.Console.WriteLine("Departments: ");
        string header = $"DepCode {"",3} | Name {"",3} | Chief {"",2}";
        System.Console.WriteLine(header);
        foreach (Department d in departments)
            System.Console.WriteLine(d);
    }
    public void RunMenu(ProfessorDAO professorDao)
    {
        while (true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            HandleMenuInput(userInput, professorDao);
        }
    }
    
    private void HandleMenuInput(string input, ProfessorDAO professorDao)
    {
        switch (input)
        {
            case "1":
                ShowAllDepartments();
                break;
            case "2":
                AddDepartment(professorDao);
                break;
            case "3":
                UpdateDepartment(professorDao);
                break;
            case "4":
                RemoveDepartment();
                break;
            case "5":
                ShowProfessorsOnDep();
                break;
            // case "6":
            //     ShowAndSortDepartments();
            //     break;
        }
    }

    private void ShowProfessorsOnDep()
    {
        System.Console.WriteLine("Departments: ");
        string header = $"Id {"",5} | Name {"",2} | Professors";
        System.Console.WriteLine(header);
        foreach (Department department in _departmentDao.GetAllDepartments())
        {
            System.Console.Write($"Id: {department.Id,5} | Name: {department.Name,2}");
            foreach (Professor professor in department.Professors)
            {
                System.Console.Write($" | {professor.Id} | {professor.Name}");
            }
            System.Console.WriteLine();
        }
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All Departments");
        System.Console.WriteLine("2: Add Department");
        System.Console.WriteLine("3: Update Department");
        System.Console.WriteLine("4: Remove Department");
        System.Console.WriteLine("5: Show All Department Professors");
        System.Console.WriteLine("0: Back");
    }
}