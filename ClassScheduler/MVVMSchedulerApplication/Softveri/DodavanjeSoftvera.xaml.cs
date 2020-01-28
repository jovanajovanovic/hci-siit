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

namespace MVVMSchedulerApplication.Softveri
{
    /// <summary>
    /// Interaction logic for DodavanjeSoftvera.xaml
    /// </summary>
    public partial class DodavanjeSoftvera : Window
    {
        public DodavanjeSoftvera()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string id = this.id_edit.Text;
            string name = this.name_edit.Text;
            string year = this.year_edit.Text;
            string os = this.os_edit.Text;
            double price = Convert.ToDouble(this.price_edit.Text);
            string man = this.man_edit.Text;
            string site = this.site_edit.Text;
            string description = this.des_edit.Text;

            Model.Enums.OS operation = SystemStringToOs(os);

            Softver s = new Softver()
            {
                Code = id,
                Name = name,
                Description = description,
                Manufacturer = man,
                OperationSystem = operation,
                Price = price,
                Website = site,
                YearOfPublication = year
            };
            DBManager db = new DBManager();
            try
            {
                db.InsertSoftver(s);
                MessageBox.Show("The software has been added successfully");
                this.Close();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private Enums.OS SystemStringToOs(string os)
        {
            if (os.Equals("Windows"))
            {
                return Enums.OS.Windows;
            }else if (os.Equals("Linux"))
            {
                return Enums.OS.Linux;
            }else if (os.Equals("Any"))
            {
                return Enums.OS.Any;
            }else if (os.Equals("Both"))
            {
                return Enums.OS.Both;
            }else if(os.Equals("Cross platform"))
            {
                return Enums.OS.CrossPlatform;
            }

            return Enums.OS.Any;
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
