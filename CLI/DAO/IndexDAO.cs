using CLI.Storage;
using Index = CLI.Model.Index;

namespace CLI.DAO;

public class IndexDAO
{
    private readonly List<Index> _indexes;
    private readonly Storage<Index> _storage;

    public IndexDAO()
    {
        _storage = new Storage<Index>("indexes.txt");
        _indexes = _storage.Load();
    }

    private Index AddIndex(Index index)
    {
        _indexes.Add(index);
        _storage.Save(_indexes);
        return index;
    }
}