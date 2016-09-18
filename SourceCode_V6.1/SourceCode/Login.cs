using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SourceCodeBL;
using SourceCodeDAL;
using Logger;
using LiveLogViewer;
using Monitor;

namespace SourceCode
{
    public partial class Login : Form
    {
        #region Declaration
        /// <summary>
        /// Declaration section for class variables
        /// </summary>
        public static SourceCode sourceCodePage = null;
        int delayTime = 100;
        SourceDataAccessLayer sourceDal = null;
        static UserSession userSession = null;
        private StringBuilder f_Sb;
        private bool f_bDirty;
        private FileSystemWatcher f_Watcher;
        private StringBuilder m_Sb;
        private bool m_bDirty;
        private FileSystemWatcher m_Watcher;
        static ProcessMonitor monitor;
        public enum NotificationType
        {
            Information,
            Success,
            Error
        }
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor Login()
        /// </summary>
        public Login()
        {
            SplashScreen.SplashScreen.UpdateAppMiniLabel("SOURCE CODE");
            SplashScreen.SplashScreen.UpdateBigAppLabel("SOURCE CODE");
            SplashScreen.SplashScreen.UpdateTaskLabel("Starting...");
            SplashScreen.SplashScreen.ShowSplashScreen();
            this.Activated += new EventHandler(this.WatchFile);
            this.Load += new EventHandler(this.WatchDirectory);
            InitializeComponent();
            monitor = ProcessMonitor.Instance();
            Page.processMonitorPage = monitor;
            f_Sb = new StringBuilder();
            f_bDirty = false;
            //added// 67 to 71
            m_Sb = new StringBuilder();
            m_bDirty = false;
            //added//71
            LoadConnections(cbConnection);
            if (InitiateDatabaseConnection(sourceDal, delayTime))
            {
                ShowPopupMessage("Connection established successfully.", "Connected to database.", NotificationType.Success);
            }
            else
            {
                return;
            }
        }
        #endregion

