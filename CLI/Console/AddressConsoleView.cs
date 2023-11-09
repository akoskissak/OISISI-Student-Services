using CLI.DAO;
using CLI.Model;

namespace CLI.Console;

public class AddressConsoleView
{
    private readonly AddressDAO _addressDao;

    public AddressConsoleView(AddressDAO addressDao)
    {
        _addressDao = addressDao;
    }

    private Address InputAddress()
    {
        System.Console.WriteLine("ADDRESS\nEnter street: ");
        string street = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter number: ");
        int number = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Enter city: ");
        string city = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter country: ");
        string country = System.Console.ReadLine() ?? string.Empty;

        return new Address(street, number, city, country);
    }

    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            // case "1":
            //     ShowAllVehicles();
            //     break;
            case "2":
                AddAddress();
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

    private void AddAddress()
    {
        Address address = InputAddress();
        _addressDao.AddAddress(address);
        System.Console.WriteLine("Address added");
    }
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All addresses");
        System.Console.WriteLine("2: Add address");
        System.Console.WriteLine("3: Update address");
        System.Console.WriteLine("4: Remove address");
        System.Console.WriteLine("5: Show and sort addresses");
        System.Console.WriteLine("0: Close");
    }
}