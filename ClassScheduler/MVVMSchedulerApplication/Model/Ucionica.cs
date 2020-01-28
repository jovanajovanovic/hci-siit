using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static MVVMSchedulerApplication.Model.Enums;

namespace MVVMSchedulerApplication.Model
{
    public class Ucionica : INotifyPropertyChanged
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
        private OS os;
        private List<Softver> allSoftware;
        private int numberOfSeats;

        public string Code { get { return code; } set { code = value; OnPropertyChanged("Code"); } }
        public string Description { get { return description; } set { description = value; OnPropertyChanged("Description"); } }
        public int NumberOfSeats { get { return numberOfSeats; } set { numberOfSeats = value; OnPropertyChanged("NumberOfSeats"); } }
        public bool Projector { get { return projector; } set { projector = value; OnPropertyChanged("Projector"); } }
        public bool Table { get { return table; } set { table = value; OnPropertyChanged("Table"); } }
        public bool SmartTable { get { return smartTable; } set { smartTable = value; OnPropertyChanged("SmartTable"); } }
        public OS Os { get { return os; } set { os = value; OnPropertyChanged("OS"); } }
        public List<Softver> AllSoftware { get { return allSoftware; } set { allSoftware = value; OnPropertyChanged("Software"); } }

        public Ucionica() { }


    }
}
