using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class StudentController
    {
        private readonly StudentDAO _studentDao;

        public StudentController()
        {
            _studentDao = new StudentDAO();
        }

        public void Add(Student student)
        {
            _studentDao.AddStudent(student);
        }

        public void Update(Student student)
        {
            _studentDao.UpdateStudent(student);
        }

        public List<Student> GetAllStudents()
        {
            return _studentDao.GetAllStudents();
        }
        public void RemoveStudent(int studentId)
        {
            _studentDao.RemoveStudent(studentId);
        }
        public void Subscribe(IObserver observer)
        {
            _studentDao.StudentObservable.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            _studentDao.NotifyObservers();
        }
        public void Save()
        {
            _studentDao.SaveStudents();
        }
    }
}
