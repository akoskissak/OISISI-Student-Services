using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class SubjectConsoleView
{
    private readonly SubjectDAO _subjectDao;
    
    private readonly ProfessorDAO _professorDao;
    
    
    public SubjectConsoleView(SubjectDAO subjectDao, ProfessorDAO professorDao)
    {
        _subjectDao = subjectDao;
        _professorDao = professorDao;
    }

    private Subject InputSubject()
    {
        System.Console.WriteLine("SUBJECT\nEnter subject code: ");
        int code = ConsoleViewUtils.SafeInputInt();
        
        System.Console.WriteLine("Enter name: ");
        string name = ConsoleViewUtils.SafeInputString("name");
        
        System.Console.WriteLine("Enter semester type(Zimski, Letnji): ");
        SemesterType semester = ConsoleViewUtils.SafeInputSemester();
        
        System.Console.WriteLine("Enter year of study: ");
        int year = ConsoleViewUtils.SafeInputInt();
        
        System.Console.WriteLine("Enter ESPB: ");
        int espb = ConsoleViewUtils.SafeInputInt();
        
        System.Console.Write("Enter professor ID: (-1 = noProfessor or ");
        foreach (Professor p in _professorDao.GetAllProfessors())
        {
            System.Console.Write($"{p.Id} ");
        }
        System.Console.Write("): ");
        int profId = ConsoleViewUtils.SafeInputInt();
        if (profId != -1)
        {
            while (_professorDao.GetAllProfessors().Find(p => p.Id == profId) == null)
            {
                System.Console.WriteLine("Not a valid professor id. Try again: ");
                profId = ConsoleViewUtils.SafeInputInt();
                if (profId == -1)
                    return new Subject(code, name, semester, year, espb, profId);
            }

            Professor prof = _professorDao.GetAllProfessors().Find(p => p.Id == profId)!;
            Subject subject = new Subject(code, name, semester, year, espb, profId);
            subject.Professor = prof;
            return subject;
        }

        return new Subject(code, name, semester, year, espb, profId);
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
       subject.Id = id;
       Subject? updateSubject = _subjectDao.UpdateSubject(subject);
       if(updateSubject == null)
       {
           System.Console.WriteLine("Subject not found");
           return;
       }
       
       System.Console.WriteLine("Subject updated");
   }

   private void RemoveSubject()
   {
       int id = InputId();
       Subject? removedSubject = _subjectDao.RemoveSubject(id);
       if (removedSubject != null && removedSubject.Id == -1)
           return;
       if (removedSubject == null)
       {
           System.Console.WriteLine("Subject not found");
           return;
       }
       
       System.Console.WriteLine("Subject removed");
   }

   private void ShowAllSubjects()
   {
       PrintSubjects(_subjectDao.GetAllSubjects());
   }

   private void PrintSubjects(List<Subject> subjects)
   {
       System.Console.WriteLine("Subjects: ");
       string header =
           $"SubjectCode {"",3} | Name {"",5} | Semester {"",5} | YearOfStudy {"",3} | ESPB {"",5} | Professor {"",3}";
       System.Console.WriteLine(header);
       foreach (Subject s in subjects)
            System.Console.WriteLine(s);
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
                ShowAllSubjects();
                break;
            case "2":
                AddSubject();
                break;
            case "3":
                UpdateSubject();
                break;
            case "4":
                RemoveSubject();
                break;
            case "5":
                ShowStudentsDidNotPass();
                break;
            // case "6":
            //     ShowAndSortSubjects();
            //     break;
        }
    }

    private void ShowStudentsDidNotPass()
    {
        System.Console.WriteLine("Subjects: ");
        string header = $"Id {"",5} | Name {"",8} | Students";
        System.Console.WriteLine(header);
        foreach (Subject subject in _subjectDao.GetAllSubjects())
        {
            System.Console.Write($"Id: {subject.Id,5} | Name: {subject.Name, 8}");
            foreach (Student student in subject.StudentsDidNotPass)
            {
                System.Console.Write($" | {student.Id} | {student.Lastname} | {student.Name}");
            }
            System.Console.WriteLine();
        }
    }
    
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All subjects");
        System.Console.WriteLine("2: Add subject");
        System.Console.WriteLine("3: Update subject");
        System.Console.WriteLine("4: Remove subject");
        System.Console.WriteLine("5: Show all subjects and students that did not pass them");
        System.Console.WriteLine("0: Back");
    }
}