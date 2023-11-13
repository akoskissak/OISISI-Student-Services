using CLI.Model;

namespace CLI.Console;

public class ConsoleViewUtils
{
    /* Sledeca metoda je kopirana iz projekta CRUDExample sa vezbi */
    public static int SafeInputInt()
    {
        int input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!int.TryParse(rawInput, out input))
        {
            System.Console.WriteLine("Not a valid number, try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    public static DateOnly SafeInputDateOnly()
    {
        DateOnly input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!DateOnly.TryParse(rawInput, out input))
        {
            System.Console.WriteLine("Not a valid date, try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }
    
    public static SemesterType SafeInputSemester()
    {
        SemesterType input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!SemesterType.TryParse(rawInput, out input))
        {
            System.Console.WriteLine("Not a valid semester type, try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    public static Status SafeInputStatus()
    {
        Status input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!Status.TryParse(rawInput, out input))
        {
            System.Console.WriteLine("Not a valid status, try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    public static double SafeInputDouble()
    {
        double input;
        
        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!double.TryParse(rawInput, out input))
        {
            System.Console.WriteLine("Not a valid number, try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }

    public static string SafeInputString(string inputName)
    {
        string rawInput = System.Console.ReadLine() ?? string.Empty;
        // string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (rawInput == string.Empty)
        {
            System.Console.WriteLine($"Not a valid {inputName}. Try again: ");
            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return rawInput;
    }
}