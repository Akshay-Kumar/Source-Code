using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceCodeBL
{
    public class Roles
    {
        
        public Roles()
        {
            tabs = new List<Tab>();
        }
        int deleted;
        public int Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        private string roleId;

        public string RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }
        private string roleName;

        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
        private string roleDescription;

        public string RoleDescription
        {
            get { return roleDescription; }
            set { roleDescription = value; }
        }
        private string accessibleTabList;

        public string AccessibleTabList
        {
            get { return accessibleTabList; }
            set { accessibleTabList = value; }
        }

        private List<Tab> tabs = null;
       
        public List<Tab> Tabs
        {
            get { return tabs; }
            set { tabs = value; }
        }

        public void Add(Tab tab)
        {
            this.tabs.Add(tab);
        }

    }
}
