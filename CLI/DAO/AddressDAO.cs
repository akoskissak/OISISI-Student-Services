/*using CLI.Model;
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

    public Address? UpdateAddress(Address address)
    {
        Address? oldAddress = GetAddressById(address.Id);
        if (oldAddress is null) return null;

        oldAddress.Street = address.Street;
        oldAddress.Number = address.Number;
        oldAddress.City = address.City;
        oldAddress.Country = address.Country;
        
        _addressStorage.Save(_addresses);
        return oldAddress;
    }

    public Address? RemoveAddress(int id)
    {
        Address? address = GetAddressById(id);
        if (address == null) return null;

        _addresses.Remove(address);
        _addressStorage.Save(_addresses);
        return address;
    }

    private Address? GetAddressById(int id)
    {
        return _addresses.Find(a => a.Id == id);
    }

    public List<Address> GetAllAddresses()
    {
        return _addresses;
    }
}*/