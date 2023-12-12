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

namespace GUI.DTO
{
    public class ProfessorDTO : INotifyPropertyChanged
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
                    _idNumber = int.Parse(value);
                    OnPropertyChanged("Id");
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
                    _yearsOfService = int.Parse(value);
                    OnPropertyChanged("Years");
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
            DateOfBirth = professor.DateOfBirth.ToString();
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
