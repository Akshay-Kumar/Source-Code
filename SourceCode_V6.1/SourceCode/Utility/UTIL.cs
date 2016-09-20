using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using SourceCodeBL;
using System.Collections;
using System.Configuration;
using System.Diagnostics;

namespace SourceCode.Utility
{
    public class UTIL
    {
        private static TreeViewCancelEventHandler checkForCheckedChildren = new TreeViewCancelEventHandler(CheckForCheckedChildrenHandler);
        public static Dictionary<string, string> oldMenuToolTips =
            new Dictionary<string, string>();
        public static DataTable dtPanelData = new DataTable("tbl_PanelControls");
        public static DataTable dtRole = new DataTable("tbl_ROLE");
        static TreeNode roleNode = null;
        static TreeNode returnedRoleNode = null;
        static TreeNode returnedPanelNode = null;
        static TreeNode returnedControlNode = null;
        //static Dictionary<Object, ListBox> _Hide = new Dictionary<Object, ListBox>();

        public static DataTable GetConnections()
        {
            DataTable dtConnection = new DataTable("Connections");
            dtConnection.Columns.Add("ConnectionName", typeof(string));
            dtConnection.Columns.Add("ConnectionString", typeof(string));
            DataRow newRow = null;
            foreach (SettingsProperty property in Properties.Settings.Default.Properties)
            {
                if (property.Name.ToLower().Contains("con"))
                {
                    newRow = dtConnection.NewRow();
                    newRow["ConnectionName"] = property.Name.ToUpper();
                    newRow["ConnectionString"] = property.DefaultValue;
                    dtConnection.Rows.Add(newRow);
                }
            }
            return dtConnection;
        }
        // Prevent expansion of a node that does not have any checked child nodes. 
        private static void CheckForCheckedChildrenHandler(object sender,
            TreeViewCancelEventArgs e)
        {
            if (!HasCheckedChildNodes(e.Node)) e.Cancel = true;
        }

        // Returns a value indicating whether the specified  
        // TreeNode has checked child nodes. 
        private static bool HasCheckedChildNodes(TreeNode node)
        {
            if (node.Nodes.Count == 0) return false;
            foreach (TreeNode childNode in node.Nodes)
            {
                if (childNode.Checked) return true;
                // Recursively check the children of the current child node. 
                if (HasCheckedChildNodes(childNode)) return true;
            }
            return false;
        }

