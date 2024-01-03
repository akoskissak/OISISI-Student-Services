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
        public static readonly StudentDAO _studentDao = new StudentDAO();

        public StudentController()
        {
            //_studentDao = new StudentDAO();
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
        public bool RemoveStudent(int studentId)
        {
            return _studentDao.RemoveStudent(studentId).Id != -1;
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

        public Student? GetStudentById(int studentId)
        {
            return _studentDao.GetStudentById(studentId);
        }

        public List<Student>? GetStudentByText(string text)
        {
            return _studentDao.FindStudentByText(text);
        }
    }
}
