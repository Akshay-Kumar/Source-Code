using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastColoredTextBoxNS;
using System.Drawing;

namespace Monitor
{
    public class ComConfig
    {
        #region StyleDeclaraton
        public static TextStyle infoStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        public static TextStyle warningStyle = new TextStyle(Brushes.Goldenrod, null, FontStyle.Regular);
        public static TextStyle errorStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        public enum Notification
        {
            Information,
            Warning,
            Error
        };
        #endregion
    }
    public class Page
    {
        public static ProcessMonitor processMonitorPage = null;
    }
    public class DEFAULT_MONITOR_LOG_FILE
    {
        private static string defaultLogFile = "ProcessMonitorLogger.log";
        private static string defaultLogFolder = "monitor";

        public static string DefaultLogFolder
        {
            get { return DEFAULT_MONITOR_LOG_FILE.defaultLogFolder; }
            set { DEFAULT_MONITOR_LOG_FILE.defaultLogFolder = value; }
        }
        public static string DefaultLogFile
        {
            get { return DEFAULT_MONITOR_LOG_FILE.defaultLogFile; }
            set { DEFAULT_MONITOR_LOG_FILE.defaultLogFile = value; }
        }
    }
}
