#region #usings
using System;
using System.Collections.Generic;
using System.ComponentModel;

#endregion #usings

namespace MVVMSchedulerApplication.ViewModel
{
    #region #mybindinglist
    partial class MainViewModel
    {
        public static BindingList<Classroom> Classrooms { get; set; }
        public static BindingList<ClassAppointment> Appointments { get; set; }

        public MainViewModel() 
        {
            Classrooms = new BindingList<Classroom>();
            Appointments = new BindingList<ClassAppointment>();
            FillClassrooms();
           // FillTasks();
        }

        public class Classroom
        {
            public object Id { get; set; }
            public string Description { get; set; }
            public bool Projector { get; set; }
            public bool Table { get; set; }
            public bool SmartTable { get; set; }
            public string OperativeSystem { get; set; }
            public object SoftwareId { get; set; }
            public int NumberOfSeats { get; set; }
        }

        public class ClassAppointment
        {
            public string SubjectId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public object ClassroomId { get; set; }
            public string Scheduler { get; set; }
            public string CourseId { get; set; }
            public string Description { get; set; }
            public int Label { get; set; }
            public int Status { get; set; }
        }
    }
    #endregion #mybindinglist

}
