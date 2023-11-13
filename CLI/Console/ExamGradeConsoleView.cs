using CLI.DAO;
using CLI.Model;
using Microsoft.VisualBasic.CompilerServices;

namespace CLI.Console;

public class ExamGradeConsoleView
{
    private readonly ExamGradeDAO _examGradeDao;

    private readonly StudentDAO _studentDao;
    private readonly SubjectDAO _subjectDao;

    public ExamGradeConsoleView(ExamGradeDAO examGradeDao, StudentDAO studentDao, SubjectDAO subjectDao)
    {
        _examGradeDao = examGradeDao;

        _studentDao = studentDao;
        _subjectDao = subjectDao;
    }

    private ExamGrade? InputExamGrade()
    {
        int num = 0;
        int studentId = -1;
        foreach (Student student in _studentDao.GetAllStudents())
        {
            num += student.UnsubmittedSubjects.Count;
            studentId++;
        }
        if (num == 0)
        {
            System.Console.WriteLine("No student has unsumbitted subjects.");
            return null;
        }

        System.Console.Write("EXAM GRADE\nEnter studentId (");
        for (int i = 0; i < studentId; i++)
        {
            System.Console.Write($"{i} ");
        }
        System.Console.WriteLine(studentId + "): ");
        int studId = ConsoleViewUtils.SafeInputInt();
        while (studId < 0 || studId > studentId)
        {
            System.Console.WriteLine("Wrong student id. Try again: ");
            studId = ConsoleViewUtils.SafeInputInt();
        }

        List<int> correctIds = new List<int>();
        System.Console.Write("Enter subjectId (");
        foreach (Subject subject in _subjectDao.GetAllSubjects())
        {
            if (subject.StudentsDidNotPass.Exists(s => s.Id == studId))
            {
                System.Console.Write(subject.Id + " ");
                correctIds.Add(subject.Id);
            }
        }
        System.Console.WriteLine(")");
        int subjectId = ConsoleViewUtils.SafeInputInt();
        while (!correctIds.Contains(subjectId))
        {
            System.Console.WriteLine("Wrong subject id. Try again: ");
            subjectId = ConsoleViewUtils.SafeInputInt();
        }
        
        System.Console.WriteLine("Enter grade: ");
        int grade = ConsoleViewUtils.SafeInputInt();
        while (grade < 6 || grade > 10)
        {
            System.Console.WriteLine("Not a valid exam grade. Enter a number from 6-10: ");
            grade = ConsoleViewUtils.SafeInputInt();
        }

        System.Console.WriteLine("Enter date (e.g. mm/dd/yy): ");
        DateOnly date = ConsoleViewUtils.SafeInputDateOnly();

        return new ExamGrade(studId, subjectId, grade, date);
    }

    private void AddExamGrade()
    {
        ExamGrade? examGrade = InputExamGrade();
        if (examGrade == null)
        {
            System.Console.WriteLine("Failed to add examGrade");
            return;
        }
        _examGradeDao.AddExamGrade(examGrade);
        System.Console.WriteLine("ExamGrade added");
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
    private int InputStudId()
    {
        System.Console.WriteLine("Enter student id: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private int InputSubjId()
    {
        System.Console.WriteLine("Enter subject id: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private void RemoveExamGrade()
    {
        int studId = InputStudId();
        int subjId = InputSubjId();
        ExamGrade? removeExamGrade = _examGradeDao.RemoveExamGrade(studId, subjId);
        if (removeExamGrade is null)
        {
            System.Console.WriteLine("Exam grade not found");
            return;
        }
        
        System.Console.WriteLine("Exam grade removed");
        

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

    private void ShowAllExamGrades()
    {
        PrintExamGrades(_examGradeDao.GetAllExamGrades());
    }

    private void PrintExamGrades(List<ExamGrade> examGrades)
    {
        System.Console.WriteLine("ExamGrades: ");
        string header = $"StudentId {"",3} | SubjectId {"",3} | Grade {"",3} | Date {"",3}";
        System.Console.WriteLine(header);
        foreach (ExamGrade e in examGrades)
            System.Console.WriteLine(e);
    }
    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                ShowAllExamGrades();
                break;
            case "2":
                AddExamGrade();
                break;
            case "3":
                UpdateExamGrade();
                break;
            case "4":
                RemoveExamGrade();
                break;
            // case "5":
            //     ShowAndSortExamGrades();
            //     break;
        }
    }
    
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All ExamGrades");
        System.Console.WriteLine("2: Add ExamGrade");
        System.Console.WriteLine("3: Update ExamGrade");
        System.Console.WriteLine("4: Remove ExamGrade");
        // System.Console.WriteLine("5: Show and sort ExamGrades");
        System.Console.WriteLine("0: Back");
    }
}