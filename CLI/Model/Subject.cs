using CLI.Storage.Serialization;

namespace CLI.Model;

public enum TipSemestra{Letnji, Zimski}
public class Subject : ISerializable
{
    public int SubjectCode { get; set; }
    public string Name { get; set; }
    public TipSemestra Semestar { get; set; }
    public int YearOfStudy { get; set; }
    public Professor Professor { get; set; }
    public int Espb { get; set; }
    public List<Student> StudentsPassed { get; set; }
    public List<Student> StudentsDidntPass{ get; set; }

    public Subject()
    {
        StudentsPassed = new List<Student>();
        StudentsDidntPass = new List<Student>();
    }
    public Subject(int subjectCode, string name, TipSemestra semestar, int yearOfStudy, int espb)
    {
        SubjectCode = subjectCode;
        Name = name;
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
            SubjectCode.ToString(),
            Name,
            Semestar.ToString(),
            YearOfStudy.ToString(),
            Espb.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        SubjectCode = int.Parse(values[0]);
        Name = values[1];
        Semestar = (TipSemestra)Enum.Parse(typeof(TipSemestra), values[2]);
        YearOfStudy = int.Parse(values[3]);
        Espb = int.Parse(values[4]);
    }
}