using CLI.Controller;
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

        private ProfessorController _professorController;

        public event PropertyChangedEventHandler? PropertyChanged;
        public AddProfessor(ProfessorController professorController, double left, double top, double width, double height)
        {
            InitializeComponent();
            DataContext = this;
            ProfessorDto = new ProfessorDTO();
            this._professorController = professorController;

            SetInitialWindowSize(left, top, width, height);

            textBoxLastname.TextChanged += TextBox_TextChanged;
            textBoxName.TextChanged += TextBox_TextChanged;
            textBoxStreet.TextChanged += TextBox_TextChanged;
            textBoxNumber.TextChanged += TextBox_TextChanged;
            textBoxCity.TextChanged += TextBox_TextChanged;
            textBoxCountry.TextChanged += TextBox_TextChanged;
            textBoxPhoneNumber.TextChanged += TextBox_TextChanged;
            textBoxEmail.TextChanged += TextBox_TextChanged;
            textBoxIdNumber.TextChanged += TextBox_TextChanged;
            textBoxTitle.TextChanged += TextBox_TextChanged;
            textBoxYearsOfService.TextChanged += TextBox_TextChanged;

            ValidateTextBoxes();
        }

        private void ValidateTextBoxes()
        {
            addButton.IsEnabled = ProfessorDto.isValid;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxes();
        }

        private void SetInitialWindowSize(double left, double top, double width, double height)
        {
            Left = left;
            Top = top; 
            Width = width; 
            Height = height;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ProfessorDto.isValid)
            {
                _professorController.AddProfessor(ProfessorDto.ToProfessor());
                Close();
            }
            else
            {
                MessageBox.Show("Professor cannot be created. Not all fields are valid.");
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
