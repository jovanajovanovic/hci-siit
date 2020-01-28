using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Scheduler.UI;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Localization;
using DevExpress.XtraScheduler.UI;
using MVVMSchedulerApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MVVMSchedulerApplication.MainWindow;

namespace MVVMSchedulerApplication
{
    /// <summary>
    /// Interaction logic for HospitalAppointmentForm.xaml
    /// </summary>
    /// 

    public partial class AppointmentForm : UserControl
    {
        public static DateTime temp;
        public ObservableCollection<ComboBoxItem> cbItems { get; set; }
        public Model.DBManager db;
        public AppointmentForm()
        {
            InitializeComponent();
            cbItems = new ObservableCollection<ComboBoxItem>();
            db = new Model.DBManager();
            FieldList(Application.Current.Properties["resId"].ToString());
            subjectEdit.ItemsSource = cbItems;
            
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            
            ViewModel.MainViewModel.ClassAppointment ca = new ViewModel.MainViewModel.ClassAppointment();
         
            ca.SubjectId = this.subjectEdit.Text;

            ca.Label = 0;
            ca.Status = 2;
            Predmet p = db.FindPredmetByCode(subjectEdit.Text);
            ca.CourseId = p.Course.Code;

            DateTime tempStart = Convert.ToDateTime(edtStartTime.Text);
            DateTime tempEnd = Convert.ToDateTime(edtEndTime.Text);


            string days = this.edtStartDate.Text;
            DateTime myDate = DateTime.ParseExact(days, "MM/dd/yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);

         

            ca.StartTime =  myDate.AddHours(tempStart.Hour).AddMinutes(tempStart.Minute);
            ca.EndTime = myDate.AddHours(tempEnd.Hour).AddMinutes(tempEnd.Minute);
            ca.ClassroomId = this.resourceEdit.Text;
            ca.Description = descriptionEdit.Text;

            temp = myDate.AddHours(tempEnd.Hour).AddMinutes(tempEnd.Minute);

            Application.Current.Properties["temp"] = temp;

            //MessageBox.Show("Appointment successfully added.");

           // ViewModel.MainViewModel.Appointments.Add(ca);



        }



        public void FieldList(string id)
        {
            cbItems.Clear();
            db = new Model.DBManager();
            List<Model.Predmet> subjects = db.SelectPredmet();
            Model.Ucionica c = db.FindUcionicaById(id);
            foreach (Model.Predmet p in subjects)

            {
                if (p.GroupSize <= c.NumberOfSeats)
                {
                    if (p.Projector && !c.Projector)
                    {
                        continue;
                    }
                    if (p.Table && !c.Table)
                    {
                        continue;
                    }
                    if (p.SmartTable && !c.SmartTable)
                    {
                        continue;                       
                    }
                 
                    //provera potrebnih termina 
                    if(CheckNumTerm(p) == true && c.AllSoftware.Any(w => p.AllSoftware.Any(a => a.Code == w.Code)) == true)
                    {
                        cbItems.Add(new ComboBoxItem() { Content = p.Code });

                    }
                }


            }
            this.subjectEdit.ItemsSource = cbItems;
            

        }

        private bool CheckNumTerm(Model.Predmet p)
        {
            int potrebnoTermina = p.NumberOfTerms;
            int dodeljenoTermina = 0;

            foreach(ViewModel.MainViewModel.ClassAppointment ca in ViewModel.MainViewModel.Appointments)
            {
                if (ca.SubjectId.Equals(p.Code))
                {
                    dodeljenoTermina += 1;
                }
            }

            if(dodeljenoTermina < potrebnoTermina)
            {
                return true;
            }

            return false;

        }

        private void resourceEdit_PopupContentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string idx = resourceEdit.Text;
            //string idx = e.AddedItems[0].ToString();
            FieldList(idx);
        }

        private void subjectEdit_PopupContentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            string code = (e.AddedItems[0] as ComboBoxItem).Content as string;  //izabrani predmet
            db = new Model.DBManager();
            Model.Predmet p = db.FindPredmetByCode(code);
            if (edtStartTime.Text == "")
            {
                //MessageBox.Show("Ovde je edtStartTime prazan");
                return;
            }
            DateTime startTime = Convert.ToDateTime(this.edtStartTime.Text);
            if (p.LengthOfTerm > 2)
            {
                this.edtEndTime.EditValue = startTime.AddMinutes(45 * p.LengthOfTerm + 15);
            }
            else
            {
                this.edtEndTime.EditValue = startTime.AddMinutes(45 * p.LengthOfTerm);  //izracunamo end time 
            }

        //    string description = "Subject name: " + p.Name + " \n Course: " + p.Course.Name;
        //    this.descriptionEdit.Text = description;

        }

        
    }

    public class CustomAppointmentFormViewModel : AppointmentFormViewModel
    {


        public static new CustomAppointmentFormViewModel Create(DevExpress.Xpf.Scheduler.SchedulerControl control, DevExpress.XtraScheduler.Appointment apt, bool readOnly, bool showRecurrenceDialog)
        {
            return ViewModelSource.Create(() => new CustomAppointmentFormViewModel(control, apt, readOnly, showRecurrenceDialog));
        }

        public CustomAppointmentFormViewModel(DevExpress.Xpf.Scheduler.SchedulerControl control, DevExpress.XtraScheduler.Appointment apt, bool readOnly, bool showRecurrenceDialog)
            : base(control, apt, readOnly, showRecurrenceDialog)
        {

            Application.Current.Properties["resId"] = apt.ResourceId.ToString();
        }      

        public override void ApplyChanges()
        {
            // implement your custom logic here

            DateTime temp = Convert.ToDateTime(Application.Current.Properties["temp"]);
            End = temp;

            base.ApplyChanges();

            



        }
    }


}
