using CLI.Storage.Serialization;

namespace CLI.Model;

public class StudentSubject : ISerializable
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int Id { get; set; }
    
    public StudentSubject() {}
    public StudentSubject(int studentId, int subjectId)
    {
        StudentId = studentId;
        SubjectId = subjectId;
    }
public string[] ToCSV()
{
    string[] csvValues =
    {
        Id.ToString(),
        StudentId.ToString(),
        SubjectId.ToString()
    };

    return csvValues;
}

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        StudentId = int.Parse(values[1]);
        SubjectId = int.Parse(values[2]);
    }
}