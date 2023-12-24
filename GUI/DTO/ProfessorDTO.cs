using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CLI.Model;
using CLI.DAO;
using System.Diagnostics.Metrics;
using System.IO;
using System.Xml.Linq;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace GUI.DTO
{
    public class ProfessorDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        private int _id;
        private string _lastname;
        private string _name;
        private DateOnly _dateOfBirth;
        private string _street;
        private string _number;
        private string _city;
        private string _country;
        private string _phoneNumber;
        private string _email;
        private int _idNumber;
        private string _title;
        private int _yearsOfService;

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
        public string Lastname
        {
            get
            {
                return _lastname;
            }
            set
            {
                if (value != _lastname)
                {
                    _lastname = value;
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
                if (_dateOfBirth == DateOnly.MinValue)
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

        public string IdNumber
        {
            get
            {
                return _idNumber.ToString();
            }
            set
            {
                if (value != _idNumber.ToString())
                {
                    try
                    {
                        _idNumber = int.Parse(value);
                    }
                    catch (Exception e)
                    {
                    }
                    OnPropertyChanged("IdNumber");
                }
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string YearsOfService
        {
            get
            {
                return _yearsOfService.ToString();
            }
            set
            {
                if (value != _yearsOfService.ToString())
                {
                    try
                    {
                        _yearsOfService = int.Parse(value);
                    } catch (Exception e)
                    {
                        _yearsOfService = 0;
                    }
                    OnPropertyChanged("Years");
                }
            }
        }

        public string Error => null;
        private Regex _lettersAndNumbersRegex = new Regex("^[a-zA-Z0-9]+([\\s][a-zA-Z0-9]+)*$");
            //"^[A-Za-z0-9]+$");
        private Regex _letters = new Regex("^[A-Za-z]+$");
        private Regex _cityCountryRegex = new Regex("^[a-zA-Z0-9]+([\\s][a-zA-Z0-9]+)*$");
        private Regex _phoneNumberRegex = new Regex("^\\+?(0-9)+$");
        private Regex _numbersRegex = new Regex("^[1-9]\\d*$");

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Lastname")
                {
                    if (string.IsNullOrEmpty(Lastname))
                        return "Lastname is required";

                    Match match = _lettersAndNumbersRegex.Match(Lastname);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Name is required";

                    Match match = _lettersAndNumbersRegex.Match(Name);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                //else if (columnName == "DateOfBirth")
                //{
                //    return "";
                //}
                else if (columnName == "Street")
                {
                    if (string.IsNullOrEmpty(Street))
                        return "Street is required";

                    Match match = _lettersAndNumbersRegex.Match(Street);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "Number")
                {
                    if (string.IsNullOrEmpty(Number))
                        return "Number is empty";

                    Match match = _lettersAndNumbersRegex.Match(Number);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "City is required";

                    Match match = _cityCountryRegex.Match(City);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Country is required";

                    Match match = _cityCountryRegex.Match(Country);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "PhoneNumber")
                {
                    if (string.IsNullOrEmpty(PhoneNumber))
                        return "PhoneNumber is required.";

                    Match match = _lettersAndNumbersRegex.Match(PhoneNumber);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                        return "Email is required.";
                }
                else if (columnName == "IdNumber")
                {
                    if (string.IsNullOrEmpty(IdNumber))
                        return "IdNumber is required.";

                    Match match = _numbersRegex.Match(IdNumber);
                    if (!match.Success)
                        return "Not a valid number. Try again.";
                }
                else if (columnName == "Title")
                {
                    if (string.IsNullOrEmpty(Title))
                        return "Title is required.";

                    Match match = _lettersAndNumbersRegex.Match(Title);
                    if (!match.Success)
                        return "Format not good. Try again.";
                }
                else if (columnName == "YearsOfService")
                {
                    if (string.IsNullOrEmpty(YearsOfService))
                        return "YearsOfService is required.";

                    Match match = _numbersRegex.Match(YearsOfService);
                    if (!match.Success)
                        return "YearsOfService must be a natural number. Try again.";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Lastname", "Name", "DateOfBirth", "Street",
            "Number", "City", "Country", "PhoneNumber", "Email", "IdNumber", "Title", "YearsOfService" };

        public bool isValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null) return false;
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
        public Professor ToProfessor()
        {
            return new Professor(_lastname, _name, _dateOfBirth, _street, _number, _city, _country, _phoneNumber,
            _email, _idNumber, _title, _yearsOfService);
        }
        public ProfessorDTO() {}
        public ProfessorDTO(Professor professor)
        {
            Lastname = professor.Lastname;
            Name = professor.Name;
            DateOfBirth = professor.DateOfBirth.ToDateTime(TimeOnly.MinValue);
            Street = professor.Address.Street;
            Number = professor.Address.Number;
            City = professor.Address.City;
            Country = professor.Address.Country;
            PhoneNumber = professor.PhoneNumber;
            Email = professor.Email;
            IdNumber = professor.Idnumber.ToString();
            Title = professor.Title;
            YearsOfService = professor.YearsOfService.ToString();
            Id = professor.Id;
        }

        public event PropertyChangedEventHandler? PropertyChanged;



    }
}
