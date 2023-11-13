using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class StudentExamGradeDAO
{
    private readonly List<StudentExamGrade> _studentExamGrade;
    private readonly Storage<StudentExamGrade> _studentExamGradeStorage;

    private StudentDAO _studentDao;
    private ExamGradeDAO _examGradeDao;

    public StudentExamGradeDAO(StudentDAO studentDao, ExamGradeDAO examGradeDao)
    {
        _studentExamGradeStorage = new Storage<StudentExamGrade>("studentExamGrade.txt");
        _studentExamGrade = _studentExamGradeStorage.Load();

        _studentDao = studentDao;
        _examGradeDao = examGradeDao;
        
    }
}