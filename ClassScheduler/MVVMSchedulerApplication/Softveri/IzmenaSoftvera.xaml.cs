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
    /// Interaction logic for IzmenaSoftvera.xaml
    /// </summary>
    public partial class IzmenaSoftvera : Window
    {
        private SoftverDTO softver;

        public IzmenaSoftvera()
        {
            InitializeComponent();
        }

        public IzmenaSoftvera(SoftverDTO softver)
        {
            this.softver = softver;
            InitializeComponent();

            this.id_edit.Label = "ID: " + softver.Code;
            this.name_edit.Text = softver.Name;
            this.des_edit.Text = softver.Description;
            this.man_edit.Text = softver.Manufacturer;
            this.site_edit.Text = softver.Website;
            this.price_edit.Text = Convert.ToString(softver.Price);
            this.year_edit.Text = softver.YearOfPublication;
            int idx = (int)Enum.Parse(typeof(Model.Enums.OS), softver.OperationSystem);
            os_edit.SelectedIndex = idx;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
                Code = softver.Code,
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
                db.UpdateSoftver(s);
                MessageBox.Show("The software has been update successfully");
                this.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private Enums.OS SystemStringToOs(string os)
        {
            if (os.Equals("Windows"))
            {
                return Enums.OS.Windows;
            }
            else if (os.Equals("Linux"))
            {
                return Enums.OS.Linux;
            }
            else if (os.Equals("Any"))
            {
                return Enums.OS.Any;
            }
            else if (os.Equals("Both"))
            {
                return Enums.OS.Both;
            }
            else if (os.Equals("Cross platform"))
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
