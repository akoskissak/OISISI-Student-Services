using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace GUI.DTO
{
    public class StudentDTO : INotifyPropertyChanged, IDataErrorInfo
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
        public List<ExamGrade> Grades { get; set; }
        public List<Subject> UnsubmittedSubjects { get; set; }

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
        public string Lastname
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
                    OnPropertyChanged("Lastname");
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

        public DateTime DateOfBirth
        {
            get
            {
                if(_dateOfBirth == DateOnly.MinValue)
                {
                    return DateTime.Now;
                }
                return _dateOfBirth.ToDateTime(TimeOnly.MinValue);
            }
            set
            {
                if (value != _dateOfBirth.ToDateTime(TimeOnly.MinValue))
                {
                    _dateOfBirth = DateOnly.FromDateTime(value);
                    OnPropertyChanged("DateOfBirth");
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
                    if (value == "")
                    {
                        _enrollmentNumber = 0;
                    }
                    else
                    {
                        try
                        {
                            _enrollmentNumber = int.Parse(value);
                        }catch
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
                if(value != _enrollmentYear.ToString())
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
                        }catch
                        {
                            _enrollmentYear = 0;
                        }
                    }
                    OnPropertyChanged("EnrollmentYear");
                }
            }
        }

        public string CurrentYearOfStudy
        {
            get
            {
                return _currentYearOfStudy.ToString();
            }
            set
            {
                if(value != _currentYearOfStudy.ToString())
                {
                    if(value == "")
                    {
                        _currentYearOfStudy = 0;
                    }
                    else
                    {
                        try
                        {
                            _currentYearOfStudy = int.Parse(value);
                        }
                        catch
                        {
                            _currentYearOfStudy = 0;
                        }

                    }
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

        public double AverageGrade
        {
            get
            {
                return _averageGrade;
            }
            set
            {
                if(value != _averageGrade)
                {
                        try
                        {
                            _averageGrade = value;
                        }catch
                        {
                            _averageGrade = 0;
                        }
                    OnPropertyChanged("AverageGrade");
                }
            }
        }
        public string Error => null;

        private Regex _NameRegex = new Regex("^[A-Za-z0-9- ČčĆćĐđŠšŽž]+$");
        private Regex _LastNameRegex = new Regex("^[A-Za-z0-9- ČčĆćĐđŠšŽž]+$");
        private Regex _PhoneNumberRegex = new Regex("^\\+?[0-9]+[0-9- ]*$");
        private Regex _EmailRegex = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
        private Regex _StudyProgrammeMarkRegex = new Regex("^[A-Za-z0-9]+$");
        private Regex _EnrollmentNumberkRegex = new Regex("^[0-9]+$");
        private Regex _EnrollmentYearRegex = new Regex("^[12]{1}[0-9]{3,3}$");
        private Regex _CurrentYearOfStudyRegex = new Regex("^[1-9][0-9]?$");
        private Regex _StreetRegex = new Regex("^[A-Za-z0-9- ČčĆćĐđŠšŽž.]+$");
        private Regex _NumberRegex = new Regex("^[a-zA-Z0-9/ ]+$");
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Lastname")
                {
                    if (string.IsNullOrEmpty(Lastname))
                        return "LastName is required";

                    Match match = _LastNameRegex.Match(Lastname);
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
                else if (columnName == "Street")
                {
                    if (string.IsNullOrEmpty(Street))
                        return "Street is required";

                    Match match = _StreetRegex.Match(Street);
                    if (!match.Success)
                        return "Format not good. Try again.";

                }
                else if (columnName == "Number")
                {
                    if (string.IsNullOrEmpty(Number))
                        return "Number is required";

                    Match match = _NumberRegex.Match(Number);
                    if (!match.Success)
                        return "Format not good. Try again.";

                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "City is required";

                    Match match = _StreetRegex.Match(City);
                    if (!match.Success)
                        return "Format not good. Try again.";

                }
                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Country is required";

                    Match match = _StreetRegex.Match(Country);
                    if (!match.Success)
                        return "Format not good. Try again.";

                }
                else if (columnName == "PhoneNumber")
                {
                    if (string.IsNullOrEmpty(PhoneNumber))
                        return "PhoneNumber is required";

                    Match match = _PhoneNumberRegex.Match(PhoneNumber);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                        return "Email is required";

                    Match match = _EmailRegex.Match(Email);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "StudyProgrammeMark")
                {
                    if (string.IsNullOrEmpty(StudyProgrammeMark))
                        return "StudyProgrammeMark is required";

                    Match match = _StudyProgrammeMarkRegex.Match(StudyProgrammeMark);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "EnrollmentNumber")
                {
                    if (string.IsNullOrEmpty(EnrollmentNumber))
                        return "EnrollmentNumber is required";

                    Match match = _EnrollmentNumberkRegex.Match(EnrollmentNumber);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "EnrollmentYear")
                {
                    if (string.IsNullOrEmpty(EnrollmentYear))
                        return "EnrollmentYear is required";

                    Match match = _EnrollmentYearRegex.Match(EnrollmentYear);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "CurrentYearOfStudy")
                {
                    if (string.IsNullOrEmpty(CurrentYearOfStudy))
                        return "CurrentYearOfStudy is required";

                    Match match = _CurrentYearOfStudyRegex.Match(CurrentYearOfStudy);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "LastName", "Name", "Street", "Number", "City", "Country", "PhoneNumber", "Email", "StudyProgrammeMark",
            "EnrollmentNumber", "EnrollmentYear", "CurrentYearOfStudy"};
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
            Lastname = student.Lastname;
            Name = student.Name;
            DateOfBirth = student.DateOfBirth.ToDateTime(TimeOnly.MinValue);
            Street = student.Address.Street;
            Number = student.Address.Number;
            City = student.Address.City;
            Country = student.Address.Country;
            PhoneNumber = student.PhoneNumber;
            Email = student.Email;
            Status = student.Status;
            AverageGrade = student.AverageGrade;
            CurrentYearOfStudy = student.CurrentYearOfStudy.ToString();
            EnrollmentNumber = student.Index.EnrollmentNumber.ToString();
            EnrollmentYear = student.Index.EnrollmentYear.ToString();
            StudyProgrammeMark = student.Index.StudyProgrammeMark;
            Index = student.Index.StudyProgrammeMark + "-" + student.Index.EnrollmentNumber + "-" + student.Index.EnrollmentYear;
            _index = new CLI.Model.Index(_studyProgrammeMark, _enrollmentNumber, _enrollmentYear);
            Grades = student.Grades;
            UnsubmittedSubjects = student.UnsubmittedSubjects;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
