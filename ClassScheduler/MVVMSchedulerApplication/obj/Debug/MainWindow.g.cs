﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1CBD6ECF13405FB2EDAD746FB0409487E7F3DEFE6D6333EFFB2D444E0307BB13"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Core;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.DataSources;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Core.ServerMode;
using DevExpress.Xpf.DXBinding;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.DataPager;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Editors.Flyout;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Editors.Popups.Calendar;
using DevExpress.Xpf.Editors.RangeControl;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Settings.Extension;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.Scheduler;
using DevExpress.Xpf.Scheduler.Commands;
using DevExpress.Xpf.Scheduler.Drawing;
using DevExpress.Xpf.Scheduler.Reporting;
using DevExpress.Xpf.Scheduler.ThemeKeys;
using DevExpress.Xpf.Scheduler.UI;
using DevExpress.Xpf.Scheduling.Panels;
using DevExpress.Xpf.Scheduling.Visual;
using MVVMSchedulerApplication;
using MVVMSchedulerApplication.Komande;
using MVVMSchedulerApplication.ViewModel;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MVVMSchedulerApplication {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MVVMSchedulerApplication.MainWindow window1;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock termsCount;
        
        #line default
        #line hidden
        
        
        #line 165 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock t1;
        
        #line default
        #line hidden
        
        
        #line 166 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ComboBoxEdit daysToDisplay;
        
        #line default
        #line hidden
        
        
        #line 187 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Scheduler.UI.ResourcesCheckedListBoxControl ResourceControler;
        
        #line default
        #line hidden
        
        
        #line 192 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel numberPanel;
        
        #line default
        #line hidden
        
        
        #line 194 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock t3;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock t4;
        
        #line default
        #line hidden
        
        
        #line 199 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel numberPanelitems;
        
        #line default
        #line hidden
        
        
        #line 213 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Scheduler.SchedulerControl Scheduler;
        
        #line default
        #line hidden
        
        
        #line 270 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Scheduler.ResourceStorage storage;
        
        #line default
        #line hidden
        
        
        #line 272 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Scheduler.ResourceMapping mapping;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MVVMSchedulerApplication;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.window1 = ((MVVMSchedulerApplication.MainWindow)(target));
            return;
            case 2:
            
            #line 81 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.CommandBinding_Executed);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 83 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.MenuItem_Click_9);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 84 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.MenuItem_Click_8);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 86 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.MenuItem_Click_6);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 87 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 89 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.MenuItem_Click_5);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 90 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.MenuItem_Click_1);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 92 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.MenuItem_Click_4);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 93 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.MenuItem_Click_2);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 95 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.MenuItem_Click_3);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 96 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.MenuItem_Click1);
            
            #line default
            #line hidden
            return;
            case 13:
            this.termsCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.t1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 15:
            this.daysToDisplay = ((DevExpress.Xpf.Editors.ComboBoxEdit)(target));
            
            #line 166 "..\..\MainWindow.xaml"
            this.daysToDisplay.PopupOpened += new System.Windows.RoutedEventHandler(this.daysToDisplay_PopupOpened);
            
            #line default
            #line hidden
            
            #line 166 "..\..\MainWindow.xaml"
            this.daysToDisplay.CustomDisplayText += new DevExpress.Xpf.Editors.CustomDisplayTextEventHandler(this.daysToDisplay_CustomDisplayText);
            
            #line default
            #line hidden
            return;
            case 16:
            this.ResourceControler = ((DevExpress.Xpf.Scheduler.UI.ResourcesCheckedListBoxControl)(target));
            return;
            case 17:
            this.numberPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 18:
            this.t3 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 19:
            this.t4 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 20:
            this.numberPanelitems = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 21:
            this.Scheduler = ((DevExpress.Xpf.Scheduler.SchedulerControl)(target));
            
            #line 219 "..\..\MainWindow.xaml"
            this.Scheduler.QueryWorkTime += new DevExpress.XtraScheduler.QueryWorkTimeEventHandler(this.Scheduler_QueryWorkTime);
            
            #line default
            #line hidden
            
            #line 220 "..\..\MainWindow.xaml"
            this.Scheduler.AppointmentDrag += new DevExpress.XtraScheduler.AppointmentDragEventHandler(this.Scheduler_AppointmentDrag);
            
            #line default
            #line hidden
            
            #line 221 "..\..\MainWindow.xaml"
            this.Scheduler.AllowInplaceEditor += new DevExpress.XtraScheduler.AppointmentOperationEventHandler(this.Scheduler_AllowInplaceEditor);
            
            #line default
            #line hidden
            
            #line 222 "..\..\MainWindow.xaml"
            this.Scheduler.ActiveViewChanged += new System.EventHandler(this.Scheduler_ActiveViewChanged);
            
            #line default
            #line hidden
            
            #line 223 "..\..\MainWindow.xaml"
            this.Scheduler.AllowAppointmentResize += new DevExpress.XtraScheduler.AppointmentOperationEventHandler(this.Scheduler_AllowAppointmentResize);
            
            #line default
            #line hidden
            
            #line 224 "..\..\MainWindow.xaml"
            this.Scheduler.EditAppointmentFormShowing += new DevExpress.Xpf.Scheduler.AppointmentFormEventHandler(this.Scheduler_EditAppointmentFormShowing);
            
            #line default
            #line hidden
            
            #line 225 "..\..\MainWindow.xaml"
            this.Scheduler.PopupMenuShowing += new DevExpress.Xpf.Scheduler.SchedulerMenuEventHandler(this.Scheduler_PopupMenuShowing);
            
            #line default
            #line hidden
            
            #line 226 "..\..\MainWindow.xaml"
            this.Scheduler.AppointmentDrop += new DevExpress.XtraScheduler.AppointmentDragEventHandler(this.Scheduler_AppointmentDrop);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 248 "..\..\MainWindow.xaml"
            ((DevExpress.Xpf.Scheduler.SchedulerStorage)(target)).AppointmentsInserted += new DevExpress.XtraScheduler.PersistentObjectsEventHandler(this.UpdatingHandler);
            
            #line default
            #line hidden
            
            #line 249 "..\..\MainWindow.xaml"
            ((DevExpress.Xpf.Scheduler.SchedulerStorage)(target)).AppointmentsChanged += new DevExpress.XtraScheduler.PersistentObjectsEventHandler(this.UpdatingHandler);
            
            #line default
            #line hidden
            
            #line 250 "..\..\MainWindow.xaml"
            ((DevExpress.Xpf.Scheduler.SchedulerStorage)(target)).AppointmentsDeleted += new DevExpress.XtraScheduler.PersistentObjectsEventHandler(this.UpdatingHandler);
            
            #line default
            #line hidden
            
            #line 251 "..\..\MainWindow.xaml"
            ((DevExpress.Xpf.Scheduler.SchedulerStorage)(target)).AppointmentCollectionLoaded += new System.EventHandler(this.UpdatingHandler);
            
            #line default
            #line hidden
            
            #line 252 "..\..\MainWindow.xaml"
            ((DevExpress.Xpf.Scheduler.SchedulerStorage)(target)).AppointmentCollectionCleared += new System.EventHandler(this.UpdatingHandler);
            
            #line default
            #line hidden
            return;
            case 23:
            this.storage = ((DevExpress.Xpf.Scheduler.ResourceStorage)(target));
            return;
            case 24:
            this.mapping = ((DevExpress.Xpf.Scheduler.ResourceMapping)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

