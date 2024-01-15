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
    public class ExamGradeController
    {
        private readonly ExamGradeDAO _examGradeDao;
        private StudentDAO _studentDao;
        private SubjectDAO _subjectDao;

        public ExamGradeController()
        {
            _studentDao = StudentController._studentDao;
            _subjectDao = SubjectController._subjectDao;
            _examGradeDao = new ExamGradeDAO(_studentDao, _subjectDao);
        }

        public bool RemoveGradeForStudent(int studentId, int subjectId)
        {
            if(_examGradeDao.RemoveExamGrade(studentId, subjectId) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void SetStudentGrade(ExamGrade examGrade, int studentId)
        {
            _examGradeDao.AddExamGrade(examGrade);
        }

        public void Subscribe(IObserver observer)
        {
            _examGradeDao.ExamGradeObservable.Subscribe(observer);
        }

        public void NotifyObservers()
        {
            _examGradeDao.NotifyObservers();
        }

        public void Save()
        {
            _examGradeDao.Save();

        public ExamGrade? GetExamByIds(int studentId, int subjectId)
        {
            return _examGradeDao.GetExamByIds(studentId, subjectId);
        }
    }
}
