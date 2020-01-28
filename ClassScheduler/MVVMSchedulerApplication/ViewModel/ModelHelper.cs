using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMSchedulerApplication.Model;
using System.Windows;

namespace MVVMSchedulerApplication.ViewModel
{
    partial class MainViewModel
    {
        public static void FillClassrooms()
        {
            Model.DBManager db = new Model.DBManager();
            List<Model.Ucionica> sveUcionice = db.SelectUcionica();
            string os;
            foreach (Model.Ucionica u in sveUcionice)
            {
                os = EnumToString(u.Os);

                Classroom classroom = new Classroom()
                {
                    Id = u.Code,
                    Description = u.Description,
                    Projector = u.Projector,
                    Table = u.Table,
                    SmartTable = u.SmartTable,
                    NumberOfSeats = u.NumberOfSeats,
                    OperativeSystem = os,
                };

                string soft = "";
                foreach (Softver s in u.AllSoftware)
                {
                    soft += s.Name + " , ";
                }
                classroom.SoftwareId = soft;

                Classrooms.Add(classroom);
            }


        }

        public static string EnumToString(Enums.OS os)
        {
            string operationSystem = "";
            if (os == Model.Enums.OS.Windows)
            {
                operationSystem = "Windows";
            }
            else if (os == Model.Enums.OS.Linux)
            {
                operationSystem = "Linux";
            }
            else if (os == Model.Enums.OS.CrossPlatform)
            {
                operationSystem = "Cross Platform";
            }
            else if (os == Model.Enums.OS.Both)
            {
                operationSystem = "Both";
            }
            else if (os == Model.Enums.OS.Any)
            {
                operationSystem = "Any";
            }

            return operationSystem;
        }

        public static void FillTask(ClassAppointment ca)
        {
            Appointments.Add(new ClassAppointment() { StartTime = ca.StartTime, EndTime = ca.EndTime, ClassroomId = ca.ClassroomId, SubjectId = ca.SubjectId });

        }

        public static void SaveAppointments(string scheduler)
        {
            DBManager db = new DBManager();

            List<string> names = db.SelectTimetableName();
            if (names.Contains(scheduler))
            {
                try
                {
                    db.DeleteAppByScheduler(scheduler);
                }catch(Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }

            try
            {
                foreach(ClassAppointment ca in Appointments)
                {
                    db.InsertAppointment(ca, scheduler);
                }
                MessageBox.Show("Scheduler has been successfull saved.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void FillTasks(string scheduler)
        {
            Appointments.Clear();

            DBManager db = new DBManager();
            try
            {
                List<ClassAppointment> app = db.SelectAppointmentByScheduler(scheduler);
                foreach(ClassAppointment ca in app)
                {
                    Appointments.Add(ca);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            /*
            Appointments.Add(new ClassAppointment() { StartTime = DateTime.Now.Date.AddHours(10), EndTime = DateTime.Now.Date.AddHours(11), ClassroomId = "L1", SubjectId = "HCI" });
            Appointments.Add(new ClassAppointment() { StartTime = DateTime.Now.Date.AddHours(9), EndTime = DateTime.Now.Date.AddHours(11), ClassroomId = "L2", SubjectId = "ORI" });
            Appointments.Add(new ClassAppointment() { StartTime = DateTime.Now.Date.AddHours(12), EndTime = DateTime.Now.Date.AddHours(15), ClassroomId = "L3", SubjectId = "PIGKUT" });
            Appointments.Add(new ClassAppointment() { StartTime = DateTime.Now.Date.AddHours(18), EndTime = DateTime.Now.Date.AddHours(20), ClassroomId = "L4", SubjectId = "MRS" });
            Appointments.Add(new ClassAppointment() { StartTime = DateTime.Now.Date.AddHours(13), EndTime = DateTime.Now.Date.AddHours(16), ClassroomId = "L1", SubjectId = "ISA" });*/
        }
    }
}
