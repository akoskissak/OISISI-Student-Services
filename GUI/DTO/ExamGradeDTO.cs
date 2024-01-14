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
        private int _subjectCode;
        private string _subjectName;
        private int _espb;
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
        
        public string SubjectName
        {
            get { return _subjectName; }
            set
            {
                if( value != _subjectName)
                {
                    _subjectName = value;
                    OnPropertyChanged("SubjectName");
                }
            }
        }

        public string SubjectCode
        {
            get
            {
                return _subjectCode.ToString();
            }
            set
            {
                if (value != _subjectCode.ToString())
                {
                    try
                    {
                        _subjectCode = int.Parse(value);
                    }
                    catch (Exception e)
                    {
                        _subjectCode = 0;
                    }
                    OnPropertyChanged("SubjectCode");

                }
            }
        }
        public int Espb
        {
            get { return _espb; }
            set
            {
                if(value != _espb)
                {
                    _espb = value;
                    OnPropertyChanged("Espb");
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

        public DateOnly DateProbni
        {
            get
            {
                return _date;
            }
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged("DateProbni");
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
        public ExamGradeDTO()
        {
            //Grade = -1;
        }

        public ExamGradeDTO(ExamGrade examgrade, Subject subject)
        {
            Id = examgrade.Id;
            StudentId = examgrade.StudentId;
            SubjectId = examgrade.SubjectId;
            Grade = examgrade.Grade;
            Date = examgrade.Date.ToString();
            SubjectCode = subject.SubjectCode.ToString();
            SubjectName = subject.Name;
            Espb = subject.Espb;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
