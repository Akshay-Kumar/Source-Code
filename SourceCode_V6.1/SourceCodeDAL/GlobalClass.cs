using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;

namespace SourceCodeDAL
{
    #region GlobalClass
    public class GlobalClass
    {
        public static SqlDataAdapter adap;
        public static string CurrentProgramName = string.Empty;
        public static string CurrentLangName = string.Empty;
        public static bool LOGGER_DISPOSED = false;
        public static DataTable dt;
        public static DataTable globalProgDt;
        public static DataTable globalRoleDt;
        public static DataTable globalUserDt;
        public static int index;
        public static string ShowOperationForm;
        public static string ShowRoleOperationForm;
        public static string SOURCE_CODE_DB_FORMAT = "MM-dd-yyyy hh-mm-ss-ffff tt";
        public static string SOURCE_CODE_FILE_FORMAT = "MM-dd-yyyy_hh-mm-ss-ffff tt";
        private static string source_code_connection_string;
        public static string SOURCE_CODE_CONNECTION_STRING
        {
            get { return GlobalClass.source_code_connection_string; }
            set { GlobalClass.source_code_connection_string = value; }
        }
    }
    #endregion
    #region MANAGE_USER_LABEL
    public class MANAGE_USER_LABEL
    {
        public const string SHOW_USER_OPERATION_FORM = "ShowOperationForm";
        public const string NODE_FETCH_ALL_USERS = "nodeFetchAllUsers";
        public const string NODE_ADD_USERS = "nodeAddUser";
        public const string NODE_EDIT_USERS = "nodeEditUser";
    }
    #endregion
    #region MANAGE_ROLE_LABEL
    public class MANAGE_ROLE_LABEL
    {
        public const string SHOW_ROLE_OPERATION_FORM = "ShowRoleOperationForm";
        public const string DELETE_ROLE_BY_ROLE_ID = "DELETE_ROLE_BY_ROLE_ID";
        public const string NODE_FETCH_ALL_ROLES = "nodeFetchAllRoles";
        public const string NODE_FETCH_TABS_BY_ROLE = "nodeFetchTabsByRole";
        public const string NODE_ADD_ROLE = "nodeAddRole";
        public const string NODE_EDIT_ROLE = "nodeEditRole";
        public const string NODE_DELETE_ROLE = "nodeDeleteRole";
    }
    #endregion
    #region BUTTONLABEL
    public class BUTTONLABEL
    {
        public static string NEW = "New";
        public static string EDIT = "Edit";
        public static string LOCK = "Lock";
        public static string UPDATE = "Update";
        public static string SAVE = "Save";
        public static string CANCEL = "Cancel";
        public static string RESET = "Reset";
        public static string ADD = "Add";
        public static string DELETE = "Delete";
    }
    #endregion
    #region ROLELABEL
    public class ROLELABEL
    {
        public static string LOCKED = "LOCKED";
        public static string OPEN = "OPEN";
        public static string DELETED = "DELETED";
        public static string ACTIVE = "ACTIVE";
    }
    #endregion
    #region TABS
    public class TABS
    {
        public const string SOURCE_CODE = "SourceCode";
        public const string ADD_SOURCE_CODE = "AddSourceCode";
        public const string USERS = "Users";
        public const string ROLES = "Roles";
        public const string ABOUT = "About";
        public const string HELP = "Help";
        public const string ARCHIVE = "Archive";
        public const string ROLE_PERMISSIONS = "RolePermissions";
    }
    #endregion
    #region MESSAGE_LABEL
    public class MESSAGE_LABEL
    {
        public const string SUCCESS_MESSSAGE = "ROLE DELETED SUCCESSFULLY";
        public const string SUCCESS_MESSSAGE_SOURCE = "SOURCE CODE SAVED SUCCESSFULLY";
        public const string ERROR_MESSSAGE_SOURCE = "SOME EXCEPTION OCCURED WHILE SAVING SOURCE CODE";
        public const string EXCEPTION_MESSAGE = "SOME EXCEPTION OCCURED";
        public const string ERROR_MESSAGE = "CANNOT DELETE THE ROLE, ROLE IS ASSIGNED TO USER";
        public const string ACCESS_GRANTED = "AUTHENTICATED SUCCESSFULLY";
        public const string ID_OR_PASSWORD_MISSING = "USER_ID OR USER_PASSWORD DOES NOT EXISTS";
        public const string ACCOUNT_LOCKED = "USER ACCOUNT LOCKED";
        public const string ACCOUNT_DELETED = "USER ACCOUNT DELETED";
    }
    #endregion
    #region LANGUAGE
    public class LANGUAGE
    {
        private string lang_id;
        public string Lang_id
        {
            get { return lang_id; }
            set { lang_id = value; }
        }
        private string lang_name;

        public string Lang_name
        {
            get { return lang_name; }
            set { lang_name = value; }
        }
        public override string ToString()
        {
            return string.Format(lang_name);
        }
    }
    #endregion
    #region FORMS
    public class FORMS
    {
        public const string SourceCode = "SourceCode";
        public const string ColorSyntax = "ColorSyntax";
        public const string Login = "Login";
        public const string MainForm = "MainForm";
        public const string Operation = "Operation";
        public const string RoleOperation = "RoleOperation";
        public const string SearchBox = "SearchBox";
    }
    #endregion
}
