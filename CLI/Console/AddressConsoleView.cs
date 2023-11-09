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
            //     break;
            case "2":
                AddAddress();
                break;
            case "3":
                UpdateAddress();
                break;
            // case "4":
            //     RemoveAddress();
            //     break;
            // case "5":
            //     ShowAndSortAddresses();
            //     break;
        }
    }

    private void AddAddress()
    {
        Address address = InputAddress();
        _addressDao.AddAddress(address);
        System.Console.WriteLine("Address added");
    }

    private int InputId()
    {
        System.Console.WriteLine("Enter address id: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    private void UpdateAddress()
    {
        int id = InputId();
        Address? address = InputAddress();
        address.Id = id;
        Address? updateAddress = _addressDao.UpdateAddress(address);
        if (updateAddress == null)
        {
            System.Console.WriteLine("Address not found");
            return;
        }
        
        System.Console.WriteLine("Address updated");
    }
    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All addresses");
        System.Console.WriteLine("2: Add address");
        System.Console.WriteLine("3: Update address");
        System.Console.WriteLine("4: Remove address");
        System.Console.WriteLine("5: Show and sort addresses");
        System.Console.WriteLine("0: Back");
    }
}