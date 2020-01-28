using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMSchedulerApplication.Predmeti
{
    public class PredmetDTO : INotifyPropertyChanged
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
        private string name;
        private string course;
        private string description;
        private int groupSize;
        private int lengthOfTerm;
        private int numberOfTerms;
        private bool projector;
        private bool table;
        private bool smartTable;
        private string operationSystem;
        private string software;
        private List<string> softwareList;


        public string Code { get { return code; } set { code = value; OnPropertyChanged("Code"); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public string Course { get { return course; } set { course = value; OnPropertyChanged("Course"); } }
        public string Description { get { return description; } set { description = value; OnPropertyChanged("Description"); } }
        public int GroupSize { get { return groupSize; } set { groupSize = value; OnPropertyChanged("GroupSize"); } }
        public int LengthOfTerm { get { return lengthOfTerm; } set { lengthOfTerm = value; OnPropertyChanged("LengthOfTerm"); } }
        public int NumberOfTerms { get { return numberOfTerms; } set { numberOfTerms = value; OnPropertyChanged("NumberOfTerms"); } }
        public bool Projector { get { return projector; } set { projector = value; OnPropertyChanged("Projector"); } }
        public bool Table { get { return table; } set { table = value; OnPropertyChanged("Table"); } }
        public bool SmartTable { get { return smartTable; } set { smartTable = value; OnPropertyChanged("SmartTable"); } }
        public string OperationSystem { get { return operationSystem; } set { operationSystem = value; OnPropertyChanged("OperationSystem"); } }
        public string Software { get { return software; } set { software = value; OnPropertyChanged("Software"); } }
        public List<String> SoftwareList { get { return softwareList; } set { softwareList = value;  OnPropertyChanged("SoftwareList"); } }
    }
}
