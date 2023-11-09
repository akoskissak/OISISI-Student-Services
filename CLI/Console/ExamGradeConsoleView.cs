using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class ExamGradeConsoleView
{
    private readonly ExamGradeDAO _examGradeDao;

    public ExamGradeConsoleView(ExamGradeDAO examGradeDao)
    {
        _examGradeDao = examGradeDao;
    }

    private ExamGrade InputExamGrade()
    {
        System.Console.WriteLine("EXAM GRADE\nEnter studentId: ");
        int studId = ConsoleViewUtils.SafeInputInt();
        
        System.Console.WriteLine("Enter subjectId: ");
        int subjectId = ConsoleViewUtils.SafeInputInt();
        
        
        System.Console.WriteLine("Enter grade: ");
        int grade = ConsoleViewUtils.SafeInputInt();
        while (grade < 6 || grade > 10)
        {
            System.Console.WriteLine("Not a valid exam grade. Enter a number from 6-10: ");
            grade = ConsoleViewUtils.SafeInputInt();
        }

        System.Console.WriteLine("Enter date (e.g. mm/dd/yy): ");
        DateTime date = ConsoleViewUtils.SafeInputDateTime();

        return new ExamGrade(studId, subjectId, grade, date);
    }

    private void AddExamGrade()
    {
        ExamGrade examGrade = InputExamGrade();
        _examGradeDao.AddExamGrade(examGrade);
        System.Console.WriteLine("ExamGrade added");
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
            //     ShowAllExamGrades();
            //     break;
            case "2":
                AddExamGrade();
                break;
            case "3":
                UpdateExamGrade();
                break;
            // case "4":
            //     RemoveExamGrade();
            //     break;
            // case "5":
            //     ShowAndSortExamGrades();
            //     break;
        }
    }
    private void UpdateExamGrade()
    {
        ExamGrade examGrade = InputExamGrade();
        ExamGrade? updatedExamGrade = _examGradeDao.UpdateExamGrade(examGrade);
        if (updatedExamGrade == null)
        {
            System.Console.WriteLine("ExamGrade not found");
            return;
        }
        
        System.Console.WriteLine("ExamGrade updated");
    }
    
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All ExamGrades");
        System.Console.WriteLine("2: Add ExamGrade");
        System.Console.WriteLine("3: Update ExamGrade");
        System.Console.WriteLine("4: Remove ExamGrade");
        System.Console.WriteLine("5: Show and sort ExamGrades");
        System.Console.WriteLine("0: Back");
    }
}