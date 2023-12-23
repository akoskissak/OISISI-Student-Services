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
    public class ProfessorController
    {
        private readonly ProfessorDAO _professorDao;

        public ProfessorController()
        {
            _professorDao = new ProfessorDAO();
        }

        public void AddProfessor(Professor professor)
        {
            _professorDao.AddProfessor(professor);
        }

        public void UpdateProfessor(Professor professor)
        {
            _professorDao.UpdateProfessor(professor);
        }

        public List<Professor> GetAllProfessors()
        {
            return _professorDao.GetAllProfessors();
        }

        public void RemoveProfessor(int professorId)
        {
            _professorDao.RemoveProfessor(professorId);
        }

        public void Subscribe(IObserver observer)
        {
            _professorDao.ProfessorObservable.Subscribe(observer);
        }
    }
}
