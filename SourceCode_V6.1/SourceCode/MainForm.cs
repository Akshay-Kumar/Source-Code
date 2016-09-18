using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using Logger;

namespace LiveLogViewer
{
    public partial class MainForm : Form
    {
        #region Declaration
        private SearchBox searchBox = null;
        internal static bool Frozen = false; //If false then TextBoxes will scroll to the bottom when text is appended
        //Default log file
        internal string fileName = Logger.ErrorLog.LogFilePath;
        /*AppDomain.CurrentDomain.BaseDirectory +
        AppDomain.CurrentDomain.RelativeSearchPath + "" + DEFAULT_LOG_FILE.DefaultLogFolder + "\\" + DEFAULT_LOG_FILE.DefaultLogFile;*/
        #endregion
        
        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            DefaultLogFile();
        }
        #endregion

        #region Methods
        /// <summary>
        /// public void DefaultLogFile()
        /// </summary>
        public void DefaultLogFile()
        {
            if (fileName.Equals(string.Empty))
            {
                //Get Default log file path "Logger.log"
                fileName = Logger.ErrorLog.GetLogFilePath();
            }
            //Create the suffix required if the file is accessed across the network
            string Suffix = "";
            FileStream fs = null;

            //If we can't find the file create the default Logger.log file
            if (!System.IO.File.Exists(fileName))
            {
                try
                {
                    fs = File.Create(fileName);
                }
                catch (Exception) { }
                fs.Close();
            }
            FileInfo fileInf0 = new FileInfo(fileName);
            //Check to see if the file is being access across the network
            Suffix = CheckForNetworkShare(fileInf0.FullName);
            AddNewTab(fileInf0.FullName, Suffix);
        }
        /// <summary>
        /// private void AddButton_Click(object sender, EventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            //Show the OpenFileDialog
            if (LogOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Return if no files are selected
                if (LogOpenFileDialog.FileNames == null) return;

                //Scan through all files selected in the fialog
                foreach (string File in LogOpenFileDialog.FileNames)
                {
                    //Create the suffix required if the file is accessed across the network
                    string Suffix = "";

                    //Check to see if the file is already being monitored
                    if (CheckForExistingTab(File)) continue;

                    //Check to see if the file is being access across the network
                    Suffix = CheckForNetworkShare(File);

                    //If we can't find the file display an error message otherwise add the tab
                    if (!System.IO.File.Exists(File)) 
                        MessageBox.Show(string.Format("File not found '{0}'", File), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else 
                        AddNewTab(File, Suffix);
                }
            }
        }
        /// <summary>
        /// private void DeleteButton_Click(System.Object sender, System.EventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(System.Object sender, System.EventArgs e)
        {
            //Delete the currently selected tab
            DeleteTab(MainTabControl.SelectedIndex);
        }
        /// <summary>
        /// private void FreezeButton_Click(object sender, EventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FreezeButton_Click(object sender, EventArgs e)
        {
            //Toggle the freeze function
            switch (Frozen)
            {
                case false:
                    Frozen = true;
                    FreezeButton.Text = "Unfreeze";
                    break;
                case true:
                    Frozen = false;
                    FreezeButton.Text = "Freeze";
                    break;
            }
        }
        /// <summary>
        /// private void ClearButton_Click(object sender, EventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            //Clear the TextBox of the currently selected tab
            if (MainTabControl.SelectedTab != null)
                ((LogTabPage)MainTabControl.SelectedTab).TextBox.Clear();
        }
        /// <summary>
        /// private void AddNewTab(string FileName, string Suffix)
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Suffix"></param>
        private void AddNewTab(string FileName, string Suffix)
        {
            //Create a new LogTabPage and add it to the TabControl
            LogTabPage Page = new LogTabPage(FileName, Suffix);
            MainTabControl.TabPages.Add(Page);

            //Enable the controls
            ClearButton.Enabled = true;
            FreezeButton.Enabled = true;
            DeleteButton.Enabled = true;

            //Subscribe to the changed event
            Page.Watcher.Changed += new FileSystemEventHandler(Watcher_Changed);

            //Select the new page
            Page.Select();
            SourceCode.GlobalPageTracker.logTabPageObj = Page;
        }
        /// <summary>
        /// private void DeleteTab(int i)
        /// </summary>
        /// <param name="i"></param>
        private void DeleteTab(int i)
        {
            //Create a reference to the tab that will be deleted
            LogTabPage Page = (LogTabPage)MainTabControl.TabPages[i];

            //Unsubscribe from the event
            Page.Watcher.Changed -= new FileSystemEventHandler(Watcher_Changed);

            //Remove the page from the TabControl
            MainTabControl.TabPages.Remove(Page);

            //Dispose the objects
            Page.Watcher.Dispose();
            Page.Dispose();

            //If there are no tabs left then disable the controls and clear the LastUpdatedLabel
            if (MainTabControl.TabCount == 0) { ClearButton.Enabled = false; FreezeButton.Enabled = false; DeleteButton.Enabled = false; LastUpdatedLabel.Text = ""; }
        }

        /// <summary>
        /// void Watcher_Changed(object sender, FileSystemEventArgs e)
        /// Occurs when the File being watched has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            //Invoke the LastUpdate method if required
            if (this.InvokeRequired) this.Invoke(new Action(delegate() { LastUpdate(e); }));
            else LastUpdate(e);
        }
        /// <summary>
        /// private void LastUpdate(FileSystemEventArgs e)
        /// </summary>
        /// <param name="e"></param>
        private void LastUpdate(FileSystemEventArgs e)
        {
            //Notify the user which file was last updated and when
            LastUpdatedLabel.Text = string.Format("{0} at {1}", Path.GetFileName(e.FullPath), DateTime.Now.ToLongTimeString());
        }
        /// <summary>
        /// private static string CheckForNetworkShare(string File)
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        private static string CheckForNetworkShare(string File)
        {
            //Check to see if the file is being accessed over a network, if not return an empty string
            if (File.Substring(0, 2) == "\\\\")
            {
                string[] SplitString = Regex.Split(File, "\\\\");

                //Return the name/IP of the remote PC
                if (SplitString.Length > 2) return string.Format("on {0}", SplitString[2]);
            }
            return "";
        }
        /// <summary>
        /// private bool CheckForExistingTab(string File)
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        private bool CheckForExistingTab(string File)
        {
            //Loop through the LogTabPages
            foreach (LogTabPage Page in MainTabControl.TabPages)
            {
                //If the file is being monitored then notify the user and return true
                if (Page.Watcher.FileName == File)
                {
                    MessageBox.Show(string.Format("File already being monitored '{0}'", File));
                    return true;
                }
            }
            //File isn't being monitored already return false
            return false;
        }
        /// <summary>
        /// private void btnFullLog_Click(object sender, EventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFullLog_Click(object sender, EventArgs e)
        {
            StreamReader reader = null;
            LogTabPage page = null;
            string log = string.Empty;
            if (System.IO.File.Exists(fileName))
            {
                reader = new StreamReader(fileName);
                log = reader.ReadToEnd();
            }
            if (MainTabControl.SelectedTab != null)
            {
                page = ((LogTabPage)MainTabControl.SelectedTab);
            }
            if (MainTabControl.SelectedTab != null && page != null)
            {
                page.TextBox.Clear();
                page.TextBox.Text = log;
                //If the Frozen function isn't enabled then scroll to the bottom of the TextBox
                if (!MainForm.Frozen)
                {
                    page.TextBox.SelectionStart = page.TextBox.Text.Length;
                    page.TextBox.SelectionLength = 0;
                    page.TextBox.ScrollToCaret();
                }
            }
        }
        /// <summary>
        /// private void MainForm_KeyDown(object sender, KeyEventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                searchBox = new SearchBox();
                SourceCode.GlobalPageTracker.searchBoxObj = searchBox;
                SourceCode.GlobalPageTracker.searchBoxObj.Show();
                SourceCode.GlobalPageTracker.searchBoxObj.Focus();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                LogTabPage tabPage = ((LogTabPage)MainTabControl.SelectedTab);
                RichTextBox textBox = null; 
                
                if (MainTabControl.SelectedTab != null)
                {
                    textBox = tabPage.TextBox;
                }
                if (!string.IsNullOrEmpty(textBox.Text.Trim()))
                {
                    string selectedText = textBox.SelectedText;
                    if (!string.IsNullOrEmpty(selectedText))
                    {
                        tabPage.SaveMyFile(selectedText);
                    }
                    else
                    {
                        MessageBox.Show("Please select some text before saving !!!", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }   
                }
                else
                {
                    MessageBox.Show("Empty log file !!!", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SourceCode.GlobalPageTracker.searchBoxObj != null)
            {
                SourceCode.GlobalPageTracker.searchBoxObj.Dispose();
                SourceCode.GlobalPageTracker.searchBoxObj = null;
            }
            if (SourceCode.GlobalPageTracker.loginObj != null)
            {
                SourceCode.GlobalPageTracker.loginObj.Focus();
            }
            else if (SourceCode.GlobalPageTracker.sourceCodeObj != null)
            {
                SourceCode.GlobalPageTracker.sourceCodeObj.Focus();
            }
        }
        /// <summary>
        /// private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SourceCodeDAL.GlobalClass.LOGGER_DISPOSED = true;
        }
        #endregion
    }
}