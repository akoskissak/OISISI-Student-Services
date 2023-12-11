using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GUI.DTO
{
    public class SubjectDTO : INotifyPropertyChanged
    {
        private int _id;
        private int _subjectCode;
        private string _name;
        private SemesterType _semester;
        private int _yearOfStudy;
        private Professor _professor;
        private int _professorId;
        private int _espb;

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
                    _subjectCode = int.Parse(value);
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

        public string Semester
        {
            get
            {
                return _semester.ToString();
            }
            set
            {
                if (value != _semester.ToString())
                {
                    _semester = (SemesterType)Enum.Parse(typeof(SemesterType), value);
                    //_semester = Enum.Parse<SemesterType>(value);
                    OnPropertyChanged("Semester");
                }
            }
        }
        
        public string YearOfStudy
        {
            get
            {
                return _yearOfStudy.ToString();
            }
            set
            {
                if (value != _yearOfStudy.ToString())
                {
                    _yearOfStudy = int.Parse(value);
                    OnPropertyChanged("YearOfStudy");
                }
            }
        }
        
        public string ProfessorId
        {
            get
            {
                return _professorId.ToString();
            }
            set
            {
                if (value != _professorId.ToString())
                {
                    _professorId = int.Parse(value);
                    OnPropertyChanged("ProfessorId");
                }
            }
        }
        
        public string Espb
        {
            get
            {
                return _espb.ToString();
            }
            set
            {
                if (value != _espb.ToString())
                {
                    _espb = int.Parse(value);
                    OnPropertyChanged("Espb");
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

        public Subject ToSubject()
        {
            return new Subject(_subjectCode, _name, _semester, _yearOfStudy, _espb, _professorId);
        }

        public SubjectDTO() { }
        public SubjectDTO(Subject subject)
        {
            SubjectCode = subject.SubjectCode.ToString();
            Name = subject.Name;
            Semester = subject.Semester.ToString();
            YearOfStudy = subject.YearOfStudy.ToString();
            ProfessorId = subject.ProfessorId.ToString();
            Espb = subject.Espb.ToString();
            Id = subject.Id;
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
