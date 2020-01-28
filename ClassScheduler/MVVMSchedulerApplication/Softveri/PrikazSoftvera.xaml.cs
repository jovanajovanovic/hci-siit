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

namespace MVVMSchedulerApplication.Softveri
{
    /// <summary>
    /// Interaction logic for PrikazSoftvera.xaml
    /// </summary>
    /// 
    public static class Stuff
    {
        public static ObservableCollection<SoftverDTO> GetSoftveri()
        {
            ObservableCollection<SoftverDTO> stuff = new ObservableCollection<SoftverDTO>();

            Model.DBManager db = new Model.DBManager();

            List<Model.Softver> softveri = db.SelectSoftver();

            SoftverDTO sdt;

            foreach (Model.Softver u in softveri)
            {
                sdt = new SoftverDTO()
                {
                    Code = u.Code,
                    Description = u.Description,
                    OperationSystem = OSString(u.OperationSystem),
                    Name = u.Name,
                    Manufacturer = u.Manufacturer,
                    Price = u.Price,
                    Website = u.Website,
                    YearOfPublication = u.YearOfPublication
                };
                stuff.Add(sdt);
            }

            return stuff;
        }

        private static string OSString(Model.Enums.OS os)
        {
            if (os == Model.Enums.OS.Windows)
            {
                return "Windows";
            }
            else if (os == Model.Enums.OS.Linux)
            {
                return "Linux";
            }
            else if (os == Model.Enums.OS.CrossPlatform)
            {
                return "Cross platform";
            }
            else if (os == Model.Enums.OS.Both)
            {
                return "Windows, Linux";
            }
            else
            {
                return "Any";
            }
        }
    }
    public partial class PrikazSoftvera : Window
    {
        public PrikazSoftvera()
        {
            InitializeComponent();
            gridControl.ItemsSource = Stuff.GetSoftveri();
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
                ObservableCollection<SoftverDTO> softveri = Stuff.GetSoftveri();
                SoftverDTO softver = null;
                foreach (SoftverDTO s in softveri)
                {
                    if (s.Code.Equals(id))
                    {
                        softver = s;
                        break;
                    }
                }
                // Treba dodati provere da li moze da se obrise i brisanje u bazi, tView.DeleteRow je samo u tabeli

                tView.DeleteRow(menuInfo.Row.RowHandle.Value);
                //brisanje u bazi
                Model.DBManager db = new Model.DBManager();
                try
                {
                    db.DeleteSoftver(softver);
                    MessageBox.Show("The software has been successfull delete");
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
                string id = (string)gridControl.GetCellValue(idx, "Code");
                ObservableCollection<SoftverDTO> ucionice = Stuff.GetSoftveri();
                SoftverDTO softver = null;
                foreach (SoftverDTO s in ucionice)
                {
                    if (s.Code.Equals(id))
                    {
                        softver = s;
                        break;
                    }
                }

                var w = new IzmenaSoftvera(softver);
                w.ShowDialog();
                w.Close();
                gridControl.ItemsSource = Stuff.GetSoftveri();

            }

        }

        private void tView_RowDoubleClick(object sender, DevExpress.Xpf.Grid.RowDoubleClickEventArgs e)
        {
            int idx = e.HitInfo.RowHandle;
            string id = (string)gridControl.GetCellValue(idx, "Code");
            ObservableCollection<SoftverDTO> smerovi = Stuff.GetSoftveri();
            SoftverDTO softver = null;
            foreach (SoftverDTO s in smerovi)
            {
                if (s.Code.Equals(id))
                {
                    softver = s;
                    break;
                }
            }

            var w = new IzmenaSoftvera(softver);
            w.ShowDialog();
            w.Close();
            gridControl.ItemsSource = Stuff.GetSoftveri();

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
