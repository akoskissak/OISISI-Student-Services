using CLI.Console;
using CLI.DAO;
using CLI.Model;

namespace CLI;
class Program
{
    static void Main()
    {
        ProfessorDAO profDAO = new ProfessorDAO();
        ProfessorConsoleView profView = new ProfessorConsoleView(profDAO);
        profView.RunMenu();

        StudentDAO studDAO = new StudentDAO();
        StudentConsoleView studView = new StudentConsoleView(studDAO);
        studView.RunMenu();
        
        DepartmentDAO depDAO = new DepartmentDAO();
        DepartmentConsoleView depView = new DepartmentConsoleView(depDAO);
        depView.RunMenu();

        SubjectDAO subjDAO = new SubjectDAO();
        SubjectConsoleView subjView = new SubjectConsoleView(subjDAO);
        subjView.RunMenu();
    }
}
