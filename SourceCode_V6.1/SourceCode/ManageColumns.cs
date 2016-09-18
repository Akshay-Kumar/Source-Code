using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SourceCode
{
    public partial class ManageColumns : Form
    {
        Dictionary<string, bool> columnList = new Dictionary<string, bool>();
        public ManageColumns(DataGridView dgvPrograms)
        {
            InitializeComponent();
            ShowColumnData(dgvPrograms);
        }
        public void ShowColumnData(DataGridView dgvPrograms)
        {
            try
            {
                foreach (DataGridViewColumn column in dgvPrograms.Columns)
                {
                    checkedListBox1.Items.Add(column.Name, column.Visible);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog.ErrorRoutine(ex);
            }
        }
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < checkedListBox1.Items.Count;i++ )
                {
                    if (columnList.ContainsKey(checkedListBox1.GetItemText(checkedListBox1.Items[i])))
                    {
                        columnList[checkedListBox1.GetItemText(checkedListBox1.Items[i])] = checkedListBox1.GetItemChecked(i);
                    }
                    else
                    {
                        columnList.Add(checkedListBox1.GetItemText(checkedListBox1.Items[i]), checkedListBox1.GetItemChecked(i));
                    }
                }
                GlobalPageTracker.sourceCodeObj.ManageColumnVisibility(columnList);
            }
            catch (Exception ex)
            {
                Logger.ErrorLog.ErrorRoutine(ex);
            }
            
        }

        private void ManageColumns_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalPageTracker.sourceCodeObj.ToggleImage(Properties.Resources.Lock_2_icon);
        }
    }
}
