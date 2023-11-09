using CLI.Storage.Serialization;

namespace CLI.Model;

public class Index : ISerializable
{
    public string StudyProgrammeMark { get; set; }
    public int EnrollmentNumber { get; set; }
    public int EnrollmentYear { get; set; }
    
    public int Id { get; set; }

    public Index()
    {
        
    }

    public Index(string studyProgMark, int enrollNum, int enrollYear)
    {
        StudyProgrammeMark = studyProgMark;
        EnrollmentNumber = enrollNum;
        EnrollmentYear = enrollYear;
    }

    public override string ToString()
    {
        return $"ID: {Id} | StudyProgrammeMark: {StudyProgrammeMark} | EnrollmentNumber: {EnrollmentNumber} | EnrollmentYear: {EnrollmentYear} |";
    }
    
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            StudyProgrammeMark,
            EnrollmentNumber.ToString(),
            EnrollmentYear.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        StudyProgrammeMark = values[1];
        EnrollmentNumber = int.Parse(values[2]);
        EnrollmentYear = int.Parse(values[3]);
    }
}