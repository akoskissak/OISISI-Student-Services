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

        public Professor? GetProfessorForSubject(int subjectId)
        {
            return _professorSubjectDao.GetProfessorBySubjectId(subjectId);
        }

        public void RemoveProfessorFromSubject(int subjectId)
        {
            _professorSubjectDao.RemoveProfessorFromSubject(subjectId);
        }

        public Professor? GetProfessorBySubject(int subjectId)
        {
            return _professorSubjectDao.GetProfessorBySubjectId(subjectId);
        }

        public List<Subject> GetAllSubjectsByProfessorId(int professorId)
        {
            return _professorSubjectDao.GetAllSubjectsByProfessorId(professorId);
        }
        public List<Subject> GetAllSubjectsNotByProfessorId(int professorId)
        {
            return _professorSubjectDao.GetAllSubjectsNotByProfessorId(professorId);
        }
        
        public void SetProfessorForSubject(int subjectId, Professor professor)
        {
            _professorSubjectDao.SetProfessorForSubject(subjectId, professor);
        }
        public void NotifyObservers()
        {
            _professorSubjectDao.NotifyObservers();
        }
        public void SetSubjectProfessor(int subjectId, Professor professor)
        {
            _subjectDao.SetSubjectProfessor(subjectId, professor);
        }

        public bool RemoveSubjectForProfessor(int subjectId)
        {
            return _professorSubjectDao.RemoveProfessorFromSubject(subjectId);
        }

        public Professor? GetProfessorById(int professorId)
        {
            return _professorDao.GetProfessorById(professorId);
        }

        public void Save()
        {
            _professorSubjectDao.Save();
        }
    }
}
