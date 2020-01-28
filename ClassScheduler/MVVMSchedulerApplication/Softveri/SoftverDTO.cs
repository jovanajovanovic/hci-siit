using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMSchedulerApplication.Softveri
{
   public class SoftverDTO : INotifyPropertyChanged
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
        private string operationSystem;
        private string manufacturer;
        private string website;
        private string yearOfPublication;
        private double price;
        private string description;

        public string Code { get { return code; } set { code = value; OnPropertyChanged("Code"); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public string OperationSystem { get { return operationSystem; } set { operationSystem = value; OnPropertyChanged("OperationSystem"); } }
        public string Manufacturer { get { return manufacturer; } set { manufacturer = value; OnPropertyChanged("Manufacture"); } }
        public string Website { get { return website; } set { website = value; OnPropertyChanged("Website"); } }
        public string YearOfPublication { get { return yearOfPublication; } set { yearOfPublication = value; OnPropertyChanged("YearOfPublication"); } }
        public double Price { get { return price; } set { price = value; OnPropertyChanged("Price"); } }
        public string Description { get { return description; } set { description = value; OnPropertyChanged("Description"); } }

    }
}
