using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using EmailAPI;
using Logger;

namespace Email
{
    public partial class MailApp : Form
    {
        SendMail mail = null;
        SendMail.RecepientData data = null;
        OpenFileDialog openFile = null;
        public MailApp()
        {
            InitializeComponent();
            mail = new SendMail();
            data = new SendMail.RecepientData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool success = false;
            data.RecepientName = txtName.Text.Trim();
            data.RecepientEmail = txtEmail.Text.Trim();
            data.Subject = txtSubject.Text.Trim();
            data.MessageContent = rtbMessage.Text.Trim();
            string[] attachmentFileArray = null;
            try
            {
                if (lstBoxAttachments.Items.Count > 0)
                {
                    attachmentFileArray = new string[lstBoxAttachments.Items.Count];
                    for (int i = 0; i < lstBoxAttachments.Items.Count; i++)
                    {
                        attachmentFileArray[i] = lstBoxAttachments.Items[i].ToString().Trim();
                    }
                    success = mail.SendMessage(SendMail.MailType.HtmlWithAttachmentEmail, data, attachmentFileArray);
                }
                else
                {
                    success = mail.SendMessage(SendMail.MailType.HtmlWithAttachmentEmail, data);
                }
                if (success)
                    MessageBox.Show("Message sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Message not sent!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }

        private void ckbAttachments_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbAttachments.Checked == true)
            {
                lstBoxAttachments.Visible = true;
                btnOpenFile.Visible = true;
                btnRemove.Visible = true;
            }
            else
            {
                lstBoxAttachments.Visible = !true;
                btnOpenFile.Visible = !true;
                btnRemove.Visible = !true;
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string[] fileNames = OpenFileAttachments();
            lstBoxAttachments.Items.Clear();
            try
            {
                if(fileNames!=null)
                foreach (string file in fileNames)
                {
                    lstBoxAttachments.Items.Add(file);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }

        public string[] OpenFileAttachments()
        {
            string[] fileNames = null;
            openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            openFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;
            // Show the dialog.
            try
            {
                DialogResult result = openFile.ShowDialog();
                // Test result.
                if (result == DialogResult.OK)
                {
                    fileNames = openFile.FileNames;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                ErrorLog.ErrorRoutine(ex);
            }
            return fileNames;
        }

        private void lstBoxAttachments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBoxAttachments.Items.Count > 0 && lstBoxAttachments.SelectedIndex != -1)
            {
                btnRemove.Enabled = true;
            }
            else
            {
                btnRemove.Enabled = !true;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstBoxAttachments.Items.Count > 1)
                {
                    for (int i = lstBoxAttachments.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        lstBoxAttachments.Items.Remove(lstBoxAttachments.SelectedItems[i]);
                    }
                }
                else
                {
                    lstBoxAttachments.Items.Remove(lstBoxAttachments.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
        }
    }
}
