using CLI.Console;
using CLI.DAO;
using CLI.Model;

namespace CLI;
class Program
{
    static void Main()
    {
        AddressDAO addrDAO = new AddressDAO();
        IndexDAO idxDAO = new IndexDAO();
        StudentDAO studDAO = new StudentDAO(addrDAO, idxDAO);
        
        ProfessorDAO profDAO = new ProfessorDAO();
        SubjectDAO subjDAO = new SubjectDAO();
        StudentSubjectDAO studSubjDAO = new StudentSubjectDAO(studDAO, subjDAO);
        
        DepartmentDAO depDAO = new DepartmentDAO();
        ProfessorDepartmentDAO profDepDAO = new ProfessorDepartmentDAO(profDAO, depDAO);
        ProfessorSubjectDAO profSubjDAO = new ProfessorSubjectDAO(profDAO, subjDAO);
        
        ExamGradeDAO examGrDAO = new ExamGradeDAO(studDAO, subjDAO);
        
        ConsoleView consoleView = new ConsoleView();
        consoleView.RunMenu(studDAO, profDAO, subjDAO, depDAO, examGrDAO, idxDAO, addrDAO);
    }
}
