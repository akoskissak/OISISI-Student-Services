using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO;

public class ProfessorDepartmentDAO
{
    private readonly List<ProfessorDepartment> _profDep;
    private readonly Storage<ProfessorDepartment> _profDepStorage;

    private ProfessorDAO _professorDao;
    private DepartmentDAO _departmentDao;

    public Observable ProfessorDepartmentObservable;

    public ProfessorDepartmentDAO(ProfessorDAO professorDao, DepartmentDAO departmentDao)
    {
        _profDepStorage = new Storage<ProfessorDepartment>("professorDepartment.txt");
        _profDep = _profDepStorage.Load();

        _professorDao = professorDao;
        _departmentDao = departmentDao;

        ProfessorDepartmentObservable = new Observable();

        HashSet<Professor> profsForDep = new HashSet<Professor>();

        foreach (Department department in departmentDao.GetAllDepartments())
        {
            foreach (Professor professor in professorDao.GetAllProfessors())
            {
                foreach (ProfessorDepartment pd in _profDep)
                {
                    if (professor.Id == pd.ProfessorId && department.Id == pd.DepartmentId)
                    {
                        profsForDep.Add(professor);
                        if (professor.Id == department.ChiefId)
                        {
                            department.Chief = professor;
                            department.ChiefId = professor.Id;
                            professor.IdOfChiefDepartment = department.Id;
                        }
                        break;
                    }
                }
            }
            
            if (profsForDep.Count > 0)
                departmentDao.AddProfessorsForDep(profsForDep.ToList(), department.Id);
            profsForDep.Clear();
        }
    }
    public List<Professor> GetAllProfessorsByDepartmentId(int departmentId)
    {
        List<Professor> professorList = new List<Professor>();
        Department dep = _departmentDao.GetDepartmentById(departmentId);
        List<ProfessorDepartment> pd = _profDep.FindAll(p => p.DepartmentId == dep.Id);
        if(pd.Count > 0)
        {
            foreach (ProfessorDepartment pdepartment in pd)
            {
                Professor prof = _professorDao.GetProfessorById(pdepartment.ProfessorId);
                professorList.Add(prof);
            }
        }

        return professorList;
    }
    
    public void Save()
    {
        _profDepStorage.Save(_profDep);
        ProfessorDepartmentObservable.NotifyObservers();
    }

}