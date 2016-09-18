using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Logger;
using SourceCodeBL;
using SourceCodeDAL;

namespace SourceCode
{
    public class TabUtils
    {
        static TabPage[] cloneTabControl = null;
        public static List<TabPage> HiddenPages = new List<TabPage>();
        public static void ActivateTabControls(TabPage tabPage, DataTable tabDataTable)
        {
            DataRow[] drarray;
            drarray = tabDataTable.Select("PANEL_NAME = '" + tabPage.Name + "'", "PANEL_CONTROL_ID", DataViewRowState.CurrentRows);

            foreach (DataRow row in drarray)
            {

                Object c = GetPageControls(tabPage.Controls, row["PANEL_CONTROL_ID"].ToString().Trim());
                //Control c = FindControl(tabPage, row["PANEL_CONTROL_ID"].ToString().Trim());
                if (c != null)
                {
                    if (c is Button)
                    {
                        Button btn = c as Button;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is TabControl)
                    {
                        TabControl btn = c as TabControl;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is GroupBox)
                    {
                        GroupBox btn = c as GroupBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is Button)
                    {
                        Button btn = c as Button;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is TreeView)
                    {
                        TreeView btn = c as TreeView;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is CheckBox)
                    {
                        CheckBox btn = c as CheckBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is Label)
                    {
                        Label btn = c as Label;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is RichTextBox)
                    {
                        RichTextBox btn = c as RichTextBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is RadioButton)
                    {
                        RadioButton btn = c as RadioButton;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));

                    }
                    if (c is DataGridView)
                    {
                        DataGridView btn = c as DataGridView;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is ListBox)
                    {
                        ListBox btn = c as ListBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is TextBox)
                    {
                        TextBox btn = c as TextBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is ComboBox)
                    {
                        ComboBox btn = c as ComboBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is TabPage)
                    {
                        TabPage btn = c as TabPage;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is TableLayoutPanel)
                    {
                        TableLayoutPanel btn = c as TableLayoutPanel;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is Panel)
                    {
                        Panel btn = c as Panel;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is ToolStripButton)
                    {
                        ToolStripButton btn = c as ToolStripButton;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is ToolStripDropDownButton)
                    {
                        ToolStripDropDownButton btn = c as ToolStripDropDownButton;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is ToolStripSeparator)
                    {
                        ToolStripSeparator btn = c as ToolStripSeparator;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is MenuStrip)
                    {
                        MenuStrip btn = c as MenuStrip;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is CheckedListBox)
                    {
                        CheckedListBox btn = c as CheckedListBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is CheckComboBoxTest.CheckedComboBox)
                    {
                        CheckComboBoxTest.CheckedComboBox btn = c as CheckComboBoxTest.CheckedComboBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is PictureBox)
                    {
                        PictureBox btn = c as PictureBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is LinkLabel)
                    {
                        LinkLabel btn = c as LinkLabel;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                }
            }
        }
        public static void ActivatePageControls(Form Page, DataTable tabDataTable)
        {
            DataRow[] drarray;
            drarray = tabDataTable.Select("PANEL_NAME = '" + Page.Name + "'", "PANEL_CONTROL_ID", DataViewRowState.CurrentRows);

            foreach (DataRow row in drarray)
            {

                Object c = GetPageControls(Page.Controls, row["PANEL_CONTROL_ID"].ToString().Trim());
                //Control c = FindControl(tabPage, row["PANEL_CONTROL_ID"].ToString().Trim());
                if (c != null)
                {
                    if (c is Button)
                    {
                        Button btn = c as Button;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is TabControl)
                    {
                        TabControl btn = c as TabControl;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is GroupBox)
                    {
                        GroupBox btn = c as GroupBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is Button)
                    {
                        Button btn = c as Button;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is TreeView)
                    {
                        TreeView btn = c as TreeView;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is CheckBox)
                    {
                        CheckBox btn = c as CheckBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is Label)
                    {
                        Label btn = c as Label;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is RichTextBox)
                    {
                        RichTextBox btn = c as RichTextBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is RadioButton)
                    {
                        RadioButton btn = c as RadioButton;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));

                    }
                    if (c is DataGridView)
                    {
                        DataGridView btn = c as DataGridView;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is ListBox)
                    {
                        ListBox btn = c as ListBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is TextBox)
                    {
                        TextBox btn = c as TextBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is ComboBox)
                    {
                        ComboBox btn = c as ComboBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is TabPage)
                    {
                        TabPage btn = c as TabPage;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is TableLayoutPanel)
                    {
                        TableLayoutPanel btn = c as TableLayoutPanel;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is Panel)
                    {
                        Panel btn = c as Panel;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is ToolStripButton)
                    {
                        ToolStripButton btn = c as ToolStripButton;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is ToolStripDropDownButton)
                    {
                        ToolStripDropDownButton btn = c as ToolStripDropDownButton;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is ToolStripSeparator)
                    {
                        ToolStripSeparator btn = c as ToolStripSeparator;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is MenuStrip)
                    {
                        MenuStrip btn = c as MenuStrip;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is CheckedListBox)
                    {
                        CheckedListBox btn = c as CheckedListBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is CheckComboBoxTest.CheckedComboBox)
                    {
                        CheckComboBoxTest.CheckedComboBox btn = c as CheckComboBoxTest.CheckedComboBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is PictureBox)
                    {
                        PictureBox btn = c as PictureBox;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                    if (c is LinkLabel)
                    {
                        LinkLabel btn = c as LinkLabel;
                        btn.Enabled = !Convert.ToBoolean(Convert.ToInt16(row["DISABLED"].ToString().Trim()));
                        btn.Visible = !Convert.ToBoolean(Convert.ToInt16(row["INVISIBLE"].ToString().Trim()));
                    }
                }
            }
        }
        //public static Control FindControl(Control root, string name)
        //{
        //    if (root == null) throw new ArgumentNullException("root");
        //    foreach (Control child in root.Controls)
        //    {
        //        if (child.Name == name) return child;
        //        Control found = FindControl(child, name);
        //        if (found != null) return found;
        //    }
        //    return null;
        //}
        //public static Control FindControl(Control.ControlCollection controlCollection, string Name)
        //{
        //     var toolstrip1 = controlCollection.Controls.Find(Name, true);
        //var toolstrip1Items = toolstrip1[0] as ToolStrip; <-- set to toolstrip control

        //var btnRead = toolstrip1Items.Items.Find("btnRead", true); <--get BtnRead on toolstrip Item.Find
        //btnRead[0].Enabled = false; <--disable/Enable btn
        //}
        public static Object GetPageControls(Control.ControlCollection controlCollection, string Name)
        {
            if (controlCollection == null) throw new ArgumentNullException("controlCollection");
            Object found = null;
            foreach (Control c in controlCollection)
            {
                try
                {
                    if (c.Name == Name)return c;
                    if (c.Controls.Count > 0)
                    {
                        found = GetPageControls(c.Controls, Name);
                        if (found != null) return found;
                    }
                    if (c is MenuStrip)
                    {
                        MenuStrip menuStrip = c as MenuStrip;
                        found = ShowMenuStripItems(menuStrip.Items, Name);
                        if (found != null) return found;
                    }
                    if (c is ToolStrip)
                    {
                        ToolStrip toolStrip = c as ToolStrip;
                        found = ShowToolStripItems(toolStrip.Items, Name);
                        if (found != null) return found;
                    }
                    
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog.ErrorRoutine(ex);
                }
            }
            return null;
        }
        public static Object ShowMenuStripItems(ToolStripItemCollection menuStripItems, string Name)
        {
            Object found = null;
            foreach (ToolStripMenuItem mi in menuStripItems)
            {
                try
                {
                    if (mi.DropDownItems.Count > 0)
                    {
                        found = ShowMenuStripItems(mi.DropDownItems, Name);
                        if (found != null) return found;
                    }
                    if (mi.Name == Name)
                        return mi;
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog.ErrorRoutine(ex);
                }
            }
            return null;
        }

        public static Object ShowToolStripItems(ToolStripItemCollection toolStripItems, string Name)
        {
            Object c = null;
            foreach (ToolStripItem mi in toolStripItems)
            {
                try
                {
                    if (mi.Name == Name)
                    {
                        switch (mi.GetType().Name)
                        {
                            case "ToolStripComboBox":
                                c = ((ToolStripComboBox)mi).Control;
                                break;
                            case "ToolStripTextBox":
                                c = ((ToolStripTextBox)mi).Control;
                                break;
                            case "ToolStripDropDownButton":
                                c = ((ToolStripDropDownButton)mi);
                                break;
                            case "ToolStripSplitButton":
                                c = ((ToolStripSplitButton)mi);
                                break;
                            case "ToolStripLabel":
                                c = ((ToolStripLabel)mi);
                                break;
                            case "ToolStripSeparator":
                                c = ((ToolStripSeparator)mi);
                                break;
                            case "ToolStripStatusLabel":
                                c = ((ToolStripStatusLabel)mi);
                                break;
                            default: c = mi;
                                break;
                        }
                        return c;
                    }
                }
                catch (Exception ex)
                {
                    Logger.ErrorLog.ErrorRoutine(ex);
                }
            }
            return null;
        }
        /// <summary>
        /// HideAllActiveTabs
        /// </summary>
        //private void HideAllActiveTabs()
        //{
        //    int tabPageCount = tabControl1.TabPages.Count;
        //    tabCollection = new Control[tabPageCount];
        //    TabPage tabPage = null;
        //    MakeRichTextBoxReadOnly(richTextBox1);
        //    ToggleEnableOrDisable(false, btnEdit, btnSave,
        //        btnCancel, checkedListBox1);
        //    try
        //    {
        //        for (int i = 0; i < tabPageCount; i++)
        //        {
        //            tabPage = tabControl1.TabPages[i];
        //            tabCollection[i] = tabPage;
        //        }
        //        for (int j = 0; j < tabPageCount; j++)
        //        {
        //            tabControl1.TabPages.Remove((TabPage)tabCollection[j]);
        //        }
        //    }
        //    catch (Exception exHandler)
        //    {
        //        ErrorLog.ErrorRoutine(exHandler);
        //    }
        //}
        
        public static void ClearHiddenPages()
        {
            HiddenPages.Clear();
        }
        private static TabPage GetCopyOfTabPage(TabPage page)
        {
            foreach (TabPage p in cloneTabControl)
            {
                if (p.Equals(page))
                {
                    return p;
                }
            }
            return null;
        }
        private static TabPage GetAnyVisibleTabPage()
        {
            foreach (TabPage p in cloneTabControl)
            {
                if (!HiddenPages.Contains(p))
                {
                    return p;
                }
            }
            return null;
        }
        public static TabPage HidePage(string name, TabControl tabControl)
        {
            TabPage aPage = null;
            if (name.Contains("tab"))
            {
                aPage = findShownPageByName(name, tabControl);
                if (aPage != null)
                {
                    if (tabControl.TabPages.Contains(aPage))
                    {
                        if (aPage.Name != "tab" + TABS.ROLE_PERMISSIONS)
                        {
                            tabControl.TabPages.Remove(aPage);
                            HiddenPages.Add(aPage);
                        }
                        else if (aPage.Name == "tab" + TABS.ROLE_PERMISSIONS &&
                            SourceCode.userData.ACCESSIBLE_TABS.Contains<string>("tab" + TABS.ROLE_PERMISSIONS))
                        {
                            //HiddenPages.Add(aPage);
                        }
                        else
                        {
                            tabControl.TabPages.Remove(aPage);
                            HiddenPages.Add(aPage);
                        }
                    }
                }
            }
            return aPage;
        }
        public static TabPage ShowPage(string name, TabControl tabControl)
        {
            TabPage aPage = null;
            if (name.Contains("tab"))
            {
                aPage = findHiddenPageByName(name, tabControl); //implement that function somewhere
                if (aPage != null)
                {
                    if (!tabControl.TabPages.Contains(aPage))
                    {
                        tabControl.TabPages.Add(aPage);
                        HiddenPages.Remove(aPage);
                    }
                }
            }
            return aPage;
        }

        public static TabPage findShownPageByName(string name,TabControl tabControl)
        {
            TabPage page = null;
            foreach (TabPage p in tabControl.TabPages)
            {
                if (p.Name == name && !HiddenPages.Contains(p))
                {
                    page = p;
                    break;
                }
            }
            return page;
        }

        public static TabPage findHiddenPageByName(string name,TabControl tabControl)
        {
            TabPage page = null;
            if (HiddenPages.Count > 0)
            {
                foreach (TabPage p in HiddenPages)
                {
                    if (p.Name == name && !tabControl.TabPages.Contains(p))
                    {
                        page = p;
                        break;
                    }
                }
            }
            return page;
        }
        public static TabPage[] CloneTabControl(TabControl tabControl)
        {
            TabPage[] tabPages = new TabPage[tabControl.TabPages.Count];
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                tabPages[i] = tabControl.TabPages[i];
            }
            TabPage[] tabPageClone = (TabPage[])tabPages.Clone();
            return tabPageClone;
        }
        
        public static void HideAllActiveTabs(TabControl tabControl)
        {
            try
            {
                cloneTabControl = CloneTabControl(tabControl);
                foreach (TabPage p in cloneTabControl)
                {
                    HidePage(p.Name, tabControl);
                }
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        public static bool InsertOrUpdatePanels(RolePermissions[] panelDataCollection,SourceDataAccessLayer dal)
        {
            bool error = false;
            error = dal.InsertOrUpdatePanels(panelDataCollection);
            return error;
        }
        public static DataTable SyncAllTabControls(TabControl tabControl, Form mainform, UserSession userData, SourceDataAccessLayer dal)
        {
            RolePermissions roleData;
            DataTable dtPanels = Utility.UTIL.GetAllTabControls(tabControl, mainform);
            if(dtPanels !=null)
            {
                if (dtPanels.Rows.Count > 0)
                {
                    RolePermissions[] roleDataCollection = new RolePermissions[dtPanels.Rows.Count];
                    for (int i = 0; i < dtPanels.Rows.Count; i++)
                    {
                        roleData = new SourceCodeBL.RolePermissions();
                        roleData.Role.RoleId = userData.RoleData.RoleId;
                        roleData.PanelName = dtPanels.Rows[i]["PanelName"].ToString().Trim();
                        roleData.PanelControlId = dtPanels.Rows[i]["PanelControlId"].ToString().Trim();
                        roleData.Invisible = 0;
                        roleData.Disabled = 0;
                        roleData.Access = 1;
                        roleDataCollection[i] = roleData;
                    }

                    dal.InsertOrUpdateRolePermissions(roleDataCollection);
                }
            }
            return dtPanels;
        }
        public static void UpdateRolePermissions(DataTable dtPermissions, SourceCodeDAL.SourceDataAccessLayer dal)
        {
            SourceCodeBL.RolePermissions roleData;
            if (dtPermissions != null)
            {
                if (dtPermissions.Rows.Count > 0)
                {
                    SourceCodeBL.RolePermissions[] roleDataCollection = new SourceCodeBL.RolePermissions[dtPermissions.Rows.Count];
                    for (int i = 0; i < dtPermissions.Rows.Count; i++)
                    {
                        roleData = new SourceCodeBL.RolePermissions();
                        roleData.Role.RoleId = dal.GetRoleIdByRoleName(dtPermissions.Rows[i]["RoleName"].ToString().Trim());
                        roleData.PanelName = dtPermissions.Rows[i]["PanelName"].ToString().Trim();
                        roleData.PanelControlId = dtPermissions.Rows[i]["PanelControlId"].ToString().Trim();
                        roleData.Invisible = Convert.ToInt32(dtPermissions.Rows[i]["Invisible"].ToString().Trim());
                        roleData.Disabled = Convert.ToInt32(dtPermissions.Rows[i]["Disabled"].ToString().Trim());
                        roleData.Access = Convert.ToInt32(dtPermissions.Rows[i]["Access"].ToString().Trim());
                        roleDataCollection[i] = roleData;
                    }

                    dal.InsertOrUpdateRolePermissions(roleDataCollection);
                }
            }
        }
    }
}
