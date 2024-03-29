using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public enum SemesterType {Letnji, Zimski}
public class Subject : ISerializable
{
    public string SubjectCode { get; set; }
    public string Name { get; set; }
    public SemesterType Semester { get; set; }
    public int YearOfStudy { get; set; }
    public Professor Professor { get; set; }
    public int ProfessorId { get; set; }
    public int Espb { get; set; }
    public List<Student> StudentsPassed { get; set; }
    public List<Student> StudentsDidNotPass{ get; set; }
    public int Id { get; set; }
    public Subject()
    {
        ProfessorId = -1;
        StudentsPassed = new List<Student>();
        StudentsDidNotPass = new List<Student>();
    }
    public Subject(string subjectCode, string name, SemesterType semester, int yearOfStudy,int espb,  int professorId)
    {
        SubjectCode = subjectCode;
        Name = name;
        ProfessorId = professorId;
        Semester = semester;
        YearOfStudy = yearOfStudy;
        Espb = espb;
        StudentsPassed = new List<Student>();
        StudentsDidNotPass = new List<Student>();
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            SubjectCode.ToString(),
            Name,
            Semester.ToString(),
            YearOfStudy.ToString(),
            Espb.ToString(),
            ProfessorId.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        SubjectCode = values[1];
        Name = values[2];
        Semester = (SemesterType)Enum.Parse(typeof(SemesterType), values[3]);
        YearOfStudy = int.Parse(values[4]);
        Espb = int.Parse(values[5]);
        ProfessorId = int.Parse(values[6]);
    }

    public override string ToString()
    {
        if (ProfessorId != -1)
            return $"Id: {Id,5} | SubjectCode: {SubjectCode,2} | Name: {Name,3} | Semester: {Semester, 3} | YearOfStudy {YearOfStudy, 3} | ESPB: {Espb, 3} | ProfessorId: {Professor.Id, 3} | LastName: {Professor.Lastname} | Name: {Professor.Name}";
        return $"Id: {Id,5} | SubjectCode: {SubjectCode,2} | Name: {Name,3} | Semester: {Semester,3} | YearOfStudy {YearOfStudy,3} | ESPB: {Espb,3} | No Professor";
    }
}