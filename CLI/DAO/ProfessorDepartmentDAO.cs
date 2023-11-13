using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class ProfessorDepartmentDAO
{
    private readonly List<ProfessorDepartment> _profDep;
    private readonly Storage<ProfessorDepartment> _profDepStorage;

    private ProfessorDAO _professorDao;
    private DepartmentDAO _departmentDao;

    public ProfessorDepartmentDAO(ProfessorDAO professorDao, DepartmentDAO departmentDao)
    {
        _profDepStorage = new Storage<ProfessorDepartment>("professorDepartment.txt");
        _profDep = _profDepStorage.Load();

        _professorDao = professorDao;
        _departmentDao = departmentDao;

        HashSet<Professor> profsForDep = new HashSet<Professor>();
        // List<Department> depsForProf = new List<Department>();

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

        // foreach (Professor professor in professorDao.GetAllProfessors())
        // {
        //     foreach (Department department in departmentDao.GetAllDepartments())
        //     {
        //         foreach (ProfessorDepartment pd in _profDep)
        //         {
        //             if (professor.Id == pd.ProfessorId && department.Id == pd.DepartmentId)
        //             {
        //                 depsForProf.Add(department);
        //                 break;
        //             }
        //         }
        //     }
        //
        //     if (depsForProf.Count > 0)
        //         professorDao.AddDepartmentsForProf();
        //     depsForProf.Clear();
        // }
    }
}