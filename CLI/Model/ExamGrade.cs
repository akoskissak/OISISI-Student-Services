using CLI.Storage.Serialization;

namespace CLI.Model;

public class ExamGrade : ISerializable
{
    public Student Student { get; set; }
    public Subject Subject { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int Grade { get; set; }
    public DateTime Date { get; set; }


    public ExamGrade()
    {
    }

    public ExamGrade(int studId, int subjectId,int grade, DateTime date)
    {
        StudentId = studId;
        SubjectId = subjectId;
        Grade = grade;
        Date = date;
    }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            StudentId.ToString(),
            SubjectId.ToString(),
            Grade.ToString(),
            Date.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        StudentId = int.Parse(values[0]);
        SubjectId = int.Parse(values[1]);
        Grade = int.Parse(values[2]);
        Date = DateTime.Parse(values[3]);
    }
}