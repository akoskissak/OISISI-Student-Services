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

    public class SubjectController
    {
        public static readonly SubjectDAO _subjectDao = new SubjectDAO();
        
        public SubjectController()
        {
           //_subjectDao = new SubjectDAO();
        }

        public void AddSubject(Subject subject)
        {
            _subjectDao.AddSubject(subject);
        }

        public void UpdateSubject(Subject subject)
        {
            _subjectDao.UpdateSubject(subject);
        }

        public List<Subject> GetAllSubjects()
        {
            return _subjectDao.GetAllSubjects();
        }

        public bool RemoveSubject(int subjectId)
        {
            return _subjectDao.RemoveSubject(subjectId).Id != -1;
        }

        public void Subscribe(IObserver observer)
        {
            _subjectDao.SubjectObservable.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            _subjectDao.NotifyObservers();
        }
        public void Save()
        {
            _subjectDao.SaveSubjects();
        }

        public List<Subject>? GetSubjectsByText(string text)
        {
            return _subjectDao.FindSubjectsByText(text);
        }
        public Subject? GetSubjectById(int subjectId)
        {
            return _subjectDao.GetSubjectById(subjectId);
        }
    }
}
