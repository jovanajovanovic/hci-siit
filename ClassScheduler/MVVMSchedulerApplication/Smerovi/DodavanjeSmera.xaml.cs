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

namespace MVVMSchedulerApplication.Smerovi
{
    /// <summary>
    /// Interaction logic for DodavanjeSmera.xaml
    /// </summary>
    public partial class DodavanjeSmera : Window
    {
        public DodavanjeSmera()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string code = code_edit.Text;
            string name = name_edit.Text;
            string description = description_edit.Text;
            DateTime date = this.date_edit.DateTime;
            
            Model.Smer s = new Model.Smer() { Code = code, Name = name, Description = description, DateOfIntroduction = date };
            Model.DBManager db = new Model.DBManager();
            try
            {
                db.InsertSmer(s);
                //prikazati poruku da je smer uspesno unesen
                MessageBox.Show("The course has been successfully added");
                Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }


        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(this);
            if (focusedControl == null)
            {
                focusedControl = this;
            }
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, this);
            }

        }

    }
}
