using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class DepartmentDTO : INotifyPropertyChanged
    {
        private int _id;
        private string _depCode;
        private string _name;
        private int _chiefId;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if(value != _id)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string DepCode
        {
            get
            {
                return _depCode;
            }
            set
            {
                if(value != _depCode)
                {
                    _depCode = value;
                    OnPropertyChanged("DepCode");
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
                if(value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public int ChiefId
        {
            get
            {
                return _chiefId;
            }
            set
            {
                if( value != _chiefId)
                {
                    _chiefId = value;
                    OnPropertyChanged("ChiefId");
                }
            }
        }
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        
        public Department ToDepartment()
        {
            return new Department(_depCode, _name, _chiefId);
        }
        public DepartmentDTO() {}

        public DepartmentDTO(Department department)
        {
            DepCode = department.DepCode;
            Name = department.Name;
            ChiefId = department.ChiefId;
            Id = department.Id;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
