using CLI.Storage.Serialization;

namespace CLI.Model;

public class Department : ISerializable
{
    public string DepCode { get; set; }
    public string Name { get; set; }
    public string Chief { get; set; }
    public List<Professor> Professors { get; set; }

    public int Id { get; set; }
    public Department()
    {
        Professors = new List<Professor>();
    }

    public Department(string code, string name, string chief)
    {
        DepCode = code;
        Name = name;
        Chief = chief;
        Professors = new List<Professor>();
    }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            DepCode.ToString(),
            Name,
            Chief
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        DepCode = values[0];
        Name = values[1];
        Chief = values[2];
    }

    public override string ToString()
    {
        return $"DepCode: {DepCode,2} | Name: {Name,2} | Chief: {Chief,2}";
    }
}