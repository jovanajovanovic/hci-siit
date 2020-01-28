using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMSchedulerApplication.Ucionice
{
    public   class UcioniceDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string code;
        private string description;
        private bool projector;
        private bool table;
        private bool smartTable;
        private string operationSystem;
        private List<string> softwareList;
        private string software;
        private int numberOfSeats;

        public string Code { get { return code; } set { code = value; OnPropertyChanged("Code"); } }
        public string Description { get { return description; } set { description = value; OnPropertyChanged("Description"); } }
        public int NumberOfSeats { get { return numberOfSeats; } set { numberOfSeats = value; OnPropertyChanged("NumberOfSeats"); } }
        public bool Projector { get { return projector; } set { projector = value; OnPropertyChanged("Projector"); } }
        public bool Table { get { return table; } set { table = value; OnPropertyChanged("Table"); } }
        public bool SmartTable { get { return smartTable; } set { smartTable = value; OnPropertyChanged("SmartTable"); } }
        public string OperationSystem { get { return operationSystem; } set { operationSystem = value; OnPropertyChanged("OS"); } }
        public List<string> SoftwareList { get { return softwareList; } set { softwareList = value; OnPropertyChanged("SoftwareList"); } }
        public string Software { get { return software; } set { software = value; OnPropertyChanged("Software"); } }


    }
}
