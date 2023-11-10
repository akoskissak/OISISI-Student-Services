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

    private int GenerateDepartmentId()
    {
        if (_departments.Count == 0)
            return 0;
        return _departments[^1].Id + 1;
    }
    
    public Department AddDepartment(Department department)
    {
        department.Id = GenerateDepartmentId();
        _departments.Add(department);
        _departmentStorage.Save(_departments);
        return department;
    }

    public Department? UpdateDepartment(Department department)
    {
        Department? oldDepartment = GetDepartmentById(department.Id);
        if (oldDepartment is null)
            return null;
        oldDepartment.Name = department.Name;
        oldDepartment.Chief = department.Chief;
        
        _departmentStorage.Save(_departments);
        return oldDepartment;
    }

    public Department? GetDepartmentById(int id)
    {
        return _departments.Find(d => d.Id == id);
    }

    public Department? RemoveDepartment(int id)
    {
        Department? department = GetDepartmentById(id);
        if (department == null) return null;

        _departments.Remove(department);
        _departmentStorage.Save(_departments);
        return department;
    }

    public List<Department> GetAllDepartments()
    {
        return _departments;
    }
}