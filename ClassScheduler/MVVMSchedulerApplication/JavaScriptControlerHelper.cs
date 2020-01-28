using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMSchedulerApplication
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JavaScriptControlerHelper
    {
        DependencyObject prozor;
        public JavaScriptControlerHelper(DependencyObject w)
        {
            prozor = w;
        }

       
    }
}