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
    public class ProfessorSubjectController
    {
        private readonly ProfessorSubjectDAO _professorSubjectDao;
        private ProfessorDAO _professorDao;
        private SubjectDAO _subjectDao;

        public ProfessorSubjectController()
        {
            _professorDao = ProfessorController._professorDao;
            _subjectDao = SubjectController._subjectDao;
            _professorSubjectDao = new ProfessorSubjectDAO(_professorDao, _subjectDao);
        }

        public List<Professor>? GetAllProfessorsOnSubjects(List<Subject> unsubmittedSubjects)
        {
            return _professorSubjectDao.FindAllProfessorsForSubjects(unsubmittedSubjects);
        }
    }
}
