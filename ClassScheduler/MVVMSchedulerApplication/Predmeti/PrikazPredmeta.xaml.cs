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
using DevExpress.Xpf.Grid;

namespace MVVMSchedulerApplication.Predmeti
{
    /// <summary>
    /// Interaction logic for PrikazPredmeta.xaml
    /// </summary>
    public partial class PrikazPredmeta : Window
    {
        public static class Stuff
        {
            public static ObservableCollection<PredmetDTO> GetPredmeti()
            {
                ObservableCollection<PredmetDTO> stuff = new ObservableCollection<PredmetDTO>();
                Model.DBManager db = new Model.DBManager();

                List<Model.Predmet> predmeti = db.SelectPredmet();
                List<Model.Softver> softveri = new List<Softver>();

                PredmetDTO pd;
                foreach (Model.Predmet p in predmeti)
                {
                    pd = new PredmetDTO()
                    {
                        Code = p.Code,
                        LengthOfTerm = p.LengthOfTerm,
                        Name = p.Name,
                        Description = p.Description,
                        NumberOfTerms = p.NumberOfTerms,
                        SmartTable = p.SmartTable,
                        Projector = p.Projector,
                        Table = p.Table,
                        OperationSystem = OSString(p.Os),
                        Course = p.Course.Code,
                        GroupSize = p.GroupSize
                    };

                    softveri = p.AllSoftware;
                    pd.SoftwareList = new List<string>();
                   
                    foreach (Softver s in softveri)
                    {
                        pd.SoftwareList.Add(s.Code);
                        pd.Software += s.Code;
                        if (p.AllSoftware.IndexOf(s) != p.AllSoftware.Count - 1)
                        {
                            pd.Software += ", ";
                        }

                    }
                    

                    stuff.Add(pd);
                }
                return stuff;
            }

            private static string OSString(Enums.OS os)
            {
                if (os == Enums.OS.Windows)
                {
                    return "Windows";
                }
                else if (os == Enums.OS.Linux)
                {
                    return "Linux";
                }
                else if (os == Enums.OS.CrossPlatform)
                {
                    return "Cross platform";
                }
                else if (os == Enums.OS.Both)
                {
                    return "Windows, Linux";
                }
                else
                {
                    return "Any";
                }
            }
        }

        public PrikazPredmeta()
        {
            InitializeComponent();
            gridControl1.ItemsSource = Stuff.GetPredmeti();

        }

        private void copyCellValue_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void deleteRowItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GridCellMenuInfo menuInfo = tView.GridMenu.MenuInfo as GridCellMenuInfo;
            if (menuInfo != null && menuInfo.Row != null)
            {
                int idx = menuInfo.Row.RowHandle.Value;
                string id = (string)gridControl1.GetCellValue(idx, "Code");
                ObservableCollection<PredmetDTO> predmeti = Stuff.GetPredmeti();
                PredmetDTO predmet = null;
                foreach (PredmetDTO p in predmeti)
                {
                    if (p.Code.Equals(id))
                    {
                        predmet = p;
                        break;
                    }
                }
                // Treba dodati provere da li moze da se obrise i brisanje u bazi, tView.DeleteRow je samo u tabeli

                tView.DeleteRow(menuInfo.Row.RowHandle.Value);
                //brisanje u bazi
                Model.DBManager db = new Model.DBManager();
                try
                {
                    db.DeletePredmet(predmet);
                    
                    MessageBox.Show("The subject has been successfully deleted");
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }


            }
        }

        private void editRowItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GridCellMenuInfo menuInfo = tView.GridMenu.MenuInfo as GridCellMenuInfo;
            if (menuInfo != null && menuInfo.Row != null)
            {
                int idx = menuInfo.Row.RowHandle.Value;
                string id = (string)gridControl1.GetCellValue(idx, "Code");
                ObservableCollection<PredmetDTO> predmeti = Stuff.GetPredmeti();
                PredmetDTO predmet = null;
                foreach (PredmetDTO p in predmeti)
                {
                    if (p.Code.Equals(id))
                    {
                        predmet = p;
                        break;
                    }
                }

                var w = new IzmenaPredmeta(predmet);
                w.ShowDialog();
                w.Close();
                gridControl1.ItemsSource = Stuff.GetPredmeti();


            }

        }



        private void tView_RowDoubleClick(object sender, DevExpress.Xpf.Grid.RowDoubleClickEventArgs e)
        {

            int idx = e.HitInfo.RowHandle;
            string id = (string)gridControl1.GetCellValue(idx, "Code");
            ObservableCollection<PredmetDTO> predmeti = Stuff.GetPredmeti();
            PredmetDTO predmet = null;
            foreach (PredmetDTO p in predmeti)
            {
                if (p.Code.Equals(id))
                {
                    predmet = p;
                    break;
                }
            }

            var w = new IzmenaPredmeta(predmet);
            w.ShowDialog();
            w.Close();
            gridControl1.ItemsSource = Stuff.GetPredmeti();
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
