using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MVVMSchedulerApplication.Model
{
    class Appointment : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private Predmet subject;
        private Ucionica classroom;
        private DateTime start;
        private DateTime end;
        private string scheduler;
        private string courseId;
        private string description;
        private int label;
        private int status;

        public Predmet Subject { get { return subject; } set { subject = value; OnPropertyChanged("Subject"); } }
        public Ucionica Classroom { get { return classroom; } set { classroom = value; OnPropertyChanged("Classroom"); } }
        public DateTime Start { get { return start; } set { start = value; OnPropertyChanged("Start"); } }
        public DateTime End { get { return end; } set { end = value; OnPropertyChanged("End"); } }
        public string Scheduler { get { return scheduler; } set { scheduler = value; OnPropertyChanged("Scheduler"); } }
        public string CourseId { get { return description; } set { description = value; OnPropertyChanged("CourseId"); } }
        public string Description { get { return courseId; } set { courseId = value; OnPropertyChanged("Description"); } }
        public int Label { get { return label; } set { label = value; OnPropertyChanged("Label"); } }
        public int Status { get { return status; } set { status = value; OnPropertyChanged("Status"); } }

    }
}
