using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class StudentDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _name;
        private string _lastName;
        private DateOnly _dateOfBirth;
        private string _street;
        private string _number;
        private string _city;
        private string _country;
        private string _phoneNumber;
        private string _email;
        private string _studyProgrammeMark;
        private int _enrollmentNumber;
        private int _enrollmentYear;
        private int _currentYearOfStudy;
        private Status _status;
        private double _averageGrade;
        private CLI.Model.Index _index;

        public string Index
        {
            get
            {
                return _index.GUIToString();
            }
            set
            {
                if(_enrollmentNumber != 0 && _enrollmentYear != 0 && _studyProgrammeMark != null)
                {
                    _index = new CLI.Model.Index(_studyProgrammeMark, _enrollmentNumber, _enrollmentYear);
                    OnPropertyChanged("Index");
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

        public string DateOfBirth
        {
            get
            {
                return _dateOfBirth.ToString();
            }
            set
            {
                if (value != _dateOfBirth.ToString())
                {
                    _dateOfBirth = DateOnly.Parse(value);
                    OnPropertyChanged("Date");
                }
            }
        }

        public string Street
        {
            get
            {
                return _street;
            }
            set
            {
                if (value != _street)
                {
                    _street = value;
                    OnPropertyChanged("Street");
                }
            }
        }

        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                if (value != _number)
                {
                    _number = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                if (value != _phoneNumber)
                {
                    _phoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
                }
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    OnPropertyChanged("Email");
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
                if(value != _enrollmentNumber.ToString())
                {
                    _enrollmentNumber = int.Parse(value);
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
                if(value != _enrollmentYear.ToString())
                {
                    _enrollmentYear = int.Parse(value);
                    OnPropertyChanged("EnrollmentYear");
                }
            }
        }

        public int CurrentYearOfStudy
        {
            get
            {
                return _currentYearOfStudy;
            }
            set
            {
                if(value != _currentYearOfStudy)
                {
                    _currentYearOfStudy = value;
                    OnPropertyChanged("CurrentYearOfStudy");
                }
            }
        }

        public Status Status
        {
            get
            {
                return _status;
            }
            set
            {
                if(value != _status)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public string AverageGrade
        {
            get
            {
                return _averageGrade.ToString();
            }
            set
            {
                if(value != _averageGrade.ToString())
                {
                    _averageGrade = double.Parse(value);
                    OnPropertyChanged("AverageGrade");
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

        public Student ToStudent()
        {
            return new Student(_lastName, _name, _dateOfBirth, _street, _number, _city, _country, _phoneNumber,
                _email, _studyProgrammeMark, _enrollmentNumber, _enrollmentYear, _currentYearOfStudy, _status);
        }
        public StudentDTO() {}
        public StudentDTO(Student student)
        {
            Id = student.Id;
            LastName = student.Lastname;
            Name = student.Name;
            DateOfBirth = student.DateOfBirth.ToString();
            Street = student.Address.Street;
            Number = student.Address.Number;
            City = student.Address.City;
            Country = student.Address.Country;
            PhoneNumber = student.PhoneNumber;
            Email = student.Email;
            Status = student.Status;
            AverageGrade = student.AverageGrade.ToString();
            CurrentYearOfStudy = student.CurrentYearOfStudy;
            EnrollmentNumber = student.Index.EnrollmentNumber.ToString();
            EnrollmentYear = student.Index.EnrollmentYear.ToString();
            StudyProgrammeMark = student.Index.StudyProgrammeMark;
            Index = student.Index.StudyProgrammeMark + "-" + student.Index.EnrollmentNumber + "-" + student.Index.EnrollmentYear;
            _index = new CLI.Model.Index(_studyProgrammeMark, _enrollmentNumber, _enrollmentYear);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
