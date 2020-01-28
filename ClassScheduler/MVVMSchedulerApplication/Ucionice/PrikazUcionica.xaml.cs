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

namespace MVVMSchedulerApplication.Ucionice
{
    /// <summary>
    /// Interaction logic for PrikazUcionica.xaml
    /// </summary>
    /// 

    public partial class PrikazUcionica : Window
    {
        public static class Stuff
        {
            public static ObservableCollection<UcioniceDTO> GetUcionice()
            {
                ObservableCollection<UcioniceDTO> stuff = new ObservableCollection<UcioniceDTO>();

                DBManager db = new DBManager();

                List<Ucionica> ucionice = db.SelectUcionica();

                UcioniceDTO udt;
                //List<Model.Softver> softveri = new List<Softver>();

                foreach (Ucionica u in ucionice)
                {
                    udt = new UcioniceDTO()
                    {
                        Code = u.Code,
                        Description = u.Description,
                        NumberOfSeats = u.NumberOfSeats,
                        Projector = u.Projector,
                        Table = u.Table,
                        SmartTable = u.SmartTable,
                        OperationSystem = OSString(u.Os),
                        SoftwareList = new List<string>(),
                        Software = ""
                    };

                    foreach (Softver s in u.AllSoftware)
                    {
                        udt.SoftwareList.Add(s.Code);
                        udt.Software += s.Code;
                        if (u.AllSoftware.IndexOf(s) != u.AllSoftware.Count-1)
                        {
                            udt.Software += ", ";
                        }
                        



                    }

                    stuff.Add(udt);
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
        public PrikazUcionica()
        {
            InitializeComponent();
            gridControl.ItemsSource = Stuff.GetUcionice();
            foreach (ColumnBase col in gridControl.Columns)
            {
                if (col.FieldName.Equals("SoftwareList"))
                {
                    col.Visible = false;
                    break;
                }
            }
        }


        private void deleteRowItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GridCellMenuInfo menuInfo = tView.GridMenu.MenuInfo as GridCellMenuInfo;
            if (menuInfo != null && menuInfo.Row != null)
            {
                int idx = menuInfo.Row.RowHandle.Value;
                string id = (string)gridControl.GetCellValue(idx, "Code");
                ObservableCollection<UcioniceDTO> ucionice = Stuff.GetUcionice();
                UcioniceDTO udt = null;
                foreach (UcioniceDTO u in ucionice)
                {
                    if (u.Code.Equals(id))
                    {
                        udt = u;
                        break;
                    }
                }

                // udt je ucionica za brisanje
                // Treba dodati provere da li moze da se obrise i brisanje u bazi, tView.DeleteRow je samo u tabeli

                Model.DBManager db = new Model.DBManager();
                try
                {
                    db.DeleteUcionica(udt);
                    MessageBox.Show("Classroom has been successfull deleted.");
                    ViewModel.MainViewModel.Classrooms.Clear();
                    ViewModel.MainViewModel.FillClassrooms();
                
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }

                tView.DeleteRow(menuInfo.Row.RowHandle.Value);




            }
        }

        private void editRowItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

            GridCellMenuInfo menuInfo = tView.GridMenu.MenuInfo as GridCellMenuInfo;
            if (menuInfo != null && menuInfo.Row != null)
            {
                int idx = menuInfo.Row.RowHandle.Value;
                string id = (string)gridControl.GetCellValue(idx, "Code");
                ObservableCollection<UcioniceDTO> ucionice = Stuff.GetUcionice();
                UcioniceDTO udt = null;
                foreach (UcioniceDTO u in ucionice)
                {
                    if (u.Code.Equals(id))
                    {
                        udt = u;
                        break;
                    }
                }

                var w = new IzmenaUcionice(udt);
                w.ShowDialog();
               
                gridControl.ItemsSource = Stuff.GetUcionice();

            }
        }

        private void copyCellValue_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GridCellMenuInfo menuInfo = tView.GridMenu.MenuInfo as GridCellMenuInfo;
            if (menuInfo != null && menuInfo.Row != null)
            {
                string text = "" +
                    gridControl.GetCellValue(menuInfo.Row.RowHandle.Value, menuInfo.Column as GridColumn).ToString();
                Clipboard.SetText(text);
            }
        }

        private void tView_RowDoubleClick(object sender, RowDoubleClickEventArgs e)
        {
            int idx = e.HitInfo.RowHandle;
            string id = (string)gridControl.GetCellValue(idx, "Code");
            ObservableCollection<UcioniceDTO> ucionice = Stuff.GetUcionice();
            UcioniceDTO udt = null;
            foreach (UcioniceDTO u in ucionice)
            {
                if (u.Code.Equals(id))
                {
                    udt = u;
                    break;
                }
            }

            var w = new IzmenaUcionice(udt);
            w.ShowDialog();
           
            gridControl.ItemsSource = Stuff.GetUcionice();

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
