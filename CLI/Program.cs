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
        ProfessorConsoleView profView = new ProfessorConsoleView(profDAO);
        profView.RunMenu();

        StudentConsoleView studView = new StudentConsoleView(studDAO);
        studView.RunMenu();
        
        DepartmentDAO depDAO = new DepartmentDAO();
        DepartmentConsoleView depView = new DepartmentConsoleView(depDAO);
        depView.RunMenu();

        SubjectConsoleView subjView = new SubjectConsoleView(subjDAO);
        subjView.RunMenu();
    }
}
