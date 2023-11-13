using CLI.Storage.Serialization;

namespace CLI.Model;

public class StudentExamGrade : ISerializable
{
    public int StudentId { get; set; }
    public int ExamGradeId { get; set; }
    public int Id { get; set; }

    public StudentExamGrade()
    {
        
    }

    public StudentExamGrade(int studentId, int examGradeId)
    {
        StudentId = studentId;
        ExamGradeId = examGradeId;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            StudentId.ToString(),
            ExamGradeId.ToString()
        };

        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        StudentId = int.Parse(values[1]);
        ExamGradeId = int.Parse(values[2]);
    }
}