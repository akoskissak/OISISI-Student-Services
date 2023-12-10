using CLI.DAO;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public ProfessorDTO ProfessorDto { get; set; }

        private ProfessorDAO _professorDao;

        public event PropertyChangedEventHandler? PropertyChanged;
        public AddProfessor(ProfessorDAO professorDao)
        {
            InitializeComponent();
            DataContext = this;
            ProfessorDto = new ProfessorDTO();
            this._professorDao = professorDao;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            _professorDao.AddProfessor(ProfessorDto.ToProfessor());
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            ProfessorDto.Lastname = "";
            ProfessorDto.Name = "";
            ProfessorDto.DateOfBirth = "11/11/2021";
            ProfessorDto.Street = "";
            ProfessorDto.Number = "";
            ProfessorDto.City = "";
            ProfessorDto.PhoneNumber = "";
            ProfessorDto.Email = "";
            ProfessorDto.IdNumber = 0;
            ProfessorDto.Title = "";
            ProfessorDto.YearsOfService = 0;
        }
    }
}
