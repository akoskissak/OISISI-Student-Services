﻿using CLI.Storage.Serialization;

namespace CLI.Model;

public class Index : ISerializable
{
    public string StudyProgrammeMark { get; set; }
    public int EnrollmentNumber { get; set; }
    public int EnrollmentYear { get; set; }
    public int Id { get; set; }

    public Index()
    {
    }

    public Index(string studyProgMark, int enrollNum, int enrollYear)
    {
        StudyProgrammeMark = studyProgMark;
        EnrollmentNumber = enrollNum;
        EnrollmentYear = enrollYear;
    }

    public override string ToString()
    {
        return $"StudyProgrammeMark: {StudyProgrammeMark, 3} | EnrollmentNumber: {EnrollmentNumber, 2} | EnrollmentYear: {EnrollmentYear, 4}";
    }

    public string GUIToString()
    {
        return $"{StudyProgrammeMark}-{EnrollmentNumber}-{EnrollmentYear}";
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            StudyProgrammeMark,
            EnrollmentNumber.ToString(),
            EnrollmentYear.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        StudyProgrammeMark = values[0];
        EnrollmentNumber = int.Parse(values[1]);
        EnrollmentYear = int.Parse(values[2]);
    }
}