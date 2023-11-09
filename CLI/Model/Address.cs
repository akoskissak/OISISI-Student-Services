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
            Id.ToString(),
            Street,
            Number.ToString(),
            City,
            Country,
            ProfessorId.ToString(),
            StudentId.ToString()
        };
        return csvValues;
    }

    public override string ToString()
    {
        return $"Id: {Id} | Street: {Street} | Number: {Number} | City: {City} | Country: {Country}";
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Street = values[1];
        Number = int.Parse(values[2]);
        City = values[3];
        Country = values[4];
        ProfessorId = int.Parse(values[5]);
        StudentId = int.Parse(values[6]);
    }
}