        #region Methods
        private bool InitiateDatabaseConnection(SourceDataAccessLayer sourceDal, int delayTime)
        {
            SplashScreen.SplashScreen.SetStatus("Connecting to database...");
            System.Threading.Thread.Sleep(delayTime);
            if (DBConnection.IsDatabaseOnline())
            {
                sourceDal = new SourceDataAccessLayer();
                this.sourceDal = sourceDal;
                SplashScreen.SplashScreen.SetStatus("Connection successfull...");
                System.Threading.Thread.Sleep(delayTime);
                SplashScreen.SplashScreen.SetStatus("Task completed...");
                System.Threading.Thread.Sleep(delayTime);
                SplashScreen.SplashScreen.CloseForm();
                return true;
            }
            else
            {
                ShowPopupMessage("Database Notification", "Database is offline.", NotificationType.Error);
                ClearTextBoxes(txtPassword, txtUser);
                SplashScreen.SplashScreen.SetStatus("Connection failed...");
                System.Threading.Thread.Sleep(delayTime);
                SplashScreen.SplashScreen.CloseForm();
                return false;
            }
        }
        private static void LoadConnections(ComboBox cbConnection)
        {
            try
            {
                DataTable dtConnections = Utility.UTIL.GetConnections();
                if (dtConnections != null)
                {
                    if (dtConnections.Rows.Count > 0)
                    {
                        if (cbConnection.SelectedIndex == -1)
                        {
                            cbConnection.DisplayMember = "ConnectionName";
                            cbConnection.ValueMember = "ConnectionString";
                            cbConnection.DataSource = dtConnections;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
        //added// 105-160
        private void WatchDirectory(object sender, EventArgs e)
        {
            m_Watcher = new FileSystemWatcher();
            m_Watcher.Filter = "*.*";
            m_Watcher.Path = FileBackUp.RootDirectory + "\\";
            m_Watcher.IncludeSubdirectories = true;
            m_Watcher.NotifyFilter =
                NotifyFilters.CreationTime |
                NotifyFilters.Size |
                NotifyFilters.LastAccess |
                NotifyFilters.LastWrite |
                NotifyFilters.FileName |
                NotifyFilters.DirectoryName;
            m_Watcher.Changed += new FileSystemEventHandler(this.OnChanged);
            m_Watcher.Created += new FileSystemEventHandler(this.OnChanged);
            m_Watcher.Deleted += new FileSystemEventHandler(this.OnChanged);
            m_Watcher.Renamed += new RenamedEventHandler(this.OnRenamed);
            m_Watcher.EnableRaisingEvents = true;   
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!m_bDirty)
            {
                m_Sb.Remove(0, m_Sb.Length);
                m_Sb.Append(e.FullPath);
                m_Sb.Append(" ");
                m_Sb.Append(e.ChangeType.ToString());
                m_Sb.Append("    ");
                m_Sb.Append(DateTime.Now.ToString());
                m_bDirty = true;
            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (!m_bDirty)
            {
                m_Sb.Remove(0, m_Sb.Length);
                m_Sb.Append(e.OldFullPath);
                m_Sb.Append(" ");
                m_Sb.Append(e.ChangeType.ToString());
                m_Sb.Append(" ");
                m_Sb.Append("to ");
                m_Sb.Append(e.Name);
                m_Sb.Append("    ");
                m_Sb.Append(string.Format("{0:MM-dd-yyyy hh-mm-ss-ffff tt}", DateTime.Now));
                m_bDirty = true;
            }
        }

        private void tmrEditNotify_Tick(object sender, EventArgs e)
        {
            if (m_bDirty)
            {
                ShowPopupMessage("Notification", m_Sb.ToString(), NotificationType.Information);
                System.Threading.Thread.Sleep(3000);
                m_bDirty = false;
            }
        }

        private void WatchFile(object sender, EventArgs e)
        {
            string file = Path.GetDirectoryName(DIRECTORY.GetLoggerFilePath());
            f_Watcher = new FileSystemWatcher();
            //filter should be just the filename not complete path
            f_Watcher.Filter = Path.GetFileName(DIRECTORY.GetLoggerFilePath());
            f_Watcher.Path = file;
            f_Watcher.NotifyFilter =
                NotifyFilters.CreationTime |
                NotifyFilters.Size |
                NotifyFilters.LastAccess |
                NotifyFilters.LastWrite |
                NotifyFilters.FileName |
                NotifyFilters.DirectoryName;
            f_Watcher.Changed += new FileSystemEventHandler(this.OnChanged_f);
            f_Watcher.Created += new FileSystemEventHandler(this.OnChanged_f);
            f_Watcher.Deleted += new FileSystemEventHandler(this.OnChanged_f);
            f_Watcher.Renamed += new RenamedEventHandler(this.OnRenamed_f);
            f_Watcher.EnableRaisingEvents = true;
        }

        private void OnChanged_f(object sender, FileSystemEventArgs e)
        {
            if (!f_bDirty)
            {
                f_Sb.Remove(0, f_Sb.Length);
                f_Sb.Append(e.FullPath);
                f_Sb.Append(" ");
                f_Sb.Append(e.ChangeType.ToString());
                f_Sb.Append("    ");
                f_Sb.Append(DateTime.Now.ToString());
                f_bDirty = true;
            }
        }

        private void OnRenamed_f(object sender, RenamedEventArgs e)
        {
            if (!f_bDirty)
            {
                f_Sb.Remove(0, f_Sb.Length);
                f_Sb.Append(e.OldFullPath);
                f_Sb.Append(" ");
                f_Sb.Append(e.ChangeType.ToString());
                f_Sb.Append(" ");
                f_Sb.Append("to ");
                f_Sb.Append(e.Name);
                f_Sb.Append("    ");
                f_Sb.Append(string.Format("{0:MM-dd-yyyy hh-mm-ss-ffff tt}", DateTime.Now));
                f_bDirty = true;
                f_Watcher.Filter = e.Name;
                f_Watcher.Path = e.FullPath.Substring(0, e.FullPath.Length - f_Watcher.Filter.Length);
            }
        }
        
        private void tmrEditNotify1_Tick(object sender, EventArgs e)
        {
            if (f_bDirty)
            {
                ShowPopupMessage("Notification", f_Sb.ToString(), NotificationType.Information);
                f_bDirty = false;
            }
        }

        //added//170
        /// <summary>
        /// Shows the PopupMessage
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="type"></param>
        public void ShowPopupMessage(string title, string content, NotificationType type)
        {
            Bitmap image = Properties.Resources.notification_warning_1_;
            popupNotifier.TitleText = title;
            popupNotifier.ContentText = content;
            switch (type)
            {
                case NotificationType.Error: image = Properties.Resources.notification_error_1_;
                    ShowProcessMonitor(title, content, ComConfig.Notification.Error);
                    break;
                case NotificationType.Information: image = Properties.Resources.notification_warning_1_;
                    ShowProcessMonitor(title, content, ComConfig.Notification.Warning);
                    break;
                case NotificationType.Success: image = Properties.Resources.notification_done_1_;
                    ShowProcessMonitor(title, content, ComConfig.Notification.Information);
                    break;
            }
            popupNotifier.Image = image;
            popupNotifier.Popup();
        }

        public static void ShowProcessMonitor(string title, string content, ComConfig.Notification notificationType)
        {
            monitor.Monitor(title, content, notificationType);
        }

        /// <summary>
        /// btnLogin_Click Event handler for handeling button click event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataTable dtUserdata = null;
            string errorMessage = null;
            try
            {
                if (CheckUserAuthentication(out errorMessage, out dtUserdata))
                {
                    Login.ShowProcessMonitor("Notification : ", "Authenticated successfully...", Monitor.ComConfig.Notification.Information);
                    if (dtUserdata != null)
                    {
                        SetUserSessionData(dtUserdata);
                        if (GlobalPageTracker.loginObj != null)
                            GlobalPageTracker.loginObj.Hide();
                        if (GlobalPageTracker.sourceCodeObj == null)
                        {
                            sourceCodePage = new SourceCode();
                            GlobalPageTracker.sourceCodeObj = sourceCodePage;
                        }
                        if (SplashScreen.SplashScreen.Isminimized == true)
                        {
                            sourceCodePage.WindowState = FormWindowState.Minimized;
                        }
                        else
                        {
                            sourceCodePage.WindowState = FormWindowState.Maximized;
                        }
                        sourceCodePage.Show();
                        sourceCodePage.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(errorMessage, "AUTHENTICATION FAILED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }
       /// <summary>
        /// SetUserSessionData for setting user data in session
        /// </summary>
        /// <param name="dtUserData"></param>
        public static void SetUserSessionData(DataTable dtUserData)
        {
            try
            {
                userSession = new UserSession();
                userSession.UserData.UserId = dtUserData.Rows[0]["USER_ID"].ToString().Trim();
                userSession.UserData.FirstName = dtUserData.Rows[0]["FIRST_NAME"].ToString().Trim();
                userSession.UserData.LastName = dtUserData.Rows[0]["LAST_NAME"].ToString().Trim();
                userSession.UserData.Password = dtUserData.Rows[0]["USER_PASSWORD"].ToString().Trim();
                userSession.UserData.DateOfBirth = Convert.ToDateTime(dtUserData.Rows[0]["DATE_OF_BIRTH"].ToString().Trim());
                userSession.UserData.IsDeleted = Convert.ToInt16(dtUserData.Rows[0]["IS_DELETED"].ToString().Trim());
                userSession.UserData.IsLocked = Convert.ToInt16(dtUserData.Rows[0]["IS_LOCKED"].ToString().Trim());
                userSession.RoleData.RoleName = dtUserData.Rows[0]["ROLE_NAME"].ToString().Trim();
                userSession.RoleData.RoleId = dtUserData.Rows[0]["ROLE_ID"].ToString().Trim();
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }
        /// <summary>
        /// GetUserSessionData
        /// </summary>
        /// <returns>UserSession</returns>
        public static UserSession GetUserSessionData()
        {
                if (userSession != null)
                    return userSession;
                else
                    return null;
        }
        /// <summary>
        /// ClearTextBoxes
        /// </summary>
        /// <param name="control"></param>
        private static void ClearTextBoxes(params Control[] control)
        {
            foreach(Control c in control)
            {
               if(c.GetType()==typeof(TextBox))
               {
                 TextBox txt = (TextBox)c;
                 txt.Text = string.Empty;
               }
            }
        }
        /// <summary>
        /// CheckUserAuthentication
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="dtUserData"></param>
        /// <returns></returns>
        public bool CheckUserAuthentication(out string errorMessage, out DataTable dtUserData)
        {
            bool isAuthenticated = false;
            string userId = txtUser.Text;
            string password = txtPassword.Text;
            dtUserData = null;
            int code = -9;
            errorMessage = ".:: SOME ERROR OCCURED ::.";
            Users user = null;
            try
            {
                ClearTextBoxes(txtUser, txtPassword);
                user = new Users();
                user.UserId = userId;
                user.Password = password;
                dtUserData = sourceDal.AuthenticateUser(user, out code);
                switch (code)
                {
                    case 0: isAuthenticated = true;
                        errorMessage = MESSAGE_LABEL.ACCESS_GRANTED;
                        break;
                    case -1: errorMessage = MESSAGE_LABEL.ID_OR_PASSWORD_MISSING;
                        break;
                    case -2: errorMessage = MESSAGE_LABEL.ACCOUNT_LOCKED;
                        break;
                    case -3: errorMessage = MESSAGE_LABEL.ACCOUNT_DELETED;
                        break;
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return isAuthenticated;
        }
        /// <summary>
        /// btnCancel_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearTextBoxes(txtPassword, txtUser);
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }

        private void errorLogLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                MainForm logViewr = null;
                logViewr = new MainForm();
                GlobalPageTracker.loggerFormObj = logViewr;
                GlobalClass.LOGGER_DISPOSED = false;
                GlobalPageTracker.loggerFormObj.Show();
                GlobalPageTracker.loggerFormObj.Focus();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }

        private void linkLblClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                GlobalPageTracker.loginObj.Close();
                GlobalPageTracker.loginObj.Dispose();
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }

        private void lnkLblForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPassword pass = new ForgotPassword();
            pass.Show();
        }

        private void cbConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbConnection.SelectedIndex !=-1){
                GlobalClass.SOURCE_CODE_CONNECTION_STRING = cbConnection.SelectedValue.ToString();
                DBConnection.Connection = null;
                if (InitiateDatabaseConnection(sourceDal, delayTime))
                {
                    ShowPopupMessage("Connection established successfully.", "Connected to database.", NotificationType.Success);
                }
                else
                {
                    return;
                }
            }
        }
        #endregion
    }
}
