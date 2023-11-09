using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class SubjectConsoleView
{
    private readonly SubjectDAO _subjectDao;
    
    
    public SubjectConsoleView(SubjectDAO subjectDao)
    {
        _subjectDao = subjectDao;
    }

    private Subject InputSubject()
    {
        System.Console.WriteLine("SUBJECT\nEnter name: ");
        string name = System.Console.ReadLine() ?? string.Empty;
        
        System.Console.WriteLine("Enter semester type: ");
        SemesterType semester = ConsoleViewUtils.SafeInputSemester();
        
        System.Console.WriteLine("Enter year of study: ");
        int year = ConsoleViewUtils.SafeInputInt();
        
        System.Console.WriteLine("Enter ESPB: ");
        int espb = ConsoleViewUtils.SafeInputInt();
        
        System.Console.WriteLine("Enter professor ID: ");
        int profId = ConsoleViewUtils.SafeInputInt();

        return new Subject(name, semester, year, espb, profId);
    }
    
   private void AddSubject()
    {
        Subject subject = InputSubject();
        _subjectDao.AddSubject(subject);
        System.Console.WriteLine("Subject added");
    }

   private int InputId()
   {
       System.Console.WriteLine("Enter subject id: ");
       return ConsoleViewUtils.SafeInputInt();
   }
   private void UpdateSubject()
   {
       int id = InputId();
       Subject subject = InputSubject();
       subject.SubjectCode = id;
       Subject? updateSubject = _subjectDao.UpdateSubject(subject);
       if(updateSubject == null)
       {
           System.Console.WriteLine("Subject not found");
           return;
       }
       
       System.Console.WriteLine("Subject updated");
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
            /*case "1":
                ShowAllSubjects();
                break;*/

            case "2":
                AddSubject();
                break;
            case "3":
                UpdateSubject();
                break;
            /*case "4":
                RemoveSubject();
                break;
            case "5":
                ShowAndSortSubjects();
                break;*/
        }
    }
    
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All subjects");
        System.Console.WriteLine("2: Add subject");
        System.Console.WriteLine("3: Update subject");
        System.Console.WriteLine("4: Remove subject");
        System.Console.WriteLine("5: Show and sort subjects");
        System.Console.WriteLine("0: Close");
    }
}