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
            //     ShowAllVehicles();
            //     break;
            case "2":
                AddIndex();
                break;
            // case "3":
            //     UpdateVehicle();
            //     break;
            // case "4":
            //     RemoveVehicle();
            //     break;
            // case "5":
            //     ShowAndSortVehicles();
            //     break;
        }
    }

    private void AddIndex()
    {
        Index index = InputIndex();
        _indexDao.AddIndex(index);
        System.Console.WriteLine("Index added");
    }
    
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All indexes");
        System.Console.WriteLine("2: Add index");
        System.Console.WriteLine("3: Update index");
        System.Console.WriteLine("4: Remove index");
        System.Console.WriteLine("5: Show and sort indexes");
        System.Console.WriteLine("0: Close");
    }
}