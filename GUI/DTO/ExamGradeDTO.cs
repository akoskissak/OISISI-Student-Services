using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class ExamGradeDTO : INotifyPropertyChanged
    {
        private int _id;
        private int _studentId;
        private int _subjectId;
        private int _grade;
        private DateOnly _date;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if(value != _id)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public int StudentId
        {
            get
            {
                return _studentId;
            }
            set
            {
                if(value != _studentId)
                {
                    _studentId = value;
                    OnPropertyChanged("StudentId");
                }
            }
        }

        public int SubjectId
        {
            get
            {
                return _subjectId;
            }
            set
            {
                if( value != _subjectId)
                {
                    _subjectId = value;
                    OnPropertyChanged("SubjectId");
                }
            }
        }
        public int Grade
        {
            get
            {
                return _grade;
            }
            set
            {
                if(value != _grade)
                {
                    _grade = value;
                    OnPropertyChanged("Grade");
                }
            }
        }

        public string Date
        {
            get
            {
                return _date.ToString();
            }
            set
            {
                if(value != _date.ToString())
                {
                    _date = DateOnly.Parse(value);
                    OnPropertyChanged("Date");
                }
            }
        }

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public ExamGrade ToExamGrade()
        {
            return new ExamGrade(_studentId, _subjectId, _grade, _date);
        }
        public ExamGradeDTO() {}

        public ExamGradeDTO(ExamGrade examgrade)
        {
            Id = examgrade.Id;
            StudentId = examgrade.StudentId;
            SubjectId = examgrade.SubjectId;
            Grade = examgrade.Grade;
            Date = examgrade.Date.ToString();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
