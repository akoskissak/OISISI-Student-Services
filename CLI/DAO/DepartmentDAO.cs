using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO;

public class DepartmentDAO
{
    private readonly List<Department> _departments;
    private readonly List<Professor> _professors;
    private readonly Storage<Department> _departmentStorage;
    private readonly Storage<Professor> _professorStorage;

    public Observable DepartmentObservable;
    public DepartmentDAO()
    {
        _departmentStorage = new Storage<Department>("departments.txt");
        _departments = _departmentStorage.Load();
        _professorStorage = new Storage<Professor>("professors.txt");
        _professors = _professorStorage.Load();

        DepartmentObservable = new Observable();
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
        DepartmentObservable.NotifyObservers();
        return department;
    }

    public Department? UpdateDepartment(Department department)
    {
        Department? oldDepartment = GetDepartmentById(department.Id);
        if (oldDepartment is null)
            return null;
        oldDepartment.DepCode = department.DepCode;
        oldDepartment.Name = department.Name;
        oldDepartment.ChiefId = department.ChiefId;
        if (department.Chief != null)
            oldDepartment.Chief = department.Chief;
        
        _departmentStorage.Save(_departments);
        DepartmentObservable.NotifyObservers();
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

        if (department.Professors.Count != 0 || department.ChiefId != -1)
        {
            System.Console.WriteLine("Cannot remove department that has professor/s or chief!");
            Department dep = new Department();
            dep.Id = -1;
            return dep;
        }

        _departments.Remove(department);
        _departmentStorage.Save(_departments);
        DepartmentObservable.NotifyObservers();
        return department;
    }

    public void AddProfessorsForDep(List<Professor> professors, int Id)
    {
        _departments.Find(department => department.Id == Id)!.Professors = professors;
        _departmentStorage.Save(_departments);
    }

    public List<Department> GetAllDepartments()
    {
        return _departments;
    }

    public bool HasDepartmentChief(int departmentId)
    {
        Department dep = GetDepartmentById(departmentId);
        if(dep.ChiefId != -1)
        {
            return true;
        }

        return false;
    }
    public void SetProfessorAsChief(int professorId, int departmentId)
    {
        Professor professor = _professors.Find(p => p.Id == professorId);
        Department department = GetDepartmentById(departmentId);
        department.Chief = professor;
        department.ChiefId = professor.Id;
        
        UpdateDepartment(department);
    }

    public void NotifyObservers()
    {
        DepartmentObservable.NotifyObservers();
    }
}