using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class DepartmentDAO
{
    private readonly List<Department> _departments;
    private readonly Storage<Department> _departmentStorage;


    public DepartmentDAO()
    {
        _departmentStorage = new Storage<Department>("departments.txt");
        _departments = _departmentStorage.Load();
    }

    public Department AddDepartment(Department department)
    {
        _departments.Add(department);
        _departmentStorage.Save(_departments);
        return department;
    }
}