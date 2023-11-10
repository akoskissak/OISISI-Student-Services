using CLI.Console;
using CLI.Storage.Serialization;

namespace CLI.Model;

public enum SemesterType {Letnji, Zimski}
public class Subject : ISerializable
{
    public int SubjectCode { get; set; }
    public string Name { get; set; }
    public SemesterType Semestar { get; set; }
    public int YearOfStudy { get; set; }
    public Professor Professor { get; set; }
    public int ProfessorId { get; set; }
    public int Espb { get; set; }
    public List<Student> StudentsPassed { get; set; }
    public List<Student> StudentsDidntPass{ get; set; }

    private StudentSubject studentSubject { get; set; }
    private int studentSubjectId { get; set; } 
    public int Id { get; set; }
    public Subject()
    {
        StudentsPassed = new List<Student>();
        StudentsDidntPass = new List<Student>();
    }
    public Subject(string name, SemesterType semestar, int yearOfStudy,int espb,  int professorId)
    {
        Name = name;
        ProfessorId = professorId;
        Semestar = semestar;
        YearOfStudy = yearOfStudy;
        Espb = espb;
        StudentsPassed = new List<Student>();
        StudentsDidntPass = new List<Student>();
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            SubjectCode.ToString(),
            Name,
            Semestar.ToString(),
            YearOfStudy.ToString(),
            Espb.ToString(),
            ProfessorId.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        SubjectCode = int.Parse(values[1]);
        Name = values[2];
        Semestar = (SemesterType)Enum.Parse(typeof(SemesterType), values[3]);
        YearOfStudy = int.Parse(values[4]);
        Espb = int.Parse(values[5]);
        ProfessorId = int.Parse(values[6]);
    }

    public override string ToString()
    {
        return $"SubjectCode: {SubjectCode,2} | Name: {Name,3} | Semester: {Semestar, 3} | YearOfStudy {YearOfStudy, 3} | ESPB: {Espb, 3} | ProfessorId: {ProfessorId, 3}";
    }
}