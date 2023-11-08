using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class AddressDAO
{
    private readonly List<Address> _addresses;
    private readonly Storage<Address> _addressStorage;


    public AddressDAO()
    {
        _addressStorage = new Storage<Address>("addresses.txt");
        _addresses = _addressStorage.Load();
    }

    public Address AddAddress(Address address)
    {
        _addresses.Add(address);
        _addressStorage.Save(_addresses);
        return address;
    }
}