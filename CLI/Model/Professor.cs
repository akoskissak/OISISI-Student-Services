using CLI.Storage.Serialization;

namespace CLI.Model;

public class Professor : ISerializable
{
    public string Lastname { get; set; }
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Address Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int Idnumber { get; set; }
    public string Title { get; set; }
    public int YearsOfService { get; set; }
    public List<Subject> Subjects { get; set; }
    public int Id { get; set; }
    
    // Polje koje sluzi da ne mozemo obrisati profesora ako je sef neke katedre
    public int IdOfChiefDepartment { get; set; }

    public Professor()
    {
        IdOfChiefDepartment = -1;
        Subjects = new List<Subject>();
        Address = new Address();
    }

    public Professor(string lastname, string name, DateOnly date, string street, string number, string city, string country, string phone,
                    string mail, int id, string title, int years)
    {
        IdOfChiefDepartment = -1;
        Lastname = lastname;
        Name = name;
        DateOfBirth = date;
        Address = new Address(street, number, city, country);
        PhoneNumber = phone;
        Email = mail;
        Idnumber = id;
        Title = title;
        YearsOfService = years;
        Subjects = new List<Subject>();

    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Lastname,
            Name,
            DateOfBirth.ToString(),
            PhoneNumber,
            Email,
            Idnumber.ToString(),
            Title,
            YearsOfService.ToString(),
            Address.Street,
            Address.Number,
            Address.City,
            Address.Country,
            IdOfChiefDepartment.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Lastname = values[1];
        Name = values[2];
        DateOfBirth = DateOnly.Parse(values[3]);
        PhoneNumber = values[4];
        Email = values[5];
        Idnumber = int.Parse(values[6]);
        Title = values[7];
        YearsOfService = int.Parse(values[8]);
        Address.Street = values[9];
        Address.Number = values[10];
        Address.City = values[11];
        Address.Country = values[12];
        IdOfChiefDepartment = int.Parse(values[13]);
    }

    public override string ToString()
    {
        return $"Id: {Id,5} | LastName: {Lastname, 2} | Name: {Name, 2} | DateOfBirth: {DateOfBirth, 2} | PhoneNumber: {PhoneNumber, 2} | Email: {Email, 2} | IdNumber: {Idnumber, 2} | Title: {Title, 2} | YearsOfService: {YearsOfService, 2} | {Address} | IdOfChiefDepartment: {IdOfChiefDepartment, 2}";
    }
}