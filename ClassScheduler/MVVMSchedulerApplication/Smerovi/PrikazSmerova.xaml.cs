using DevExpress.Xpf.Grid;
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

namespace MVVMSchedulerApplication.Smerovi
{
    /// <summary>
    /// Interaction logic for PrikazSmerova.xaml
    /// </summary>

    public static class Stuff
    {
        public static ObservableCollection<Model.Smer> GetSmerovi()
        {
            ObservableCollection<Model.Smer> stuff = new ObservableCollection<Model.Smer>();
            /*     stuff.Add(new Model.Smer() { Oznaka = "SW", Naziv = "SIIT", Opis = "Softversko inzenjerstvo", DatumUvodjenja = DateTime.Now });
                 stuff.Add(new Model.Smer() { Oznaka = "RA", Naziv = "E2", Opis = "Racunarstvo i automatika", DatumUvodjenja = DateTime.Now });
                 stuff.Add(new Model.Smer() { Oznaka = "E3", Naziv = "PSI", Opis = "Primenjeno softversko inzenjerstvo", DatumUvodjenja = DateTime.Now });
            */
            Model.DBManager db = new Model.DBManager();
            List<Model.Smer> smerovi = db.SelectSmer();
            foreach(Model.Smer s in smerovi)
            {
                stuff.Add(s);
            }
            return stuff;
        }
    }

    public partial class PrikazSmerova : Window
    {

        public PrikazSmerova()
        {
            InitializeComponent();
            gridControl.ItemsSource = Stuff.GetSmerovi();
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
                string id = (string)gridControl.GetCellValue(idx, "Code");
                ObservableCollection<Model.Smer> ucionice = Stuff.GetSmerovi();
                Model.Smer smer = null;
                foreach (Model.Smer s in ucionice)
                {
                    if (s.Code.Equals(id))
                    {
                        smer = s;
                        break;
                    }
                }
                // Treba dodati provere da li moze da se obrise i brisanje u bazi, tView.DeleteRow je samo u tabeli

                tView.DeleteRow(menuInfo.Row.RowHandle.Value);
                //brisanje u bazi
                Model.DBManager db = new Model.DBManager();
                try
                {
                    db.DeleteSmer(smer);
                    MessageBox.Show("the course has been successfully deleted");
                }catch(Exception exp)
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
                string id = (string)gridControl.GetCellValue(idx, "Code");
                ObservableCollection<Model.Smer> ucionice = Stuff.GetSmerovi();
                Model.Smer smer = null;
                foreach (Model.Smer s in ucionice)
                {
                    if (s.Code.Equals(id))
                    {
                        smer = s;
                        break;
                    }
                }

                var w = new IzmenaSmera(smer);
                w.ShowDialog();
                w.Close();
                gridControl.ItemsSource = Stuff.GetSmerovi();
                
            }
        }

        private void TableView_RowDoubleClick(object sender, DevExpress.Xpf.Grid.RowDoubleClickEventArgs e)
        {

            int idx = e.HitInfo.RowHandle;
            string id = (string)gridControl.GetCellValue(idx, "Code");
            ObservableCollection<Model.Smer> smerovi = Stuff.GetSmerovi();
            Model.Smer smer = null;
            foreach (Model.Smer s in smerovi)
            {
                if (s.Code.Equals(id))
                {
                    smer = s;
                    break;
                }
            }

           var w = new IzmenaSmera(smer);
            w.ShowDialog();
            w.Close();
            gridControl.ItemsSource = Stuff.GetSmerovi();

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
