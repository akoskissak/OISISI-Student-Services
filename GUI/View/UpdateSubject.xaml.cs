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
    /// Interaction logic for UpdateSubject.xaml
    /// </summary>
    public partial class UpdateSubject : Window
    {
        public SubjectDTO SubjectDto { get; set; }
        private SubjectController _subjectController;

        public event PropertyChangedEventHandler? PropertyChanged;

        public UpdateSubject(SubjectController subjectController, SubjectDTO subjectDto, double left, double top, double width, double height)
        {
            InitializeComponent();
            DataContext = this;
            SubjectDto = subjectDto;
            this._subjectController = subjectController;

            semesterComboBox.Items.Clear();
            semesterComboBox.SelectedIndex = 0;
            semesterComboBox.ItemsSource = Enum.GetValues(typeof(SemesterType));

            SetInitialWindowSize(left, top, width, height);

            textBoxSubjectCode.TextChanged += TextBox_Changed;
            textBoxName.TextChanged += TextBox_Changed;
            textBoxEspb.TextChanged += TextBox_Changed;
            textBoxProfessorId.TextChanged += TextBox_Changed;
            textBoxYearOfStudy.TextChanged += TextBox_Changed;
        }
        private void ValidateTextBoxes()
        {
            updateButton.IsEnabled = SubjectDto.IsValid;
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectDto.IsValid)
            {
                Subject subject = SubjectDto.ToSubject();
                subject.Id = SubjectDto.Id;
                _subjectController.UpdateSubject(subject);
                Close();
            }
            else
            {
                MessageBox.Show("Subject can not be updated. Not all fields are valid.");
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _subjectController.NotifyObservers();
        }
    }
}
