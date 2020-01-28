using DevExpress.Mvvm.POCO;
using DevExpress.Schedule;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Scheduler.Menu;
using DevExpress.Xpf.Scheduler.UI;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraScheduler.Services;
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
using static MVVMSchedulerApplication.AppointmentForm;
using static MVVMSchedulerApplication.ViewModel.MainViewModel;

namespace MVVMSchedulerApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
   
    

    public partial class MainWindow : Window
    {
        private Model.DBManager db;
        public ObservableCollection<ListBoxItem> cbItems { get; set; }
        

        public MainWindow()
        {
            InitializeComponent();

            Scheduler.OptionsCustomization.AllowAppointmentConflicts = AppointmentConflictsMode.Forbidden;
            Scheduler.OptionsCustomization.AllowAppointmentResize = UsedAppointmentType.None;
            Scheduler.OptionsCustomization.AllowInplaceEditor = UsedAppointmentType.None;

            Scheduler.WorkDays.BeginUpdate();
            Scheduler.WorkDays.Clear();
            Scheduler.WorkDays.Add(WeekDays.WorkDays | WeekDays.Saturday);
            Scheduler.WorkDays.EndUpdate();

            Scheduler.TimeRulerMenuCustomizations.Add(new RemoveBarItemAndLinkAction()
            {
                ItemName = SchedulerMenuItemName.NewAllDayEvent
            });

            Scheduler.TimeRulerMenuCustomizations.Add(new RemoveBarItemAndLinkAction()
            {
                ItemName = SchedulerMenuItemName.CustomizeTimeRuler
            });

            Scheduler.TimeRulerMenuCustomizations.Add(new RemoveBarItemAndLinkAction()
            {
                ItemName = SchedulerMenuItemName.SwitchViewMenu
            });

            Scheduler.TimeRulerMenuCustomizations.Add(new RemoveBarItemAndLinkAction()
            {
                ItemName = SchedulerMenuItemName.NewAppointment
            });


            ResourceControler.SchedulerControl = Scheduler;

            Application.Current.Properties["subDict"] = new Dictionary<string, int>();
            db = new Model.DBManager();

            UpdateSubjectsDictionaryAndTerms();
        }

        

        private void Scheduler_QueryWorkTime(object sender, QueryWorkTimeEventArgs e)
        {
            e.WorkTime = new TimeOfDayInterval(TimeSpan.FromHours(07), TimeSpan.FromHours(22));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var w = new Predmeti.PrikazPredmeta();
            w.Show();
            w.Closing += UpdatingHandler;
        }

        private void MenuItem_Click1(object sender, RoutedEventArgs e)
        {
            var w = new Smerovi.PrikazSmerova();
            w.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var w = new Ucionice.PrikazUcionica();
            w.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var w = new Softveri.PrikazSoftvera();
            w.Show();
        }


        //za unos smera
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            var w = new Smerovi.DodavanjeSmera();
            w.ShowDialog();
            
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            var w = new Softveri.DodavanjeSoftvera();
            w.ShowDialog();
            
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            var w = new Ucionice.DodavanjeUcionica();
            w.ShowDialog();
            
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            var w = new Predmeti.DodavanjePredmeta();
            w.ShowDialog();
            w.Closing += UpdatingHandler;
            
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure? This operation can not be undone.", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (res == MessageBoxResult.OK)
            {
                db.ClearDatabase();
                MessageBox.Show("Database cleared.");
                ViewModel.MainViewModel.Classrooms.Clear();
                UpdateSubjectsDictionaryAndTerms();
            }
            

        }

        private bool MeetsConditions(string classroomId, string subjectId)
        {
            Ucionica c = db.FindUcionicaById(classroomId);
            Predmet p = db.FindPredmetByCode(subjectId);

            if (p.GroupSize <= c.NumberOfSeats)
            {
                if (p.Projector && !c.Projector)
                {
                    return false;
                }
                if (p.Table && !c.Table)
                {
                    return false;
                }
                if (p.SmartTable && !c.SmartTable)
                {
                    return false;
                }

                if (c.AllSoftware.Any(w => p.AllSoftware.Any(a => a.Code == w.Code)) == true)
                {
                    return true;
                } else
                {
                    return false;
                }

            }
            else
            {
                return false;

            }
        }

        private void Scheduler_AppointmentDrag(object sender, AppointmentDragEventArgs e)
        {
            string classroomId = e.HitResource.Id.ToString();
            string subjectId = e.SourceAppointment.Subject;

            if (MeetsConditions(classroomId, subjectId))
            {
                e.Allow = true;
            } else
            {
                e.Allow = false;
            }
        }

        private void Scheduler_AllowInplaceEditor(object sender, AppointmentOperationEventArgs e)
        {
            e.Allow = false;
        }

        private void Scheduler_ActiveViewChanged(object sender, EventArgs e)
        {
            if (Scheduler.ActiveViewType == SchedulerViewType.WorkWeek)
            {
                t1.Visibility = Visibility.Visible;
                
                daysToDisplay.Visibility = Visibility.Visible;

            } else
            {
                t1.Visibility = Visibility.Collapsed;
                
                daysToDisplay.Visibility = Visibility.Collapsed;
            }
        }

        private void dayCountEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);

            Scheduler.DayView.DayCount = int.Parse(lbi.Content.ToString());

            
        }

        private void daysToDisplay_PopupOpened(object sender, RoutedEventArgs e)
        {
            Button okBtn = daysToDisplay.GetOkButton();
            if (okBtn != null)
                okBtn.Click += new RoutedEventHandler(okBtn_Click);
        }

        void okBtn_Click(object sender, RoutedEventArgs e)
        {
            Scheduler.WorkDays.BeginUpdate();
            Scheduler.WorkDays.Clear();
            foreach(ComboBoxEditItem item in daysToDisplay.Items)
            {
                if(item.IsSelected)
                {
                    switch (item.Content.ToString())
                    {
                        case "Monday":
                            Scheduler.WorkDays.Add(WeekDays.Monday);
                            break;

                        case "Tuesday":
                            Scheduler.WorkDays.Add(WeekDays.Tuesday);
                            break;

                        case "Wednesday":
                            Scheduler.WorkDays.Add(WeekDays.Wednesday);
                            break;

                        case "Thursday":
                            Scheduler.WorkDays.Add(WeekDays.Thursday);
                            break;

                        case "Friday":
                            Scheduler.WorkDays.Add(WeekDays.Friday);
                            break;

                        case "Saturday":
                            Scheduler.WorkDays.Add(WeekDays.Saturday);
                            break;

                        default:
                            break;
                    }
                }
            }
            if(Scheduler.WorkDays.Count == 0)
            {
                Scheduler.WorkDays.Add(WeekDays.WorkDays | WeekDays.Saturday);
            }

            Scheduler.WorkDays.EndUpdate();
        }

        private void daysToDisplay_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            Debug.WriteLine("Display text: " + e.DisplayText);
            e.DisplayText = "";
            e.EditValue = null;
        }

        private void Scheduler_AllowAppointmentResize(object sender, AppointmentOperationEventArgs e)
        {
            e.Allow = false;
        }



        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            //otvaranje izabranog rasporeda 
            var w = new OpenWindow();
            w.Show();
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            //cuvanje rasporeda u bazi podataka 
            var w = new SaveScheduler();
            w.Show();
        }

     
        

        private void Scheduler_EditAppointmentFormShowing(object sender, DevExpress.Xpf.Scheduler.EditAppointmentFormEventArgs e)
        {
            e.ViewModel = CustomAppointmentFormViewModel.Create(sender as DevExpress.Xpf.Scheduler.SchedulerControl, e.Appointment, e.ReadOnly, e.OpenRecurrenceDialog);
        }

        public void UpdateAppointmentStatus()
        {
            foreach(ClassAppointment apt in Appointments)
            {
                if (MeetsConditions(apt.ClassroomId.ToString(), apt.SubjectId))
                {
                    apt.Label = 0;
                } else
                {
                    apt.Label = 1;
                }
            }
        }

        public void UpdateSubjectsDictionaryAndTerms()
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            List<Predmet> subs = new List<Predmet>();

            try
            {
                subs = db.SelectPredmet();
            } catch (NullReferenceException)
            {
                //MessageBox.Show("Error while loading subjects!\n" + e.ToString());
            }

            if (subs == null || subs.Count == 0)
            {
                //numberPanel.Visibility = Visibility.Collapsed;
                return;
            }
            

            foreach (Predmet p in subs)
            {
                temp[p.Code] = 0;
                
            }

            foreach (ClassAppointment apt in Appointments)
            {
                temp[apt.SubjectId] += 1;
            }

            Application.Current.Properties["subDict"] = temp;
            
            bool allFine = true;

            numberPanelitems.Children.Clear();            
            
            foreach (KeyValuePair<string, int> pair in temp)
            {
                foreach (Predmet p in subs)
                {

                    if (p.Code.Equals(pair.Key) && pair.Value < p.NumberOfTerms)
                    {

                        numberPanelitems.Children.Add(new TextBlock()
                        {
                            Text = pair.Key + ": " + pair.Value + "/" + p.NumberOfTerms,
                            Foreground = Brushes.Red,
                            FontSize = 15,
                            TextAlignment = TextAlignment.Center
                        });

                        allFine = false;

                        break;
                    }
                }
            }

            if (!allFine)
            {
                numberPanel.Visibility = Visibility.Visible;
            }
            else
            {
                numberPanel.Visibility = Visibility.Collapsed;
            }

            UpdateAppointmentStatus();
        }

        public void UpdateSubjectsTerms()
        {
            bool allFine = true;

            numberPanelitems.Children.Clear();
            List<Predmet> subs = db.SelectPredmet();
            Dictionary<string, int> temp = (Dictionary<string, int>)Application.Current.Properties["subDict"];
            foreach (KeyValuePair<string, int> pair in temp)
            {
                foreach(Predmet p in subs)
                {
                    
                    if (p.Code.Equals(pair.Key) && pair.Value < p.NumberOfTerms)
                    {

                        numberPanelitems.Children.Add(new TextBlock() {
                            Text = pair.Key + ": " + pair.Value + "/" + p.NumberOfTerms,
                            Foreground = Brushes.Red,
                            FontSize = 15,
                            TextAlignment = TextAlignment.Center
                        });

                        allFine = false;

                        break;
                    }
                }
            }

            if(!allFine)
            {
                numberPanel.Visibility = Visibility.Visible;
            } else
            {
                numberPanel.Visibility = Visibility.Collapsed;
            }

            UpdateAppointmentStatus();

        }

        private void Scheduler_PopupMenuShowing(object sender, DevExpress.Xpf.Scheduler.SchedulerMenuEventArgs e)
        {
            // Check whether this event was raised for a default popup menu of the Scheduler control.
            if (e.Menu.Name == SchedulerMenuItemName.DefaultMenu)
            {
                for (int i = 0; i < e.Menu.Items.Count; i++)
                {
                    SchedulerBarItem menuItem = e.Menu.Items[i] as SchedulerBarItem;
                    if (menuItem != null)
                    {                        
                        if (menuItem.Name == SchedulerMenuItemName.NewAllDayEvent || 
                            menuItem.Name == SchedulerMenuItemName.GotoDate ||
                            menuItem.Name == SchedulerMenuItemName.SwitchToTimelineView ||
                            menuItem.Name == SchedulerMenuItemName.SwitchToMonthView ||
                            menuItem.Name == SchedulerMenuItemName.SwitchToWeekView
                            )
                        {
                            menuItem.IsVisible = false;                     
                        }
                    }

                    
                    BarSubItem subMenu = e.Menu.Items[i] as BarSubItem;
                    if (subMenu != null)
                    {   
                                             
                        for (int j = 0; j < subMenu.Items.Count; j++)
                        {
                            SchedulerBarCheckItem subMenuItem = subMenu.Items[j] as SchedulerBarCheckItem;
                            if (subMenuItem != null)
                            {
                                if (subMenuItem.Name != SchedulerMenuItemName.SwitchToWorkWeekView &&
                                    subMenuItem.Name != SchedulerMenuItemName.SwitchToDayView
                                    )
                                {
                                    subMenuItem.IsVisible = false;
                                    //subMenu.Items.RemoveAt(j);
                                }
                            }
                                
                        }
                    }
                   
                }

                // Remove the New Recurring Event menu item using the Action approach.
                //e.Customizations.Add(new RemoveBarItemAndLinkAction()
                //{
                //    ItemName = SchedulerMenuItemName.NewRecurringEvent
                //});

                // Create a custom menu item.
                //BarButtonItem myMenuItem = new BarButtonItem();
                //myMenuItem.Name = "customItem";
                //myMenuItem.Content = "Item Added at Runtime";
                //myMenuItem.ItemClick += new ItemClickEventHandler(customItem_ItemClick);

                // Insert a menu separator.
                //e.Customizations.Add(new BarItemLinkSeparator());
                // Insert a new item into the Scheduler popup menu.

                //e.Customizations.Add(myMenuItem);
            }
        }

        private void UpdatingHandler(object sender, EventArgs e)
        {
            UpdateSubjectsDictionaryAndTerms();
            UpdateAppointmentStatus();
            termsCount.Text = Appointments.Count.ToString();
        }

       
       
        

        private void Scheduler_AppointmentDrop(object sender, AppointmentDragEventArgs e)
        {
            UpdateAppointmentStatus();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }

    public class DateTimeToShortDateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            DateTime dateTimeValue = (DateTime)value;

            string param = parameter != null ? parameter.ToString() : string.Empty;
            if (param == string.Empty)
                param = "MM/dd";

            return dateTimeValue.ToString(param);
        }
        public object ConvertBack(object value, Type targetType,
                                  object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }


        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, (Window)sender);
            }

        }
    }

   
}
