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
        public static readonly ProfessorDAO _professorDao = new ProfessorDAO();

        public ProfessorController()
        {
            //_professorDao = new ProfessorDAO();
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

        public bool RemoveProfessor(int professorId)
        {
            return _professorDao.RemoveProfessor(professorId).Id != -1;
        }

        public void Subscribe(IObserver observer)
        {
            _professorDao.ProfessorObservable.Subscribe(observer);
        }
        public void Save()
        {
            _professorDao.SaveProfessors();
        }
    }
}
