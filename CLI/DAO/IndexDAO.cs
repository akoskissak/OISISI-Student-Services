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

    private int GenerateId()
    {
        if (_indexes.Count == 0)
            return 0;
        return _indexes[^1].Id + 1;
    }
    public Index AddIndex(Index index)
    {
        index.Id = GenerateId();
        _indexes.Add(index);
        _storage.Save(_indexes);
        return index;
    }

    public Index? UpdateIndex(Index index)
    {
        Index? oldIndex = GetIndexById(index.Id);
        if (oldIndex is null) return null;

        oldIndex.StudyProgrammeMark = index.StudyProgrammeMark;
        oldIndex.EnrollmentNumber = index.EnrollmentNumber;
        oldIndex.EnrollmentYear = index.EnrollmentYear;

        _storage.Save(_indexes);
        return oldIndex;
    }

    private Index? GetIndexById(int id)
    {
        return _indexes.Find(i => i.Id == id);
    }

    public Index? RemoveIndex(int id)
    {
        Index? index = GetIndexById(id);
        if (index == null) return null;

        _indexes.Remove(index);
        _storage.Save(_indexes);
        return index;

    }
}