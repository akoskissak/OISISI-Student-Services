using System.Text;
using CLI.Storage.Serialization;

namespace CLI.Model;

public enum Status
{
    B,
    S
};
public class Student : ISerializable
{
    public string Lastname { get; set; }
    public string Name { get; set; }
    public int Id { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Address Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Index Index { get; set; }
    public int CurrentYearOfStudy { get; set; }
    public Status Status { get; set; }
    public double AverageGrade { get; set; }
    public List<Subject> SubmittedSubjects { get; set; }
    public List<Subject> UnsubmittedSubjects { get; set; }

    public Student()
    {
        SubmittedSubjects = new List<Subject>();
        UnsubmittedSubjects = new List<Subject>();
    }

    public Student(string lastname, string name, DateTime date, string street, int number, string city, string country,
        string phonenumber, string email, string programme, int enrollNum, int enrollYear, int year, Status status, double avgGrade)
    {
        Lastname = lastname;
        Name = name;
        DateOfBirth = date;
        Address = new Address(street, number, city, country);
        Index = new Index(programme, enrollNum, enrollYear);
        PhoneNumber = phonenumber;
        Email = email;
        CurrentYearOfStudy = year;
        Status = status;
        AverageGrade = avgGrade;
        SubmittedSubjects = new List<Subject>();
        UnsubmittedSubjects = new List<Subject>();
    }

    public override string ToString()
    {
        return $"ID: {Id} | LASTNAME: {Lastname} | NAME: {Name} | DATEOFBIRTH: {DateOfBirth} | {Address} {Index} PHONENUMBER: {PhoneNumber} | EMAIL: {Email} | CURRENTYEAROFSTUDY: {CurrentYearOfStudy} | STATUS: {Status.ToString()} | AVERAGEGRADE: {AverageGrade} |";
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
            CurrentYearOfStudy.ToString(),
            Status.ToString(),
            AverageGrade.ToString(),
            Index.Id.ToString(),
        };
        
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Lastname = values[1];
        Name = values[2];
        DateOfBirth = DateTime.Parse(values[3]);
        PhoneNumber = values[4];
        Email = values[5];
        CurrentYearOfStudy = int.Parse(values[6]);
        Status = (Status)Enum.Parse(typeof(Status), values[7]);
        AverageGrade = double.Parse(values[8]);
        Index.Id = int.Parse(values[9]);
    }
}