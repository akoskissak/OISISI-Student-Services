using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class StudentSubjectController
    {
        private readonly StudentSubjectDAO _studentSubjectDao;
        private StudentDAO _studentDao;
        private SubjectDAO _subjectDao;

        public StudentSubjectController()
        {
            _studentDao = StudentController._studentDao;
            _subjectDao = SubjectController._subjectDao;
            _studentSubjectDao = new StudentSubjectDAO(_studentDao, _subjectDao);
        }

        public bool RemoveSubjectForStudent(int studentId, int subjectId)
        {
            return _studentDao.RemoveSubjectForStudent(studentId, subjectId, _studentSubjectDao).Id != -1;
        }

        public List<Student> GetAllStudentsOnSubjects(List<Subject> subjects)
        {
            return _studentSubjectDao.FindAllStudentsForSubjects(subjects);
        }
        public List<Subject> GetAllSubjectsNotByStudent(int sutdentId)
        {
            return _studentSubjectDao.GetAllSubjectsNotByStudentId(sutdentId);
        }

        public void AddStudentForSubject(Student student, int subjectId)
        {
            _subjectDao.AddStudentForSubject(student, subjectId);
        }

        public void AddSubjectForStudent(int subjectId, int studentId)
        {
            _studentDao.AddSubjectForStudent(subjectId, studentId);
        }

        public void AddStudentSubject(StudentSubject studentSubject)
        {
            _studentSubjectDao.AddStudentSubject(studentSubject);
        }
    }
}
