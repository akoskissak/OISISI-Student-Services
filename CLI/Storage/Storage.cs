
using CLI.Storage.Serialization;

namespace CLI.Storage;
/*
 * Klasa čuva podatke u fajl sistem. Piše i čita podatke iz njega.
 * Storage koristi Serializer klasu za konverziju objekta u string
 * i obrnuto
 *
 * Ovaj kod smo uzeli sa trećih vežbi iz CRUDEexample
 */
public class Storage<T> where T : ISerializable, new()
{
    private readonly string _fileName = @"../../../Data/{0}";
    private readonly Serializer<T> _serializer = new();

    public Storage(string fileName)
    {
        _fileName = string.Format(_fileName, fileName);
    }

    public List<T> Load()
    {
        if (!File.Exists((_fileName)))
        {
            FileStream fs = File.Create((_fileName));
            fs.Close();
        }

        IEnumerable<string> lines = File.ReadLines(_fileName);
        List<T> objects = _serializer.FromCSV(lines);

        return objects;
    }

    public void Save(List<T> objects)
    {
        string serializedObjects = _serializer.ToCSV(objects);
        using (StreamWriter streamWriter = new StreamWriter(_fileName))
        {
            streamWriter.Write((serializedObjects));
        }
    }
}