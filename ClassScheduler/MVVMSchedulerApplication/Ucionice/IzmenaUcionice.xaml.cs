using DevExpress.Xpf.Editors;
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
using MVVMSchedulerApplication.Model;

namespace MVVMSchedulerApplication.Ucionice
{
    /// <summary>
    /// Interaction logic for IzmenaUcionice.xaml
    /// </summary>
    public partial class IzmenaUcionice : Window
    {
        private UcioniceDTO udt;
        private Model.DBManager db;
        private ObservableCollection<ComboBoxEditItem> cbItems;

        public IzmenaUcionice()
        {
            InitializeComponent();
            FieldList();

        }

        public IzmenaUcionice(UcioniceDTO udt)
        {
            this.udt = udt;
            FieldList();
            InitializeComponent();


            id_edit.Label = "ID: " + udt.Code;
            des_edit.Text = udt.Description;
            projector_edit.SelectedIndex = udt.Projector ? 0 : 1;
            table_edit.SelectedIndex = udt.Table ? 0 : 1;
            st_edit.SelectedIndex = udt.SmartTable ? 0 : 1;
            int idx = (int)Enum.Parse(typeof(Model.Enums.OS), udt.OperationSystem);
            os_edit.SelectedIndex = idx;
            ns_edit.Text = udt.NumberOfSeats.ToString();
            soft_edit.ItemsSource = cbItems;
            this.soft_edit.EditValue = udt.Software;

        }

        public void FieldList()
        {
            cbItems = new ObservableCollection<ComboBoxEditItem>();
            db = new Model.DBManager();
            List<Model.Softver> softveri = db.SelectSoftver();
            foreach (Model.Softver s in softveri)
            {
                cbItems.Add(new ComboBoxEditItem() { Content = s.Code });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string id = udt.Code;
            string des = this.des_edit.Text;
            Boolean projector = StringToBoolean(this.projector_edit.Text);
            Boolean table = StringToBoolean(this.table_edit.Text);
            Boolean smartTable = StringToBoolean(this.st_edit.Text);
            int num = Convert.ToInt16(this.ns_edit.Text);
            Model.Enums.OS os = SystemStringToOs(this.os_edit.Text);
            string soft = this.soft_edit.Text;
            //parsirati string soft po ;
            List<Model.Softver> softveri = new List<Model.Softver>();
            Model.Softver so;
            if (soft.Contains(';'))
            {
                string[] tokens = soft.Split(';');


                foreach (string s in tokens)
                {
                    so = db.FindSoftverByOznaka(s);
                    softveri.Add(so);
                }
            }
            else
            {
                so = db.FindSoftverByOznaka(soft);
                softveri.Add(so);
            }



            Model.Ucionica u = new Model.Ucionica()
            {
                Code = id,
                Description = des,
                NumberOfSeats = num,
                Os = os,
                Projector = projector,
                Table = table,
                SmartTable = smartTable,
                AllSoftware = softveri
            };

            try
            {
                db.UpdateUcionica(u);
                MessageBox.Show("Classroom has been successfully updated.");
                ViewModel.MainViewModel.Classrooms.Clear();
                ViewModel.MainViewModel.FillClassrooms();
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
                return Model.Enums.OS.Windows;
            }
            else if (os.Equals("Linux"))
            {
                return Model.Enums.OS.Linux;
            }
            else if (os.Equals("Any"))
            {
                return Model.Enums.OS.Any;
            }
            else if (os.Equals("Both"))
            {
                return Model.Enums.OS.Both;
            }
            else if (os.Equals("Cross platform"))
            {
                return Model.Enums.OS.CrossPlatform;
            }

            return Model.Enums.OS.Any;
        }

        private bool StringToBoolean(string text)
        {
            if (text.Equals("True"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void os_edit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string select = (e.AddedItems[0] as ComboBoxItem).Content as string;
            //Model.Enums.OS os = SystemStringToOs(select);
            soft_edit.Clear();
            FieldList(select);
        }

        private void FieldList(string os)
        {
            cbItems.Clear();
            db = new Model.DBManager();
            List<Model.Softver> softveri = db.FindSoftwareByOs(SystemStringToOs(os));
            foreach (Model.Softver s in softveri)
            {
                cbItems.Add(new ComboBoxEditItem() { Content = s.Code });
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
