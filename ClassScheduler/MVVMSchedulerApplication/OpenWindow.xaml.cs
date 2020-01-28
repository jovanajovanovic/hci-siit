using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MVVMSchedulerApplication
{
    /// <summary>
    /// Interaction logic for OpenWindow.xaml
    /// </summary>
    public partial class OpenWindow : Window
    {
        public ObservableCollection<ComboBoxItem> cbItems { get; set; }
        public Model.DBManager db; 
        public OpenWindow()
        {
            cbItems = new ObservableCollection<ComboBoxItem>();
            db = new Model.DBManager();
            InitializeComponent();
            FieldList();
        }

        public void FieldList()
        {
            List<String> timetables = db.SelectTimetableName();
            List<String> timetableName = timetables.Distinct().ToList();
            foreach(string s in timetableName)
            {
                cbItems.Add(new ComboBoxItem() { Content = s });
                
            }

            this.open_edit.ItemsSource = cbItems;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //iscitamo iz baze trazeni raspored 
            string scheduler = this.open_edit.Text;

            ViewModel.MainViewModel.FillTasks(scheduler);

            this.Close();
        }
    }
}
