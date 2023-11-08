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
}