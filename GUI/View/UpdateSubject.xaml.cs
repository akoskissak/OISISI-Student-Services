using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class UpdateSubject : Window, INotifyPropertyChanged, IObserver
    {
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";
        public SubjectDTO SubjectDto { get; set; }
        private SubjectController _subjectController;
        public ProfessorDTO? ProfessorForSubjectDto { get; set; }

        private ProfessorSubjectController _professorSubjectController;

        public event PropertyChangedEventHandler? PropertyChanged;


        public UpdateSubject(SubjectController subjectController, SubjectDTO subjectDto, ProfessorSubjectController professorSubjectController, double left, double top, double width, double height)
        {
            InitializeComponent();
            DataContext = this;
            app = (App)Application.Current;
            app.ChangeLanguage(ENG);

            SubjectDto = subjectDto;
            this._subjectController = subjectController;
            this._professorSubjectController = professorSubjectController;

            semesterComboBox.Items.Clear();
            semesterComboBox.SelectedIndex = 0;
            semesterComboBox.ItemsSource = Enum.GetValues(typeof(SemesterType));

            SetInitialWindowSize(left, top, width, height);

            textBoxSubjectCode.TextChanged += TextBox_Changed;
            textBoxName.TextChanged += TextBox_Changed;
            textBoxEspb.TextChanged += TextBox_Changed;
            textBoxYearOfStudy.TextChanged += TextBox_Changed;

            Update();
        }
        private void ValidateTextBoxes()
        {
            updateButton.IsEnabled = SubjectDto.IsValid;
            bool exist = SubjectDto.Professor != null;
            plusButton.IsEnabled = !exist;
            minusButton.IsEnabled = exist;
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
                subject.Professor = SubjectDto.Professor;
                subject.ProfessorId = SubjectDto.ProfessorId;
                _subjectController.UpdateSubject(subject);
                if (SubjectDto.Professor == null)
                {
                    _professorSubjectController.RemoveProfessorFromSubject(SubjectDto.Id);
                }
                else
                    _professorSubjectController.SetProfessorForSubject(subject.Id, subject.Professor);
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

        private void Plus_Button_Click(object sender, RoutedEventArgs e)
        {
            SelectProfessor selectProfessor = new SelectProfessor(SubjectDto, ProfessorForSubjectDto, _professorSubjectController, Left, Top, Width, Height);
            selectProfessor.ShowDialog();
            Update();
        }

        private void Minus_Button_Click(object sender, RoutedEventArgs e)
        {
            SubjectDto.Professor = null;
            Update();
        }

        public void Update()
        {
            if (SubjectDto.Professor != null)
            {
                ProfessorForSubjectDto = new ProfessorDTO(SubjectDto.Professor);
                textBoxProfessor.Text = ProfessorForSubjectDto.Name + " " + ProfessorForSubjectDto.Lastname;
            }
            else
                textBoxProfessor.Text = "";

            ValidateTextBoxes();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) && Keyboard.IsKeyDown(Key.E))
                MenuItem_Click_English(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftShift) && Keyboard.IsKeyDown(Key.R))
                MenuItem_Click_Serbian(sender, e);
        }

        private void MenuItem_Click_English(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(ENG);
        }

        private void MenuItem_Click_Serbian(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(SRB);
        }
    }
}
