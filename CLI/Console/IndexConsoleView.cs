using CLI.DAO;
using Index = CLI.Model.Index;

namespace CLI.Console;

public class IndexConsoleView
{
    private readonly IndexDAO _indexDao;

    public IndexConsoleView(IndexDAO indexDao)
    {
        _indexDao = indexDao;
    }

    private Index InputIndex()
    {
        System.Console.WriteLine("INDEX\nEnter programme: ");
        string programme = System.Console.ReadLine() ?? string.Empty;
        
        System.Console.WriteLine("Enter enrollment number: ");
        int enrollNum = ConsoleViewUtils.SafeInputInt();
        
        System.Console.WriteLine("Enter enrollment year: ");
        int enrollYear = ConsoleViewUtils.SafeInputInt();

        return new Index(programme, enrollNum, enrollYear);
    }
    
    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            // case "1":
            //     ShowAllIndexes();
            //     break;
            case "2":
                AddIndex();
                break;
            case "3":
                 UpdateIndex();
                 break;
            case "4":
                RemoveIndex(); 
                break;
            // case "5":
            //     ShowAndSortIndexes();
            //     break;
        }
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
    private void AddIndex()
    {
        Index index = InputIndex();
        _indexDao.AddIndex(index);
        System.Console.WriteLine("Index added");
    }

    private int InputId()
    {
        System.Console.WriteLine("Enter index id: ");
        return ConsoleViewUtils.SafeInputInt();
    }
    private void UpdateIndex()
    {
        int id = InputId();
        Index index = InputIndex();
        index.Id = id;
        Index? updateIndex = _indexDao.UpdateIndex(index);
        if (updateIndex == null)
        {
            System.Console.WriteLine("Index not found");
            return;
        }
        
        System.Console.WriteLine("Index updated");
    }

    private void RemoveIndex()
    {
        int id = InputId();
        Index? removedIndex = _indexDao.RemoveIndex(id);
        if (removedIndex is null)
        {
            System.Console.WriteLine("Index not found");
            return;
        }
        
        System.Console.WriteLine("Index removed");
    }
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All indexes");
        System.Console.WriteLine("2: Add index");
        System.Console.WriteLine("3: Update index");
        System.Console.WriteLine("4: Remove index");
        System.Console.WriteLine("5: Show and sort indexes");
        System.Console.WriteLine("0: Back");
    }
}