using CLI.Console;
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

    public ExamGrade? UpdateExamGrade(ExamGrade examGrade)
    {
        ExamGrade? oldExamGrade = GetExamByIds(examGrade.StudentId, examGrade.SubjectId);
        if (oldExamGrade is null)
            return null;
        oldExamGrade.Grade = examGrade.Grade;
        oldExamGrade.Date = examGrade.Date;

        return oldExamGrade;
    }

    private ExamGrade? GetExamByIds(int studId, int subjId)
    {
        return _examGrades.Find(exam => (exam.StudentId == studId
                                         && exam.SubjectId == subjId));
    }
}