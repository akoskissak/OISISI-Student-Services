namespace CLI.Storage.Serialization;
/*
 * Ovaj kod smo uzeli sa trećih vežbi iz CRUDEexample
 */
public interface ISerializable
{
    string[] ToCSV();

    void FromCSV(string[] values);
}