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
using DevExpress.Xpf.Editors;

namespace MVVMSchedulerApplication.Ucionice
{
    /// <summary>
    /// Interaction logic for DodavanjeUcionica.xaml
    /// </summary>
    public partial class DodavanjeUcionica : Window
    {
        public ObservableCollection<ComboBoxEditItem> cbItems { get; set; }
        public Model.DBManager db;
        public DodavanjeUcionica()
        {
            InitializeComponent();
            DataContext = this;
            cbItems = new ObservableCollection<ComboBoxEditItem>();
            FieldList(os_edit.Text);

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string id = this.id_edit.Text;
            string des = this.des_edit.Text;
            Boolean projector = StringToBoolean(this.projector_edit.Text);
            Boolean table = StringToBoolean(this.table_edit.Text);
            Boolean smartTable = StringToBoolean(this.st_edit.Text);
            int num = Convert.ToInt16(this.ns_edit.Text);
            Model.Enums.OS os = SystemStringToOs(this.os_edit.Text);
            string soft = this.soft_edit.Text;
            
            //parsirati string soft po ;
            List<Model.Softver> softveri = new List<Softver>();
            Model.Softver so;
            if (soft.Contains(';'))
            {
                string[] tokens = soft.Split(';');


                foreach (string s in tokens)
                {
                    so = db.FindSoftverByOznaka(s);
                    softveri.Add(so);
                }
            }else
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
                db.InsertUcionica(u);
                MessageBox.Show("Classroom has been successfully added.");
                ViewModel.MainViewModel.Classrooms.Clear();
                ViewModel.MainViewModel.FillClassrooms();
                this.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            
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

        private Model.Enums.OS SystemStringToOs(string os)
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

        private void os_edit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string select = (e.AddedItems[0] as ComboBoxItem).Content as string;
            //Model.Enums.OS os = SystemStringToOs(select);
            soft_edit.Clear();
            FieldList(select);
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
