﻿using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class ProfessorSubjectDAO
{
    private readonly List<ProfessorSubject> _professorSubject;
    private readonly Storage<ProfessorSubject> _professorSubjectStorage;

    private ProfessorDAO _professorDao;
    private SubjectDAO _subjectDao;

    public ProfessorSubjectDAO(ProfessorDAO professorDao, SubjectDAO subjectDao)
    {
        _professorSubjectStorage = new Storage<ProfessorSubject>("professorSubject.txt");
        _professorSubject = _professorSubjectStorage.Load();

        _professorDao = professorDao;
        _subjectDao = subjectDao;

        HashSet<Subject> subjectsForProfessor = new HashSet<Subject>();

        foreach (Professor professor in professorDao.GetAllProfessors())
        {
            foreach (Subject subject in subjectDao.GetAllSubjects())
            {
                foreach (ProfessorSubject ps in _professorSubject)
                {
                    if (subject.Id == ps.SubjectId && professor.Id == ps.ProfessorId)
                    {
                        subjectsForProfessor.Add(subject);
                        subject.ProfessorId = professor.Id;
                        subject.Professor = professor;
                        break;
                    }
                }
            }

            if (subjectsForProfessor.Count > 0)
                professorDao.AddSubjectsForProfessor(subjectsForProfessor.ToList(), professor.Id);
            subjectsForProfessor.Clear();
        }
        
    }

    public List<Professor>? FindAllProfessorsForSubjects(List<Subject> unsubmittedSubjects)
    {
        List<Professor> professors = new List<Professor>();
        foreach (Subject subject in unsubmittedSubjects)
        {
            Professor p = GetProfessorBySubjectId(subject.Id);
            if (p != null)
                professors.Add(p);
        }

        if (professors.Count > 0)
            return professors.Distinct().ToList();
        return null;
    }

    public Professor? GetProfessorBySubjectId(int subjectId)
    {
        ProfessorSubject? ps = _professorSubject.Find(ps => ps.SubjectId == subjectId);
        if (ps != null)
            return _professorDao.FindProfessorById(ps.ProfessorId);
        return null;
    }
}