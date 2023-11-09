using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class ProfessorConsoleView
{
    private readonly ProfessorDAO _professorsDao;

    public ProfessorConsoleView(ProfessorDAO professorsDao)
    {
        _professorsDao = professorsDao;
    }

    private Professor InputProfessor()
    {
        System.Console.WriteLine("PROFESSOR\nEnter last name: ");
        string lastname = System.Console.ReadLine() ?? string.Empty;
        
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
        string phoneNumber = System.Console.ReadLine() ?? string.Empty;
        
        System.Console.WriteLine("Enter email: ");
        string mail = System.Console.ReadLine() ?? string.Empty;
        
        System.Console.WriteLine("Enter id number: ");
        int id = ConsoleViewUtils.SafeInputInt();
        
        System.Console.WriteLine("Enter title: ");
        string title = System.Console.ReadLine() ?? string.Empty;
        
        System.Console.WriteLine("Enter years of service: ");
        int years = ConsoleViewUtils.SafeInputInt();

        return new Professor(lastname, name, date, street, number, city, country, phoneNumber, mail, id, title, years);
    }

    private void AddProfessor()
    {
        Professor professor = InputProfessor();
        _professorsDao.AddProfessor(professor);
        System.Console.WriteLine("Professor added");
    }
    
    private int InputId()
    {
        System.Console.WriteLine("Enter professor id: ");
        return ConsoleViewUtils.SafeInputInt();
    }
    private void UpdateProfessor()
    {
        int idNumber = InputId();
        Professor professor = InputProfessor();
        professor.Idnumber = idNumber;
        Professor? updatedProfessor = _professorsDao.UpdateProfessor(professor);
        if (updatedProfessor == null)
        {
            System.Console.WriteLine("Professor not found");
            return;
        }
        
        System.Console.WriteLine("Professor updated");
    }

    private void RemoveProfessor()
    {
        int id = InputId();
        Professor? removedProfessor = _professorsDao.RemoveProfessor(id);
        if (removedProfessor is null)
        {
            System.Console.WriteLine("Professor not found");
            return;
        }
        
        System.Console.WriteLine("Professor removed");
    }

    private void ShowAllProfessors()
    {
        PrintProfessors(_professorsDao.GetAllProfessors());
    }

    private void PrintProfessors(List<Professor> professors)
    {
        System.Console.WriteLine("Professors: ");
        string header = $"LastName {"", 7} | Name {"", 8} | DateOfBirth {"", 20} | PhoneNumber {"", 10} | Email {"", 10} | IdNumber {"", 5} | Title {"", 8} | YearsOfService {"", 4}";
        System.Console.WriteLine(header);
        foreach (Professor p in professors)
            System.Console.WriteLine(p);
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
                ShowAllProfessors();
                break;
            case "2":
                AddProfessor();
                break;
            case "3":
                UpdateProfessor();
                break;
            case "4": 
                RemoveProfessor(); 
                break;
            // case "5":
            //     ShowAndSortProfessors();
            //     break;
        }
    }

    
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All Professors");
        System.Console.WriteLine("2: Add Professor");
        System.Console.WriteLine("3: Update Professor");
        System.Console.WriteLine("4: Remove Professor");
        System.Console.WriteLine("5: Show and sort professors");
        System.Console.WriteLine("0: Back");
    }
}