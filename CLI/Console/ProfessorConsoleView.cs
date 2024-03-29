﻿using CLI.DAO;
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
        string lastname = ConsoleViewUtils.SafeInputString("last name");
        
        System.Console.WriteLine("Enter name: ");
        string name = ConsoleViewUtils.SafeInputString("name");
        
        System.Console.WriteLine("Enter birth date (e.g. mm/dd/yy): ");
        DateOnly date = ConsoleViewUtils.SafeInputDateOnly();
        
        System.Console.WriteLine("Enter address street: ");
        string street = ConsoleViewUtils.SafeInputString("street");
        
        System.Console.WriteLine("Enter address number: ");
        string number = ConsoleViewUtils.SafeInputString("number");
        
        System.Console.WriteLine("Enter address city: ");
        string city = ConsoleViewUtils.SafeInputString("city");
        
        System.Console.WriteLine("Enter address country: ");
        string country = ConsoleViewUtils.SafeInputString("country");
        
        System.Console.WriteLine("Enter phone number: ");
        string phoneNumber = ConsoleViewUtils.SafeInputString("phone number");
        
        System.Console.WriteLine("Enter e-mail: ");
        string mail = ConsoleViewUtils.SafeInputString("e-mail");
        
        System.Console.WriteLine("Enter id number: ");
        int id = ConsoleViewUtils.SafeInputInt();
        
        System.Console.WriteLine("Enter title: ");
        string title = ConsoleViewUtils.SafeInputString("title");
        
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
        if (removedProfessor != null && removedProfessor.Id == -1)
            return;
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
            case "5":
                ShowProfessorSubjects();
                break;
            // case "6":
            //     ShowAndSortProfessors();
            //     break;
        }
    }

    private void ShowProfessorSubjects()
    {
        System.Console.WriteLine("Professors: ");
        string header = $"Id {"",5} | LastName {"",8} | Name {"",8} | Subjects";
        System.Console.WriteLine(header);
        foreach (Professor professor in _professorsDao.GetAllProfessors())
        {
            System.Console.Write($"Id: {professor.Id,5} | LastName: {professor.Lastname,8} | Name: {professor.Name,8}");
            foreach (Subject subject in professor.Subjects)
            {
                System.Console.Write($" | {subject.Id} | {subject.Name}");
            }
            System.Console.WriteLine();
        }
    }

    
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All Professors");
        System.Console.WriteLine("2: Add Professor");
        System.Console.WriteLine("3: Update Professor");
        System.Console.WriteLine("4: Remove Professor");
        System.Console.WriteLine("5: Show professor subjects");
        System.Console.WriteLine("0: Back");
    }
}