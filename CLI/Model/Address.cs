using CLI.Storage.Serialization;

namespace CLI.Model;

public class Address : ISerializable
{
    public string Street { get; set; }
    public int Number { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int ProfessorId { get; set; }
    public int StudentId { get; set; }
    
    public int Id { get; set; }

    public Address()
    {
        ProfessorId = -1;
        StudentId = -1;
    }

    public Address(string street, int num, string city, string country)
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
            Country,
            ProfessorId.ToString(),
            StudentId.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Street = values[0];
        Number = int.Parse(values[1]);
        City = values[2];
        Country = values[3];
        ProfessorId = int.Parse(values[4]);
        StudentId = int.Parse(values[5]);
    }
}