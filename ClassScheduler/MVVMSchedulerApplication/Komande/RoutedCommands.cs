using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMSchedulerApplication.Komande
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand SaveScheduler = new RoutedUICommand(
            "Save Scheduler",
            "SaveScheduler",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S, ModifierKeys.Control)
            });

        public static readonly RoutedUICommand OpenScheduler = new RoutedUICommand(
            "Open Scheduler",
            "OpenScheduler",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.O, ModifierKeys.Control)
            });

        #region subject
        public static readonly RoutedUICommand AddSubject = new RoutedUICommand(
            "Add Subject",
            "AddSubject",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.P, ModifierKeys.Alt)
            });
        public static readonly RoutedUICommand ViewSubject = new RoutedUICommand(
            "View Subject",
            "ViewSubject",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.P, ModifierKeys.Control)
            });
        #endregion

        #region ucionice
        public static readonly RoutedUICommand AddClassroom = new RoutedUICommand(
            "Add Classroom",
            "AddClassroom",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.R, ModifierKeys.Alt)
            });
        public static readonly RoutedUICommand ViewClassroom = new RoutedUICommand(
            "View Classroom",
            "ViewClassroom",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.R, ModifierKeys.Control)
            });
        #endregion

#region softver
      
        public static readonly RoutedUICommand AddSoftware = new RoutedUICommand(
            "Add Software",
            "AddSoftware",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.W, ModifierKeys.Alt)
            });
        public static readonly RoutedUICommand ViewSoftware = new RoutedUICommand(
            "View Software",
            "ViewSoftware",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.W, ModifierKeys.Control)
            });
        #endregion

        #region kurs
        public static readonly RoutedUICommand AddCourse = new RoutedUICommand(
            "Add Course",
            "AddCourse",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.K, ModifierKeys.Alt)
            });
        public static readonly RoutedUICommand ViewCourse = new RoutedUICommand(
            "View Course",
            "ViewCourse",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.K, ModifierKeys.Control)
            });
        #endregion

        

    }
}
