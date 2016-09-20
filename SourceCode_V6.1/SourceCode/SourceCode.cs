using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SourceCodeBL;
using SourceCodeDAL;
using System.Collections;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Logger;
using System.Data.SqlClient;
using LiveLogViewer;
using System.Text.RegularExpressions;
using System.Globalization;
using Monitor;
using SourceCode;
using Email;

namespace SourceCode
{
    public partial class SourceCode : Form
    {
        #region Declaration
        bool firstLoad = true;
        static int delayTime = 10;

        public static int DelayTime
        {
            get { return SourceCode.delayTime; }
            set { SourceCode.delayTime = value; }
        }
        bool Event = true;
        int prevWidth;
        int dataRowIndex = 0;
        bool isSelected = false;
        private string userName;
        ManageColumns manageCols = null;
        FileBackUp backupLog = null;
        DiffMergeSample codeDiffApp = null;
        DocumentMapSample documentMap = null;
        MarkerToolSample marker = null;
        FileBackUp backUp = null;
        FormWindowState prevWindowState;
        Timer timer;
        StringBuilder sb = new StringBuilder();
        static SourceDataAccessLayer sourceDal = null;
        public static UserSession userData = null;
        ColorSyntax SyntexColor = null;
        DataView dvUserData;
        DataView dvRoleData;
        DataView dvProgramNames;
        DataView dvDeletedProgramNames;
        DataTable dtLangCode;
        DataTable dtPrograms;
        DataTable dtDeletedPrograms;
        DataTable dtTabs = null;
        Roles role;
        Users user;
        LANGUAGE lang;
        static ProcessMonitor monitor;
        PowerfulSample editor = null;
        MailApp mailApplication = null;
        TabPage[] tabControls;
        //Define different context menus for different columns
        private ContextMenu contextMenuForColumns = new ContextMenu();
        #endregion

