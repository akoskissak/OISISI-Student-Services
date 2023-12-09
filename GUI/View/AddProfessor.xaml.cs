using CLI.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddProfessor.xaml
    /// </summary>
    public partial class AddProfessor : Window, INotifyPropertyChanged
    {
        private readonly ProfessorDAO _professorDao;
        private string _lastname;
        private string _name;
        private DateOnly _date;
        private string _street;
        private int _number;
        private string _city;
        private string _country;
        private string _phoneNumber;
        private string _mail;
        private int _id;
        private string _title;
        private int _years;

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
        public DateOnly Date
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


        public int Number
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

        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                if (value != _mail)
                {
                    _mail = value;
                    OnPropertyChanged("Mail");
                }
            }
        }


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

        public int Years
        {
            get
            {
                return _years;
            }
            set
            {
                if (value != _years)
                {
                    _years = value;
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

        public AddProfessor()
        {
            InitializeComponent();
            _professorDao = new ProfessorDAO();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
