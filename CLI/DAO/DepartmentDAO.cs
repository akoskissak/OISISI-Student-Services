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

    public Department? UpdateDepartment(Department department)
    {
        Department? oldDepartment = GetDepartmentByCode(department.DepCode);
        if (oldDepartment is null)
            return null;
        oldDepartment.Name = department.Name;
        oldDepartment.Chief = department.Chief;
        
        _departmentStorage.Save(_departments);
        return oldDepartment;
    }

    public Department? GetDepartmentByCode(int code)
    {
        return _departments.Find(d => d.DepCode == code);
    }

    public Department? RemoveDepartment(int id)
    {
        Department? department = GetDepartmentByCode(id);
        if (department == null) return null;

        _departments.Remove(department);
        _departmentStorage.Save(_departments);
        return department;
    }
}