using CLI.DAO;
using CLI.Model;
using CLI.Observer;

namespace CLI.Controller
{
    public class DepartmentController
    {
        private readonly DepartmentDAO _departmentDao;
        private readonly ProfessorDepartmentDAO _professorDepartmentDao;
        private readonly ProfessorDAO _professorDAO;

        public DepartmentController()
        {
            _departmentDao = new DepartmentDAO();
            _professorDAO = new ProfessorDAO();
            _professorDepartmentDao = new ProfessorDepartmentDAO(_professorDAO, _departmentDao);
        }
        public List<Department> getAllDepartments()
        {
            return _departmentDao.GetAllDepartments();
        }
        public void Subscribe(IObserver observer)
        {
            _departmentDao.DepartmentObservable.Subscribe(observer);
        }

        public List<Professor> GetAllProfessorsByDepartmentId(int departmentId)
        {
            return _professorDepartmentDao.GetAllProfessorsByDepartmentId(departmentId);
        }

        public bool HasDepartmentChief(int departmentId)
        {
            return _departmentDao.HasDepartmentChief(departmentId);
        }
        public void SetProfessorAsAChief(int professorId, int departmentId)
        {
            _departmentDao.SetProfessorAsChief(professorId, departmentId);
        }
        public void NotifyObservers()
        {
            _departmentDao.NotifyObservers();
        }
    }
}
