using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCodeBL;

namespace SourceCode
{
    #region UserSession
    public class UserSession
    {
        private Users userData;
        private Roles roleData;
        public string[] ACCESSIBLE_TABS;
        public Users UserData
        {
            get
            {
                return this.userData;
            }
            set
            {
                this.userData = value;
            }
        }
        public Roles RoleData
        {
            get
            {
                return this.roleData;
            }
            set
            {
                this.roleData = value;
            }
        }
        public UserSession()
        {
            this.userData = new Users();
            this.roleData = new Roles();
        }
        public void ClearUserSession()
        {
            this.roleData = null;
            this.userData = null;
        }
    }
    #endregion
}
