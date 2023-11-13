namespace CLI.Storage.Serialization;

public interface ISerializable
{
    string[] ToCSV();

    void FromCSV(string[] values);
}