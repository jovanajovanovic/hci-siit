using System;
using System.Collections.Generic;
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
    /// Interaction logic for SaveScheduler.xaml
    /// </summary>
    public partial class SaveScheduler : Window
    {
        public SaveScheduler()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = this.name_edit.Text;
            ViewModel.MainViewModel.SaveAppointments(name);
            this.Close();
        }
    }
}
