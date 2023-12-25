using CLI.DAO;
using CLI.Model;
using CLI.Observer;

namespace CLI.Controller
{
    public class DepartmentController
    {
        private readonly DepartmentDAO _departmentDao;

        public DepartmentController()
        {
            _departmentDao = new DepartmentDAO();
        }
        public List<Department> getAllDepartments()
        {
            return _departmentDao.GetAllDepartments();
        }
        public void Subscribe(IObserver observer)
        {
            _departmentDao.DepartmentObservable.Subscribe(observer);
        }
    }
}
