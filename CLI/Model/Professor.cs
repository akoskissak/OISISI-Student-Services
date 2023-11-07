using System.Text;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Professor : ISerializable
{
    public string Lastname { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Address Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int Idnumber { get; set; }
    public string Title { get; set; }
    public int YearsOfService { get; set; }
    public List<Subject> Subjects { get; set; }

    public Professor()
    {
        Subjects = new List<Subject>();
    }

    public Professor(string lastname, string name, DateTime date, Address add, string phone,
                    string mail, int id, string title, int years)
    {
        Lastname = lastname;
        Name = name;
        DateOfBirth = date;
        Address = add;
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
            Lastname,
            Name,
            DateOfBirth.ToString(),
            PhoneNumber,
            Email,
            Idnumber.ToString(),
            Title,
            YearsOfService.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Lastname = values[0];
        Name = values[1];
        DateOfBirth = DateTime.Parse(values[2]);
        PhoneNumber = values[3];
        Email = values[4];
        Idnumber = int.Parse(values[5]);
        Title = values[6];
        YearsOfService = int.Parse(values[7]);
    }
}