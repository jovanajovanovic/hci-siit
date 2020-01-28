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
    /// Interaction logic for IzmenaPredmeta.xaml
    /// </summary>
    public partial class IzmenaPredmeta : Window
    {
        private PredmetDTO predmet;
        private ObservableCollection<ComboBoxEditItem> cbItemsCourse;
        private ObservableCollection<ComboBoxEditItem> cbItemsSoft;
        private Model.DBManager db;

        public IzmenaPredmeta()
        {
            InitializeComponent();
            FieldList();
        }

        public IzmenaPredmeta(PredmetDTO predmet)
        {
            this.predmet = predmet;

            FieldList();
            InitializeComponent();

            this.id_edit.Label = "ID: " + predmet.Code;
            this.name_edit.Text = predmet.Name;
            this.des_edit.Text = predmet.Description;
            projector_edit.SelectedIndex = predmet.Projector ? 0 : 1;
            table_edit.SelectedIndex = predmet.Table ? 0 : 1;
            st_edit.SelectedIndex = predmet.SmartTable ? 0 : 1;
            int idx = (int)Enum.Parse(typeof(Model.Enums.OS), predmet.OperationSystem);
            os_edit.SelectedIndex = idx;
            this.size_edit.Text = Convert.ToString(predmet.GroupSize);
            this.num_edit.Text = Convert.ToString(predmet.NumberOfTerms);
            this.len_edit.Text = Convert.ToString(predmet.LengthOfTerm);
            soft_edit.ItemsSource = cbItemsSoft;

            soft_edit.EditValue = predmet.Software;

           


            this.course_edit.ItemsSource = cbItemsCourse;
            foreach(ListBoxEditItem item in course_edit.Items)
            {
                if (predmet.Course.Equals(item.Content))
                {
                    item.IsSelected = true;
                }
            }



        }

        public void FieldList()
        {
            cbItemsCourse = new ObservableCollection<ComboBoxEditItem>();
            cbItemsSoft = new ObservableCollection<ComboBoxEditItem>();
            db = new Model.DBManager();
            List<Model.Softver> softveri = db.SelectSoftver();
            List<Model.Smer> smerovi = db.SelectSmer();

            foreach(Model.Softver s in softveri)
            {
                cbItemsSoft.Add(new ComboBoxEditItem() { Content = s.Code });
            }

            foreach(Model.Smer sm  in smerovi)
            {
                cbItemsCourse.Add(new ComboBoxEditItem() { Content = sm.Code });
            }

        }

        private void os_edit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string select = (e.AddedItems[0] as ComboBoxItem).Content as string;
            Model.Enums.OS os = SystemStringToOs(select);
            soft_edit.Clear();
            FieldSoftwareList(os);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
                Code = predmet.Code,
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
                db.UpdatePredmet(p);
                MessageBox.Show("The subject has been successfully updated.");
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
