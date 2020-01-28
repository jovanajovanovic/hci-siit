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
using MVVMSchedulerApplication.Model;

namespace MVVMSchedulerApplication.Smerovi
{
    /// <summary>
    /// Interaction logic for IzmenaSmera.xaml
    /// </summary>
    public partial class IzmenaSmera : Window
    {
        private Smer smer;
        private Model.DBManager db;

        public IzmenaSmera()
        {
            InitializeComponent();
        }

        public IzmenaSmera(Smer smer)
        {
            this.smer = smer;
            InitializeComponent();

            this.id_edit.Label = "ID: " + smer.Code;
            this.description_edit.Text = smer.Description;
            this.name_edit.Text = smer.Name;
            this.date_edit.EditValue = smer.DateOfIntroduction;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //izmena smera
            string name = name_edit.Text;
            string description = description_edit.Text;
            DateTime date = this.date_edit.DateTime;
            Model.Smer s = new Model.Smer() { Code = smer.Code,  Name = name, Description = description, DateOfIntroduction = date };
            db = new Model.DBManager();
            try
            {
                db.UpdateSmer(s);
                //prikazati poruku da je smer uspesno unesen
                
                MessageBox.Show("the course has been successfully updated.");
                this.Close();
            }
            catch (Exception exc)
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