        public static void ClearAllControls(params ListBox[] controls)
        {
            foreach (ListBox control in controls)
            {
                control.DataSource = null;
                control.Items.Clear();
            }
        }
        public static DataTable GetAllTabControls(TabControl tabControl,Form mainForm)
        {
            if(!dtPanelData.Columns.Contains("PanelName"))
            dtPanelData.Columns.Add("PanelName",typeof(string));
            if (!dtPanelData.Columns.Contains("PanelControlId"))
            dtPanelData.Columns.Add("PanelControlId", typeof(string));
            dtPanelData.Clear();
            foreach (TabPage page in tabControl.TabPages)
            {
                ShowControls(page.Controls,page);
            }
            Control.ControlCollection controls = mainForm.Controls;
            foreach (Control c in controls)
            {
                if (!(c is TabControl))
                {
                    DataRow row = dtPanelData.NewRow();
                    row["PanelName"] = mainForm.Name;
                    row["PanelControlId"] = c.Name;
                    dtPanelData.Rows.Add(row);
                }
            }
            return dtPanelData;
        }
        public static void ShowControls(Control.ControlCollection controlCollection,TabPage page)
        {
            foreach (Control c in controlCollection)
            {
                try
                {
                    if (c.Controls.Count > 0)
                    {
                        ShowControls(c.Controls,page);
                    }
                    if (c is MenuStrip)
                    {
                        MenuStrip menuStrip = c as MenuStrip;
                        ShowToolStripMenuItems(menuStrip.Items,page);
                    }
                    if (c is ToolStrip)
                    {
                        ToolStrip toolStrip = c as ToolStrip;
                        ShowToolStripItems(toolStrip.Items,page);
                    }

                    if (c is Button || c is ComboBox || c is TextBox ||
                        c is ListBox || c is DataGridView || c is RadioButton ||
                        c is RichTextBox || c is Label || c is TreeView ||
                        c is GroupBox || c is TabControl || c is TabPage ||
                        c is TableLayoutPanel || c is Panel || c is CheckBox ||
                        c is PictureBox || c is LinkLabel || 
                        c is CheckComboBoxTest.CheckedComboBox ||
                        c is CheckedListBox)
                    {
                        DataRow row = dtPanelData.NewRow();
                        row["PanelName"] = page.Name;
                        row["PanelControlId"] = c.Name;
                        dtPanelData.Rows.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog.ErrorRoutine(ex);
                }
            }
        }
        public static void PopulateRoleData(DataTable roleData, ListBox roleList)
        {
            try
            {
                ClearAllControls(roleList);
                dtRole = roleData;
                ArrayList Roles = new ArrayList();
                for (int i = 0; i < dtRole.Rows.Count; i++)
                {
                    Roles.Add(new Roles() { RoleId = dtRole.Rows[i]["ROLE_ID"].ToString().Trim(), RoleName = dtRole.Rows[i]["ROLE_NAME"].ToString().Trim() });
                }
                roleList.DataSource = Roles;
                roleList.DisplayMember = "RoleName";
                roleList.ValueMember = "RoleId";
                roleList.ClearSelected();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog.ErrorRoutine(ex);
            }
        }
        
        private static bool NodeExists(TreeNodeCollection nodes, string key, out TreeNode treeNode) 
        {
            treeNode = null;
            foreach (TreeNode node in nodes)
            {
                if (node.Text.Equals(key))
                {
                    treeNode = node;
                    return true;
                }
            }
            return false;
        }
        /*
        public static void AddItem(Object item, ListBox listBox)
        {
            if (listBox != null && item != null)
                listBox.Items.Add(item);
        }
        public static void RemoveItem(Object item,ListBox listBox)
        {
            if(listBox !=null && item != null)
            listBox.Items.Remove(item);
        }
        public static void HideSelectedItem(ListBox List)
        {
            try
            {
                foreach (Object item in List.SelectedItems)
                {
                    _Hide.Add(item, List);
                }
                foreach (var val in _Hide)
                {
                    RemoveItem(val.Key, val.Value);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog.ErrorRoutine(ex);
            }
        }
        public static void ShowSelectedItem(string item)
        {
            Dictionary<Object, ListBox> toRemove = new Dictionary<Object, ListBox>();
          
            try
            {
                foreach (var pair in _Hide)
                {
                    string key = pair.Key as string;
                    if (key == item)
                    {
                        toRemove.Add(pair.Key, pair.Value);   
                    }
                }
                foreach (var val in toRemove)
                {
                    _Hide.Remove(val.Key);
                    AddItem(val.Key,val.Value);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog.ErrorRoutine(ex);
            }
        }
        */
        public static void RemoveRolePermissions(TreeView tvSource)
        {
            TreeNode selectedNode = tvSource.SelectedNode;
            if (selectedNode != null)
            {
                tvSource.Nodes.Remove(selectedNode);
            }
        }
        public static void ShowCheckedNodes(TreeView treeView)
        {
            // Disable redrawing of treeView1 to prevent flickering  
            // while changes are made.
            treeView.BeginUpdate();

            // Collapse all nodes of treeView1.
            treeView.CollapseAll();

            // Add the checkForCheckedChildren event handler to the BeforeExpand event.
            treeView.BeforeExpand += checkForCheckedChildren;

            // Expand all nodes of treeView1. Nodes without checked children are  
            // prevented from expanding by the checkForCheckedChildren event handler.
            treeView.ExpandAll();

            // Remove the checkForCheckedChildren event handler from the BeforeExpand  
            // event so manual node expansion will work correctly.
            treeView.BeforeExpand -= checkForCheckedChildren;

            // Enable redrawing of treeView1.
            treeView.EndUpdate();
        }
        public static void ManageRolePermissions(ListBox panelList,ListBox controlList,ListBox roleList, TreeView roleTree)
        {
            string roleName = string.Empty;
            roleTree.BeginUpdate();
            try
            {
                TreeNode[] controlNames = new TreeNode[controlList.SelectedItems.Count];
                int j = 0;
                foreach (Object control in controlList.SelectedItems)
                {
                    controlNames[j++] = new TreeNode(control as string);
                }

                TreeNode[] panelNames = new TreeNode[panelList.SelectedItems.Count];
                int i = 0;
                foreach (Object panel in panelList.SelectedItems)
                {
                    int count = 0;
                    string panelName = panel as string;
                    DataRow[] drarray;
                    drarray = dtPanelData.Select("PanelName = '" + panelName + "'", 
                        "PanelControlId", DataViewRowState.CurrentRows);
                    foreach (DataRow row in drarray)
                    {
                        foreach (TreeNode node in controlNames)
                        {
                            if (node.Text.Trim().Equals(row["PanelControlId"].ToString().Trim()))
                            {
                                count++;
                            }
                        }
                    }
                    if (count == controlNames.Length)
                    {
                        if (controlNames.Length > 0)
                        {
                            panelNames[i++] = new TreeNode(panelName, controlNames);
                        }
                    }
                }

                //foreach (DataRowView roleRow in roleList.SelectedItems)
                //{
                //    DataRow[] drarray;
                //    drarray = dtRole.Select("ROLE_ID = '" + roleRow["RoleId"] + "'", "ROLE_NAME",
                //        DataViewRowState.CurrentRows);
                //    roleName = drarray[0]["ROLE_NAME"].ToString();
                //}

                foreach (Roles roleRow in roleList.SelectedItems)
                {
                    DataRow[] drarray;
                    drarray = dtRole.Select("ROLE_ID = '" + roleRow.RoleId + "'", "ROLE_NAME",
                        DataViewRowState.CurrentRows);
                    roleName = drarray[0]["ROLE_NAME"].ToString();
                }

                //if role node exists update it
                if(roleTree !=null)
                if (NodeExists(roleTree.Nodes, roleName, out returnedRoleNode))
                {
                    if (returnedRoleNode != null)
                    {
                        if(panelNames!=null)
                        foreach (TreeNode panel in panelNames)
                        {
                            if (panel != null)
                            if (NodeExists(returnedRoleNode.Nodes, panel.Text.Trim(), out returnedPanelNode))
                            {
                                if (returnedPanelNode != null)
                                {
                                    if(controlNames != null)
                                    foreach (TreeNode control in controlNames)
                                    {
                                        if (control != null)
                                        if (!NodeExists(returnedPanelNode.Nodes, control.Text.Trim(), out returnedControlNode))
                                        {
                                            returnedPanelNode.Nodes.Add(control);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                roleNode.Nodes.Add(new TreeNode(panel.Text, controlNames));
                            }
                        }
                    }
                }
                else
                {
                    roleNode = new TreeNode(roleName, panelNames);
                    roleTree.Nodes.Add(roleNode);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog.ErrorRoutine(ex);
            }
            roleTree.ExpandAll();
            roleTree.EndUpdate();
        }
        public static DataTable CreatePermissionsDataTable(List<Roles> roles)
        {
            DataTable dtPermissions = new DataTable("tbl_Permissions");
            dtPermissions.Clear();
            dtPermissions.Columns.Clear();
            try
            {
                if (!dtPermissions.Columns.Contains("RoleName"))
                    dtPermissions.Columns.Add("RoleName", typeof(string));

                if (!dtPermissions.Columns.Contains("PanelName"))
                    dtPermissions.Columns.Add("PanelName", typeof(string));

                if (!dtPermissions.Columns.Contains("PanelControlId"))
                    dtPermissions.Columns.Add("PanelControlId", typeof(string));

                if (!dtPermissions.Columns.Contains("Invisible"))
                    dtPermissions.Columns.Add("Invisible", typeof(string));

                if (!dtPermissions.Columns.Contains("Disabled"))
                    dtPermissions.Columns.Add("Disabled", typeof(string));

                if (!dtPermissions.Columns.Contains("Access"))
                    dtPermissions.Columns.Add("Access", typeof(string));

                string roleName = string.Empty, tabName = string.Empty;
                if (roles != null)
                {
                    foreach (Roles role in roles)
                    {
                        roleName = role.RoleName;
                        if (role.Tabs !=null)
                        {
                            foreach(Tab tab in role.Tabs)
                            {
                                tabName = tab.Name;
                                if (tab.ControlId.Count > 0)
                                {
                                    foreach (ControlId c in tab.ControlId)
                                    {
                                        DataRow row = dtPermissions.NewRow();
                                        row["RoleName"] = roleName;
                                        row["PanelName"] = tabName;
                                        row["PanelControlId"] = c.Id;
                                        row["Invisible"] = c.Invisible == true ? 1 : 0;
                                        row["Disabled"] = c.Disabled == true ? 1 : 0;
                                        row["Access"] = 1;
                                        dtPermissions.Rows.Add(row);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog.ErrorRoutine(ex);
            }
            if (dtPermissions != null)
            {
                if (dtPermissions.Rows.Count > 0)
                    Print(roles,"success");
            }
            else
            {
                Print(roles, "error");
            }
            return dtPermissions;
        }
        //public static void AddChildren(TreeView treeView, DataTable dtPermissions)
        //{
        //    foreach (TreeNode roleNode in treeView.Nodes)
        //    {
        //        if (roleNode.Checked)
        //        {
        //            foreach (TreeNode panel in roleNode.Nodes)
        //            {
        //                if (panel.Checked)
        //                foreach (TreeNode control in panel.Nodes)
        //                {
        //                    if (control.Checked)
        //                    {
        //                        DataRow row = dtPermissions.NewRow();
        //                        row["RoleId"] = roleNode.Text.Trim();
        //                        row["PanelName"] = panel.Text.Trim();
        //                        row["PanelControlId"] = control.Text.Trim();
        //                        dtPermissions.Rows.Add(row);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        public static void Print(List<Roles> roles,string messageType)
        {
            StringBuilder sb = null;
            string roleName = string.Empty,
                tabName = string.Empty,
                controlids = string.Empty;
            if (roles != null && messageType == "success")
            {
                foreach (Roles role in roles)
                {
                    roleName = role.RoleName;
                    sb = new StringBuilder();
                    
                    if (role.Tabs != null)
                    {
                        foreach (Tab tab in role.Tabs)
                        {
                            tabName = tab.Name;
                            sb.Append("\nRoleName : " + roleName);
                            sb.Append("\nPanelName : " + tabName);
                            if (tab.ControlId.Count > 0)
                            {
                                foreach (ControlId c in tab.ControlId)
                                {
                                    controlids += c.Id + "," + " Invisible = " + (c.Invisible == true ? "Yes" : "No") + "," +
                                                       " Disabled = " + (c.Disabled == true ? "Yes" : "No");
                                    controlids += "\n";
                                }
                                sb.Append("\nControlId(s) : \n" + controlids);
                            }
                            sb.AppendLine(string.Empty);
                        }
                    }
                }
                //sb.Append("\nRoleName : " + roleName + "\nPanelName : " + tabName + "\nControlId(s) : " + controlids);
                MessageBox.Show(sb.ToString(), ".::ROLE-PERMISSIONS UPDATED SUCCESSFULLY::.");
            }
            else if(messageType == "error")
            {
                MessageBox.Show("Error in updating rolePermissions!");
            }
        }
        public static List<Roles> UpdateRolePermissions(TreeView treeView,bool invisible,bool disabled)
        {
            int index = 0;
            Roles role = null;
            Tab tab = null;
            ControlId controlId = null;
            List<Roles> roleCollection = new List<Roles>();
            try
            {
                foreach (TreeNode roleNode in treeView.Nodes)
                {
                    if (roleNode.Checked)
                    {
                        role = new Roles();
                        role.RoleName = roleNode.Text;
                        foreach (TreeNode panel in roleNode.Nodes)
                        {
                            if (panel.Checked)
                            {
                                tab = new Tab();
                                tab.Name = panel.Text;
                                foreach (TreeNode control in panel.Nodes)
                                {
                                    if (control.Checked)
                                    {
                                        controlId = new ControlId(control.Text, invisible, disabled);
                                        tab.Add(controlId);
                                    }
                                }
                                role.Add(tab);
                            }
                        }
                        roleCollection.Add(role);
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog.ErrorRoutine(ex);
                return null;
            }
            if (index == 0)
            {
                MessageBox.Show("PLEASE SELECT AT LEAST ONE ROLE TO UPDATE.", "ERROR_MESSAGE", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return null;
            }
            return roleCollection;
        }
        public static Dictionary<TreeNode, TreeNode> Nodes = new Dictionary<TreeNode, TreeNode>();
        public static void CheckTreeViewIntegrity(object sender, TreeViewEventArgs e)
        {
            CheckChildNodes(e.Node, e.Node.Checked);
        }
        public static void CheckChildNodes(TreeNode node,bool check)
        {
            if (node.Nodes.Count == 0) return;
            foreach (TreeNode childNode in node.Nodes)
            {
                childNode.Checked = check;
                // Recursively check the children of the current child node. 
                CheckChildNodes(childNode, check);
            }
            return;
        }
        
        public static void PopulatePermissionTree(TreeView PermissionTree,DataTable dtTabs)
        {
            PermissionTree.BeginUpdate();
            PermissionTree.Nodes.Clear();
            TreeNode parentNode = null;
            TreeNode subNode = null;

            string currentName = string.Empty;
            foreach (DataRow row in dtTabs.Rows)
            {
                string subNodeText = row["PANEL_CONTROL_ID"].ToString();// = ByControlRB.Checked ? row["RoleName"].ToString() : row["ControlID"].ToString();
                subNodeText += ":";
                subNodeText += Convert.ToInt32(row["INVISIBLE"]) == 0 ? " visible " : " not visible ";
                subNodeText += " and ";
                subNodeText += Convert.ToInt32(row["DISABLED"]) == 0 ? " enabled " : " disabled ";

                subNode = new TreeNode(subNodeText);
                string dataName = row["PANEL_NAME"].ToString();//ByControlRB.Checked ? row["ControlID"].ToString() : row["RoleName"].ToString();
                if (currentName != dataName)
                {
                    parentNode = new TreeNode(dataName);
                    currentName = dataName;
                    PermissionTree.Nodes.Add(parentNode);
                }

                if (parentNode != null)
                {
                    parentNode.Nodes.Add(subNode);
                }
            }
            PermissionTree.EndUpdate();
        }
        public static void ShowControls(Control.ControlCollection controlCollection, ListBox PageControls, ListBox TabControls)
        {
            foreach (Control c in controlCollection)
            {
                try
                {
                    if (c.Controls.Count > 0)
                    {
                        ShowControls(c.Controls, PageControls, TabControls);
                    }
                    if (c is MenuStrip)
                    {
                        MenuStrip menuStrip = c as MenuStrip;
                        ShowToolStripMenuItems(menuStrip.Items, PageControls);
                    }
                    if (c is ToolStrip)
                    {
                        ToolStrip toolStrip = c as ToolStrip;
                        ShowToolStripItems(toolStrip.Items, PageControls);
                    }
                    if (c is Button || c is ComboBox || c is TextBox ||
                        c is ListBox || c is DataGridView || c is RadioButton ||
                        c is RichTextBox || c is Label || c is TreeView ||
                        c is GroupBox || c is TabControl || c is TabPage ||
                        c is TableLayoutPanel || c is Panel || c is CheckBox ||
                        c is PictureBox || c is LinkLabel || 
                        c is CheckComboBoxTest.CheckedComboBox ||
                        c is CheckedListBox)
                    {
                        PageControls.Items.Add(c.Name);
                    }
                    if (c is TabPage)
                    {
                        TabControls.Items.Add(c.Name);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog.ErrorRoutine(ex);
                }
            }
        }
        public static void ShowControls(Control.ControlCollection controlCollection, ListBox PageControls)
        {
            foreach (Control c in controlCollection)
            {
                try
                {
                    if (c.Controls.Count > 0)
                    {
                        ShowControls(c.Controls, PageControls);
                    }
                    if (c is MenuStrip)
                    {
                        MenuStrip menuStrip = c as MenuStrip;
                        ShowToolStripMenuItems(menuStrip.Items, PageControls);
                    }
                    if (c is ToolStrip)
                    {
                        ToolStrip toolStrip = c as ToolStrip;
                        ShowToolStripItems(toolStrip.Items, PageControls);
                    }

                    if (c is Button || c is ComboBox || c is TextBox ||
                        c is ListBox || c is DataGridView || c is RadioButton ||
                        c is RichTextBox || c is Label || c is TreeView ||
                        c is GroupBox || c is TabControl || c is TabPage ||
                        c is TableLayoutPanel || c is Panel || c is CheckBox ||
                         c is PictureBox || c is LinkLabel ||
                        c is CheckComboBoxTest.CheckedComboBox ||
                        c is CheckedListBox)
                    {
                        PageControls.Items.Add(c.Name);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog.ErrorRoutine(ex);
                }
            }
        }
        public static TabPage GetTabByName(TabControl tabControl, string tabPage)
        {
            foreach (TabPage page in tabControl.TabPages)
            {
                try
                {
                    if (page.Name == tabPage)
                        return page;
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog.ErrorRoutine(ex);
                }
            }
            return null;
        }

        public static void ShowToolStripMenuItems(ToolStripItemCollection toolStripItems, ListBox PageControls)
        {
            foreach (ToolStripMenuItem mi in toolStripItems)
            {
                try
                {
                    if(!oldMenuToolTips.ContainsKey(mi.Name))
                    oldMenuToolTips.Add(mi.Name, mi.ToolTipText);
                    mi.ToolTipText = mi.Name;

                    if (mi.DropDownItems.Count > 0)
                    {
                        ShowToolStripMenuItems(mi.DropDownItems, PageControls);
                    }
                    PageControls.Items.Add(mi.Name);
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog.ErrorRoutine(ex);
                }
            }
        }

        public static void ShowToolStripItems(ToolStripItemCollection toolStripItems, ListBox PageControls)
        {
            foreach (ToolStripItem mi in toolStripItems)
            {
                try
                {
                    if (!oldMenuToolTips.ContainsKey(mi.Name))
                    oldMenuToolTips.Add(mi.Name, mi.ToolTipText);
                    mi.ToolTipText = mi.Name;
                    PageControls.Items.Add(mi.Name);
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog.ErrorRoutine(ex);
                }
            }
        }
        public static void ShowToolStripMenuItems(ToolStripItemCollection toolStripItems,TabPage page)
        {
            foreach (ToolStripMenuItem mi in toolStripItems)
            {
                try
                {
                    if (!oldMenuToolTips.ContainsKey(mi.Name))
                        oldMenuToolTips.Add(mi.Name, mi.ToolTipText);
                    mi.ToolTipText = mi.Name;

                    if (mi.DropDownItems.Count > 0)
                    {
                        ShowToolStripMenuItems(mi.DropDownItems,page);
                    }
                    DataRow row = dtPanelData.NewRow();
                    row["PanelName"] = page.Name;
                    row["PanelControlId"] = mi.Name;
                    dtPanelData.Rows.Add(row);
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog.ErrorRoutine(ex);
                }
            }
        }

        public static void ShowToolStripItems(ToolStripItemCollection toolStripItems,TabPage page)
        {
            foreach (ToolStripItem mi in toolStripItems)
            {
                try
                {
                    if (!oldMenuToolTips.ContainsKey(mi.Name))
                        oldMenuToolTips.Add(mi.Name, mi.ToolTipText);
                    mi.ToolTipText = mi.Name;
                    DataRow row = dtPanelData.NewRow();
                    row["PanelName"] = page.Name;
                    row["PanelControlId"] = mi.Name;
                    dtPanelData.Rows.Add(row);
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog.ErrorRoutine(ex);
                }
            }
        }

        public static void PopulateControlIdByTabPage(ListBox panelControlIdList, params string[] parentTab)
        {
            if (parentTab != null)
            {
                if (parentTab.Length > 0)
                {
                    if (dtPanelData != null)
                    {
                        if (dtPanelData.Rows.Count > 0)
                        {
                            foreach (string tab in parentTab)
                            {
                                DataRow[] drarray;
                                drarray = dtPanelData.Select("PanelName = '" + tab + "'", "PanelControlId", DataViewRowState.CurrentRows);
                                foreach (DataRow row in drarray)
                                {
                                    panelControlIdList.Items.Add(row["PanelControlId"].ToString().Trim());
                                }
                            }
                        }
                    }
                }
            }
        }
        //private void RestoreMenuStripToolTips(ToolStripItemCollection toolStripItems)
        //{
        //    foreach (ToolStripMenuItem mi in toolStripItems)
        //    {
        //        if (mi.DropDownItems.Count > 0)
        //        {
        //            RestoreMenuStripToolTips(mi.DropDownItems);
        //        }

        //        if (oldMenuToolTips.ContainsKey(mi.Name))
        //        {
        //            mi.ToolTipText = oldMenuToolTips[mi.Name];
        //        }
        //        else
        //        {
        //            mi.ToolTipText = string.Empty;
        //        }       // end else
        //    }           // end foreach
        //}               // end RestoreMenuStripToolTips

        public static void LoadConfigurations()
        {
            Dictionary<string, string> config = GetConfigurations();
            if(config !=null)
            if (config.Count > 0)
            {
                foreach (var pair in config)
                {
                    if (!string.IsNullOrEmpty(pair.Value.Trim()))
                    {
                        switch (pair.Key)
                        {
                            case "delayTime": SourceCode.DelayTime = Convert.ToInt32(pair.Value.Trim());
                                break;
                            case "logFile": Logger.ErrorLog.LogFilePath = pair.Value.Trim();
                                break;
                            case "appDirectory": FileBackUp.RootDirectory = pair.Value.Trim();
                                break;
                            case "processorMonitorFile": Monitor.ProcessMonitor.LogFilePath = pair.Value.Trim();
                                break;
                        }
                    }
                }
            }
        }

        public static Dictionary<string, string> GetConfigurations()
        {
            string file = string.Empty;
            Dictionary<string, string> config = new Dictionary<string, string>();
            FileBackUp b = new FileBackUp();
            file = DIRECTORY.GetConfigFileName();
            if (!string.IsNullOrEmpty(file))
            {
                string str = b.ReadFile(file);
                string[] configArray = str.Split('|', '\n');
                if (configArray.Length > 0 && !string.Empty.Equals(str))
                {
                    for (int i = 0; i < configArray.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            config.Add(configArray[i].Trim(), configArray[i + 1].Trim());
                        }
                    }
                }
                return config;
            }
            else
            {
                return null;
            }
        }
        
        public static string ProcStartargs(string name, string args)
        {
            try
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = name,
                        Arguments = args,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.GetEncoding(866)
                    }
                };
                proc.Start();
                string line = null;
                while (!proc.StandardOutput.EndOfStream)
                {
                    line += "\n" + proc.StandardOutput.ReadLine();
                }
                return line;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
