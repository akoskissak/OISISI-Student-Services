using CLI.Storage.Serialization;

namespace CLI.Model;

public class Address : ISerializable
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    
    public int Id { get; set; }

    public Address() {}

    public Address(string street, string num, string city, string country)
    {
        Street = street;
        Number = num;
        City = city;
        Country = country;
    }
    
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Street,
            Number.ToString(),
            City,
            Country
        };
        return csvValues;
    }

    public override string ToString()
    {
        return $"Street: {Street, 10} | Number: {Number, 2} | City: {City, 6} | Country: {Country, 6}";
    }

    public void FromCSV(string[] values)
    {
        Street = values[0];
        Number = values[1];
        City = values[2];
        Country = values[3];
    }
}