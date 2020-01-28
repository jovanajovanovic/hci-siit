using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMSchedulerApplication.Model
{
    public class Smer : INotifyPropertyChanged
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
        private DateTime dateOfIntroduction;
        private string description;

        public string Code { get { return code; } set { code = value; OnPropertyChanged("Code"); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public DateTime DateOfIntroduction { get { return dateOfIntroduction; } set { dateOfIntroduction = value; OnPropertyChanged("DateOfIntroduction"); } }
        public string Description { get { return description; } set { description = value; OnPropertyChanged("Description"); } }

        public Smer() { }
        
    }
}
