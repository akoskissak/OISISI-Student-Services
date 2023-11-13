using CLI.Storage.Serialization;

namespace CLI.DAO;

public class ProfessorSubject : ISerializable
{
    public int ProfessorId { get; set; }
    public int SubjectId { get; set; }
    public int Id { get; set; }
    
    public ProfessorSubject() {}
    public ProfessorSubject(int professorId, int subjectId)
    {
        ProfessorId = professorId;
        SubjectId = subjectId;
    }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            ProfessorId.ToString(),
            SubjectId.ToString()
        };

        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        ProfessorId = int.Parse(values[1]);
        SubjectId = int.Parse(values[2]);
    }
}