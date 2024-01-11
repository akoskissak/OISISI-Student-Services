using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class StudentsForProfessorDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _name;
        private string _lastName;
        private string _studyProgrammeMark;
        private int _enrollmentNumber;
        private int _enrollmentYear;
        private CLI.Model.Index _index;

        public string Index
        {
            get
            {
                return _index.GUIToString();
            }
            set
            {
                if (_enrollmentNumber != 0 && _enrollmentYear != 0 && _studyProgrammeMark != null)
                {
                    _index = new CLI.Model.Index(_studyProgrammeMark, _enrollmentNumber, _enrollmentYear);
                    OnPropertyChanged("Index");
                }
            }
        }

        public string StudyProgrammeMark
        {
            get
            {
                return _studyProgrammeMark;
            }
            set
            {
                if (value != _studyProgrammeMark)
                {
                    _studyProgrammeMark = value;
                    OnPropertyChanged("StudyProgrammeMark");
                }
            }
        }

        public string EnrollmentNumber
        {
            get
            {
                return _enrollmentNumber.ToString();
            }
            set
            {
                if (value != _enrollmentNumber.ToString())
                {
                    if (value == "")
                    {
                        _enrollmentNumber = 0;
                    }
                    else
                    {
                        try
                        {
                            _enrollmentNumber = int.Parse(value);
                        }
                        catch (Exception e)
                        {
                            _enrollmentNumber = 0;
                        }

                    }
                    OnPropertyChanged("EnrollmentNumber");
                }
            }
        }

        public string EnrollmentYear
        {
            get
            {
                return _enrollmentYear.ToString();
            }
            set
            {
                if (value != _enrollmentYear.ToString())
                {
                    if (value == "")
                    {
                        _enrollmentYear = 0;
                    }
                    else
                    {
                        try
                        {
                            _enrollmentYear = int.Parse(value);
                        }
                        catch (Exception e)
                        {
                            _enrollmentYear = 0;
                        }
                    }
                    OnPropertyChanged("EnrollmentYear");
                }
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        protected virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public StudentsForProfessorDTO() {}

        public StudentsForProfessorDTO(Student student)
        {
            Id = student.Id;
            LastName = student.Lastname;
            Name = student.Name;
            EnrollmentNumber = student.Index.EnrollmentNumber.ToString();
            EnrollmentYear = student.Index.EnrollmentYear.ToString();
            StudyProgrammeMark = student.Index.StudyProgrammeMark;
            Index = student.Index.StudyProgrammeMark + "-" + student.Index.EnrollmentNumber + "-" + student.Index.EnrollmentYear;
            _index = new CLI.Model.Index(_studyProgrammeMark, _enrollmentNumber, _enrollmentYear);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
