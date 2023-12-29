using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GUI.DTO
{
    public class SubjectDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        private int _id;
        private int _subjectCode;
        private string _name;
        private SemesterType _semester;
        private int _yearOfStudy;
        private int _professorId;
        private int _espb;
        //public Professor Professor { get; set; }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged("Id");
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
                    }catch(Exception e)
                    {
                        _subjectCode = 0;
                    }
                    OnPropertyChanged("SubjectCode");

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

        public SemesterType Semester
        {
            get
            {
                return _semester;
            }
            set
            {
                if (value != _semester)
                {
                    _semester = value;
                    OnPropertyChanged("Semester");
                }
            }
        }
        
        public int YearOfStudy
        {
            get
            {
                return _yearOfStudy;
            }
            set
            {
                if (value != _yearOfStudy)
                {
                    _yearOfStudy = value;
                    OnPropertyChanged("YearOfStudy");
                }
            }
        }
        
        public int ProfessorId
        {
            get
            {
                return _professorId;
            }
            set
            {
                if (value != _professorId)
                {
                    _professorId = value;
                    OnPropertyChanged("ProfessorId");
                }
            }
        }
        
        public int Espb
        {
            get
            {
                return _espb;
            }
            set
            {
                if (value != _espb)
                {
                    _espb = value;
                    OnPropertyChanged("Espb");
                }
            }
        }

        public string Error => null;

        private Regex _SubjectCodeRegex = new Regex("^[0-9]+$");
        private Regex _ProfessorRegex = new Regex("^[0-9]+$");
        private Regex _NameRegex = new Regex("^[a-zA-Z0-9/]+$");

        public string this[string columnName]
        {
            get
            {
                if (columnName == "SubjectCode")
                {
                    if (string.IsNullOrEmpty(SubjectCode))
                        return "SubjectCode is required";

                    Match match = _SubjectCodeRegex.Match(SubjectCode);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Name is required";

                    Match match = _NameRegex.Match(Name);
                    if (!match.Success)
                        return "Format not good. Try again.";

                }
                else if (columnName == "YearOfStudy")
                {
                    if (YearOfStudy <= 0)
                        return "YearOfStudy is required";
                }
                else if (columnName == "ProfessorId")
                {
                    if (string.IsNullOrEmpty(ProfessorId.ToString()))
                        return "ProfessorId is required";

                    Match match = _ProfessorRegex.Match(ProfessorId.ToString());
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "Espb")
                {
                    if (Espb <= 0)
                        return "Espb is required";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "SubjectCode", "Name", "YearOfStudy", "ProfessorId", "Espb" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public Subject ToSubject()
        {
            return new Subject(_subjectCode, _name, _semester, _yearOfStudy, _espb, _professorId);
        }

        public SubjectDTO() { }
        public SubjectDTO(Subject subject)
        {
            SubjectCode = subject.SubjectCode.ToString();
            Name = subject.Name;
            Semester = subject.Semester;
            YearOfStudy = subject.YearOfStudy;
            ProfessorId = subject.ProfessorId;
            Espb = subject.Espb;
            Id = subject.Id;
            //Professor = subject.Professor;
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