        #region Constructor
        /// <summary>
        /// SourceCode constructor
        /// </summary>
        public SourceCode()
        {
            timer = new Timer();
            this.timer.Tick += new EventHandler(this.CheckForAccesibleTabs);
            this.timer.Interval = 1000;
            StartTimer();
            Login.ShowProcessMonitor("Notification : ", "Initializing components...", Monitor.ComConfig.Notification.Information);
            if (firstLoad)
            {
                SplashScreen.SplashScreen.ShowSplashScreen();
                SplashScreen.SplashScreen.UpdateAppMiniLabel("SOURCE CODE");
                SplashScreen.SplashScreen.UpdateBigAppLabel("SOURCE CODE");
            }
            InitializeComponent();
            try
            {
                //this.fileBrowser.Visible = browser = false;
                monitor = ProcessMonitor.Instance();
                Page.processMonitorPage = monitor;
                prevWidth = this.Width;
                prevWindowState = this.WindowState;
                backUp = new FileBackUp();
                backupLog = new FileBackUp();
                userData = Login.GetUserSessionData();
                if (DBConnection.IsDatabaseOnline())
                {
                    Login.ShowProcessMonitor("Notification : ", "Creating database connection...", Monitor.ComConfig.Notification.Information);
                    sourceDal = new SourceDataAccessLayer();
                    Login.ShowProcessMonitor("Notification : ", "Database connection successful...", Monitor.ComConfig.Notification.Information);
                    if (firstLoad)
                    {
                        SplashScreen.SplashScreen.SetStatus("Connection successfull...");
                        Login.ShowProcessMonitor("Notification : ", "Connection successfull...", Monitor.ComConfig.Notification.Information);
                        System.Threading.Thread.Sleep(delayTime);
                        SplashScreen.SplashScreen.SetStatus("Task complete.");
                        System.Threading.Thread.Sleep(delayTime);
                    }
                }
                else
                {
                    GlobalPageTracker.loginObj.ShowPopupMessage("Database Notification", "Database is offline.", Login.NotificationType.Error);
                    Login.ShowProcessMonitor("Notification : ", "Database is offline.", Monitor.ComConfig.Notification.Information);
                    if (firstLoad)
                    {
                        SplashScreen.SplashScreen.SetStatus("Connecting to database failed...");
                        Login.ShowProcessMonitor("Notification : ", "Connecting to database failed...", Monitor.ComConfig.Notification.Information);
                        System.Threading.Thread.Sleep(delayTime);
                    }
                    return;
                }
                if (userData.UserData.UserId == null || userData.UserData.Password == null || userData == null)
                {
                    Login.ShowProcessMonitor("Notification : ", "Authentication failed. Access denied.", Monitor.ComConfig.Notification.Information);
                    MessageBox.Show("ACCESS DENIED", "AUTHENTICATION FAILED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Hide();
                    GlobalPageTracker.loginObj.Show();
                    GlobalPageTracker.loginObj.Focus();
                }
                else
                {
                    bool isRolePermissions = false;
                    bool isProgramTab = false;
                    PopulatePageControls(this, PanelList, PanelControlsList);
                    isProgramTab = GetAccessibleTabs(out isRolePermissions);
                    if (isProgramTab || isRolePermissions)
                    {
                        if (isProgramTab)
                        {
                            PopulateProgramNames(firstLoad);
                            if (cbLanguage.SelectedIndex == -1)
                            {
                                PopulateLanguageCode(firstLoad);
                            }
                        }
                        if (isRolePermissions)
                        {
                            IntializeControlsAndPermissions();
                        }
                    }
                    else { }
                    DisplayWelcomeMessage();
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            if (firstLoad)
            {
                SplashScreen.SplashScreen.CloseForm();
                firstLoad = false;
            }
        }
        #endregion

        #region Methods
        private void DisplayWelcomeMessage()
        {
            GlobalPageTracker.loginObj.ShowPopupMessage("Logged in successfully...",
                        "Welcome " + userData.UserData.FirstName.Trim() + " " +
                        userData.UserData.LastName.Trim(),
                        Login.NotificationType.Success);
        }
        private void IntializeControlsAndPermissions()
        {
            try
            {
                Utility.UTIL.PopulatePermissionTree(tvPanelControls, dtTabs);
                Utility.UTIL.PopulateRoleData(GetAllRoles(), RoleList);
                checkedListBox1.Items.Clear();
                ToggleEnableOrDisable(false, btnEdit, btnSave, btnCancel, checkedListBox1);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog.ErrorRoutine(ex);
            }
        }
        private static DataTable dtPanelData = null;
        private void PopulatePageControls(Form workingForm, ListBox tabListBox, ListBox ctrlListBox)
        {
            try
            {
                Utility.UTIL.ClearAllControls(tabListBox, ctrlListBox);
                if(dtPanelData == null)
                    dtPanelData = Utility.UTIL.GetAllTabControls(tabControl1, workingForm);
                if (dtPanelData != null)
                {
                    if (dtPanelData.Rows.Count > 0)
                    {
                        if (SyncPanels(dtPanelData))
                        {
                            DataView tabView = new DataView(dtPanelData);
                            DataTable distinctTabs = new DataTable();
                            distinctTabs = tabView.ToTable(true, "PanelName");
                            for (int i = 0; i < distinctTabs.Rows.Count; i++)
                            {
                                tabListBox.Items.Add(distinctTabs.Rows[i]["PanelName"].ToString().Trim());
                            }
                            tabListBox.SelectedIndex = 0;

                            Object workingTabPage = PanelList.SelectedItem;
                            try
                            {
                                string workingPage = workingTabPage as string;
                                DataRow[] drarray;
                                drarray = dtPanelData.Select("PanelName = '" + workingPage + "'", "PanelControlId", DataViewRowState.CurrentRows);
                                foreach (DataRow row in drarray)
                                {
                                    ctrlListBox.Items.Add(row["PanelControlId"].ToString().Trim());
                                }
                                ctrlListBox.SelectedIndex = 0;
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.ErrorRoutine(ex);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }     
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
        /// <summary>
        /// GetAccessibleTabs
        /// </summary>
        /// <returns></returns>
        public bool GetAccessibleTabs(out bool isRolePermissions)
        {
            //if (userData.RoleData.RoleName.Trim() == ROLE.SUPER_ADMIN)
            //{
            //    dtPanelData = TabUtils.SyncAllTabControls(tabControl1, this, userData, sourceDal);
            //}
            if (firstLoad)
            {
                SplashScreen.SplashScreen.UpdateTaskLabel("Getting all tabs...");
                Login.ShowProcessMonitor("Notification : ", "Getting all accessible tabs for the user.", Monitor.ComConfig.Notification.Information);
                SplashScreen.SplashScreen.SetStatus("Getting all accessible tabs for the user.");
                System.Threading.Thread.Sleep(delayTime);
                //linkLogLbl.Text = delayTime.ToString();
                //dtPanelData = TabUtils.SyncAllTabControls(tabControl1, this, userData, sourceDal);
            }
            int errorCode = -9;
            userName = string.Empty;
            bool isProgramTab = false;
            string rolePermissionsText = string.Empty;
            isRolePermissions = false;
            //archiveToolStrip.Visible = true;
            //addSourceCodeToolStrip.Visible = true;
            try
            {
                userName = " " + userData.UserData.FirstName.Trim() + " " 
                    + userData.UserData.LastName.Trim()+
                    " ["+userData.RoleData.RoleName.Trim()+"] "+
                    "["+userData.UserData.UserId.Trim()+"]";
                rolePermissionsText = "[" + userData.RoleData.RoleName.Trim() + "]"+" : [PERMISSIONS]";
                lblPermissions.Text = rolePermissionsText.Trim();
                lblPermissions.Font = new Font("Calibri", 8, FontStyle.Bold);
                lblPermissions.AutoSize = true;
                lblUser.Text = AutoEllipsis.Ellipsis.Compact(userName, lblUser, AutoEllipsis.EllipsisFormat.End);
                sourceCodeTooltip.SetToolTip(lblUser, userName);
                lblUser.Font = new Font("Calibri", 8, FontStyle.Bold);
                lblUser.AutoSize = true;
                tsDDBtnUserName.Text = userData.UserData.FirstName.Trim() + " "
                    + userData.UserData.LastName.Trim();
                dtTabs = sourceDal.GetPanelsByRole(userData.RoleData, out errorCode);
                /*
                if (errorCode == 0)//NO ERROR
                {
                    HideAllActiveTabs();
                    userData.ACCESSIBLE_TABS = new string[dtTabs.Rows.Count];
                    int tabIndex = 0;
                    for (int i = 0; i < tabCollection.Length; i++)
                    {
                        TabPage tabPage = (TabPage)tabCollection[i];
                        for (int j = 0; j < dtTabs.Rows.Count; j++)
                        {
                            if (tabPage.Name == dtTabs.Rows[j]["PANEL_NAME"].ToString().Trim())
                            {
                                tabControl1.TabPages.Add(tabPage);
                                userData.ACCESSIBLE_TABS[tabIndex] = tabPage.Text;
                                tabIndex++;
                                if (firstLoad)
                                {
                                    SplashScreen.SplashScreen.SetStatus("Loading tabs... " + tabPage.Text);
                                    System.Threading.Thread.Sleep(delayTime);
                                }
                            }
                        }
                    }

                    foreach (TabPage page in tabControl1.TabPages)
                    {
                        if (page.Text == TABS.SOURCE_CODE)
                        {
                            isProgramTab = true;
                        }
                    }
                    lblUser.ForeColor = System.Drawing.Color.Green;
                }
                */
                if (errorCode == 0 && dtTabs.Rows.Count>0)//NO ERROR
                {
                    TabPage tabPage = null;
                    tabControls = TabUtils.CloneTabControl(tabControl1);
                    DataView tabView = new DataView(dtTabs);
                    tabView.RowFilter = "ACCESS = 1";
                    DataTable distinctTabs = new DataTable();
                    distinctTabs = tabView.ToTable(true, "PANEL_NAME");
                    userData.ACCESSIBLE_TABS = new string[distinctTabs.Rows.Count];
                    for (int tabIndex = 0; tabIndex < distinctTabs.Rows.Count; tabIndex++)
                    {
                        userData.ACCESSIBLE_TABS[tabIndex] = distinctTabs.Rows[tabIndex]["PANEL_NAME"].ToString().Trim();
                    }
                    //Hide all tabs
                    TabUtils.HideAllActiveTabs(tabControl1);
                    
                    for (int j = 0; j < distinctTabs.Rows.Count; j++)
                    {
                        if (distinctTabs.Rows[j]["PANEL_NAME"].ToString().Trim().Contains("tab"))
                        {
                            tabPage = TabUtils.ShowPage(distinctTabs.Rows[j]["PANEL_NAME"].ToString().Trim(), tabControl1);
                        }
                        else
                        {
                            if (distinctTabs.Rows[j]["PANEL_NAME"].ToString().Trim() == this.Name)
                            {
                                TabUtils.ActivatePageControls(this, dtTabs);
                            }
                        }
                        if (tabPage != null)
                        {
                            TabUtils.ActivateTabControls(tabPage, dtTabs);
                        }
                        if (firstLoad && tabPage != null)
                        {
                            SplashScreen.SplashScreen.SetStatus("Loading tabs... " + tabPage.Text);
                            System.Threading.Thread.Sleep(delayTime);
                        }
                        else if (firstLoad && tabPage == null)
                        {
                            SplashScreen.SplashScreen.SetStatus("Loading tabs... " + this.Name);
                            System.Threading.Thread.Sleep(delayTime);
                        }
                    }

                    foreach (TabPage page in tabControl1.TabPages)
                    {
                        if (page.Text == TABS.SOURCE_CODE)
                        {
                            isProgramTab = true;
                        }
                        if (page.Text == TABS.ROLE_PERMISSIONS)
                        {
                            isRolePermissions = true;
                        }
                    }
                    lblUser.ForeColor = System.Drawing.Color.Green;
                }
                else if(errorCode == 1)
                {
                    TabUtils.HideAllActiveTabs(tabControl1);
                    lblUser.ForeColor = System.Drawing.Color.Red;
                    MessageBox.Show("SOME ERROR OCCURED, UNABLE TO LOAD TABS FOR USER : " + (userData.UserData.FirstName.Trim() + " " + userData.UserData.LastName.Trim()).ToUpper(), "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dtTabs.Rows.Count == 0)
                {
                    TabUtils.HideAllActiveTabs(tabControl1);
                    lblUser.ForeColor = System.Drawing.Color.Red;
                    MessageBox.Show("NO TABS ASSIGNED FOR USER : " + (userData.UserData.FirstName.Trim() + " " + userData.UserData.LastName.Trim()).ToUpper(), "WARNING_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            return (isProgramTab);
        }

        
        /// <summary>
        /// SourceCode_FormClosed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceCode_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.loginPage.Dispose();
        }
        private DataTable GetAllRoles()
        {
            DataTable dt = null;
            try
            {
                dt = sourceDal.GetAllRoles();
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            return dt;
        }
        /// <summary>
        /// FetchAllRoles
        /// </summary>
        private void FetchAllRoles()
        {
            try
            {
                DataTable dtRoles = GetAllRoles();
                GlobalClass.dt = dtRoles;
                //dgvRoleData.Columns.Clear();
                dgvRoleData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvRoleData.AllowUserToAddRows = false;
                dgvRoleData.RowHeadersVisible = false;
                /************************************/
                dvRoleData = dtRoles.DefaultView;
                dvRoleData.Sort = "ROLE_ID";
                MakeDataGridReadOnly(dgvRoleData);
                if (!dgvRoleData.Columns.Contains("chkBxSelect"))
                 {
                    AddCheckBox(dgvRoleData);
                 }
                dgvRoleData.DataSource = dvRoleData;
                FormatDataView(dgvRoleData,12,FontStyle.Regular);
                /************************************/
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// addItems
        /// </summary>
        /// <param name="col"></param>
        /// <param name="dtRoles"></param>
        public void addItems(AutoCompleteStringCollection col, DataTable dtRoles)
        {
            int count = dtRoles.Rows.Count;
            try
            {
                for (int index = 0; index < count; index++)
                {
                    col.Add(dtRoles.Rows[index]["ROLE_NAME"].ToString().Trim());
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /**************************static methods*****************************/
        /// <summary>
        /// GetAllPanels
        /// </summary>
        /// <param name="checkedListBox"></param>
        public static void GetAllPanels(CheckedListBox checkedListBox)
        {
            try
            {
                DataTable dtPanels = sourceDal.GetAllPanels();
                checkedListBox.Items.Clear();
                foreach (DataRow row in dtPanels.Rows)
                {
                    checkedListBox.Items.Add(row["PANEL_NAME"].ToString());
                }
                ClearAllCheckBox(checkedListBox);
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// ClearAllCheckBox
        /// </summary>
        /// <param name="checkedListBox"></param>
        public static void ClearAllCheckBox(CheckedListBox checkedListBox)
        {
            try
            {
                for (int i = 0; i < checkedListBox.Items.Count; i++)
                {
                    //checkedListBox.SetItemChecked(i, false);
                    SetControlCheck(checkedListBox, i, false);
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// GetPanelsByRole
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="checkedListBox"></param>
        public static void GetPanelsByRole(string roleName, CheckedListBox checkedListBox)
        {
            Roles role = new Roles();
            role.RoleName = roleName;
            int value = -1;
            try
            {
                DataTable dtTabs = sourceDal.GetPanelsByRole(role, out value);
                DataView tabView = new DataView(dtTabs);
                tabView.RowFilter = "ACCESS = 1";
                DataTable distinctTabs = new DataTable();
                distinctTabs = tabView.ToTable(true, "PANEL_NAME");
                ClearAllCheckBox(checkedListBox);
                for (int i = 0; i < checkedListBox.Items.Count; i++)
                {
                    for (int j = 0; j < distinctTabs.Rows.Count; j++)
                    {
                        if (checkedListBox.Items[i].ToString().Trim().Equals(distinctTabs.Rows[j]["PANEL_NAME"].ToString().Trim()))
                        {
                            //checkedListBox.SetItemChecked(i, true);
                            SetControlCheck(checkedListBox, i, true);
                        }
                    }
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        static Action<CheckedListBox, int, bool> setterCallback = (checkedBox, index, flag) => checkedBox.SetItemChecked(index,flag);

        static void SetControlCheck(CheckedListBox checkedBox,int index, bool flag)
        {
            if (checkedBox.InvokeRequired)
            {
                checkedBox.Invoke(setterCallback, checkedBox, index, flag);
            }
            else
            {
                setterCallback(checkedBox, index, flag);
            }
        }
        /****************************************************************************************/
        /// <summary>
        /// tabControl1_Selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            //To be coded//
        }
        /// <summary>
        /// PopulateLanguageCode
        /// </summary>
        private void PopulateLanguageCode(bool load)
        {
            if (load)
            {
                SplashScreen.SplashScreen.UpdateTaskLabel("Getting all language codes...");
            }
            try
            {
                try
                {
                    dtLangCode = sourceDal.FetchAllLanguageCode();
                }
                catch (SqlException ex) { MessageBox.Show("EXCEPTION :" + ex.Message, "EXCEPTION_MESAGE", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                if (cbLanguage.Items.Count > 0)
                cbLanguage.Items.Clear();
                if (ccbCodeLanguage.Items.Count > 0)
                ccbCodeLanguage.Items.Clear();
                ccbCodeLanguage.Text = "--Select--";
                cbLanguage.Text = "--Select--";
                for (int i = 0; i < dtLangCode.Rows.Count; i++)
                {
                    lang = new LANGUAGE();
                    lang.Lang_id = dtLangCode.Rows[i]["LANG_ID"].ToString().Trim();
                    lang.Lang_name = dtLangCode.Rows[i]["LANG_NAME"].ToString().Trim();
                    ccbCodeLanguage.Items.Add(lang, false);
                    cbLanguage.Items.Add(lang);
                    if (load)
                    {
                        SplashScreen.SplashScreen.SetStatus("Loading languages " + lang.Lang_name);
                        System.Threading.Thread.Sleep(delayTime);
                        SplashScreen.SplashScreen.SetStatus("Loading language codes " + lang.Lang_id);
                        System.Threading.Thread.Sleep(delayTime);
                    }
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// btnEdit_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            ToggleEnableOrDisable(true, btnSave, btnCancel, checkedListBox1);
            ToggleEnableOrDisable(false, btnEdit);
        }
        /// <summary>
        /// btnCancel_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ToggleEnableOrDisable(false, btnSave, btnCancel, checkedListBox1);
            ToggleEnableOrDisable(true, btnEdit);
        }
        /// <summary>
        /// ToggleVisibility
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="controls"></param>
        public void ToggleVisibility(bool visibility, params Control[] controls)
        {
            foreach (Control ctrl in controls)
            {
                if(ctrl.GetType()==typeof(Button))
                        ctrl.Visible = visibility;
                if(ctrl.GetType()==typeof(CheckBox))
                        ctrl.Visible = visibility;
                if(ctrl.GetType()==typeof(TextBox))
                        ctrl.Visible = visibility;
                if(ctrl.GetType()==typeof(ComboBox))
                        ctrl.Visible = visibility;
                if(ctrl.GetType()==typeof(TableLayoutPanel))
                        ctrl.Visible = visibility;
            }
        }
        /// <summary>
        /// ToggleEnableOrDisable
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="control"></param>
        public static void ToggleEnableOrDisable(bool flag, params Control[] control)
        {
            foreach (Control c in control)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    TextBox txtBox = (TextBox)c;
                    txtBox.Enabled = flag;
                }
                else if (c.GetType() == typeof(ComboBox))
                {
                    ComboBox cbBox = (ComboBox)c;
                    cbBox.Enabled = flag;
                }
                else if (c.GetType() == typeof(Button))
                {
                    Button b = (Button)c;
                    b.Enabled = flag;
                }
                else if (c.GetType() == typeof(CheckedListBox))
                {
                    CheckedListBox cbox = (CheckedListBox)c;
                    cbox.Enabled = flag;
                }
                else if (c.GetType() == typeof(RichTextBox))
                {
                    RichTextBox cbox = (RichTextBox)c;
                    cbox.Enabled = flag;
                }
            }
        }
        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Roles role = new Roles();
            int i = 0;
            List<int> ChkedRow = new List<int>();
            try
            {
                for (i = 0; i <= dgvRoleData.RowCount - 1; i++)
                {
                    if (Convert.ToBoolean(dgvRoleData.Rows[i].Cells["chkBxSelect"].Value) == true)
                    {
                        ChkedRow.Add(i);
                    }
                }
                role.RoleName = dgvRoleData.Rows[ChkedRow[0]].Cells["ROLE_NAME"].Value.ToString();
                if (UpdateTabsByRole(role, GetTabListPanels(checkedListBox1)))
                {
                    MessageBox.Show("Tabs updated successfully for " + role.RoleName, "SUCCESS_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ToggleEnableOrDisable(false, btnSave, btnCancel, checkedListBox1);
                    ToggleEnableOrDisable(true, btnEdit);
                    UpdateAccessibleTabs();
                    UpdateAccessibleControls();
                }
                else
                {
                    MessageBox.Show("Error in updating tabs for " + role.RoleName, "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// GetTabListPanels
        /// </summary>
        /// <param name="checkedListBox"></param>
        /// <returns></returns>
        public static string GetTabListPanels(CheckedListBox checkedListBox)
        {
            string tabList = string.Empty;
            try
            {
                for (int i = 0; i < checkedListBox.Items.Count; i++)
                {
                    if (checkedListBox.GetItemChecked(i))
                    {
                        tabList += checkedListBox.Items[i].ToString().Trim() + "1,";
                    }
                    else
                    {
                        tabList += checkedListBox.Items[i].ToString().Trim() + "0,";
                    }
                }
                int index = tabList.LastIndexOf(",");
                if (index == tabList.Length - 1)
                {
                    tabList = tabList.Remove(index, 1);
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            return tabList;
        }
        /// <summary>
        /// UpdateTabsByRole
        /// </summary>
        /// <param name="role"></param>
        /// <param name="tabList"></param>
        /// <returns></returns>
        private bool UpdateTabsByRole(Roles role, string tabList)
        {
            bool ret = false;
            try
            {
                ret = sourceDal.UpdateTabsByRole(role, tabList);
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            return ret;
        }
        /// <summary>
        /// logoutLinkLbl_LinkClicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logoutLinkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LogOff();
        }
        /// <summary>
        /// LogOff
        /// </summary>
        public void LogOff()
        {
            try
            {
                firstLoad = true;
                StopTimer();
                DisposeResources();
                if (LogOut(userData))
                {
                    UserOperation.StopTimer();
                    if (GlobalPageTracker.sourceCodeObj != null)
                    {
                        GlobalPageTracker.sourceCodeObj.Hide();
                        GlobalPageTracker.sourceCodeObj = null;
                    }
                    if (GlobalPageTracker.roleOperationPageObj != null)
                    {
                        GlobalPageTracker.roleOperationPageObj.Hide();
                        GlobalPageTracker.roleOperationPageObj = null;
                    }
                    if (GlobalPageTracker.operationPageObj != null)
                    {
                        GlobalPageTracker.operationPageObj.Hide();
                        GlobalPageTracker.operationPageObj = null;
                    }
                    if (GlobalPageTracker.colorSyntexPageObj != null)
                    {
                        GlobalPageTracker.colorSyntexPageObj.Hide();
                        GlobalPageTracker.colorSyntexPageObj = null;
                    }
                    if (GlobalPageTracker.loginObj != null)
                    {
                        GlobalPageTracker.loginObj.ShowPopupMessage("Notification", "Logged out successfully.", Login.NotificationType.Success);
                        GlobalPageTracker.loginObj.Show();
                        GlobalPageTracker.loginObj.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Failed to logout", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// Disposes all of the resources assigned
        /// </summary>
        public void DisposeResources()
        {
            TabUtils.ClearHiddenPages();
        }
        /// <summary>
        /// LogOut
        /// </summary>
        /// <param name="userSessionData"></param>
        /// <returns></returns>
        private bool LogOut(UserSession userSessionData)
        {
            bool flag = false;
            try
            {
                if (userSessionData != null)
                {
                    userSessionData.ClearUserSession();
                    flag = true;
                }
                else if (userSessionData == null)
                {
                    flag = false;
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            return flag;
        }



/**************************************************************************************/
        //public void AddButtonToDataGrid()
        //{
        //    DataGridViewButtonColumn editBtn = new DataGridViewButtonColumn();
        //    dgvUserData.Columns.Add(editBtn);
        //    editBtn.HeaderText = "click to edit";
        //    editBtn.Text = "Edit";
        //    editBtn.Name = "btnEdit";
        //    editBtn.UseColumnTextForButtonValue = true;

        //    DataGridViewButtonColumn saveBtn = new DataGridViewButtonColumn();
        //    dgvUserData.Columns.Add(saveBtn);
        //    saveBtn.HeaderText = "click to save";
        //    saveBtn.Text = "Save";
        //    saveBtn.Name = "btnSave";
        //    saveBtn.UseColumnTextForButtonValue = true;

        //    DataGridViewButtonColumn cancelBtn = new DataGridViewButtonColumn();
        //    dgvUserData.Columns.Add(cancelBtn);
        //    cancelBtn.HeaderText = "click to cancel";
        //    cancelBtn.Text = "Cancel";
        //    cancelBtn.Name = "btnCancel";
        //    cancelBtn.UseColumnTextForButtonValue = true;
        //}
/************************************************************************************/

        /// <summary>
        /// AddCheckBox
        /// </summary>
        /// <param name="dataGridView"></param>
        private static void AddCheckBox(DataGridView dataGridView)
        {
            try
            {
                DataGridViewCheckBoxColumn chkBox = new DataGridViewCheckBoxColumn();
                dataGridView.Columns.Add(chkBox);
                if(dataGridView.Name == "dgvUserData")
                    chkBox.HeaderText = "SELECT_USER";
                else if(dataGridView.Name == "dgvRoleData")
                    chkBox.HeaderText = "SELECT_ROLE";
                chkBox.Name = "chkBxSelect";
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    dataGridView.Rows[i].Cells["chkBxSelect"].Value = false;
                    dataGridView.Rows[i].Cells["chkBxSelect"].ReadOnly = false;
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// FetchAllUsers
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable FetchAllUsers()
        {
            DataTable dt = null;
            try
            {
                dt = sourceDal.FetchAllUsers();
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            return dt;
        }
        /// <summary>
        /// FetchAllPrograms
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable FetchAllPrograms()
        {
            DataTable dt = null;
            try
            {
                dt = sourceDal.FetchAllPrograms();
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            return dt;
        }
        /// <summary>
        /// FetchAllDeletedPrograms
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable FetchAllDeletedPrograms()
        {
            DataTable dt = null;
            try
            {
                dt = sourceDal.FetchAllDeletedPrograms();
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            return dt;
        }
        /// <summary>
        /// PopulateProgramNames
        /// </summary>
        private void PopulateProgramNames(bool load)
        {
            if (load)
            {
                SplashScreen.SplashScreen.UpdateTaskLabel("Getting all programs...");
                Login.ShowProcessMonitor("Notification : ", "Getting all programs...", Monitor.ComConfig.Notification.Information);
            }
            string programId = string.Empty;
            string langName = string.Empty;
            string progName = string.Empty;
            string createdDate = string.Empty;
            string lastModifiedDate = string.Empty;
            string filePath = string.Empty;
            string userId = string.Empty;

            try
            {
                dtPrograms = FetchAllPrograms();
                dgvPrograms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPrograms.AllowUserToAddRows = false;
                dgvPrograms.ColumnHeadersVisible = true;
                dgvPrograms.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPrograms.RowHeadersVisible = false;
                MakeDataGridReadOnly(dgvPrograms);
                DataTable dtFormattedProg = new DataTable("tbl_PROGRAMS_LOCAL_DB");
                dtFormattedProg.Columns.Add("PROG_ID", typeof(string));
                dtFormattedProg.Columns.Add("LANG_NAME", typeof(string));
                dtFormattedProg.Columns.Add("PROG_NAME", typeof(string));
                dtFormattedProg.Columns.Add("CREATED_DATE", typeof(string));
                dtFormattedProg.Columns.Add("LAST_MODIFIED_DATE", typeof(string));
                dtFormattedProg.Columns.Add("USER_ID", typeof(string));
                dtFormattedProg.Columns.Add("FILE_PATH", typeof(string));

                for (int i = 0; i < dtPrograms.Rows.Count; i++)
                {
                    programId = dtPrograms.Rows[i]["PROG_ID"].ToString().Trim();
                    langName = sourceDal.GetLangNameByLangId(dtPrograms.Rows[i]["LANG_ID"].ToString()).Trim();
                    progName = dtPrograms.Rows[i]["PROG_NAME"].ToString().Trim();
                    lastModifiedDate = dtPrograms.Rows[i]["LAST_MODIFIED_DATE"].ToString().Trim();
                    filePath = dtPrograms.Rows[i]["FILE_PATH"].ToString().Trim();
                    userId = dtPrograms.Rows[i]["USER_ID"].ToString().Trim();
                    /****************************************************************************************************/
                    createdDate = dtPrograms.Rows[i]["CREATED_DATE"].ToString().Trim();
                    //string createTime = createdDate.Contains("PM") ? " PM" : " AM";
                    //string modifiedTime = lastModifiedDate.Contains("PM") ? " PM" : " AM";
                    //createdDate = createdDate.Substring(0, createdDate.LastIndexOf('-')) + createTime;
                    //lastModifiedDate = lastModifiedDate.Substring(0, lastModifiedDate.LastIndexOf('-')) + modifiedTime;
                    //dtFormattedProg.Rows.Add(programId, langName, progName,createdDate,lastModifiedDate);
                    /*****************************************************************************************************/
                    if (load)
                    {
                        SplashScreen.SplashScreen.SetStatus("Loading Program id... " + programId);
                        Login.ShowProcessMonitor("Notification : ", "Loading Program id... " + programId, Monitor.ComConfig.Notification.Information);
                        System.Threading.Thread.Sleep(delayTime);
                        SplashScreen.SplashScreen.SetStatus("Loading Program name... " + progName);
                        Login.ShowProcessMonitor("Notification : ", "Loading Program name... " + progName, Monitor.ComConfig.Notification.Information);
                        System.Threading.Thread.Sleep(delayTime);
                        SplashScreen.SplashScreen.SetStatus("Loading File path... " + filePath);
                        Login.ShowProcessMonitor("Notification : ", "Loading File path... " + filePath, Monitor.ComConfig.Notification.Information);
                        System.Threading.Thread.Sleep(delayTime);
                        SplashScreen.SplashScreen.SetStatus("Loading Last modified date... " + lastModifiedDate);
                        Login.ShowProcessMonitor("Notification : ", "Loading Last modified date... " + lastModifiedDate, Monitor.ComConfig.Notification.Information);
                        System.Threading.Thread.Sleep(delayTime);
                    }
                    dtFormattedProg.Rows.Add(programId, langName, progName,createdDate, lastModifiedDate, userId, filePath);
                }
                /**************Miracle Lines**************/
                dgvPrograms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvPrograms.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                /*****************************************/
                dvProgramNames = dtFormattedProg.DefaultView;
                dvProgramNames.Sort = "PROG_ID";
                //dgvPrograms.Columns.Clear();
                dgvPrograms.DataSource = dvProgramNames;
                foreach (DataGridViewColumn column in dgvPrograms.Columns)
                {
                    if (column.Name == "FILE_PATH")
                    {
                        column.Visible = false;
                    }
                    else if (column.Name == "CREATED_DATE")
                    {
                        column.Visible = false;
                    }
                    else if (column.Name == "USER_ID")
                    {
                        column.Visible = true;
                    }
                }
                //dgvPrograms.Columns["FILE_PATH"].Visible = false;
                //dgvPrograms.Columns["USER_ID"].Visible = true;
                //if(!dgvPrograms.Columns.Contains("btnDeletePrograms"))
                //AddCustomButtonsToDataGrid(dgvPrograms, "btnDeletePrograms", "Delete", string.Empty);
                //DisableNonAccesibleButtons(dvProgramNames, dgvPrograms, "btnDeletePrograms");
                FormatDataView(dgvPrograms,24,FontStyle.Bold);
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        public void ManageColumnVisibility(Dictionary<String,bool> columnList)
        {
            StringBuilder message = new StringBuilder();
            foreach(var pair in columnList)
            {
                foreach (DataGridViewColumn column in dgvPrograms.Columns)
                {
                    if (pair.Key == column.Name)
                    {
                        column.Visible = pair.Value;
                    }
                }
                message.AppendLine(string.Format("{0} ------------ {1}", pair.Key.Trim(), (pair.Value ? "Visible" : "Invisible")));
            }
            MessageBox.Show(message.ToString(), "COLUMNS CHANGED");
        }
        public static string Indent(int count)
        {
            return "-".PadLeft(count);
        }
        private void dgvPrograms_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!dgvPrograms.Columns.Contains("btnDeletePrograms"))
                AddCustomButtonsToDataGrid(dgvPrograms, "btnDeletePrograms", "Delete", string.Empty, dgvPrograms.Columns.Count);
            DisableNonAccesibleButtons(dvProgramNames, dgvPrograms, "btnDeletePrograms");
            if (!dgvPrograms.Columns.Contains("btnLangNameIcon"))
                AddImageToDataGrid(dgvPrograms, "btnLangNameIcon", "Language", string.Empty, 0);
            PopulateIconsInDataGrid(dvProgramNames, dgvPrograms, "btnLangNameIcon");
        }

        private void dgvPrograms_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    int currentMouseOverRow = dgvPrograms.HitTest(e.X, e.Y).RowIndex;
                    GlobalClass.index = currentMouseOverRow;
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("View code", new EventHandler(this.ViewCode)));
                    m.MenuItems.Add(new MenuItem("Delete", new EventHandler(this.Delete)));
                    DisableNonAccessibleMenu(currentMouseOverRow, m);
                    m.Show(dgvPrograms, new Point(e.X, e.Y));
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
        private void DisableNonAccessibleMenu(int index,ContextMenu menu)
        {
            DataTable dtTemp = dvProgramNames.ToTable();
            string userId = dtTemp.Rows[index]["USER_ID"].ToString().Trim();
            if (userData.RoleData.RoleName.Trim() != ROLE.SUPER_ADMIN)
            {
                if (userId != userData.UserData.UserId.Trim())
                {
                    menu.MenuItems[1].Enabled = false;
                }
                else if (userId != userData.UserData.UserId.Trim())
                {
                    menu.MenuItems[1].Enabled = true;
                }
            }
            else
            {
                menu.MenuItems[1].Enabled = true;
            }
        }

        private void DisableNonAccesibleButtons(DataView dv,DataGridView dgv,string buttonName)
        {
            DataTable dtTemp = dv.ToTable();
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                if (userData.RoleData.RoleName.Trim() != ROLE.SUPER_ADMIN)
                {
                    if (dtTemp.Rows[i]["USER_ID"].ToString().Trim() != userData.UserData.UserId.Trim())
                    {
                        SetDGVButtonColumnEnable(dgv, i, buttonName, false);
                    }
                    else if (dtTemp.Rows[i]["USER_ID"].ToString().Trim() == userData.UserData.UserId.Trim())
                    {
                        SetDGVButtonColumnEnable(dgv, i, buttonName, true);
                    }
                }
                else
                {
                    SetDGVButtonColumnEnable(dgv, i, buttonName, true);
                }
            }
        }
        /// <summary>
        /// Populates icons in datagridview based upon the programming language code
        /// </summary>
        /// <param name="info"></param>
        private void PopulateIconsInDataGrid(DataView dv, DataGridView dgv, string buttonName)
        {
            DataTable dtTemp = dv.ToTable();
            DataTable dtLang = sourceDal.FetchAllLanguageCode();
            DataView dvLang = new DataView(dtLang);
            try
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {

                    dvLang.RowFilter = "LANG_NAME = '" + dtTemp.Rows[i]["LANG_NAME"].ToString().Trim() + "'";
                    string path = GetPath(dvLang[0]["LANG_ICON_PATH"].ToString().Trim());
                    Icon icon = new System.Drawing.Icon(path);
                    SetDGVColumnImage(dgv, i, buttonName, icon);
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        private void PopulateIcons(DataView dv, DataGridView dgv, string buttonName)
        {
            DataTable dtTemp = dv.ToTable();
            try
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string path = GetPath(GetUserStatus(dtTemp.Rows[i]["" + buttonName.Replace("_IMAGE",string.Empty) + ""].ToString().Trim()));
                    Icon icon = new System.Drawing.Icon(path);
                    SetDGVColumnImage(dgv, i, buttonName, icon);
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        public static string GetUserStatus(string status)
        {
            string relativePath = string.Empty;
            switch (status)
            {
                case "LOCKED": relativePath = USER_ICONS.locked_user_icon;
                    break;
                case "DELETED": relativePath = USER_ICONS.deleted_user_icon;
                    break;
                case "OPEN": relativePath = USER_ICONS.unlocked_user_icon;
                    break;
                case "ACTIVE": relativePath = USER_ICONS.unlocked_user_icon;
                    break;
            }
            return relativePath;
        }
        public static string GetPath(string relativePath)
        {
            return Path.Combine(DIRECTORY.GetApplicationPath().Replace(GetDirectoryName(DIRECTORY.GetApplicationPath()), DIRECTORY.icons), relativePath);
        }
        public static void SetDGVColumnImage(DataGridView dgv, int index, string btnName, Icon icon)
        {
            DataGridViewImageCell buttonCell = (DataGridViewImageCell)dgv.Rows[index].Cells[btnName];
            buttonCell.Value = icon;
        }
        public static string GetDirectoryName(string path)
        {
            return (path.Substring(path.LastIndexOf("\\") + 1)).Trim();
        }
        private void SetDGVButtonColumnEnable(DataGridView dgv,int index,string btnName,bool enabled)
        {
            DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)dgv.Rows[index].Cells[btnName];
            buttonCell.Enabled = enabled;
        }
        private void ViewCode(object sender, EventArgs e)
        {
            ShowColorSyntaxForm(GlobalClass.index, dvProgramNames);
        }
        private void Delete(object sender, EventArgs e)
        {
            DeleteCode();
        }
        private void dgvPrograms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = 0;
            DataTable dtProgs = null;
            try
            {
                if (dgvPrograms.Columns[e.ColumnIndex].Name == "btnDeletePrograms")
                {
                    DataGridViewDisableButtonCell buttonCell =
                        (DataGridViewDisableButtonCell)dgvPrograms.
                        Rows[e.RowIndex].Cells["btnDeletePrograms"];

                    if (buttonCell.Enabled)
                    {
                        if (e.ColumnIndex == dgvPrograms.Columns["btnDeletePrograms"].Index)
                        {
                            rowIndex = e.RowIndex;
                            GlobalClass.index = rowIndex;
                            dtProgs = dvProgramNames.ToTable();
                            //progId = dtProgs.Rows[rowIndex]["PROG_ID"].ToString().Trim();
                            DeleteCode();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
                MessageBox.Show("ERROR WHILE DELETING PROGRAMS !", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// This method is used to create buttons dynamically in datagridview control
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="btnName"></param>
        /// <param name="btnText"></param>
        /// <param name="buttonHeader"></param>
        public void AddButtonToDataGrid(DataGridView dgv, string btnName, string btnText, string btnHeader, int position = 0)
        {
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = btnHeader;
            button.Text = btnText;
            button.Name = btnName;
            button.UseColumnTextForButtonValue = true;
            //dgv.Columns.Add(button);
            dgv.Columns.Insert(position, button);
        }
        public void AddImageToDataGrid(DataGridView dgv, string btnName, string btnText, string btnHeader, int position = 0)
        {
            DataGridViewImageColumn button = new DataGridViewImageColumn();
            button.HeaderText = btnHeader;
            button.ToolTipText = btnText;
            button.Name = btnName;
            button.ValuesAreIcons = true;
            //dgv.Columns.Add(button);
            dgv.Columns.Insert(position, button);
        }
        /// <summary>
        /// This method is used to create custom buttons dynamically in datagridview control
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="btnName"></param>
        /// <param name="btnText"></param>
        /// <param name="buttonHeader"></param>
        public void AddCustomButtonsToDataGrid(DataGridView dgv, string btnName, string btnText, string btnHeader, int position = 0)
        {
            DataGridViewDisableButtonColumn button = new DataGridViewDisableButtonColumn();
            button.HeaderText = btnHeader;
            button.Text = btnText;
            button.Name = btnName;
            button.UseColumnTextForButtonValue = true;
            //dgv.Columns.Add(button);
            dgv.Columns.Insert(position, button);
        }
        /// <summary>
        /// dgvDeletedPrograms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDeletedPrograms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = 0;
            DataTable dtDeletedProgs = null;
            string progId = string.Empty;
            if (e.ColumnIndex == dgvDeletedPrograms.Columns["btnUndoDeletePrograms"].Index)
            {
                rowIndex = e.RowIndex;
                dtDeletedProgs = dvDeletedProgramNames.ToTable();
                progId = dtDeletedProgs.Rows[rowIndex]["PROG_ID"].ToString().Trim();
                if (UndoDeletePrograms(progId))
                {
                    dgvDeletedPrograms.Rows.RemoveAt(rowIndex);
                    PopulateProgramNames(false);
                    if (cbLanguage.SelectedIndex == -1)
                    {
                        PopulateLanguageCode(false);
                    }
                }
                else
                {
                    MessageBox.Show("ERROR WHILE PERFORMING UNDO DELETE !", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Code difference application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCodeDiff_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// undo deleted programs from the database
        /// </summary>
        /// <param name="programId"></param>
        /// <returns></returns>
        public bool UndoDeletePrograms(string programId)
        {
            bool success = false;
            ProgramCode code = new ProgramCode();
            code.ProgramId = programId;
            success = sourceDal.UndoDeleteProgramCode(code);
            return success;
        }
        /// <summary>
        /// Populate deleted programs names in datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeletedPrograms_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// PopulateDeletedProgramNames
        /// </summary>
        private void PopulateDeletedProgramNames()
        {
            string programId = string.Empty;
            string langName = string.Empty;
            string progName = string.Empty;
            string createdDate = string.Empty;
            string lastModifiedDate = string.Empty;
            string filePath = string.Empty;
            string userId = string.Empty;

            try
            {
                dtDeletedPrograms = FetchAllDeletedPrograms();
                dgvDeletedPrograms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDeletedPrograms.AllowUserToAddRows = false;
                dgvDeletedPrograms.ColumnHeadersVisible = true;
                dgvDeletedPrograms.RowHeadersVisible = false;
                MakeDataGridReadOnly(dgvDeletedPrograms);
                DataTable dtFormattedProg = new DataTable("tbl_PROGRAMS_LOCAL_DB");
                dtFormattedProg.Columns.Add("PROG_ID", typeof(string));
                dtFormattedProg.Columns.Add("LANG_NAME", typeof(string));
                dtFormattedProg.Columns.Add("PROG_NAME", typeof(string));
                dtFormattedProg.Columns.Add("CREATED_DATE", typeof(string));
                dtFormattedProg.Columns.Add("LAST_MODIFIED_DATE", typeof(string));
                dtFormattedProg.Columns.Add("USER_ID", typeof(string));
                dtFormattedProg.Columns.Add("FILE_PATH", typeof(string));

                for (int i = 0; i < dtDeletedPrograms.Rows.Count; i++)
                {
                    programId = dtDeletedPrograms.Rows[i]["PROG_ID"].ToString().Trim();
                    langName = sourceDal.GetLangNameByLangId(dtDeletedPrograms.Rows[i]["LANG_ID"].ToString()).Trim();
                    progName = dtDeletedPrograms.Rows[i]["PROG_NAME"].ToString().Trim();
                    lastModifiedDate = dtDeletedPrograms.Rows[i]["LAST_MODIFIED_DATE"].ToString().Trim();
                    createdDate = dtDeletedPrograms.Rows[i]["CREATED_DATE"].ToString().Trim();
                    filePath = dtDeletedPrograms.Rows[i]["FILE_PATH"].ToString().Trim();
                    userId = dtDeletedPrograms.Rows[i]["USER_ID"].ToString().Trim();
                    dtFormattedProg.Rows.Add(programId, langName, progName, createdDate, lastModifiedDate, userId, filePath);
                }
                /**************Miracle Lines**************/
                dgvDeletedPrograms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvDeletedPrograms.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                /*****************************************/
                dvDeletedProgramNames = dtFormattedProg.DefaultView;
                dvDeletedProgramNames.Sort = "PROG_ID";
                //dgvPrograms.Columns.Clear();
                dgvDeletedPrograms.DataSource = dvDeletedProgramNames;
                if (!dgvDeletedPrograms.Columns.Contains("btnUndoDeletePrograms"))
                AddButtonToDataGrid(dgvDeletedPrograms, "btnUndoDeletePrograms", "Undo Delete", string.Empty, 0);
                //dgvPrograms.Columns["FILE_PATH"].Visible = false;
                //dgvPrograms.Columns["USER_ID"].Visible = false;
                FormatDataView(dgvDeletedPrograms, 12, FontStyle.Regular);
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// void FormatDataGridView(DataGridView dgvPrograms)
        /// Format datagridview cells
        /// </summary>
        /// <param name="dgvPrograms"></param>
        //public static void FormatDataGridView(DataGridView dgvPrograms)
        //{
        //    dgvPrograms.RowsDefaultCellStyle.BackColor = Color.Bisque;
        //    dgvPrograms.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
        //    dgvPrograms.DefaultCellStyle.SelectionBackColor = Color.Red;
        //    dgvPrograms.DefaultCellStyle.SelectionForeColor = Color.Yellow;
        //    dgvPrograms.Columns["PROG_ID"].DefaultCellStyle.Font = new Font("Calibri", 24, FontStyle.Italic);
        //    dgvPrograms.Columns["LANG_NAME"].DefaultCellStyle.Font = new Font("Calibri", 24, FontStyle.Italic);
        //    dgvPrograms.Columns["PROG_NAME"].DefaultCellStyle.Font = new Font("Calibri", 24, FontStyle.Italic);
        //    dgvPrograms.Columns["PROG_ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    dgvPrograms.Columns["LANG_NAME"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    dgvPrograms.Columns["PROG_NAME"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //}
           //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
           //MessageBox.Show(dgvUserData.SelectedCells[e.ColumnIndex].Value.ToString());

        /// <summary>
        /// selected rows index and values passed to Operation Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MakeDataGridReadOnly(DataGridView dgv)
        {
            try
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        dgv.Rows[i].Cells[j].ReadOnly = true;
                    }
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        private static void MakeRichTextBoxReadOnly(RichTextBox rtbSource)
        {
            try
            {
                if (rtbSource.ReadOnly == false)
                {
                    rtbSource.ReadOnly = true;
                    rtbSource.Refresh();
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// dgvUserData_CellDoubleClick_1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUserData_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ShowOperationForm(e.RowIndex,dvUserData);
        }
        /// <summary>
        /// ShowOperationForm
        /// </summary>
        private void ShowOperationForm()
        {
            UserOperation op = null;
            try
            {
                op = new UserOperation();
                GlobalPageTracker.operationPageObj = op;
                GlobalPageTracker.operationPageObj.Show();
                GlobalPageTracker.operationPageObj.Focus();
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// ShowOperationForm
        /// </summary>
        /// <param name="index"></param>
        /// <param name="dvUsers"></param>
        private void ShowOperationForm(int index,DataView dvUsers)
        {
            UserOperation op = null;
            try
            {
                dataRowIndex = index;
                op = new UserOperation(dataRowIndex, dvUsers);
                GlobalPageTracker.operationPageObj = op;
                GlobalPageTracker.operationPageObj.Show();
                GlobalPageTracker.operationPageObj.Focus();
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// PopulateDataGrid
        /// </summary>
        public void PopulateUserDataGrid()
        {
            DataTable dt = null;
            DataColumn role_name = null;
            try
            {
                dt = FetchAllUsers();
                //dgvUserData.Columns.Clear();
                dgvUserData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUserData.AllowUserToAddRows = false;
                dgvUserData.RowHeadersVisible = false;
                role_name = new DataColumn("ROLE_NAME", typeof(string));
                dt.Columns.Add(role_name);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["ROLE_NAME"] = GetRoleNameByRoleId(dt.Rows[i]["ROLE_ID"].ToString().Trim());
                }
                GlobalClass.dt = dt;
                /*************************************************/
                dvUserData = dt.DefaultView;
                dvUserData.Sort = "USER_ID";
                /*************************************************/
                MakeDataGridReadOnly(dgvUserData);
                if (!dgvUserData.Columns.Contains("chkBxSelect"))
                {
                    AddCheckBox(dgvUserData);
                }
                dgvUserData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvUserData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvUserData.DataSource = dvUserData;
                FormatDataView(dgvUserData,12,FontStyle.Regular);
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// GetRoleNameByRoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        private string GetRoleNameByRoleId(string roleId)
        {
            string roleName = string.Empty;
            try
            {
                roleName = sourceDal.GetRoleNameByRoleId(roleId);
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            return roleName;
        }
        /// <summary>
        /// FillTabsByRole
        /// </summary>
        /// <param name="dataGridView"></param>
        public void FillTabsByRole(DataGridView dataGridView)
        {
            int i = 0;
            List<int> ChkedRow = new List<int>();
            try
            {
                for (i = 0; i <= dataGridView.RowCount - 1; i++)
                {
                    if (Convert.ToBoolean(dataGridView.Rows[i].Cells["chkBxSelect"].Value) == true)
                    {
                        ChkedRow.Add(i);
                    }
                }

                if (ChkedRow.Count == 0)
                {
                    checkedListBox1.Items.Clear();
                    MessageBox.Show("SELECT ATLEAST ONE ROLE !", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ChkedRow.Count == 1)
                {
                    string roleName = dataGridView.Rows[ChkedRow[0]].Cells["ROLE_NAME"].Value.ToString();
                    SourceCode.ToggleEnableOrDisable(true, btnEdit);
                    GetPanelsByRole(roleName, checkedListBox1);
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// RowCheckBoxClick
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="function"></param>
        private void RowCheckBoxClick(DataGridView dataGridView,string function)
        {
            int i = 0;
            List<int> ChkedRow = new List<int>();
            try
            {
                for (i = 0; i <= dataGridView.RowCount - 1; i++)
                {
                    if (Convert.ToBoolean(dataGridView.Rows[i].Cells["chkBxSelect"].Value) == true)
                    {
                        ChkedRow.Add(i);
                    }
                }

                if (ChkedRow.Count == 0)
                {
                    string msg = string.Empty;
                    string USER_MESSAGE = "SELECT ATLEAST ONE USER !";
                    string ROLE_MESSAGE = "SELECT ATLEAST ONE ROLE !";
                    if (function == MANAGE_USER_LABEL.SHOW_USER_OPERATION_FORM)
                    {
                        msg = USER_MESSAGE;
                    }
                    else if (function == MANAGE_ROLE_LABEL.SHOW_ROLE_OPERATION_FORM)
                    {
                        msg = ROLE_MESSAGE;
                    }
                    else if (function == MANAGE_ROLE_LABEL.DELETE_ROLE_BY_ROLE_ID)
                    {
                        msg = ROLE_MESSAGE;
                    }
                    MessageBox.Show(msg, "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkedListBox1.Items.Clear();
                }
                else if (ChkedRow.Count == 1)
                {
                    if (function == MANAGE_USER_LABEL.SHOW_USER_OPERATION_FORM)
                    {
                        ShowOperationForm(ChkedRow[0], dvUserData);
                    }
                    else if (function == MANAGE_ROLE_LABEL.SHOW_ROLE_OPERATION_FORM)
                    {
                        ShowRoleOperationForm(ChkedRow[0], dvRoleData);
                    }
                    else if (function == MANAGE_ROLE_LABEL.DELETE_ROLE_BY_ROLE_ID)
                    {
                        role = new Roles();
                        role.RoleId = dataGridView.Rows[ChkedRow[0]].Cells["ROLE_ID"].Value.ToString().Trim();
                        DialogResult result = MessageBox.Show("DO YOU WANT TO DELETE SELECTED ROLE ?",
                        "CONFIRMATION",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            DeleteRoleByRoleId(role.RoleId);
                        }
                    }
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// DeleteRoleByRoleId
        /// </summary>
        /// <param name="roleId"></param>
        private void DeleteRoleByRoleId(string roleId)
        {
            role = new Roles();
            user = new Users();
            role.RoleId = roleId;
            string message = string.Empty;
            string userId = string.Empty;
            try
            {
                if (sourceDal.DeleteRoleByRoleId(role, out message, out userId))
                {
                    MessageBox.Show(MESSAGE_LABEL.SUCCESS_MESSSAGE + " FOR " + role.RoleId + "!", "SUCCESS_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FetchAllRoles();
                }
                else
                {
                    if (message.Equals(MESSAGE_LABEL.EXCEPTION_MESSAGE))
                    {
                        MessageBox.Show(MESSAGE_LABEL.EXCEPTION_MESSAGE, "EXCEPTION_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (message.Equals(MESSAGE_LABEL.ERROR_MESSAGE))
                    {
                        user.UserId = userId;
                        MessageBox.Show(MESSAGE_LABEL.ERROR_MESSAGE + " " + user.UserId + "!", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// dgvUserData_CellClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUserData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AllowSingleCheckInCheckBox(dgvUserData,e);
        }
        /// <summary>
        /// AllowSingleCheckInCheckBox
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="e"></param>
        private void AllowSingleCheckInCheckBox(DataGridView dataGridView,DataGridViewCellEventArgs e)
        {
            try
            {
                SourceCode.ToggleEnableOrDisable(false, btnEdit, btnSave, btnCancel,checkedListBox1);
                checkedListBox1.Items.Clear();
                if (e.ColumnIndex == dataGridView.Rows[0].Cells["chkBxSelect"].ColumnIndex)
                {
                    if (Convert.ToBoolean(dataGridView.Rows[e.RowIndex].Cells["chkBxSelect"].Value) == false)
                    {
                        for (int i = 0; i <= dataGridView.Rows.Count - 1; i++)
                        {
                            dataGridView.Rows[i].Cells["chkBxSelect"].Value = false;
                        }
                    }
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// ManageUser
        /// </summary>
        private void ManageUser()
        {
            try
            {
                GlobalClass.ShowOperationForm = MANAGE_USER_LABEL.SHOW_USER_OPERATION_FORM;
                if (treeView1.SelectedNode != null)
                {
                    switch (treeView1.SelectedNode.Name)
                    {
                        case MANAGE_USER_LABEL.NODE_FETCH_ALL_USERS:
                            GetAllUsers();
                            break;
                        case MANAGE_USER_LABEL.NODE_ADD_USERS:
                            AddUser();
                            break;
                        case MANAGE_USER_LABEL.NODE_EDIT_USERS:
                            EditUser(GlobalClass.ShowOperationForm);
                            break;
                    }
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// GetAllUsers
        /// </summary>
        private void GetAllUsers()
        {
            PopulateUserDataGrid();
        }
        /// <summary>
        /// AddUser
        /// </summary>
        private void AddUser()
        {
            ShowOperationForm();
        }
        /// <summary>
        /// EditUser
        /// </summary>
        /// <param name="function"></param>
        private void EditUser(string function)
        {
            RowCheckBoxClick(dgvUserData,function);
        }
        /// <summary>
        /// ManageRoles
        /// </summary>
        private void ManageRoles()
        {
            try
            {
                GlobalClass.ShowRoleOperationForm = MANAGE_ROLE_LABEL.SHOW_ROLE_OPERATION_FORM;
                if (treeView2.SelectedNode != null)
                {
                    switch (treeView2.SelectedNode.Name)
                    {
                        case MANAGE_ROLE_LABEL.NODE_FETCH_ALL_ROLES:
                            FetchAllRoles();
                            break;
                        case MANAGE_ROLE_LABEL.NODE_FETCH_TABS_BY_ROLE:
                            FetchTabsByRole();
                            break;
                        case MANAGE_ROLE_LABEL.NODE_ADD_ROLE:
                            AddRole();
                            break;
                        case MANAGE_ROLE_LABEL.NODE_EDIT_ROLE:
                            EditRole(GlobalClass.ShowRoleOperationForm);
                            break;
                        case MANAGE_ROLE_LABEL.NODE_DELETE_ROLE:
                            DeleteRole();
                            break;
                    }
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /*******Manage Roles******/
        /// <summary>
        /// AddRole
        /// </summary>
        private void AddRole()
        {
            ShowRoleOperationForm();
        }
        /// <summary>
        /// EditRole
        /// </summary>
        /// <param name="function"></param>
        private void EditRole(string function)
        {
            RowCheckBoxClick(dgvRoleData, function);
        }
        /// <summary>
        /// DeleteRole
        /// </summary>
        private void DeleteRole()
        {
            RowCheckBoxClick(dgvRoleData,MANAGE_ROLE_LABEL.DELETE_ROLE_BY_ROLE_ID);
        }
        /***********************/
        /// <summary>
        /// ShowRoleOperationForm
        /// </summary>
        private void ShowRoleOperationForm()
        {
            RoleOperation op = null;
            try
            {
                op = new RoleOperation();
                GlobalPageTracker.roleOperationPageObj = op;
                GlobalPageTracker.roleOperationPageObj.Show();
                GlobalPageTracker.roleOperationPageObj.Focus();
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// ShowRoleOperationForm
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="dvRoles"></param>
        private void ShowRoleOperationForm(int rowId,DataView dvRoles)
        {            
          RoleOperation op = null;
          try
          {
              op = new RoleOperation(rowId, dvRoles);
              GlobalPageTracker.roleOperationPageObj = op;
              GlobalPageTracker.roleOperationPageObj.Show();
              GlobalPageTracker.roleOperationPageObj.Focus();
          }
        catch (Exception exHandler)
        {
            ErrorLog.ErrorRoutine(exHandler);
        }
        }
        /// <summary>
        /// FetchTabsByRole
        /// </summary>
        private void FetchTabsByRole()
        {
            try
            {
                checkedListBox1.Items.Clear();
                GetAllPanels(checkedListBox1);
                ClearAllCheckBox(checkedListBox1);
                FillTabsByRole(dgvRoleData);
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// treeView1_DoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            ManageUser();
        }
        /// <summary>
        /// treeView2_DoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView2_DoubleClick(object sender, EventArgs e)
        {
            ManageRoles();
        }
        /// <summary>
        /// dgvRoleData_CellClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRoleData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AllowSingleCheckInCheckBox(dgvRoleData, e);
        }
        /// <summary>
        /// SearchFilter
        /// </summary>
        public void SearchFilter()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    if (dgvUserData.Rows.Count > 0 && tabControl1.SelectedTab.Text == TABS.USERS)
                    {
                        SearchByKeywords(dgvUserData, dvUserData, txtSearch.Text.Trim());
                    }
                    else if (dgvRoleData.Rows.Count > 0 && tabControl1.SelectedTab.Text == TABS.ROLES)
                    {
                        SearchByKeywords(dgvRoleData, dvRoleData, txtSearch.Text.Trim());
                    }
                    else if (dgvDeletedPrograms.Rows.Count > 0 && tabControl1.SelectedTab.Text == TABS.ARCHIVE)
                    {
                        SearchByKeywords(dgvDeletedPrograms, dvDeletedProgramNames, txtSearch.Text.Trim());
                    }
                }
                else if (tabControl1.SelectedTab.Text == TABS.USERS)
                {
                    ClearFilter(dgvUserData, dvUserData);
                }
                else if (tabControl1.SelectedTab.Text == TABS.ROLES)
                {
                    ClearFilter(dgvRoleData, dvRoleData);
                }
                else if (tabControl1.SelectedTab.Text == TABS.ARCHIVE)
                {
                    ClearFilter(dgvDeletedPrograms, dvDeletedProgramNames);
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// ClearFilter
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="dataView"></param>
        public static void ClearFilter(DataGridView dataGridView, DataView dataView)
        {
            try
            {
                dataView.RowFilter = string.Empty;
                dataGridView.DataSource = dataView;
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// SearchProgramByKeywords
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="dataView"></param>
        /// <param name="keyWord"></param>
        public void SearchProgramByKeywords(DataGridView dataGridView, DataView dataView, string keyWord)
        {
            string filter = "";
            string[] keyWords = null;
            string column = "";
            string langNameColumn = string.Empty;
            string IN = " IN ";
            string AND = " AND ";
            string OR = " OR ";
            langNameColumn = dataView.ToTable().Columns["LANG_NAME"].ColumnName;
            
            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWords = keyWord.Split(' ');
            }
            try
            {
                if (!string.IsNullOrEmpty(keyWord) && keyWords != null)
                {
                    foreach (string word in keyWords)
                    {
                        if (filter.Length == 0)
                        {
                            filter += "(";
                            for (int i = 0; i < dataView.ToTable().Columns.Count; i++)
                            {
                                if (column.Length == 0)
                                {
                                    column = dataView.ToTable().Columns[i].ColumnName;
                                    filter += "(" + column + " LIKE " + "'%" + word + "%')";
                                }
                                else
                                {
                                    column = dataView.ToTable().Columns[i].ColumnName;
                                    filter += OR + "(" + column + " LIKE " + "'%" + word + "%')";
                                }
                            }
                            filter += ")";
                            column = "";
                        }
                        else
                        {
                            filter += AND + "(";
                            for (int i = 0; i < dataView.ToTable().Columns.Count; i++)
                            {
                                if (column.Length == 0)
                                {
                                    column = dataView.ToTable().Columns[i].ColumnName;
                                    filter += "(" + column + " LIKE " + "'%" + word + "%')";
                                }
                                else
                                {
                                    column = dataView.ToTable().Columns[i].ColumnName;
                                    filter += OR + "(" + column + " LIKE " + "'%" + word + "%')";
                                }
                            }
                            filter += ")";
                        }
                    }
                    filter += AND + "(" + langNameColumn + IN + "(" + sb.ToString() + "))";
                }
                else
                {
                    filter += langNameColumn + IN + "(" + sb.ToString() + ")";
                }
                dataView.RowFilter = filter;
                dvProgramNames = dataView;
                dataGridView.DataSource = dataView;
                FormatDataView(dataGridView, 24, FontStyle.Bold);
                filter = string.Empty;
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// SearchByKeywords
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="dataView"></param>
        /// <param name="keyWord"></param>
        public static void SearchByKeywords(DataGridView dataGridView, DataView dataView, string keyWord)
        {
            string filter = "";
            string column = "";
            string OR = " OR ";
            string AND = " AND ";
            string[] keyWords = null;
            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWords = keyWord.Split(' ');
            }
            try
            {
                if (!string.IsNullOrEmpty(keyWord) && keyWords != null)
                {
                    foreach (string word in keyWords)
                    {
                        if (filter.Length == 0)
                        {
                            filter += "(";
                            for (int i = 0; i < dataView.ToTable().Columns.Count; i++)
                            {
                                if (column.Length == 0)
                                {
                                    column = dataView.ToTable().Columns[i].ColumnName;
                                    filter += column + " LIKE " + "'%" + word + "%'";
                                }
                                else
                                {
                                    column = dataView.ToTable().Columns[i].ColumnName;
                                    filter += OR + column + " LIKE " + "'%" + word + "%'";
                                }
                            }
                            filter += ")";
                            column = "";
                        }
                        else
                        {
                            filter += AND + "(";
                            for (int i = 0; i < dataView.ToTable().Columns.Count; i++)
                            {
                                if (column.Length == 0)
                                {
                                    column = dataView.ToTable().Columns[i].ColumnName;
                                    filter += column + " LIKE " + "'%" + word + "%'";
                                }
                                else
                                {
                                    column = dataView.ToTable().Columns[i].ColumnName;
                                    filter += OR + column + " LIKE " + "'%" + word + "%'";
                                }
                            }
                            filter += ")";
                        }
                    }
                }
                dataView.RowFilter = filter;
                dataGridView.DataSource = dataView;
                FormatDataView(dataGridView,12,FontStyle.Regular);
                filter = string.Empty;
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        public static void FormatDataView(DataGridView dgv,int fontSize,FontStyle style)
        {
            dgv.RowsDefaultCellStyle.BackColor = Color.Bisque;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            dgv.DefaultCellStyle.SelectionBackColor = Color.Green;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Yellow;
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].DefaultCellStyle.Font = new Font("Calibri", fontSize, style);
                dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        /// <summary>
        /// public static void AutoResizeCol(DataGridView dgv)
        /// </summary>
        /// <param name="dgv"></param>
        public static void AutoResizeCol(DataGridView dgv)
        {
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.AutoResizeColumn(i);
            }
        }
        /// <summary>
        /// txtSearch_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
           // Determine whether the keystroke is a delete key.
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    txtSearch.Text = string.Empty;
                }
                // if pressed key is enter
                if (e.KeyCode == Keys.Enter)
                {
                    SearchFilter();
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// txtSearch_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == TABS.SOURCE_CODE)
            {
                SearchProgramFilter();
            }
            else
            {
                SearchFilter();
            }
        }
        /// <summary>
        /// btnExport_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (exportToExcelFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportFileToExcel();
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// btnExportUser_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (exportToExcelFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportFileToExcel();
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// ExportFileToExcel
        /// </summary>
        private void ExportFileToExcel()
        {
            try
            {
                string excelFileName = exportToExcelFileDialog.FileName;
                if (Export(excelFileName))
                {
                    MessageBox.Show("FILE EXPORTED TO EXCEL SHEET SUCCESSFULLY!", "SUCCESS_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ERROR IN EXPORTING FILE!", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// Export
        /// </summary>
        /// <param name="excelFileName"></param>
        /// <returns></returns>
        private bool Export(string excelFileName)
        {
            bool flag = false;
            DataGridView dgvExcel = null;
            try
            {
                if (tabControl1.SelectedTab.Text == TABS.USERS)
                {
                    dgvExcel = dgvUserData;
                }
                else if (tabControl1.SelectedTab.Text == TABS.ROLES)
                {
                    dgvExcel = dgvRoleData;
                }
                if (!string.IsNullOrEmpty(excelFileName) && dgvExcel != null)
                {
                    flag = ExportToExcel(excelFileName, dgvExcel);
                }
                else if (string.IsNullOrEmpty(excelFileName))
                {
                    MessageBox.Show("PLEASE ENTER A FILE NAME!", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dgvExcel == null)
                {
                    MessageBox.Show("SOME EXCEPTION OCCURED!", "EXCEPTION_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            return flag;
        }
        /// <summary>
        /// ExportToExcel
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dgvExcel"></param>
        /// <returns></returns>
        private bool ExportToExcel(string fileName, DataGridView dgvExcel)
        {
            bool flag = false;
            try
            {
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                Int16 i, j;
                xlApp = new Excel.ApplicationClass();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                // storing header part in Excel
                for (i = 1; i < dgvExcel.Columns.Count; i++)
                {
                    xlWorkSheet.Cells[1, i] = dgvExcel.Columns[i - 1].HeaderText;
                }
                // storing Each row and column value to excel sheet
                for (i = 0; i < dgvExcel.Rows.Count - 1; i++)
                {
                    for (j = 0; j < dgvExcel.Columns.Count - 1; j++)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = dgvExcel.Rows[i].Cells[j].Value.ToString();
                    }
                }
                xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
                if (File.Exists(fileName))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            return flag;
        }
        /// <summary>
        /// releaseObject
        /// </summary>
        /// <param name="obj"></param>
        private void releaseObject(object obj)
        {
                try
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
                catch (Exception ex)
                {
                    obj = null;
                    MessageBox.Show("EXCEPTION OCCURED WHILE RELEASING OBJECT " + ex.ToString().ToUpper().Trim(), "EXCEPTION_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    GC.Collect();
                }
        }
        /// <summary>
        /// SearchProgramFilter
        /// </summary>
        public void SearchProgramFilter()
        {
            string searchString = txtSearchBox.Text.Trim();
            try
            {
                if (!isSelected)
                {
                    if (sb.Length > 0)
                    {
                        sb.Remove(0, sb.Length);
                    }
                    for (int i = 0; i < dtLangCode.Rows.Count; i++)
                    {
                        sb.Append("'").Append(dtLangCode.Rows[i]["LANG_NAME"].ToString().Trim()).Append("'").Append(ccbCodeLanguage.ValueSeparator);
                    }
                }
                if (tabControl1.SelectedTab.Text == TABS.SOURCE_CODE)
                {
                    System.Diagnostics.StackFrame fr = new System.Diagnostics.StackFrame(1);
                    if (fr.GetMethod().Name == "txtSearch_TextChanged"
                        || fr.GetMethod().Name == "pictureBox1_Click")
                    {
                        searchString = txtSearch.Text.Trim();
                    }
                    else if (fr.GetMethod().Name == "btnSearchCode_Click"
                        || fr.GetMethod().Name == "txtSearchBox_KeyDown"
                        || fr.GetMethod().Name == "txtSearchBox_TextChanged")
                    {
                        searchString = txtSearchBox.Text.Trim();
                    }
                    if (string.IsNullOrEmpty(searchString))
                    {
                        PopulateProgramNames(false);
                        if (cbLanguage.SelectedIndex == -1)
                        {
                            PopulateLanguageCode(false);
                        }
                        ClearFilter(dgvPrograms, dvProgramNames);
                    }
                    else
                    {
                        SearchProgramByKeywords(dgvPrograms, dvProgramNames, searchString);
                    }
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// ccbCodeLanguage_DropDownClosed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccbCodeLanguage_DropDownClosed(object sender, EventArgs e)
        {
            HandleCheckedItems();
        }
        /// <summary>
        /// HandleCheckedItems
        /// </summary>
        private void HandleCheckedItems()
        {
            // Checked items can be found via the CheckedItems property.
            try
            {
                sb.Remove(0, sb.Length);
                foreach (LANGUAGE item in ccbCodeLanguage.CheckedItems)
                {
                    sb.Append("'").Append(item.Lang_name).Append("'").Append(ccbCodeLanguage.ValueSeparator);
                }
                if (sb.Length < ccbCodeLanguage.ValueSeparator.Length)
                {
                    isSelected = false;
                }
                else
                {
                    sb.Remove(sb.Length - ccbCodeLanguage.ValueSeparator.Length, ccbCodeLanguage.ValueSeparator.Length);
                    isSelected = true;
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// btnSearchCode_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchCode_Click(object sender, EventArgs e)
        {
            SearchProgramFilter();
        }
        /// <summary>
        /// txtSearchBox_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Determine whether the keystroke is a delete key.
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    txtSearchBox.Text = string.Empty;
                }
                // if pressed key is enter
                if (e.KeyCode == Keys.Enter)
                {
                    SearchProgramFilter();
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// txtSearchBox_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchBox_TextChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == TABS.SOURCE_CODE)
            {
                SearchProgramFilter();
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// dgvPrograms_CellDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPrograms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowColorSyntaxForm(e.RowIndex, dvProgramNames);
        }
        /// <summary>
        /// ShowColorSyntaxForm
        /// </summary>
        /// <param name="index"></param>
        /// <param name="dvPrograms"></param>
        private void ShowColorSyntaxForm(int index,DataView dvPrograms)
        {
            try
            {
                ColorSyntax.ADD_SOURCECODE = false;
                Event = false;
                int rowid = index;
                GlobalClass.index = index;
                DataTable dt = null;
                string SOURCE_CODE = string.Empty;
                string prog_id = string.Empty;
                string createdFilePath = string.Empty;
                string existingFilePath = string.Empty;
                string lastModifiedDate = string.Empty;
                ProgramCode CODE = new ProgramCode();
                DateTime dbTimeStamp = DateTime.Now;
                DateTime fileTimeStamp = DateTime.Now;
                CultureInfo provider = CultureInfo.InvariantCulture;
                string langName = string.Empty;
                string filePath = string.Empty;
                string progName = string.Empty;
                string userId = string.Empty;

                try
                {
                    GlobalClass.dt = dvPrograms.ToTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("EXCEPTION :" + ex.Message, "EXCEPTION_MESAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                prog_id = GlobalClass.dt.Rows[index]["PROG_ID"].ToString().Trim();
                lastModifiedDate = GlobalClass.dt.Rows[index]["LAST_MODIFIED_DATE"].ToString().Trim();
                langName = GlobalClass.dt.Rows[index]["LANG_NAME"].ToString().Trim();
                dbTimeStamp = DateTime.ParseExact(lastModifiedDate, GlobalClass.SOURCE_CODE_DB_FORMAT, provider);
                filePath = GlobalClass.dt.Rows[index]["FILE_PATH"].ToString().Trim();
                progName = GlobalClass.dt.Rows[index]["PROG_NAME"].ToString().Trim();
                userId = GlobalClass.dt.Rows[index]["USER_ID"].ToString().Trim();
                GlobalClass.CurrentProgramName = progName;
                GlobalClass.CurrentLangName = langName;
                //if file doesnot exist create a backup of it
                if (!CodeFileExists(prog_id,out existingFilePath))
                {
                    dt = sourceDal.GetProgramCodeByProgramId(prog_id.Trim());
                    SOURCE_CODE = dt.Rows[0]["PROG_CODE"].ToString().Trim();
                    CODE.ProgCode = SOURCE_CODE;
                    if (CreateFileBackup(dbTimeStamp, CODE, langName, prog_id, out createdFilePath))
                    {
                        string relativePath = createdFilePath.Replace(FileBackUp.RootDirectory.Replace(GetDirectoryName(FileBackUp.RootDirectory), string.Empty), string.Empty).Trim();
                        if (relativePath.Equals(filePath))
                        {
                            MessageBox.Show(string.Format("BACKUP FILE {0} CREATED SUCCESSFULLY!!!", Path.GetFileName(createdFilePath)));
                        }
                    }
                }
                else
                {
                    //else if backup file exist then check last modified date with the central database.

                    fileTimeStamp = GetLastModifiedFileDate(Path.GetFileName(existingFilePath));

                    //if back up file is an old oudated file then replace it with latest version
                    if (DateTime.Compare(fileTimeStamp, dbTimeStamp) < 0)
                    {
                        dt = sourceDal.GetProgramCodeByProgramId(prog_id.Trim());
                        SOURCE_CODE = dt.Rows[0]["PROG_CODE"].ToString().Trim();
                        File.Delete(existingFilePath);
                        CreateFileBackup(dbTimeStamp, CODE, langName, prog_id, out createdFilePath);
                        CODE.ProgramId = prog_id;
                        string relativePath = createdFilePath.Replace(FileBackUp.RootDirectory.Replace(GetDirectoryName(FileBackUp.RootDirectory), string.Empty), string.Empty);
                        CODE.Filepath = relativePath;
                        sourceDal.InsertOrUpdateProgramCode(CODE, userData.UserData);
                    }
                    else if(DateTime.Compare(fileTimeStamp, dbTimeStamp) == 0)
                    {
                        //if backup file is same as db file do not hit db to fetch the code 
                        //fetch the code from the local file
                        SOURCE_CODE = backUp.DeSerializeFile(existingFilePath);
                    }
                }
                SyntexColor = new ColorSyntax(SOURCE_CODE.Trim(),userId);
                SyntexColor.UpdateCode = new UpdateSourceCode(this.UpdateSourceCode);
                SyntexColor.DeleteSourceCode = new DeleteCode(this.DeleteCode);
                SyntexColor.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColorSyntax_FormClosing);
                //SyntexColor.Text = progName;
                ColorSyntax.ADD_SOURCECODE = false;
                GlobalPageTracker.colorSyntexPageObj = SyntexColor;
                SyntexColor.Show();
                SyntexColor.Focus();
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        public DateTime GetLastModifiedFileDate(string fileName)
        {
            DateTime modifiedDate = DateTime.Now;
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                string[] fileString = fileName.Split('_');
                string[] dateString = fileString[1].Split('-');
                string[] timeString = fileString[2].Split('-');
                string month = dateString[0];
                string day = dateString[1];
                string year = dateString[2];
                string hour = timeString[0];
                string minute = timeString[1];
                string second = timeString[2];
                string milliSeconds = timeString[3].Substring(0, 4);
                string timeFlag = timeString[3].Substring(5, 2);
                string format = GlobalClass.SOURCE_CODE_FILE_FORMAT;
                string date = month + "-" +
                                    day + "-" +
                                    year + "_" +
                                    hour + "-" +
                                    minute + "-" +
                                    second + "-" +
                                    milliSeconds + " " +
                                    timeFlag;
                modifiedDate = DateTime.ParseExact(date, format, provider);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return modifiedDate;
        }
        public bool CodeFileExists(string progId,out string path)
        {
            bool exists = false;
            path = string.Empty;
            ArrayList fileList = null;
            string filePattern = string.Format("{0}*.scd", progId);
            fileList = backUp.DirSearch(FileBackUp.RootDirectory, filePattern, true);
            if (fileList.Count > 0)
            {
                foreach (string file in fileList)
                {
                    path = file;
                }
                exists = true;
            }
            return exists;
        }
        /// <summary>
        /// RemoveTab(TabControl tabControl, TabPage tabPage)
        /// </summary>
        /// <param name="tabControl"></param>
        /// <param name="tabPage"></param>
        private void RemoveTab(TabControl tabControl, TabPage tabPage)
        {
            tabControl.TabPages.Remove(tabPage);
        }
        /// <summary>
        /// AddTab
        /// </summary>
        /// <param name="tabControl"></param>
        /// <param name="tabPage"></param>
        private void AddTab(TabControl tabControl, TabPage tabPage)
        {
            tabControl.TabPages.Add(tabPage);
        }
        /// <summary>
        /// GetTabs
        /// </summary>
        private string[] GetTabs()
        {
            int errorCode = 1;
            DataTable dtTabs = null;
            try
            {
                dtTabs = sourceDal.GetPanelsByRole(userData.RoleData, out errorCode);
                DataView tabView = new DataView(dtTabs);
                tabView.RowFilter = "ACCESS = 1";
                DataTable distinctTabs = new DataTable();
                distinctTabs = tabView.ToTable(true, "PANEL_NAME");
                int tabIndex = 0;
                userData.ACCESSIBLE_TABS = new string[distinctTabs.Rows.Count];
                for (int j = 0; j < distinctTabs.Rows.Count; j++)
                {
                    userData.ACCESSIBLE_TABS[tabIndex++] = distinctTabs.Rows[j]["PANEL_NAME"].ToString().Trim();
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
            return userData.ACCESSIBLE_TABS;
        }
        /// <summary>
        /// RemoveNonAccessibleTabs
        /// </summary>
        /// <param name="tabs"></param>

        private void RemoveNonAccessibleTabs(Dictionary<string, bool> tabs)
        {
            try
            {
                foreach (var tab in tabs)
                {
                    if (tab.Value == true)
                    {
                        TabUtils.ShowPage(tab.Key, tabControl1);
                    }
                    else if (tab.Value == false)
                    {
                        TabUtils.HidePage(tab.Key, tabControl1);
                    }
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// ScanForAccessibleTabs
        /// </summary>
        public void UpdateAccessibleTabs()
        {
            try
            {
                userData.ACCESSIBLE_TABS = GetTabs();
                Dictionary<string, bool> tabs = new Dictionary<string, bool>();
                List<string> invisibleTabs = new List<string>();
                tabs.Clear();
                invisibleTabs.Clear();
                if (tabControls != null && invisibleTabs != null)
                {
                    for (int i = 0; i < tabControls.Length; i++)
                    {
                        invisibleTabs.Add(tabControls[i].Name);
                    }
                }
                for (int j = 0; j < userData.ACCESSIBLE_TABS.Length; j++)
                {
                    if (invisibleTabs.Contains<string>(userData.ACCESSIBLE_TABS[j]))
                    {
                        invisibleTabs.Remove(userData.ACCESSIBLE_TABS[j]);
                    }
                }
                foreach (string visibleTab in userData.ACCESSIBLE_TABS)
                {
                    tabs.Add(visibleTab, true);
                }
                foreach (string invisibleTab in invisibleTabs)
                {
                    tabs.Add(invisibleTab, false);
                }
                RemoveNonAccessibleTabs(tabs);
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        public void UpdateAccessibleControls()
        {
            try
            {
                bool isRolePermissions = false;
                bool isProgramTab = false;
                isProgramTab = GetAccessibleTabs(out isRolePermissions);
                if (isProgramTab || isRolePermissions)
                {
                    if (isProgramTab)
                    {
                        PopulateProgramNames(false);
                        if (cbLanguage.SelectedIndex == -1)
                        {
                            PopulateLanguageCode(false);
                        }
                    }
                    if (isRolePermissions)
                    {
                        IntializeControlsAndPermissions();
                    }
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        /// <summary>
        /// tabControl1_SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAccessibleTabs();
        }
        /// <summary>
        /// CheckForAccesibleTabs
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void CheckForAccesibleTabs(object obj, EventArgs e)
        {
            UpdateAccessibleTabs();
        }
        /// <summary>
        /// StartTimer
        /// </summary>
        public void StartTimer()
        {
            timer.Start();
        }
        /// <summary>
        /// StopTimer
        /// </summary>
        public  void StopTimer()
        {
            timer.Stop();
        }
        /// <summary>
        /// linkLogLbl_LinkClicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLogLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                MainForm logViewr = null;
                logViewr = new MainForm();
                GlobalPageTracker.loggerFormObj = logViewr;
                GlobalPageTracker.loggerFormObj.Show();
                GlobalPageTracker.loggerFormObj.Focus();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
        /// <summary>
        /// private void SourceCode_Resize(object sender, EventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceCode_Resize(object sender, EventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    lblUser.Text = userName;
                }
                else if (WindowState == FormWindowState.Minimized)
                {
                    lblUser.Text = AutoEllipsis.Ellipsis.Compact(userName, lblUser, AutoEllipsis.EllipsisFormat.End);
                }
                
                DataGridView currentDatagridView = dgvPrograms;
                if (WindowState != prevWindowState && WindowState !=
                FormWindowState.Minimized)
                {
                    prevWindowState = this.WindowState;
                    if (tabControl1.TabPages.Count > 0)
                    {
                        switch (tabControl1.SelectedTab.Text)
                        {
                            case TABS.SOURCE_CODE: currentDatagridView = dgvPrograms;
                                break;
                            case TABS.USERS: currentDatagridView = dgvUserData;
                                break;
                            case TABS.ROLES: currentDatagridView = dgvRoleData;
                                break;
                        }
                        AutoResizeDataGridview.ResizeGrid(currentDatagridView, ref prevWidth);
                    }
                }
                else
                {
                    prevWindowState = this.WindowState;
                    if (tabControl1.TabPages.Count > 0)
                    {
                        switch (tabControl1.SelectedTab.Text)
                        {
                            case TABS.SOURCE_CODE: currentDatagridView = dgvPrograms;
                                break;
                            case TABS.USERS: currentDatagridView = dgvUserData;
                                break;
                            case TABS.ROLES: currentDatagridView = dgvRoleData;
                                break;
                        }
                        AutoResizeDataGridview.ResizeGrid(currentDatagridView, ref prevWidth);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
        /// <summary>
        /// private void dgvRoleData_DataSourceChanged(object sender, EventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRoleData_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    prevWindowState = WindowState;
                    AutoResizeDataGridview.ResizeGrid(dgvRoleData, ref prevWidth);
                }
                checkedListBox1.Items.Clear();
                ToggleEnableOrDisable(false, btnEdit, btnSave, btnCancel, checkedListBox1);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
        /// <summary>
        /// private void dgvPrograms_DataSourceChanged(object sender, EventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPrograms_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    prevWindowState = WindowState;
                    AutoResizeDataGridview.ResizeGrid(dgvPrograms, ref prevWidth);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
        /// <summary>
        /// private void dgvUserData_DataSourceChanged(object sender, EventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUserData_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    prevWindowState = WindowState;
                    AutoResizeDataGridview.ResizeGrid(dgvUserData, ref prevWidth);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
        /// <summary>
        /// private void btnSaveSource_Click(object sender, EventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSource_Click(object sender, EventArgs e)
        {
            InsertProgramCode();
        }
        public void FillRichTextBox(string code)
        {
            richTextBox1.Text = code;
        }
        private void ToggleEvent(bool flag)
        {
            Event = flag;
        }
        private void DeleteCode()
        {
            ColorSyntax.ADD_SOURCECODE = false;
            Event = false;
            ProgramCode code = null;
            int index = GlobalClass.index;
            try
            {
                code = new ProgramCode();
                GlobalClass.dt = dvProgramNames.ToTable();
                code.ProgramId = GlobalClass.dt.Rows[index]["PROG_ID"].ToString().Trim();
                if (sourceDal.DeleteProgramCode(code))
                {
                    MessageBox.Show("SOURCE CODE DELETED SUCCESSFULLY!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvPrograms.Rows.RemoveAt(index);
                }
                else
                {
                    MessageBox.Show("ERROR OCCURED WHILE DELETING SOURCE CODE!", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
        private void UpdateSourceCode(string sourceCode)
        {
            ColorSyntax.ADD_SOURCECODE = false;
            Event = false;
            ProgramCode code = null;
            int index = GlobalClass.index;
            string existingFilePath = string.Empty;
            //string dbFilePath = GlobalClass.dt.Rows[index]["FILE_PATH"].ToString().Trim();
            string prog_code = sourceCode;
            string prog_id = GlobalClass.dt.Rows[index]["PROG_ID"].ToString().Trim();
            string langName = GlobalClass.dt.Rows[index]["LANG_NAME"].ToString().Trim();
            
            string path = string.Empty;
            try
            {
                code = new ProgramCode();
                code.ProgramId = prog_id;
                code.ProgCode = prog_code;
                DateTime timeStamp = DateTime.Now;
                code.LastModified = string.Format("{0:MM-dd-yyyy hh-mm-ss-ffff tt}", timeStamp);
                //string fileName = string.Format("{0}_{1:MM-dd-yyyy_hh-mm-ss-ffff tt}.scd", prog_id, timeStamp);
                //string pattern = Path.GetFileName(dbFilePath);
                //dbFilePath = dbFilePath.Replace(pattern, fileName);
                
                //if file exist replace it
                if (CodeFileExists(prog_id, out existingFilePath))
                {
                    File.Delete(existingFilePath);
                    if (CreateFileBackup(timeStamp, code, langName, prog_id, out path))
                    {
                        string relativePath = path.Replace(FileBackUp.RootDirectory.Replace(GetDirectoryName(FileBackUp.RootDirectory), string.Empty), string.Empty);
                        code.Filepath = relativePath;
                    }
                    else
                    {
                        MessageBox.Show("ERROR OCCURED WHILE UPDATING SOURCE BACKUP FILE CODE!", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (CreateFileBackup(timeStamp, code, langName, prog_id, out path))
                    {
                        string relativePath = path.Replace(FileBackUp.RootDirectory.Replace(GetDirectoryName(FileBackUp.RootDirectory), string.Empty), string.Empty);
                        code.Filepath = relativePath;
                    }
                    else
                    {
                        MessageBox.Show("ERROR OCCURED WHILE CREATING SOURCE BACKUP FILE CODE!", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            if (sourceDal.InsertOrUpdateProgramCode(code,userData.UserData))
            {
                MessageBox.Show("SOURCE CODE UPDATED SUCCESSFULLY!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ERROR OCCURED WHILE UPDATING SOURCE CODE!", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cbLanguage.SelectedIndex == -1)
            {
                PopulateLanguageCode(false);
            }
            PopulateProgramNames(false);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
        public void InsertProgramCode()
        {
            ColorSyntax.ADD_SOURCECODE = true;
            Users user = null;
            ProgramCode code = null;
            try
            {
                if (btnSaveSource.Text == BUTTONLABEL.SAVE &&
                    !string.IsNullOrEmpty(richTextBox1.Text.Trim()))
                {
                    code = new ProgramCode();
                    user = new Users();
                    richTextBox1.ReadOnly = true;
                    string programId = GenerateProgramId().Trim();
                    string progName = txtProgramName.Text.Trim();
                    string progCode = richTextBox1.Text.Trim();
                    string langId = string.Empty;
                    string langName = string.Empty;
                    string path = string.Empty;
                    if (cbLanguage.SelectedItem != null)
                    {
                        langId = ((LANGUAGE)cbLanguage.SelectedItem).Lang_id.ToString().Trim();
                        langName = ((LANGUAGE)cbLanguage.SelectedItem).Lang_name.ToString().Trim();
                    }
                    else
                    {
                        langId = null;
                    }
                    string userId = userData.UserData.UserId.Trim();
                    user.UserId = userId;
                    code.ProgramId = programId;
                    code.ProgName = progName;
                    code.ProgCode = progCode;
                    code.LangId = langId;
                    DateTime timeStamp = DateTime.Now;
                    code.LastModified = string.Format("{0:MM-dd-yyyy hh-mm-ss-ffff tt}",timeStamp);
                    code.CreatedDate = code.LastModified;

                    if (langId == string.Empty
                        || langId == null
                        || langId == "--Select--"
                        || progName == string.Empty
                        || progName == ""
                        || progName == null)
                    {
                        MessageBox.Show("PLEASE ENTER PROPER PROGRAM NAME OR SELECT A LANGUAGE", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (CreateFileBackup(timeStamp, code, langName, programId, out path))
                        {
                            string relativePath = path.Replace(FileBackUp.RootDirectory.Replace(GetDirectoryName(FileBackUp.RootDirectory), string.Empty), string.Empty);
                            code.Filepath = relativePath;
                            if (sourceDal.InsertOrUpdateProgramCode(code, user))
                            {
                                MessageBox.Show(MESSAGE_LABEL.SUCCESS_MESSSAGE_SOURCE, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearControlText(richTextBox1, txtProgramName, cbLanguage, ccbCodeLanguage);
                                if (cbLanguage.SelectedIndex == -1)
                                {
                                    PopulateLanguageCode(false);
                                }
                                PopulateProgramNames(false);
                            }
                            else
                            {
                                MessageBox.Show(MESSAGE_LABEL.ERROR_MESSSAGE_SOURCE, "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
        /// <summary>
        /// public static void ClearControlText(params Control[] c)
        /// </summary>
        /// <param name="c"></param>
        public static void ClearControlText(params Control[] controls)
        {
            try
            {
                foreach (Control c in controls)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        TextBox txtBox = (TextBox)c;
                        txtBox.Clear();
                    }
                    else if (c.GetType() == typeof(ComboBox))
                    {
                        ComboBox cbBox = (ComboBox)c;
                        cbBox.Items.Clear();
                    }
                    else if (c.GetType() == typeof(CheckedListBox))
                    {
                        CheckedListBox cbox = (CheckedListBox)c;
                        cbox.Items.Clear();
                    }
                    else if (c.GetType() == typeof(RichTextBox))
                    {
                        RichTextBox cbox = (RichTextBox)c;
                        cbox.Clear();
                    }
                    else if (c.GetType() == typeof(RadioButton))
                    {
                        RadioButton rb = (RadioButton)c;
                        rb.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
        public bool CreateFileBackup(DateTime timeStamp, ProgramCode code,string langName,string programId,out string path)
        {
            bool success = false;
            path = string.Empty;
            try
            {
                
                DateTime lastModifiedDate = timeStamp;
                string fileName = string.Format("{0}_{1:MM-dd-yyyy_hh-mm-ss-ffff tt}.scd", programId, lastModifiedDate);
                
                //create the specific lanuage directory
                if (backUp.CreateDirectory(langName))
                {
                    success = backUp.SerializeFile(fileName, langName, code, out path);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return success;
        }

        /// <summary>
        /// private string GenerateProgramId()
        /// </summary>
        /// <returns></returns>
        private string GenerateProgramId()
        {
            string programId = string.Empty;
            int maxId = 0;
            try
            {
                GlobalClass.globalProgDt = sourceDal.FetchAllPrograms();
                if (GlobalClass.globalProgDt.Rows.Count > 0)
                {
                    programId = sourceDal.GetMaxProgId();
                    maxId = Convert.ToInt32(programId.Substring(1));
                    maxId = maxId + 1;
                    programId = programId.Substring(0, 1) + maxId.ToString();
                }
                else if (GlobalClass.globalProgDt.Rows.Count == 0)
                {
                    programId = "P1";
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return programId;
        }
        /// <summary>
        /// private bool CheckOpened(string name)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool CheckOpened(string name)
        {
            try
            {
                FormCollection fc = Application.OpenForms;
                foreach (Form frm in fc)
                {
                    if (frm.Name == name)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return false;
        }
        /// <summary>
        /// private void btnCancelSource_Click(object sender, EventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelSource_Click(object sender, EventArgs e)
        {
            DefaultConfiguration();
        }
        /// <summary>
        /// public void DefaultConfiguration()
        /// </summary>
        public void DefaultConfiguration()
        {
            ClearControlText(richTextBox1,ccbCodeLanguage,cbLanguage);
            MakeRichTextBoxReadOnly(richTextBox1);
            if (cbLanguage.SelectedIndex == -1)
            {
                PopulateLanguageCode(false);
            }
            PopulateProgramNames(false);
        }
        private void tsBtnProcessMonitor_Click(object sender, EventArgs e)
        {
            monitor.Visible = true;
        }

        private void tsBtnViewDeletedPrograms_Click(object sender, EventArgs e)
        {
            PopulateDeletedProgramNames();
        }
        private void tsBtnSendMail_Click(object sender, EventArgs e)
        {
            mailApplication = new MailApp();
            mailApplication.Text = "Email Application";
            mailApplication.Show();
        }

        private void tsBtnDocumentMap_Click(object sender, EventArgs e)
        {
            documentMap = new DocumentMapSample(richTextBox1.Text.Trim());
            GlobalPageTracker.documentMapPageObj = documentMap;
            documentMap.Show();
        }

        private void tsBtnMarker_Click(object sender, EventArgs e)
        {
            marker = new MarkerToolSample(richTextBox1.Text.Trim());
            GlobalPageTracker.markerToolPageObj = marker;
            marker.Show();
        }

        private void tsBtnEditor_Click(object sender, EventArgs e)
        {
            editor = new PowerfulSample(richTextBox1.Text.Trim());
            editor.Text = "SourceCode Editor";
            editor.Show();
        }

        private void tsBtnAddSourceCode_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text.Trim()))
            {
                ColorSyntax.dirtyForm = true;
            }
            else
            {
                ColorSyntax.dirtyForm = false;
            }
            if (ColorSyntax.dirtyForm)
            {
                DialogResult result = MessageBox.Show(this, "You have not saved the current data. Do you want to save the code before adding a different code?", "Save current data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ColorSyntax.dirtyForm = !false;
                    return;
                }
                else
                {
                    richTextBox1.Text = string.Empty;
                    ColorSyntax.dirtyForm = false;
                }
            }
            ColorSyntax.ADD_SOURCECODE = true;
            SyntexColor = new ColorSyntax(string.Empty, userData.UserData.UserId.ToString().Trim());
            SyntexColor.SetSourceCode = new SetSourceCodeDelegate(this.FillRichTextBox);
            SyntexColor.CustomEvents = new SetEvents(this.ToggleEvent);
            SyntexColor.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColorSyntax_FormClosing);
            SyntexColor.Show();
            SyntexColor.Focus();
        }

        private void tsBtnEditSourceCode_Click(object sender, EventArgs e)
        {
            ColorSyntax.ADD_SOURCECODE = true;
            SyntexColor = new ColorSyntax(richTextBox1.Text.Trim(), userData.UserData.UserId.ToString().Trim());
            SyntexColor.SetSourceCode = new SetSourceCodeDelegate(this.FillRichTextBox);
            SyntexColor.CustomEvents = new SetEvents(this.ToggleEvent);
            SyntexColor.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColorSyntax_FormClosing);
            SyntexColor.Show();
            SyntexColor.Focus();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text.Trim()))
            {
                btnSaveSource.Enabled = true;
                tsBtnSaveSource.Enabled = true;
                tsBtnEditSourceCode.Enabled = true;
            }
            else
            {
                btnSaveSource.Enabled = !true;
                tsBtnSaveSource.Enabled = !true;
                tsBtnEditSourceCode.Enabled = !true;
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            richTextBox1.ReadOnly = false;
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = !false;
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = !false;
        }

        private void tsBtnSaveSource_Click(object sender, EventArgs e)
        {
            btnSaveSource_Click(this, new EventArgs());
        }

        private void tsBtnViewLog_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm logViewr = null;
                logViewr = new MainForm();
                GlobalPageTracker.loggerFormObj = logViewr;
                GlobalPageTracker.loggerFormObj.Show();
                GlobalPageTracker.loggerFormObj.Focus();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }

        private void tsBtnLogViewer_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm logViewr = null;
                logViewr = new MainForm();
                GlobalPageTracker.loggerFormObj = logViewr;
                GlobalPageTracker.loggerFormObj.Show();
                GlobalPageTracker.loggerFormObj.Focus();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == TABS.SOURCE_CODE)
            {
                SearchProgramFilter();
            }
            else
            {
                SearchFilter();
            }
        }
        private void ColorSyntax_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Event)
                {
                    if (ColorSyntax.dirtyForm)
                    {
                        if (!string.IsNullOrEmpty(SyntexColor.GetRichTextBoxContent()))
                        {
                            richTextBox1.Text = SyntexColor.GetRichTextBoxContent();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void viewProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogOff();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void tsBtnCompareCode_Click_Click(object sender, EventArgs e)
        {
            codeDiffApp = new DiffMergeSample();
            GlobalPageTracker.diffMergeMapPageObj = codeDiffApp;
            codeDiffApp.Show();
        }

        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            PopulateProgramNames(false);
            if (cbLanguage.SelectedIndex == -1)
            {
                PopulateLanguageCode(false);
            }
        }

        private void PopulatePanelsAndControlId(ListBox panelControlIdList)
        {
            Utility.UTIL.ClearAllControls(PanelControlsList);
            ListBox.SelectedObjectCollection workingTabPages = PanelList.SelectedItems;
            string[] tabArray = new string[workingTabPages.Count];
            int index = 0;
            foreach (Object workingTabPage in workingTabPages)
            {
                try
                {
                    string workingPage = workingTabPage as string;
                    tabArray[index++] = workingPage;
                }
                catch (Exception ex)
                {
                    ErrorLog.ErrorRoutine(ex);
                }
            }
            try
            {
                Utility.UTIL.PopulateControlIdByTabPage(panelControlIdList, tabArray);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }

        private void PanelList_MouseUp(object sender, MouseEventArgs e)
        {
            PopulatePanelsAndControlId(PanelControlsList);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Utility.UTIL.ManageRolePermissions(PanelList, PanelControlsList, RoleList, tvRolesToControls);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Utility.UTIL.RemoveRolePermissions(tvRolesToControls);
        }

        private void tsBtnRefreshPanels_Click(object sender, EventArgs e)
        {
            //IntializeControlsAndPermissions();
            Utility.UTIL.ShowCheckedNodes(tvRolesToControls);
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            //Utility.UTIL.CreatePermissionsDataTable(tvRolesToControls);
            DataTable dtPermissions = Utility.UTIL.CreatePermissionsDataTable(Utility.UTIL.UpdateRolePermissions(tvRolesToControls, Invisible.Checked, Disabled.Checked));
            TabUtils.UpdateRolePermissions(dtPermissions, sourceDal);
            UpdateAccessibleControls();
        }

        private void tvRolesToControls_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Utility.UTIL.CheckTreeViewIntegrity(sender, e);
            //StringBuilder sb = new StringBuilder();
            //int count = Utility.UTIL.Nodes.Count;
            //foreach (var pair in Utility.UTIL.Nodes)
            //{
            //    sb.AppendLine("PanelId : " + pair.Key + " ControlId : " + pair.Value + " Total : " + count);
            //}
            //MessageBox.Show(sb.ToString());
        }
        private static bool SyncPanels(DataTable dtPanel)
        {
            bool flag = false;
            try
            {
                RolePermissions[] panelDataCollection = new RolePermissions[dtPanel.Rows.Count];
                RolePermissions roleData = null;
                int index = 0;
                foreach (DataRow row in dtPanel.Rows)
                {
                    roleData = new RolePermissions();
                    roleData.PanelName = row["PanelName"].ToString().Trim();
                    roleData.PanelControlId = row["PanelControlId"].ToString().Trim();
                    panelDataCollection[index++] = roleData;
                }
                flag = TabUtils.InsertOrUpdatePanels(panelDataCollection, sourceDal);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return flag;
        }

        private void RoleList_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable tabs = null;
            Roles role = new Roles();
            int errorCode = -9;
            string rolePermissionsText = string.Empty;
            /* validations */
            /* selected item should not be empty, blank or null */
            if (RoleList.SelectedIndex != -1)
            {
                if (!string.IsNullOrEmpty(RoleList.SelectedItem.ToString().Trim()) && RoleList.SelectedItem.ToString().Trim() != "")
                {
                    string roleName = ((Roles)RoleList.SelectedItem).RoleName;
                    role.RoleName = roleName;
                    tabs = sourceDal.GetPanelsByRole(role, out errorCode);
                    DataView dvFilter = new DataView(tabs);
                    dvFilter.RowFilter = "ACCESS = 1";
                    tabs = dvFilter.ToTable();
                    if (errorCode == 0)
                    {
                        /* populate role name as label */
                        rolePermissionsText = "[" + role.RoleName + "]" + " : [PERMISSIONS]";
                        lblPermissions.Text = rolePermissionsText.Trim();
                        /* populate all role permissions in role permission tree */
                        Utility.UTIL.PopulatePermissionTree(tvPanelControls, tabs);
                    }
                    else
                    {
                        lblPermissions.Text = "Error!";
                        tvPanelControls.Nodes.Clear();
                    }
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ToggleImage(Properties.Resources.Unlock_2_icon);
            manageCols = new ManageColumns(dgvPrograms);
            manageCols.Show();
        }
        public void ToggleImage(Bitmap image)
        {
            toolStripButton2.Image = image;
        }

        private void cbLanguage_MouseClick(object sender, MouseEventArgs e)
        {
            if (cbLanguage.SelectedIndex == -1)
            {
                PopulateLanguageCode(false);
            }
        }

        private void dgvUserData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!dgvUserData.Columns.Contains("IS_LOCKED_IMAGE"))
                AddImageToDataGrid(dgvUserData, "IS_LOCKED_IMAGE", "IS_LOCKED_IMAGE", string.Empty, 1);
            PopulateIcons(dvUserData, dgvUserData, "IS_LOCKED_IMAGE");

            if (!dgvUserData.Columns.Contains("IS_DELETED_IMAGE"))
                AddImageToDataGrid(dgvUserData, "IS_DELETED_IMAGE", "IS_DELETED_IMAGE", string.Empty, 2);
            PopulateIcons(dvUserData, dgvUserData, "IS_DELETED_IMAGE");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string message = Utility.UTIL.ProcStartargs("notepad", DIRECTORY.GetConfigFileName());
            GlobalPageTracker.loginObj.ShowPopupMessage("Notification", message + "\n" + Path.GetFileName(DIRECTORY.GetConfigFileName()), Login.NotificationType.Information);
        }
        #endregion

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            FileExplorer.Explorer explorer = new FileExplorer.Explorer(GetDirectoryListing());
            explorer.Show();
        }
        private DataTable GetDirectoryListing()
        {
            DataTable dtDirectory = new DataTable("tbl_Directory");
            try
            {
                dtDirectory.Columns.Add("Directory", typeof(string));
                dtDirectory.Columns.Add("File", typeof(string));
                DataRow newRow = null;
                FileBackUp b = new FileBackUp();
                ArrayList directoryList = b.GetDirectory(FileBackUp.RootDirectory, true);
                foreach (string directory in directoryList)
                {
                    ArrayList fileList = b.GetFiles(directory, true);
                    foreach (string file in fileList)
                    {
                        if (!string.IsNullOrEmpty(file))
                        {
                            newRow = dtDirectory.NewRow();
                            newRow["Directory"] = directory;
                            newRow["File"] = file;
                            dtDirectory.Rows.Add(newRow);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return dtDirectory;
        }
    }
}

