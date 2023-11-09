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

    private int GenerateId()
    {
        if (_addresses.Count == 0)
            return 0;
        return _addresses[^1].Id + 1;
    }

    public Address AddAddress(Address address)
    {
        address.Id = GenerateId();
        _addresses.Add(address);
        _addressStorage.Save(_addresses);
        return address;
    }
}