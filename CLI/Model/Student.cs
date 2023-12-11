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
    public DateOnly DateOfBirth { get; set; }
    public Address Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Index Index { get; set; }
    public int CurrentYearOfStudy { get; set; }
    public Status Status { get; set; }
    public double AverageGrade { get; set; }
    
    public List<ExamGrade> Grades { get; set; }
    public List<Subject> UnsubmittedSubjects { get; set; }

    public Student()
    {
        Grades = new List<ExamGrade>();
        UnsubmittedSubjects = new List<Subject>();
        Address = new Address();
        Index = new Index();
    }

    public Student(string lastname, string name, DateOnly date, string street, string number, string city, string country,
        string phonenumber, string email, string programme, int enrollNum, int enrollYear, int year, Status status)
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
        Grades = new List<ExamGrade>();
        UnsubmittedSubjects = new List<Subject>();
    }

    public void SetAverageGrade()
    {
        AverageGrade = 0;
        foreach (ExamGrade examGrade in Grades)
        {
            AverageGrade += examGrade.Grade;
        }

        AverageGrade /= Grades.Count;
    }

    public override string ToString()
    {
        return $"Id: {Id, 5} | LastName: {Lastname, 8} | Name: {Name, 8} | DateOfBirth: {DateOfBirth, 15} | PhoneNumber: {PhoneNumber, 12} | Email: {Email, 12} | CurrentYearOfStudy: {CurrentYearOfStudy, 1} | Status: {Status.ToString(), 1} | AverageGrade: {String.Format("{0:0.00}", AverageGrade), 4} | {Address} | {Index} ";
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
            String.Format("{0:0.00}", AverageGrade),
            Address.Street,
            Address.Number,
            Address.City,
            Address.Country,
            Index.StudyProgrammeMark,
            Index.EnrollmentNumber.ToString(),
            Index.EnrollmentYear.ToString()
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
        CurrentYearOfStudy = int.Parse(values[6]);
        Status = (Status)Enum.Parse(typeof(Status), values[7]);
        AverageGrade = double.Parse(values[8]);
        Address.Street = values[9];
        Address.Number = values[10];
        Address.City = values[11];
        Address.Country = values[12];
        Index.StudyProgrammeMark = values[13];
        Index.EnrollmentNumber = int.Parse(values[14]);
        Index.EnrollmentYear = int.Parse(values[15]);
    }
}