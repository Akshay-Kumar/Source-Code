using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SourceCodeDAL;
using SourceCodeBL;
using Logger;
using System.Data.SqlClient;
using System.Threading;

namespace SourceCode
{
    public partial class RoleOperation : Form
    {
        SourceDataAccessLayer sourceDal = null;
        int rowid = 0;
        string roleId;
        string roleName;
        string roleDesc;
        Roles role;
        DataView dvRoleDataFilter;
        public RoleOperation()
        {
            try
            {
                InitializeComponent();
                if (DBConnection.IsDatabaseOnline())
                {
                    sourceDal = new SourceCodeDAL.SourceDataAccessLayer();
                }
                else
                {
                    GlobalPageTracker.loginObj.ShowPopupMessage("Database Notification", "Database is offline.", Login.NotificationType.Error);
                }
                btnSave.Text = BUTTONLABEL.NEW;
                btnCancel.Text = BUTTONLABEL.RESET;
                GlobalClass.dt = null;
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }
        public RoleOperation(int i,DataView dvRolesData)
        {
            try
            {
                InitializeComponent();
                btnSave.Text = BUTTONLABEL.EDIT;
                btnCancel.Text = BUTTONLABEL.CANCEL;
                this.rowid = i;
                dvRoleDataFilter = dvRolesData;
                sourceDal = new SourceCodeDAL.SourceDataAccessLayer();
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }

        private void LoadRoleData()
        {
            try
            {
                SourceCode.GetAllPanels(checkedListBox1);
                if (dvRoleDataFilter != null)
                {
                    try
                    {
                        GlobalClass.dt = dvRoleDataFilter.ToTable();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("EXCEPTION :" + ex.Message, "EXCEPTION_MESAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception dalException)
                    {
                        ErrorLog.ErrorRoutine(dalException);
                    }
                    roleId = GlobalClass.dt.Rows[rowid]["ROLE_ID"].ToString();
                    roleDesc = GlobalClass.dt.Rows[rowid]["ROLE_DESC"].ToString();
                    roleName = GetRoleNameByRoleId(roleId.Trim());
                    txtRoleId.Text = roleId.Trim();
                    //SetControlText(txtRoleId, roleId.Trim());
                    txtRoleName.Text = roleName.Trim();
                    //SetControlText(txtRoleName, roleName.Trim());
                    richTextBox1.Text = roleDesc.Trim();
                    //SetControlText(richTextBox1, roleDesc.Trim());
                    SourceCode.GetPanelsByRole(txtRoleName.Text.Trim(), checkedListBox1);
                }
                
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            ToggleEnableOrDisable(false, txtRoleId, txtRoleName, checkedListBox1, richTextBox1);
        }
        Action<Control, string> setterCallback = (toSet, text) => toSet.Text = text;

        void SetControlText(Control toSet, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(setterCallback, toSet, text);
            }
            else
            {
                setterCallback(toSet, text);
            }
        }
        private string GetRoleNameByRoleId(string roleId)
        {
            string roleName = string.Empty;
            try
            {
                roleName = sourceDal.GetRoleNameByRoleId(roleId);
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return roleName;
        }
        private string GetRoleIdByRoleName(string roleName)
        {
            string roleId = string.Empty;
            try
            {
                roleId = sourceDal.GetRoleIdByRoleName(roleName);
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return roleId;
        }
        
        private void RoleOperation_Load(object sender, EventArgs e)
        {
            LoadRoleData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == BUTTONLABEL.EDIT)
                {
                    btnSave.Text = BUTTONLABEL.UPDATE;
                    ToggleEnableOrDisable(true, txtRoleName, checkedListBox1, richTextBox1);
                }
                else if (btnSave.Text == BUTTONLABEL.UPDATE)
                {
                    if (InsertOrUpdateRoleData())
                    {
                        MessageBox.Show("RoleData updated successfully for " + role.RoleName + "!", 
                            "SUCCESS_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Text = BUTTONLABEL.EDIT;
                    }
                    else
                    {
                        MessageBox.Show("Error in updating roledata!", 
                            "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    GlobalPageTracker.sourceCodeObj.UpdateAccessibleTabs();
                    GlobalPageTracker.sourceCodeObj.UpdateAccessibleControls();
                    SourceCode.GetAllPanels(checkedListBox1);
                    SourceCode.GetPanelsByRole(txtRoleName.Text.Trim(), checkedListBox1);
                    ToggleEnableOrDisable(false, txtRoleName, checkedListBox1, richTextBox1);

                }
                else if (btnSave.Text == BUTTONLABEL.NEW)
                {
                    btnSave.Text = BUTTONLABEL.SAVE;
                    txtRoleId.Text = GenerateRoleId();
                    ToggleEnableOrDisable(true, txtRoleName, checkedListBox1, richTextBox1);
                }
                else if (btnSave.Text == BUTTONLABEL.SAVE)
                {
                    if (InsertOrUpdateRoleData())
                    {
                        MessageBox.Show("RoleData inserted successfully for " + role.RoleName + "!",
                            "SUCCESS_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Text = BUTTONLABEL.NEW;
                    }
                    else
                    {
                        MessageBox.Show("Error in inserting roledata!",
                            "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnSave.Text = BUTTONLABEL.NEW;
                    }
                    GlobalPageTracker.sourceCodeObj.UpdateAccessibleTabs();
                    GlobalPageTracker.sourceCodeObj.UpdateAccessibleControls();
                    SourceCode.GetAllPanels(checkedListBox1);
                    SourceCode.GetPanelsByRole(txtRoleName.Text.Trim(), checkedListBox1);
                    ToggleEnableOrDisable(false, txtRoleName, checkedListBox1, richTextBox1);
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }  
        private string GenerateRoleId()
        {
            string roleId = string.Empty;
            int maxId = 0;
            try
            {
                GlobalClass.globalRoleDt = sourceDal.GetAllRoles();
                if (GlobalClass.globalRoleDt.Rows.Count > 0)
                {
                    roleId = sourceDal.GetMaxRoleId();
                    maxId = Convert.ToInt32(roleId.Substring(1));
                    maxId = maxId + 1;
                    roleId = roleId.Substring(0, 1) + maxId.ToString();
                }
                else if (GlobalClass.globalRoleDt.Rows.Count == 0)
                {
                    roleId = "R1";
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return roleId;
        }
        //private void ThreadSafeInsertOrUpdate()
        //{
        //    Thread[] threadArray = new Thread[2];
        //    threadArray[0] = new Thread(() => GlobalPageTracker.sourceCodeObj.UpdateAccessibleTabs());
        //    threadArray[1] = new Thread(() => LoadRoleData());
        //    threadArray[0].Start();
        //    Thread.Sleep(3000);
        //    threadArray[1].Start();
        //}
        public delegate void ToggleEnableOrDisableControls(bool flag, Control control);
        
        public void InvokeControl(bool flag, Control c)
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
                Button button = (Button)c;
                button.Enabled = flag;
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
        /// <summary>
        /// ToggleEnableOrDisable
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="control"></param>
        //public void ToggleEnableOrDisable(bool flag, params Control[] control)
        //{
        //    foreach (Control c in control)
        //    {
        //        if (c.GetType() == typeof(TextBox))
        //        {
        //            TextBox txtBox = (TextBox)c;
        //            if (txtBox.InvokeRequired)
        //            {
        //                txtBox.Invoke(new ToggleEnableOrDisableControls(this.InvokeControl), flag, txtBox);

        //            }
        //            else
        //            {
        //                txtBox.Enabled = flag;
        //            }
        //        }
        //        else if (c.GetType() == typeof(ComboBox))
        //        {
        //            ComboBox cbBox = (ComboBox)c;
        //            if (cbBox.InvokeRequired)
        //            {
        //                cbBox.Invoke(new ToggleEnableOrDisableControls(this.InvokeControl), flag, cbBox);

        //            }
        //            else
        //            {
        //                cbBox.Enabled = flag;
        //            }
        //        }
        //        else if (c.GetType() == typeof(Button))
        //        {
        //            Button button = (Button)c;
        //            if (button.InvokeRequired)
        //            {
        //                button.Invoke(new ToggleEnableOrDisableControls(this.InvokeControl), flag, button);

        //            }
        //            else
        //            {
        //                button.Enabled = flag;
        //            }
        //        }
        //        else if (c.GetType() == typeof(CheckedListBox))
        //        {
        //            CheckedListBox cbox = (CheckedListBox)c;
        //            if (cbox.InvokeRequired)
        //            {
        //                cbox.Invoke(new ToggleEnableOrDisableControls(this.InvokeControl), flag, cbox);

        //            }
        //            else
        //            {
        //                cbox.Enabled = flag;
        //            }
        //        }
        //        else if (c.GetType() == typeof(RichTextBox))
        //        {
        //            RichTextBox cbox = (RichTextBox)c;
        //            if (cbox.InvokeRequired)
        //            {
        //                cbox.Invoke(new ToggleEnableOrDisableControls(this.InvokeControl), flag, cbox);

        //            }
        //            else
        //            {
        //                cbox.Enabled = flag;
        //            }
        //        }
        //    }
        //}
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
        private bool InsertOrUpdateRoleData()
        {
            bool flag = false;
            role = new Roles();
            try
            {
                if (txtRoleId.Text.Trim() != string.Empty
                   && txtRoleName.Text.Trim() != string.Empty
                   )
                {
                    role.RoleId = txtRoleId.Text.Trim();
                    role.RoleName = txtRoleName.Text.Trim();
                    role.RoleDescription = richTextBox1.Text.Trim();
                    role.AccessibleTabList = SourceCode.GetTabListPanels(checkedListBox1);
                    flag = sourceDal.InsertOrUpdateRoles(role);
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return flag;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnSave.Text = BUTTONLABEL.NEW;
            ToggleEnableOrDisable(false, txtRoleName, checkedListBox1, richTextBox1);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GlobalPageTracker.roleOperationPageObj.Close();
        }
    }
}
