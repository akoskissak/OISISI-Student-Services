using CLI.DAO;
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
    }
}
