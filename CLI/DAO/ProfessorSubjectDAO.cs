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

    public void SetProfessorForSubject(int subjectId, Professor professor)
    {
        Subject? subject = _subjectDao.GetSubjectById(subjectId);
        subject.Professor = professor;
        subject.ProfessorId = professor.Id;

        ProfessorSubject? ps = _professorSubject.Find(ps => ps.SubjectId == subjectId);
        if (ps != null)
            ps.ProfessorId = subject.ProfessorId;
        else
            _professorSubject.Add(new ProfessorSubject(subject.ProfessorId, subjectId));

        _professorSubjectStorage.Save(_professorSubject);
    }

    public void RemoveProfessorFromSubject(int subjectId)
    {
        Subject? subject = _subjectDao.GetSubjectById(subjectId);
        Professor? professor = _professorDao.GetProfessorById(subject.ProfessorId);
        if (professor != null && subject != null)
        {
            ProfessorSubject? ps = _professorSubject.Find(ps => ps.SubjectId == subjectId && ps.ProfessorId == subject.ProfessorId);

            subject.Professor = null;
            subject.ProfessorId = -1;

            professor.Subjects.Remove(subject);

            _professorSubject.Remove(ps);

            _professorSubjectStorage.Save(_professorSubject);
        }

    }

    public List<Subject> GetAllSubjectsByProfessorId(int professorId)
    {
        List<Subject> subjectsForProfessor = new List<Subject>();

        List<ProfessorSubject> allps = _professorSubject.FindAll(ps => ps.ProfessorId == professorId);
        if (allps.Count > 0)
        {
            foreach (ProfessorSubject ps in allps)
            {
                Subject? s = _subjectDao.GetSubjectById(ps.SubjectId);
                subjectsForProfessor.Add(s);
            }
        }

        return subjectsForProfessor;
    }

}