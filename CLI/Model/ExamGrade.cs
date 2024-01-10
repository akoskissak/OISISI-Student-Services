using CLI.Storage.Serialization;

namespace CLI.Model;

public class ExamGrade : ISerializable
{
    public Student Student { get; set; }
    public Subject Subject { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int Grade { get; set; }
    public DateOnly Date { get; set; }
    public int Id { get; set; }


    public ExamGrade()
    {
    }

    public ExamGrade(int studId, int subjectId,int grade, DateOnly date)
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
            Id.ToString(),
            StudentId.ToString(),
            SubjectId.ToString(),
            Grade.ToString(),
            Date.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        StudentId = int.Parse(values[1]);
        SubjectId = int.Parse(values[2]);
        Grade = int.Parse(values[3]);
        Date = DateOnly.Parse(values[4]);
    }

    public override string ToString()
    {
        return $"Id: {Id,5} | StudentId: {StudentId,2} | SubjectId: {SubjectId,2} | Grade: {Grade,2} | Date {Date,5}";
    }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        ExamGrade other = (ExamGrade)obj;
        return StudentId == other.StudentId && SubjectId == other.SubjectId;
    }

}