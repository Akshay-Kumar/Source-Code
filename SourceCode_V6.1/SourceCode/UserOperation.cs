using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SourceCodeDAL;
using SourceCodeBL;
using Logger;

namespace SourceCode
{
    public partial class UserOperation : Form
    {
        #region Declaration
        public static Timer timer = null;
        static SourceDataAccessLayer sourceDal = null;
        Users users = null;
        int rowId;
        DataTable dtRoles;
        string[] isLockArr = { ROLELABEL.OPEN, ROLELABEL.LOCKED };
        string[] isDeletedArr = { ROLELABEL.ACTIVE, ROLELABEL.DELETED };
        string roleId;
        string roleName;
        string isLocked;
        string isDeleted;
        DataView dvUserDataFilter;
        #endregion

        public UserOperation(int i,DataView dvUsers)
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
                    return;
                }
                btnUpdate.Text = BUTTONLABEL.EDIT;
                btnCancel.Text = BUTTONLABEL.CANCEL;
                rowId = i;
                dvUserDataFilter = dvUsers;
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }

        public UserOperation()
        {
            try
            {
                SourceCodeDAL.GlobalClass.dt = null;
                InitializeComponent();
                btnUpdate.Text = BUTTONLABEL.NEW;
                btnCancel.Text = BUTTONLABEL.RESET;
                sourceDal = new SourceCodeDAL.SourceDataAccessLayer();
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }
        private void Operation_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void FillRoles(string roleId, string roleName)
        {
            try
            {
                dtRoles = sourceDal.GetAllRoles();
            }
            catch (SqlException ex) { MessageBox.Show("EXCEPTION :" + ex.Message, "EXCEPTION_MESAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);}
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            foreach (DataRow row in dtRoles.Rows)
            {
                cbRoleId.Items.Add(row["ROLE_ID"].ToString().Trim());
                cbRoleName.Items.Add(row["ROLE_NAME"].ToString().Trim());
            }
            if (roleId != null && roleName != null)
            {
                cbRoleId.SelectedItem = roleId.Trim();
                cbRoleName.SelectedItem = roleName.Trim();
            }
            ToggleEnableOrDisableControls(false, cbRoleId, cbRoleName);
        }

        private void FillLockedDeleted(string isLocked,string isDeleted)
        {
            try
            {
                if (btnUpdate.Text == BUTTONLABEL.UPDATE && isDeleted == isDeletedArr[0])
                {
                    ToggleEnableOrDisableControls(false, cbIsLocked);
                }
                else if (btnUpdate.Text == BUTTONLABEL.UPDATE && isDeleted != isDeletedArr[0])
                {
                    ToggleEnableOrDisableControls(true, cbIsLocked);
                }
                cbIsDeleted.DataSource = isDeletedArr;
                cbIsLocked.DataSource = isLockArr;
                if (isLocked != null && isDeleted != null)
                {
                    cbIsLocked.SelectedItem = isLocked.Trim();
                    cbIsDeleted.SelectedItem = isDeleted.Trim();
                }
                ToggleEnableOrDisableControls(false, cbIsDeleted, cbIsLocked);
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }
        private void LoadUserData()
        {
            try
            {
                if (dvUserDataFilter != null)
                {
                    try
                    {
                        GlobalClass.dt = dvUserDataFilter.ToTable();
                    }
                    catch (Exception ex) { MessageBox.Show("EXCEPTION :" + ex.Message, "EXCEPTION_MESAGE", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    roleId = GlobalClass.dt.Rows[rowId]["ROLE_ID"].ToString();
                    roleName = GetRoleNameByRoleId(roleId.Trim());
                    textUserId.Text = GlobalClass.dt.Rows[rowId]["USER_ID"].ToString();
                    textFirstName.Text = GlobalClass.dt.Rows[rowId]["FIRST_NAME"].ToString();
                    textLastName.Text = GlobalClass.dt.Rows[rowId]["LAST_NAME"].ToString();
                    textUserPassword.Text = GlobalClass.dt.Rows[rowId]["USER_PASSWORD"].ToString();
                    textEmailId.Text = GlobalClass.dt.Rows[rowId]["EMAIL_ID"].ToString();
                    textDateOfBirth.Text = GlobalClass.dt.Rows[rowId]["DATE_OF_BIRTH"].ToString();
                    isLocked = GlobalClass.dt.Rows[rowId]["IS_LOCKED"].ToString();
                    isDeleted = GlobalClass.dt.Rows[rowId]["IS_DELETED"].ToString();
                }
                FillRoles(roleId, roleName);
                FillLockedDeleted(isLocked, isDeleted);
                ToggleEnableOrDisableControls(false, cbRoleId, cbRoleName, textUserId, textFirstName, textLastName,
                textUserPassword, textEmailId, textDateOfBirth, cbIsDeleted, cbIsLocked);
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }
        private string GetRoleNameByRoleId(string roleId)
        {
            string roleName=string.Empty;
            try
            {
                roleName = sourceDal.GetRoleNameByRoleId(roleId);
            }
            catch (SqlException ex) { MessageBox.Show("EXCEPTION :" + ex.Message, "EXCEPTION_MESAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);}
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return roleName;
        }
        private string GetRoleIdByRoleName(string roleName)
        {
            string roleId=string.Empty;
            try
            {
                roleId = sourceDal.GetRoleIdByRoleName(roleName);
            }
            catch (SqlException ex) { MessageBox.Show("EXCEPTION :" + ex.Message, "EXCEPTION_MESAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);}
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return roleId;
        }
        private void ToggleEnableOrDisableControls(bool flag,params Control[] control)
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
            }
        }

        private void EmptyControls(params Control[] control)
        {
            foreach (Control c in control)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    TextBox txtBox = (TextBox)c;
                    txtBox.Text = string.Empty;
                }
                else if (c.GetType() == typeof(ComboBox))
                {
                    ComboBox cbBox = (ComboBox)c;
                    cbBox.Text = string.Empty;
                }
            }
        }

        private void UpdateUserData()
        {
            try
            {
                int error_code = 0;
                bool isSuccess = false;
                SourceCodeDAL.GlobalClass.dt.Rows[rowId]["USER_ID"] = textUserId.Text;
                SourceCodeDAL.GlobalClass.dt.Rows[rowId]["ROLE_ID"] = cbRoleId.Text;
                SourceCodeDAL.GlobalClass.dt.Rows[rowId]["FIRST_NAME"] = textFirstName.Text;
                SourceCodeDAL.GlobalClass.dt.Rows[rowId]["LAST_NAME"] = textLastName.Text;
                SourceCodeDAL.GlobalClass.dt.Rows[rowId]["USER_PASSWORD"] = textUserPassword.Text;
                SourceCodeDAL.GlobalClass.dt.Rows[rowId]["EMAIL_ID"] = textEmailId.Text;
                SourceCodeDAL.GlobalClass.dt.Rows[rowId]["DATE_OF_BIRTH"] = textDateOfBirth.Text;
                SourceCodeDAL.GlobalClass.dt.Rows[rowId]["IS_LOCKED"] = cbIsLocked.Text;
                SourceCodeDAL.GlobalClass.dt.Rows[rowId]["IS_DELETED"] = cbIsDeleted.Text;
                try
                {
                    isSuccess = sourceDal.UpdateUserData(SourceCodeDAL.GlobalClass.dt, rowId, out error_code);
                }
                catch (Exception ex) { MessageBox.Show("EXCEPTION :" + ex.Message, "EXCEPTION_MESAGE", MessageBoxButtons.OK, MessageBoxIcon.Error); isSuccess = false; }
                if (isSuccess)
                {
                    GlobalPageTracker.sourceCodeObj.PopulateUserDataGrid();
                    MessageBox.Show("Userdata updated successfully for " +
                    SourceCodeDAL.GlobalClass.dt.Rows[rowId]["FIRST_NAME"].ToString() + " " +
                    SourceCodeDAL.GlobalClass.dt.Rows[rowId]["LAST_NAME"].ToString(), "SUCCESS_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error in updating user data for " +
                    SourceCodeDAL.GlobalClass.dt.Rows[rowId]["FIRST_NAME"].ToString() + " " +
                    SourceCodeDAL.GlobalClass.dt.Rows[rowId]["LAST_NAME"].ToString(), "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnUpdate.Text = BUTTONLABEL.EDIT;
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnUpdate.Text == BUTTONLABEL.EDIT)
                {
                    btnUpdate.Text = BUTTONLABEL.UPDATE;
                    ToggleEnableOrDisableControls(true, cbRoleName, textFirstName, textLastName,
                                                  textUserPassword, textEmailId, textDateOfBirth, cbIsDeleted, cbIsLocked, btnClose);
                }
                else if (btnUpdate.Text == BUTTONLABEL.UPDATE)
                {
                    btnUpdate.Text = BUTTONLABEL.EDIT;
                    StartTimer();
                    UpdateUserData();
                    ToggleEnableOrDisableControls(false, cbRoleId, cbRoleName, textFirstName, textLastName,
                                                  textUserPassword, textEmailId, textDateOfBirth, cbIsDeleted, cbIsLocked);
                }
                else if (btnUpdate.Text == BUTTONLABEL.NEW)
                {
                    btnUpdate.Text = BUTTONLABEL.SAVE;
                    textUserId.Text = GenerateUserId();
                    ToggleEnableOrDisableControls(true, cbRoleName, textFirstName, textLastName,
                                                  textUserPassword, textEmailId, textDateOfBirth, cbIsDeleted, cbIsLocked, btnClose);
                }
                else if (btnUpdate.Text == BUTTONLABEL.SAVE)
                {
                    if (InsertUserData())
                    {
                        GlobalPageTracker.sourceCodeObj.PopulateUserDataGrid();
                        MessageBox.Show("Userdata inserted successfully for " + users.FirstName + " " + users.LastName + "!", "SUCCESS_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnUpdate.Text = BUTTONLABEL.NEW;
                    }
                    else
                    {
                        MessageBox.Show("Error in inserting userdata!", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnUpdate.Text = BUTTONLABEL.NEW;
                    }
                    ToggleEnableOrDisableControls(false, cbRoleId, cbRoleName, textFirstName, textLastName,
                                                  textUserPassword, textEmailId, textDateOfBirth, cbIsDeleted, cbIsLocked);
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }
        /// <summary>
        /// CheckAccessability
        /// </summary>
        public static void CheckAccessability(object obj, EventArgs e)
        {
            string errorMsg = string.Empty;
            DataTable dt = null;
            try
            {
                bool isAuthentic = CheckUserAuthenticity(out errorMsg, out dt);
                if (!isAuthentic)
                {
                    GlobalPageTracker.sourceCodeObj.LogOff();
                    MessageBox.Show(errorMsg + "\n" + "CONTACT YOUR SYSTEM ADMINISTRATOR.", "ACCESS DENIED!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }

        public static bool CheckUserAuthenticity(out string errorMessage, out DataTable dtUserData)
        {

            int code = -9;
            bool isAuthenticated = false;
            Users user = null;
            dtUserData = null;
            errorMessage = "";
            try
            {
                UserSession userData = Login.GetUserSessionData();
                user = new Users();
                user.UserId = userData.UserData.UserId.ToString().Trim();
                user.Password = userData.UserData.Password.ToString().Trim();
                dtUserData = sourceDal.AuthenticateUser(user, out code);
                switch (code)
                {
                    case 0: isAuthenticated = true;
                        errorMessage = MESSAGE_LABEL.ACCESS_GRANTED;
                        break;
                    case -1: isAuthenticated = false;
                        errorMessage = MESSAGE_LABEL.ID_OR_PASSWORD_MISSING;
                        break;
                    case -2: isAuthenticated = false;
                        errorMessage = MESSAGE_LABEL.ACCOUNT_LOCKED;
                        break;
                    case -3: isAuthenticated = false;
                        errorMessage = MESSAGE_LABEL.ACCOUNT_DELETED;
                        break;
                }
            }
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return isAuthenticated;
        }


        public static void StartTimer()
        {
            timer.Start();
        }
        public static void StopTimer()
        {
            timer.Stop();
        }
        private string GenerateUserId()
        {
            string userId = string.Empty;
            try
            {
                GlobalClass.globalUserDt = sourceDal.FetchAllUsers();
                if (GlobalClass.globalUserDt.Rows.Count > 0)
                {
                    userId = sourceDal.GetMaxUserId();
                    int maxId = Convert.ToInt32(userId.Substring(1));
                    maxId = maxId + 1;
                    userId = userId.Substring(0, 1) + maxId.ToString();
                }
                else if (GlobalClass.globalUserDt.Rows.Count == 0)
                {
                    userId = "U1";
                }
            }
            catch (SqlException ex) { MessageBox.Show("EXCEPTION :" + ex.Message, "EXCEPTION_MESAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);}
            catch (Exception dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return userId;
        }

        private bool InsertUserData()
        {
            bool flag = false;
            users = new Users();
            try
            {
                if (textUserId.Text != string.Empty
                   && textFirstName.Text != string.Empty
                   && textLastName.Text != string.Empty
                   && textUserPassword.Text != string.Empty
                   && textDateOfBirth.Text != string.Empty
                   && textEmailId.Text != string.Empty
                   )
                {
                    users.UserId = textUserId.Text.Trim();
                    users.Role.RoleId = cbRoleId.SelectedItem.ToString().Trim();
                    users.Role.RoleName = cbRoleName.SelectedItem.ToString().Trim();
                    users.FirstName = textFirstName.Text.Trim();
                    users.LastName = textLastName.Text.Trim();
                    users.Password = textUserPassword.Text.Trim();
                    users.EmailId = textEmailId.Text.Trim();
                    users.DateOfBirth = Convert.ToDateTime(textDateOfBirth.Text.Trim());
                    if (cbIsLocked.SelectedItem.ToString().Trim().Equals(ROLELABEL.LOCKED))
                    {
                        users.IsLocked = 1;
                    }
                    else if (cbIsLocked.SelectedItem.ToString().Trim().Equals(ROLELABEL.OPEN))
                    {
                        users.IsLocked = 0;
                    }
                    if (cbIsDeleted.SelectedItem.ToString().Trim().Equals(ROLELABEL.DELETED))
                    {
                        users.IsDeleted = 1;
                    }
                    else if (cbIsDeleted.SelectedItem.ToString().Trim().Equals(ROLELABEL.ACTIVE))
                    {
                        users.IsDeleted = 0;
                    }
                    try
                    {
                        flag = sourceDal.InsertUserData(users);
                    }
                    catch (SqlException ex) { MessageBox.Show("EXCEPTION :" + ex.Message, "EXCEPTION_MESAGE", MessageBoxButtons.OK, MessageBoxIcon.Error); flag = false; }
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
            if (btnCancel.Text == BUTTONLABEL.CANCEL)
            {
                btnUpdate.Text = BUTTONLABEL.EDIT;
                ToggleEnableOrDisableControls(false, cbRoleId, cbRoleName, textUserId, textFirstName, textLastName,
                                               textUserPassword, textEmailId, textDateOfBirth, cbIsDeleted, cbIsLocked);
            }
            else if (btnCancel.Text == BUTTONLABEL.RESET)
            {
                btnUpdate.Text = BUTTONLABEL.NEW;
                EmptyControls(textFirstName, textLastName,
                              textUserPassword, textEmailId, textDateOfBirth);
                ToggleEnableOrDisableControls(false, cbRoleId, cbRoleName, textUserId, textFirstName, textLastName,
                                               textUserPassword, textEmailId, textDateOfBirth, cbIsDeleted, cbIsLocked);
            }
        }

        private void cbRoleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbRoleName.SelectedItem = GetRoleNameByRoleId(cbRoleId.SelectedItem.ToString().Trim());
        }

        private void cbIsDeleted_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsDeleted.SelectedItem.ToString() == ROLELABEL.DELETED)
            {
                cbIsLocked.SelectedItem = ROLELABEL.LOCKED;
                ToggleEnableOrDisableControls(false, cbIsLocked);
            }
            else
            {
                ToggleEnableOrDisableControls(true, cbIsLocked);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GlobalPageTracker.operationPageObj.Close();
        }

        private void cbRoleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbRoleId.SelectedItem = GetRoleIdByRoleName(cbRoleName.SelectedItem.ToString().Trim());
        }

        private void textDateOfBirth_MouseClick(object sender, MouseEventArgs e)
        {
            textDateOfBirth.Text = string.Empty;
        }
        public void ClearDate()
        {
            if (textDateOfBirth.Text == string.Empty)
            {
                textDateOfBirth.Text = "MM-DD-YYYY";
            }
        }

        private void Operation_MouseClick(object sender, MouseEventArgs e)
        {
            ClearDate();
        }
    }
}
