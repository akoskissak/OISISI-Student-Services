using CLI.Storage.Serialization;

namespace CLI.Model;

public class ProfessorDepartment : ISerializable
{
    public int ProfessorId { get; set; }
    public int DepartmentId { get; set; }
    public int Id { get; set; }

    public ProfessorDepartment() {}
    
    public ProfessorDepartment(int professorId, int departmentId)
    {
        ProfessorId = professorId;
        DepartmentId = departmentId;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            ProfessorId.ToString(),
            DepartmentId.ToString()
        };

        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        ProfessorId = int.Parse(values[1]);
        DepartmentId = int.Parse(values[2]);
    }
}