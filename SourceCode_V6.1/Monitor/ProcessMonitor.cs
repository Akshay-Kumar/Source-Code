using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.IO;
using System.Globalization;

namespace Monitor
{
    public partial class ProcessMonitor : Form
    {
        protected static string strLogFilePath = string.Empty;
        private static StreamWriter sw = null;
        private const double MAX_SIZE = 1000;
        #region Methods
        /// <summary>
        /// Setting LogFile path. If the logfile path is null then it will update error info into LogFile.txt under
        /// application directory.
        /// </summary>
        public static string LogFilePath
        {
            set
            {
                FileInfo fileInf0 = new FileInfo(value);
                if (!fileInf0.Exists)
                    fileInf0.Create();
                strLogFilePath = fileInf0.FullName;
            }
            get
            {
                return strLogFilePath;
            }
        }
        public static ProcessMonitor Instance()
        {
            if (Page.processMonitorPage == null)
                Page.processMonitorPage = new ProcessMonitor();
            return Page.processMonitorPage;
        }
        public ProcessMonitor()
        {
            InitializeComponent();
        }

        public void Monitor(string title, string content,ComConfig.Notification notification)
        {
            switch (notification)
            {
                case ComConfig.Notification.Error:
                    Log(string.Format("{0:MM-dd-yyyy hh-mm-ss tt} Error :: {1} :: {2}\r\n", DateTime.Now,title,content), ComConfig.errorStyle); break;
                case ComConfig.Notification.Warning:
                    Log(string.Format("{0:MM-dd-yyyy hh-mm-ss tt} Warning :: {1} :: {2}\r\n", DateTime.Now,title,content), ComConfig.warningStyle); break;
                case ComConfig.Notification.Information:
                    Log(string.Format("{0:MM-dd-yyyy hh-mm-ss tt} Info :: {1} :: {2}\r\n", DateTime.Now, title, content), ComConfig.infoStyle); break;
            }
        }

        private void Log(string text, Style style)
        {
            //write log
            CustomErrorRoutine(text);
            //some stuffs for best performance
            fctb.BeginUpdate();
            fctb.Selection.BeginUpdate();
            //remember user selection
            var userSelection = fctb.Selection.Clone();
            //add text with predefined style
            fctb.TextSource.CurrentTB = fctb;
            fctb.AppendText(text, style);
            //restore user selection
            if (!userSelection.IsEmpty || userSelection.Start.iLine < fctb.LinesCount - 2)
            {
                fctb.Selection.Start = userSelection.Start;
                fctb.Selection.End = userSelection.End;
            }
            else
                fctb.GoEnd();
            //scroll to end of the text
            fctb.Selection.EndUpdate();
            fctb.EndUpdate();
        }

        private static bool WriteErrorLog(string strPathName, string log)
        {
            bool bReturn = false;
            try
            {
                sw = new StreamWriter(strPathName, true);
                sw.Write(log);
                sw.Flush();
                sw.Close();
                bReturn = true;
            }
            catch (Exception)
            {
                bReturn = false;
            }
            return bReturn;
        }
        private static bool CheckDirectory(string strLogPath)
        {
            try
            {
                int nFindSlashPos = strLogPath.Trim().LastIndexOf("\\");
                string strDirectoryname = strLogPath.Trim().Substring(0, nFindSlashPos);

                if (false == Directory.Exists(strDirectoryname))
                    Directory.CreateDirectory(strDirectoryname);

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }


        private static bool CustomErrorRoutine(string log)
        {
            string strPathName = string.Empty;
            if (strLogFilePath.Equals(string.Empty))
            {
                //Get Default log file path "ProcessMonitorLogger.log"
                strPathName = GetProcessMonitorLogFilePath();
            }
            else
            {
                //If the log file path is not empty but the file is not available it will create it
                if (false == File.Exists(strLogFilePath))
                {
                    if (false == CheckDirectory(strLogFilePath))
                        return false;

                    FileStream fs = new FileStream(strLogFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }
                strPathName = strLogFilePath;
            }
            bool bReturn = true;
            // write the error log to that text file
            if (true != WriteErrorLog(strPathName, log))
            {
                bReturn = false;
            }
            return bReturn;
        }

        private static string GetProcessMonitorLogFilePath()
        {
            try
            {
                // get the base directory
                string baseDir = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath;

                // search the file below the current directory
                string retFilePath = baseDir + "" + DEFAULT_MONITOR_LOG_FILE.DefaultLogFolder + "\\" + DEFAULT_MONITOR_LOG_FILE.DefaultLogFile;
                // check if log file needs to be rolled
                loggerRoll(retFilePath);
                // if exists, return the path
                if (File.Exists(retFilePath) == true)
                    return retFilePath;
                //create a text file
                else
                {
                    if (false == CheckDirectory(retFilePath))
                        return string.Empty;

                    FileStream fs = new FileStream(retFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }

                return retFilePath;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        private static void loggerRoll(string filePath)
        {
            FileInfo fInfo = new FileInfo(filePath);
            double len = fInfo.Length;
            double size = len / 1024;
            if (size >= MAX_SIZE)
            {
                 // get the base directory
                string baseDir = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath;

                // search the file below the current directory
                string retFilePath = baseDir + "" + DEFAULT_MONITOR_LOG_FILE.DefaultLogFolder + "\\" + string.Format("{0:MM-dd-yyyy_hh-mm-ss}.log", DateTime.Now);
                fInfo.MoveTo(retFilePath);
            }
        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            fctb.GoEnd();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        #endregion
    }
}
