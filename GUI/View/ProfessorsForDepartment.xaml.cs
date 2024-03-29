﻿using CLI.Controller;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ProfessorsForDepartment.xaml
    /// </summary>
    public partial class ProfessorsForDepartment : Window
    {
        private App app;
        private const string SRB = "sr-RS";
        private const string ENG = "en-US";
        private DepartmentController _departmentController {  get; set; }
        private ProfessorController _professorController { get; set; }

        public ObservableCollection<ProfessorDTO> ProfessorDtos { get; set; }
        private ProfessorDTO SelectedProfessor { get; set; }
        private DepartmentDTO SelectedDepartment { get; set; }

        public ProfessorsForDepartment(DepartmentDTO selectedDepartment, DepartmentController departmentController, ProfessorController professorController)
        {
            InitializeComponent();
            DataContext = this;
            app = (App)Application.Current;
            SelectedDepartment = selectedDepartment;
            this._departmentController = departmentController;
            this._professorController = professorController;

            ProfessorDtos = new ObservableCollection<ProfessorDTO>();
            Update();
            SetDepChief.IsEnabled = false;
        }

        private void Update()
        {
            ProfessorDtos.Clear();
            List<Professor> professors = _departmentController.GetAllProfessorsByDepartmentId(SelectedDepartment.Id);
            foreach(Professor professor in professors)
            {
                ProfessorDtos.Add(new ProfessorDTO(professor));
            }
        }

        private void ProfessorsForDepartmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedProfessor = ProfessorsForDepartmentDataGrid.SelectedItem as ProfessorDTO;
            SetDepChief.IsEnabled = true;
        }

        private void SetChief_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedProfessor == null)
            {
                MessageBox.Show("Please choose a professor to set as a chief");
            }
            else
            {
                bool sucess = _professorController.CanProfessorBeAChief(SelectedProfessor.Id);
                bool has_already_a_chief = _departmentController.HasDepartmentChief(SelectedDepartment.Id);
                if (!sucess)
                {
                    MessageBox.Show("Professor cannot be a chief.", "Set as a chief failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }else if(has_already_a_chief)
                {
                    MessageBox.Show("Department already has a chief.", "Set as a chief failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    _departmentController.SetProfessorAsAChief(SelectedProfessor.Id, SelectedDepartment.Id);
                    Professor professor = _professorController.GetProfessorById(SelectedProfessor.Id);
                    professor.IdOfChiefDepartment = SelectedDepartment.Id;
                    _professorController.NotifyObservers();
                    MessageBox.Show("Professor successfully set as a chief!", "Chief successfully set", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.G))
                MenuItem_Click_English(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R))
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
