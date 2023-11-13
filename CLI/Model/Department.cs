using CLI.Storage.Serialization;

namespace CLI.Model;

public class Department : ISerializable
{
    public string DepCode { get; set; }
    public string Name { get; set; }
    public int ChiefId { get; set; }
    public Professor? Chief { get; set; }
    public List<Professor> Professors { get; set; }
    public int Id { get; set; }
    
    public Department()
    {
        Professors = new List<Professor>();
    }

    public Department(string code, string name, int chiefId)
    {
        DepCode = code;
        Name = name;
        ChiefId = chiefId;
        Professors = new List<Professor>();
    }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            DepCode.ToString(),
            Name,
            ChiefId.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        DepCode = values[1];
        Name = values[2];
        ChiefId = int.Parse(values[3]);
    }

    public override string ToString()
    {
        if (Chief != null)
            return $"Id: {Id,5} | DepCode: {DepCode,2} | Name: {Name,2} | Chief: {Chief.Id} | LastName: {Chief.Lastname} | Name: {Chief.Name}";
        return $"Id: {Id,5} | DepCode: {DepCode,2} | Name: {Name,2} | No Chief";
    }
}