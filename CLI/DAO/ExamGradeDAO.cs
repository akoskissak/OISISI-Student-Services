using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class ExamGradeDAO
{
    private readonly List<ExamGrade> _examGrades;
    private readonly Storage<ExamGrade> _examGradeStorage;


    public ExamGradeDAO()
    {
        _examGradeStorage = new Storage<ExamGrade>("examGrades.txt");
        _examGrades = _examGradeStorage.Load();
    }

    public ExamGrade AddExamGrade(ExamGrade examGrade)
    {
        _examGrades.Add(examGrade);
        _examGradeStorage.Save(_examGrades);
        return examGrade;
    }
}