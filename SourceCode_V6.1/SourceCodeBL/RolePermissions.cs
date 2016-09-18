using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceCodeBL
{
    public class RolePermissions
    {

        public RolePermissions()
        {
            role = new Roles();
        }
        Roles role;

        public Roles Role
        {
            get { return role; }
            set { role = value; }
        }
        string panelName;
 
        public string PanelName
        {
            get { return panelName; }
            set { panelName = value; }
        }
        string panelControlId;

        public string PanelControlId
        {
            get { return panelControlId; }
            set { panelControlId = value; }
        }
        int invisible;

        public int Invisible
        {
            get { return invisible; }
            set { invisible = value; }
        }
        int disabled;

        public int Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }
        int access;

        public int Access
        {
            get { return access; }
            set { access = value; }
        } 
    }
}
