using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SourceCodeDAL;
using System.Text;
using System.Collections;
using Logger;

namespace SourceCode
{
    static class Program
    {
        public static Login loginPage = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Utility.UTIL.LoadConfigurations();
                Monitor.ProcessMonitor.Instance().Monitor("Notification : ", "Application starting...", Monitor.ComConfig.Notification.Information);
                GlobalClass.SOURCE_CODE_CONNECTION_STRING = Properties.Settings.Default.sourceCodeConString;
                Monitor.ProcessMonitor.Instance().Monitor("Notification : ", "Initializing connection string.", Monitor.ComConfig.Notification.Information);
                loginPage = new Login();
                UserOperation.timer = new Timer();
                UserOperation.timer.Tick += new EventHandler(UserOperation.CheckAccessability);
                UserOperation.timer.Interval = 1000;
                GlobalPageTracker.loginObj = loginPage;
                loginPage.Focus();
                Application.Run(loginPage);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
    }
}
