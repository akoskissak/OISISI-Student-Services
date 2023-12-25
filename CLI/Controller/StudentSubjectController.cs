using CLI.DAO;
using System;
using System.Collections.Generic;
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
    }
}
