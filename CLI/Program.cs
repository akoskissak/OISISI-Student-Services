using CLI.Console;
using CLI.DAO;
using CLI.Model;

namespace CLI;
class Program
{
    static void Main()
    {
        ProfessorDAO profDAO = new ProfessorDAO();
        StudentDAO studDAO = new StudentDAO();
        SubjectDAO subjDAO = new SubjectDAO();
        AddressDAO addrDAO = new AddressDAO();
        ExamGradeDAO examGrDAO = new ExamGradeDAO();
        IndexDAO indDAO = new IndexDAO();
        DepartmentDAO depDAO = new DepartmentDAO();
        
        ConsoleView consoleView = new ConsoleView();
        consoleView.RunMenu(studDAO, profDAO, subjDAO, depDAO, examGrDAO, indDAO, addrDAO);
    }
}
