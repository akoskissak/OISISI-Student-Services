using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using GUI.View;
using System.Windows.Threading;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CLI.Controller;
using System.Linq;
using System.Windows.Controls.Ribbon;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        public ObservableCollection<ProfessorDTO> ProfessorDtos { get; set; }
        public ObservableCollection<SubjectDTO> SubjectDtos { get; set; }
        public ObservableCollection<StudentDTO> StudentDtos { get; set; }
        
        public ProfessorDTO SelectedProfessor { get; set; }
        public SubjectDTO SelectedSubject { get; set; }
        public StudentDTO SelectedStudent { get; set; }

        private ProfessorSubjectController _professorSubjectController { get; set; }
        private StudentSubjectController _studentSubjectController { get; set; }
        private ProfessorController _professorController {  get; set; }
        private StudentController _studentController { get; set; }    
        private SubjectController _subjectController { get; set; }
        private ExamGradeController _examGradeController;

        public PagingCollectionView ProfessorPagingCollectionView {  get; set; }
        public PagingCollectionView SubjectPagingCollectionView {  get; set; }
        public PagingCollectionView StudentPagingCollectionView { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ProfessorDtos = new ObservableCollection<ProfessorDTO>();
            StudentDtos = new ObservableCollection<StudentDTO>();
            SubjectDtos = new ObservableCollection<SubjectDTO>();

            SetInitialWindowSize();

            ProfessorPagingCollectionView = new PagingCollectionView(ProfessorDtos, 16);
            SubjectPagingCollectionView = new PagingCollectionView(SubjectDtos, 16);
            StudentPagingCollectionView = new PagingCollectionView(StudentDtos, 16);
            
            _professorSubjectController = new ProfessorSubjectController();
            _studentSubjectController = new StudentSubjectController();
            _examGradeController = new ExamGradeController();

            _studentController = new StudentController();
            _professorController = new ProfessorController();
            _subjectController = new SubjectController();
            

            _studentController.Subscribe(this);
            _professorController.Subscribe(this);
            _subjectController.Subscribe(this);
            _examGradeController.Subscribe(this);
            //_studentSubjectController.Subscribe(this);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            Update();
        }

        private void SetInitialWindowSize()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            double targetWidth = screenWidth * 0.75;
            double targetHeight = screenHeight * 0.75;

            Width = targetWidth;
            Height = targetHeight;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                AddProfessor addProfessorWindow = new AddProfessor(_professorController, Left, Top, Width, Height);
                addProfessorWindow.ShowDialog();
            }
            else if(ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                AddStudent addStudent = new AddStudent(_studentController, Left, Top, Width, Height);
                addStudent.ShowDialog();
            }
            else if(ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                AddSubject addSubject = new AddSubject(_subjectController, _professorSubjectController, Left, Top, Width, Height);
                addSubject.ShowDialog();
            }
            
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                if (SelectedProfessor != null)
                {
                    UpdateProfessor updateProfessorWindow = new UpdateProfessor(_professorController, SelectedProfessor, Left, Top, Width, Height);
                    updateProfessorWindow.ShowDialog();
                }
                else
                    MessageBox.Show("Please choose a professor to update!");
            }
            else if(ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                if (SelectedStudent != null)
                {
                    UpdateStudent updateStudentWindow = new UpdateStudent(_studentController, _examGradeController, SelectedStudent, _subjectController ,Left, Top, Width, Height);
                    updateStudentWindow.ShowDialog();
                }
                else
                    MessageBox.Show("Please choose a student to update!");
            }
            else if(ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                if (SelectedSubject != null)
                {
                    UpdateSubject updateSubjectWindow = new UpdateSubject(_subjectController, SelectedSubject, _professorSubjectController, Left, Top, Width, Height);
                    updateSubjectWindow.ShowDialog();
                }
                else
                    MessageBox.Show("Please choose a subject to update!");
            }
        }

        public void Update()
        {
            ProfessorDtos.Clear();
            foreach (Professor professor in _professorController.GetAllProfessors())
                ProfessorDtos.Add(new ProfessorDTO(professor));

            StudentDtos.Clear();
            foreach(Student student in _studentController.GetAllStudents())
                StudentDtos.Add(new StudentDTO(student));

            SubjectDtos.Clear();
            foreach (Subject subject in _subjectController.GetAllSubjects())
                SubjectDtos.Add(new SubjectDTO(subject));

            ProfessorPagingCollectionView.InnerList = ProfessorDtos;
            SubjectPagingCollectionView.InnerList = SubjectDtos;
            StudentPagingCollectionView.InnerList = StudentDtos;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                if (SelectedProfessor == null)
                    MessageBox.Show("Please choose a professor to delete!");
                else
                {
                    string messageBoxText = "Are you sure you want to delete professor?";
                    string caption = "Delete professor";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (_professorController.RemoveProfessor(SelectedProfessor.Id))
                        {
                            ProfessorDtos.Remove(SelectedProfessor);
                            MessageBox.Show("Professor deleted successfully.", "Deletion Successful", MessageBoxButton.OK);
                        }
                        else
                        {
                            MessageBox.Show("Professor cannot be deleted.", "Deletion failed", MessageBoxButton.OK);
                        }
                    }

                }
            }
            else if (ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                if (SelectedStudent == null)
                    MessageBox.Show("Please choose a student to delete!");
                else
                {
                    string messageBoxText = "Are you sure you want to delete student?";
                    string caption = "Delete student";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (_studentController.RemoveStudent(SelectedStudent.Id))
                        {

                            StudentDtos.Remove(SelectedStudent);
                            MessageBox.Show("Student deleted successfully.", "Deletion Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Student cannot be deleted.", "Deletion failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }

            }
            else if (ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                if (SelectedSubject == null)
                {
                    MessageBox.Show("Please choose a subject to delete!");
                }
                else
                {
                    string messageBoxText = "Are you sure you want to delete subject?";
                    string caption = "Delete subject";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.Question);
                    if(result == MessageBoxResult.Yes)
                    {
                        if (_subjectController.RemoveSubject(SelectedSubject.Id))
                        {
                            SubjectDtos.Remove(SelectedSubject);
                            MessageBox.Show("Subject deleted successfully.", "Deletion Successful", MessageBoxButton.OK);
                        }
                        else
                        {
                            MessageBox.Show("Subject cannot be deleted.", "Deletion failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void ProfessorsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedProfessor = ProfessorsDataGrid.SelectedItem as ProfessorDTO;
        }

        private void SubjectDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedSubject = SubjectsDataGrid.SelectedItem as SubjectDTO;
        }
        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedStudent = StudentsDataGrid.SelectedItem as StudentDTO;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            DisplayDateTextBlock.Text = DateTime.Now.ToString();
        }

        private void NavigateToStudents(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedItem = StudentsTab;
        }

        private void NavigateToProfessors(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedItem = ProfessorsTab;
        }

        private void NavigateToSubjects(object sender, RoutedEventArgs e)
        {
            Tabs.SelectedItem = SubjectsTab;
        }

        private void CLose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.N))
                Add_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
                Update_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                Delete_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.X))
                CLose_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.B))
                About_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.A))
                Add_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.U))
                Update_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                Delete_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.O))
                Open_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.S))
                Save_Click(sender, e);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _subjectController.Save();
            _professorController.Save();
            _studentController.Save();
            MessageBox.Show("Successfully saved.", "Saving Successful", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutMain aboutMain = new AboutMain();
            aboutMain.ShowDialog();
        }

        private void Department_Click(object sender, RoutedEventArgs e)
        {
            DepartmentView department = new DepartmentView(_professorController, Left, Top, Width, Height);
            department.ShowDialog();
            Update();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            filemenu.IsSubmenuOpen = true;
            openMenuItem.IsSubmenuOpen = true;

            e.Handled = true;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                if (ProfessorPagingCollectionView.CurrentPage != 1)
                {
                    ProfessorPagingCollectionView.MoveToFirstPage();
                    currentPageLabel.Content = 1;
                }

                if (searchTextBox.Text.Length == 0)
                    Update();
                else
                {
                    ProfessorDtos.Clear();
                    List<Professor>? professors = _professorController.GetProfessorsByText(searchTextBox.Text);
                    if (professors != null)
                        foreach (Professor professor in professors)
                            ProfessorDtos.Add(new ProfessorDTO(professor));
                }
            }
            else if (ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                if(StudentPagingCollectionView.CurrentPage != 1)
                {
                    StudentPagingCollectionView.MoveToFirstPage();
                    currentPageLabel.Content = 1;
                }

                if (searchTextBox.Text.Length == 0)
                    Update();
                else
                {
                    StudentDtos.Clear();
                    List<Student>? students = _studentController.GetStudentByText(searchTextBox.Text);
                    if(students != null)
                        foreach (Student student in students)
                            StudentDtos.Add(new StudentDTO(student));
                }
            }
            else if (ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                if (SubjectPagingCollectionView.CurrentPage != 1)
                {
                    SubjectPagingCollectionView.MoveToFirstPage();
                    currentPageLabel.Content = 1;
                }

                if (searchTextBox.Text.Length == 0)
                    Update();
                else
                {
                    SubjectDtos.Clear();
                    List<Subject>? subjects = _subjectController.GetSubjectsByText(searchTextBox.Text);
                    if (subjects != null)
                        foreach (Subject subject in subjects)
                            SubjectDtos.Add(new SubjectDTO(subject));
                }
            }
        }

        private void ShowProfessorsForStudent_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                if (SelectedStudent == null)
                    MessageBox.Show("Please choose a student!", "Professors for student", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    List<Professor> professors = _professorSubjectController.GetAllProfessorsOnSubjects(SelectedStudent.UnsubmittedSubjects);
                    if (professors != null)
                    {
                        ProfessorsForStudent professorsForStudent = new ProfessorsForStudent(professors);
                        professorsForStudent.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Student does not have professors", "Professors for student", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select tab Students", "Professors for student", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowStudentsForProfessor_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if(ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                if(SelectedProfessor == null)
                {
                    MessageBox.Show("Please choose a professor!", "Students for professor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    StudentsForProfessor studentsForProfessor = new StudentsForProfessor(SelectedProfessor.Id);
                    studentsForProfessor.ShowDialog();

                }
            }
            else
            {
                MessageBox.Show("Please select tab Professors", "Students for professor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // sortDirection: 0 -> ascending, 1 -> descending
        private void ArrowUp_Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                TabItem ti = Tabs.SelectedItem as TabItem;
                string columnName = (button.DataContext as string)!.Trim();

                if (ti != null && ti.Name == "ProfessorsTab")
                {
                    if (ProfessorPagingCollectionView.CurrentPage != 1)
                    {
                        ProfessorPagingCollectionView.MoveToFirstPage();
                        currentPageLabel.Content = 1;
                    }


                    if (ProfessorDtos.Count != _professorController.GetAllProfessors().Count)
                        SortSearchedProfessors(columnName, 0);
                    else
                        _professorController.SortProfessors(columnName, 0);

                }
                else if (ti != null && ti.Name == "StudentsTab")
                {
                    if(StudentPagingCollectionView.CurrentPage != 1)
                    {
                        StudentPagingCollectionView.MoveToFirstPage();
                        currentPageLabel.Content = 1;
                    }

                    if (StudentDtos.Count != _studentController.GetAllStudents().Count)
                        SortSearchedStudents(columnName, 0);
                    else
                        _studentController.SortStudents(columnName, 0);
                }
                else if (ti != null && ti.Name == "SubjectsTab")
                {
                    if (SubjectPagingCollectionView.CurrentPage != 1)
                    {
                        SubjectPagingCollectionView.MoveToFirstPage();
                        currentPageLabel.Content = 1;
                    }

                    if (SubjectDtos.Count != _subjectController.GetAllSubjects().Count)
                        SortSearchedSubjects(columnName, 0);
                    else
                        _subjectController.SortSubjects(columnName, 0);
                }
            }
        }

        private void ArrowDown_Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                TabItem ti = Tabs.SelectedItem as TabItem;
                string columnName = (button.DataContext as string)!.Trim();

                if (ti != null && ti.Name == "ProfessorsTab")
                {
                    if (ProfessorPagingCollectionView.CurrentPage != 1)
                    {
                        ProfessorPagingCollectionView.MoveToFirstPage();
                        currentPageLabel.Content = 1;
                    }

                    if (ProfessorDtos.Count != _professorController.GetAllProfessors().Count)
                        SortSearchedProfessors(columnName, 1);
                    else
                        _professorController.SortProfessors(columnName, 1);
                }
                else if (ti != null && ti.Name == "StudentsTab")
                {
                    if(StudentPagingCollectionView.CurrentPage != 1)
                    {
                        StudentPagingCollectionView.MoveToFirstPage();
                        currentPageLabel.Content = 1;
                    }
                    
                    if(StudentDtos.Count != _studentController.GetAllStudents().Count)
                        SortSearchedStudents(columnName, 1);
                    else
                        _studentController.SortStudents(columnName, 1);
                }
                else if (ti != null && ti.Name == "SubjectsTab")
                {
                    if (SubjectPagingCollectionView.CurrentPage != 1)
                    {
                        SubjectPagingCollectionView.MoveToFirstPage();
                        currentPageLabel.Content = 1;
                    }

                    if (SubjectDtos.Count != _subjectController.GetAllSubjects().Count)
                        SortSearchedSubjects(columnName, 1);
                    else
                        _subjectController.SortSubjects(columnName, 1);
                }
            }

        }

        public void SortSearchedProfessors(string columnName, int sortDirection)
        {
            List<int> ids = new List<int>();
            foreach (ProfessorDTO profDto in ProfessorDtos)
            {
                ids.Add(profDto.Id);
            }
            _professorController.SortProfessors(columnName, sortDirection);
            ProfessorDtos.Clear();
            List<Professor>? professors = _professorController.GetSortedSearchedProfessors(ids);
            if (professors != null)
                foreach (Professor professor in professors)
                    ProfessorDtos.Add(new ProfessorDTO(professor));
        }

        public void SortSearchedStudents(string columnName, int sortDirection)
        {
            List<int> ids = new List<int>();
            foreach(StudentDTO studentDto in StudentDtos)
            {
                ids.Add(studentDto.Id);
            }
            _studentController.SortStudents(columnName, sortDirection);
            StudentDtos.Clear();
            List<Student>? students = _studentController.GetSortedSearchedStudents(ids);
            if(students != null)
                foreach (Student student in students)
                    StudentDtos.Add(new StudentDTO(student));
        }

        public void SortSearchedSubjects(string columnName, int sortDirection)
        {
            List<int> ids = new List<int>();
            foreach (SubjectDTO subjDto in SubjectDtos)
            {
                ids.Add(subjDto.Id);
            }
            _subjectController.SortSubjects(columnName, 0);
            SubjectDtos.Clear();
            List<Subject>? subjects = _subjectController.GetSortedSearchedSubjects(ids);
            if (subjects != null)
                foreach (Subject subject in subjects)
                    SubjectDtos.Add(new SubjectDTO(subject));
        }

        private void NextPage_Button_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                this.ProfessorPagingCollectionView.MoveToNextPage();
                currentPageLabel.Content = this.ProfessorPagingCollectionView.CurrentPage;
                //this.CurrentPage = ProfessorPagingCollectionView.CurrentPage;
            }
            else if (ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                this.StudentPagingCollectionView.MoveToNextPage();
                currentPageLabel.Content = this.StudentPagingCollectionView.CurrentPage;
            }
            else if (ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                this.SubjectPagingCollectionView.MoveToNextPage();
                currentPageLabel.Content = this.SubjectPagingCollectionView.CurrentPage;
            }
        }

        private void PreviousPage_Button_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                this.ProfessorPagingCollectionView.MoveToPreviousPage();
                currentPageLabel.Content = this.ProfessorPagingCollectionView.CurrentPage;
            }
            else if (ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                this.StudentPagingCollectionView.MoveToPreviousPage();
                currentPageLabel.Content = this.StudentPagingCollectionView.CurrentPage;
            }
            else if (ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                this.SubjectPagingCollectionView.MoveToPreviousPage();
                currentPageLabel.Content = this.SubjectPagingCollectionView.CurrentPage;
            }
        }

        private void FirstPage_Button_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                this.ProfessorPagingCollectionView.MoveToFirstPage();
                currentPageLabel.Content = this.ProfessorPagingCollectionView.CurrentPage;
            }
            else if (ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                this.StudentPagingCollectionView.MoveToFirstPage();
                currentPageLabel.Content = this.StudentPagingCollectionView.CurrentPage;
            }
            else if (ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                this.SubjectPagingCollectionView.MoveToFirstPage();
                currentPageLabel.Content = this.SubjectPagingCollectionView.CurrentPage;
            }
        }

        private void TabControl_Changed(object sender, SelectionChangedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "ProfessorsTab")
            {
                currentPageLabel.Content = this.ProfessorPagingCollectionView.CurrentPage;
            }
            else if (ti != null && ti.Name != null && ti.Name == "StudentsTab")
            {
                currentPageLabel.Content = this.StudentPagingCollectionView.CurrentPage;
            }
            else if (ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                currentPageLabel.Content = this.SubjectPagingCollectionView.CurrentPage;
            }
        }

        private void StudentConditionSubject_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = Tabs.SelectedItem as TabItem;
            if (ti != null && ti.Name != null && ti.Name == "SubjectsTab")
            {
                if (SelectedSubject != null)
                {
                    StudentConditionSubject studentConditionSubject = new StudentConditionSubject(_studentController, _subjectController, SelectedSubject);
                    studentConditionSubject.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Please choose a subject to make a condition", "Student condition subject", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose subjects tab to choose a subject!", "Student condition subject", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
