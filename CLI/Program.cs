using CLI.Console;
using CLI.DAO;
using CLI.Model;

namespace CLI;
class Program
{
    static void Main()
    {
        AddressDAO addressDao = new AddressDAO();
        DepartmentDAO departmentDao = new DepartmentDAO();
        ExamGradeDAO examGradeDao = new ExamGradeDAO();
        IndexDAO indexDao = new IndexDAO();
        ProfessorDAO professorDao = new ProfessorDAO();
        StudentDAO studentDao = new StudentDAO();
        SubjectDAO subjectDao = new SubjectDAO();

        AddressConsoleView addressConsoleView = new AddressConsoleView(addressDao);
        addressConsoleView.RunMenu();
        
        DepartmentConsoleView departmentConsoleView = new DepartmentConsoleView(departmentDao);
        departmentConsoleView.RunMenu();
        
        ExamGradeConsoleView examGradeConsoleView = new ExamGradeConsoleView(examGradeDao);
        examGradeConsoleView.RunMenu();
        
        IndexConsoleView indexConsoleView = new IndexConsoleView(indexDao);
        indexConsoleView.RunMenu();
        
        ProfessorConsoleView professorConsoleView = new ProfessorConsoleView(professorDao);
        professorConsoleView.RunMenu();
        
        StudentConsoleView studentConsoleView = new StudentConsoleView(studentDao);
        studentConsoleView.RunMenu();
        
        SubjectConsoleView subjectConsoleView = new SubjectConsoleView(subjectDao);
        subjectConsoleView.RunMenu();
        
    }
}
