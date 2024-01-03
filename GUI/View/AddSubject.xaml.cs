using CLI.Controller;
using CLI.DAO;
using CLI.Model;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddSubject.xaml
    /// </summary>
    public partial class AddSubject : Window
    {
        public SubjectDTO SubjectDto { get; set; }

        private SubjectController _subjectController;

        public AddSubject(SubjectController subjectController, double left, double top, double width, double height)
        {
            InitializeComponent();
            DataContext = this;
            SubjectDto = new SubjectDTO();
            this._subjectController = subjectController;

            SetInitialWindowSize(left, top, width, height);

            semesterComboBox.Items.Clear();
            semesterComboBox.SelectedIndex = 0;
            semesterComboBox.ItemsSource = Enum.GetValues(typeof(SemesterType));

            textBoxSubjectCode.TextChanged += TextBox_Changed;
            textBoxName.TextChanged += TextBox_Changed;
            textBoxEspb.TextChanged += TextBox_Changed;
            textBoxProfessorId.TextChanged += TextBox_Changed;
            textBoxYearOfStudy.TextChanged += TextBox_Changed;

            ValidateTextBoxes();
        }

        private void ValidateTextBoxes()
        {
            addButton.IsEnabled = SubjectDto.IsValid;
        }

        private void TextBox_Changed(object sender, EventArgs e)
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
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectDto.IsValid)
            {
                _subjectController.AddSubject(SubjectDto.ToSubject());
                Close();
            }
            else
            {
                MessageBox.Show("Subject can not be created. Not all fields are valid.");
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
