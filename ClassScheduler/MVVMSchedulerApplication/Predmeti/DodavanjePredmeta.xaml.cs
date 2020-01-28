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

namespace MVVMSchedulerApplication.Predmeti
{
    /// <summary>
    /// Interaction logic for DodavanjePredmeta.xaml
    /// </summary>
    public partial class DodavanjePredmeta : Window
    {
        public ObservableCollection<ComboBoxItem> cbItemsCours { get; set; }
        public ObservableCollection<ComboBoxEditItem> cbItemsSoft { get; set; }
        public Model.DBManager db;
        public DodavanjePredmeta()
        {
            InitializeComponent();
            FieldLists();
            DataContext = this;
            cbItemsSoft = new ObservableCollection<ComboBoxEditItem>();
        }

        private void FieldLists()
        {
            db = new Model.DBManager();

            cbItemsCours = new ObservableCollection<ComboBoxItem>();
            List<Model.Smer> smerovi = db.SelectSmer();
            foreach (Model.Smer s in smerovi)
            {
                cbItemsCours.Add(new ComboBoxItem() { Content = s.Code });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string id = this.id_edit.Text;
            string name = this.name_edit.Text;
            string course = this.course_edit.Text;
            string des = this.des_edit.Text;
            Boolean proj = StringToBoolean(this.projector_edit.Text);
            Boolean table = StringToBoolean(this.table_edit.Text);
            Boolean smart = StringToBoolean(this.st_edit.Text);
            int group = Convert.ToInt16(this.size_edit.Text);
            int len = Convert.ToInt16(this.len_edit.Text);
            int num = Convert.ToInt16(this.num_edit.Text);
            Model.Enums.OS os = SystemStringToOs(this.os_edit.Text);
            string soft = this.soft_edit.Text;

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


            Model.Smer c = db.FindSmerByOznaka(course);
         

            
            Model.Predmet p = new Model.Predmet()
            {
                Code = id,
                Name = name,
                Course = c,
                Description = des,
                Projector = proj,
                Table = table,
                SmartTable = smart,
                GroupSize = group,
                LengthOfTerm = len,
                NumberOfTerms = num,
                Os = os,
                AllSoftware = softveri

            };
            try
            {
                db.InsertPredmet(p);
                MessageBox.Show("The subject has been successfully added");
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
            Model.Enums.OS os = SystemStringToOs(select);
            soft_edit.Clear();
            FieldSoftwareList(os);

        }

        private void FieldSoftwareList(Model.Enums.OS os)
        {
            cbItemsSoft.Clear();
            db = new Model.DBManager();
            List<Model.Softver> softveri = db.FindSoftwareByOs(os);
            foreach (Model.Softver s in softveri)
            {
                cbItemsSoft.Add(new ComboBoxEditItem() { Content = s.Code });
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

        public void doThings(string param)
        {
            course_edit.Background = new SolidColorBrush(Color.FromRgb(32, 64, 128));
            Title = param;
        }
    }


}
