using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtilitPackage;

namespace SourceCode
{
    public partial class ColorSyntax
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (ColorSyntax.ADD_SOURCECODE)
            {
                CustomEvents(true);
                ColorSyntax.dirtyForm = !fileSaved;
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private System.Windows.Forms.MainMenu sourceMainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem OpenMenu;
        private System.Windows.Forms.MenuItem SaveMenu;
        private System.Windows.Forms.MenuItem PrintPreviewMenu;
        private System.Windows.Forms.MenuItem PrintMenu;
        private System.Windows.Forms.MenuItem ExitMenu;
        private System.Windows.Forms.OpenFileDialog openSourceCodeFileDialog;
        private System.Windows.Forms.SaveFileDialog saveCodeFileDialog;
        private System.Windows.Forms.PrintPreviewDialog printPreviewSourceCodeDialog;
        private System.Drawing.Printing.PrintDocument printSourceCodeDocument;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorSyntax));
            this.sourceMainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.OpenMenu = new System.Windows.Forms.MenuItem();
            this.SaveMenu = new System.Windows.Forms.MenuItem();
            this.PrintPreviewMenu = new System.Windows.Forms.MenuItem();
            this.PrintMenu = new System.Windows.Forms.MenuItem();
            this.ExitMenu = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.EditCodeMenu = new System.Windows.Forms.MenuItem();
            this.UpdateMenu = new System.Windows.Forms.MenuItem();
            this.DeleteMenu = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.openSourceCodeFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveCodeFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.printPreviewSourceCodeDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printSourceCodeDocument = new System.Drawing.Printing.PrintDocument();
            this.cmsSourceCode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Undo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.Compare = new System.Windows.Forms.ToolStripMenuItem();
            this.CodeWithFile = new System.Windows.Forms.ToolStripMenuItem();
            this.FileWithFile = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Font = new System.Windows.Forms.ToolStripMenuItem();
            this.Forcolor = new System.Windows.Forms.ToolStripMenuItem();
            this.Backcolor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new UtilitPackage.FlickerFreeRichEditTextBox();
            this.cmsSourceCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // sourceMainMenu
            // 
            this.sourceMainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.OpenMenu,
            this.SaveMenu,
            this.PrintPreviewMenu,
            this.PrintMenu,
            this.ExitMenu});
            this.menuItem1.Text = "File";
            // 
            // OpenMenu
            // 
            this.OpenMenu.Index = 0;
            this.OpenMenu.Text = "Open";
            this.OpenMenu.Click += new System.EventHandler(this.OpenMenu_Click);
            // 
            // SaveMenu
            // 
            this.SaveMenu.Index = 1;
            this.SaveMenu.Text = "Save";
            this.SaveMenu.Click += new System.EventHandler(this.SaveMenu_Click);
            // 
            // PrintPreviewMenu
            // 
            this.PrintPreviewMenu.Index = 2;
            this.PrintPreviewMenu.Text = "Print Preview...";
            this.PrintPreviewMenu.Click += new System.EventHandler(this.PrintPreviewMenu_Click);
            // 
            // PrintMenu
            // 
            this.PrintMenu.Index = 3;
            this.PrintMenu.Text = "Print";
            this.PrintMenu.Click += new System.EventHandler(this.PrintMenu_Click);
            // 
            // ExitMenu
            // 
            this.ExitMenu.Index = 4;
            this.ExitMenu.Text = "Close";
            this.ExitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.EditCodeMenu,
            this.UpdateMenu,
            this.DeleteMenu,
            this.menuItem3});
            this.menuItem2.Text = "Edit";
            // 
            // EditCodeMenu
            // 
            this.EditCodeMenu.Index = 0;
            this.EditCodeMenu.Text = "Edit Code";
            this.EditCodeMenu.Click += new System.EventHandler(this.EditCodeMenu_Click);
            // 
            // UpdateMenu
            // 
            this.UpdateMenu.Index = 1;
            this.UpdateMenu.Text = "Update";
            this.UpdateMenu.Visible = false;
            this.UpdateMenu.Click += new System.EventHandler(this.UpdateMenu_Click);
            // 
            // DeleteMenu
            // 
            this.DeleteMenu.Index = 2;
            this.DeleteMenu.Text = "Delete";
            this.DeleteMenu.Visible = false;
            this.DeleteMenu.Click += new System.EventHandler(this.DeleteMenu_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.Text = "Refresh..";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // openSourceCodeFileDialog
            // 
            this.openSourceCodeFileDialog.Title = "Open Code File";
            // 
            // saveCodeFileDialog
            // 
            this.saveCodeFileDialog.DefaultExt = "doc";
            this.saveCodeFileDialog.FileName = "Untitled";
            this.saveCodeFileDialog.Filter = "Word Documents (*.doc)|*.doc|C (*.c)|*.c|C++ (*.cpp)|*.cpp|C# (*.cs)|*.cs|JAVA (*" +
    ".java)|*.java|SQL (*.sql)|*.sql|Text (*.txt)|*.txt|Pdf (*.pdf)|*.pdf;";
            this.saveCodeFileDialog.Title = "Save Code File";
            // 
            // printPreviewSourceCodeDialog
            // 
            this.printPreviewSourceCodeDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewSourceCodeDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewSourceCodeDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewSourceCodeDialog.Document = this.printSourceCodeDocument;
            this.printPreviewSourceCodeDialog.Enabled = true;
            this.printPreviewSourceCodeDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewSourceCodeDialog.Icon")));
            this.printPreviewSourceCodeDialog.Name = "printPreviewDialog1";
            this.printPreviewSourceCodeDialog.Visible = false;
            // 
            // printSourceCodeDocument
            // 
            this.printSourceCodeDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printSourceCodeDocument_PrintPage);
            // 
            // cmsSourceCode
            // 
            this.cmsSourceCode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Undo,
            this.toolStripSeparator3,
            this.Cut,
            this.Copy,
            this.Paste,
            this.toolStripSeparator4,
            this.Delete,
            this.toolStripSeparator5,
            this.Compare,
            this.SelectAll,
            this.toolStripSeparator1,
            this.Font,
            this.Forcolor,
            this.Backcolor,
            this.toolStripSeparator2,
            this.Refresh});
            this.cmsSourceCode.Name = "cmsSourceCode";
            this.cmsSourceCode.Size = new System.Drawing.Size(127, 276);
            this.cmsSourceCode.Opening += new System.ComponentModel.CancelEventHandler(this.cmsSourceCode_Opening);
            // 
            // Undo
            // 
            this.Undo.Image = global::SourceCode.Properties.Resources.Arrows_Undo_icon;
            this.Undo.Name = "Undo";
            this.Undo.Size = new System.Drawing.Size(126, 22);
            this.Undo.Text = "Undo";
            this.Undo.ToolTipText = "Undo";
            this.Undo.Click += new System.EventHandler(this.Undo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(123, 6);
            // 
            // Cut
            // 
            this.Cut.Image = global::SourceCode.Properties.Resources.Editing_Cut_Filled_icon;
            this.Cut.Name = "Cut";
            this.Cut.Size = new System.Drawing.Size(126, 22);
            this.Cut.Text = "Cut";
            this.Cut.ToolTipText = "Cut";
            this.Cut.Click += new System.EventHandler(this.Cut_Click);
            // 
            // Copy
            // 
            this.Copy.Image = global::SourceCode.Properties.Resources.copy_icon;
            this.Copy.Name = "Copy";
            this.Copy.Size = new System.Drawing.Size(126, 22);
            this.Copy.Text = "Copy";
            this.Copy.ToolTipText = "Copy";
            this.Copy.Click += new System.EventHandler(this.Copy_Click);
            // 
            // Paste
            // 
            this.Paste.Image = global::SourceCode.Properties.Resources.paste_icon;
            this.Paste.Name = "Paste";
            this.Paste.Size = new System.Drawing.Size(126, 22);
            this.Paste.Text = "Paste";
            this.Paste.ToolTipText = "Paste";
            this.Paste.Click += new System.EventHandler(this.Paste_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(123, 6);
            // 
            // Delete
            // 
            this.Delete.Image = global::SourceCode.Properties.Resources.Editing_Delete_icon;
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(126, 22);
            this.Delete.Text = "Delete";
            this.Delete.ToolTipText = "Delete";
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(123, 6);
            // 
            // Compare
            // 
            this.Compare.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CodeWithFile,
            this.FileWithFile});
            this.Compare.Image = global::SourceCode.Properties.Resources.Document_Copy_icon;
            this.Compare.Name = "Compare";
            this.Compare.Size = new System.Drawing.Size(126, 22);
            this.Compare.Text = "Compare";
            // 
            // CodeWithFile
            // 
            this.CodeWithFile.Name = "CodeWithFile";
            this.CodeWithFile.Size = new System.Drawing.Size(147, 22);
            this.CodeWithFile.Text = "Code with file";
            this.CodeWithFile.Click += new System.EventHandler(this.CodeWithFile_Click);
            // 
            // FileWithFile
            // 
            this.FileWithFile.Name = "FileWithFile";
            this.FileWithFile.Size = new System.Drawing.Size(147, 22);
            this.FileWithFile.Text = "File with file";
            this.FileWithFile.Click += new System.EventHandler(this.FileWithFile_Click);
            // 
            // SelectAll
            // 
            this.SelectAll.Image = global::SourceCode.Properties.Resources.Cursor_Select_icon;
            this.SelectAll.Name = "SelectAll";
            this.SelectAll.Size = new System.Drawing.Size(126, 22);
            this.SelectAll.Text = "Select All";
            this.SelectAll.ToolTipText = "Select All";
            this.SelectAll.Click += new System.EventHandler(this.SelectAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // Font
            // 
            this.Font.Image = global::SourceCode.Properties.Resources.Font_Window_icon2;
            this.Font.Name = "Font";
            this.Font.Size = new System.Drawing.Size(126, 22);
            this.Font.Text = "Font";
            this.Font.ToolTipText = "Font";
            this.Font.Click += new System.EventHandler(this.Font_Click);
            // 
            // Forcolor
            // 
            this.Forcolor.Image = global::SourceCode.Properties.Resources.Editing_Border_Color_icon2;
            this.Forcolor.Name = "Forcolor";
            this.Forcolor.Size = new System.Drawing.Size(126, 22);
            this.Forcolor.Text = "Forcolor";
            this.Forcolor.ToolTipText = "Forcolor";
            this.Forcolor.Click += new System.EventHandler(this.Forcolor_Click);
            // 
            // Backcolor
            // 
            this.Backcolor.Image = global::SourceCode.Properties.Resources.Download_2_icon2;
            this.Backcolor.Name = "Backcolor";
            this.Backcolor.Size = new System.Drawing.Size(126, 22);
            this.Backcolor.Text = "Backcolor";
            this.Backcolor.ToolTipText = "Backcolor";
            this.Backcolor.Click += new System.EventHandler(this.Backcolor_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(123, 6);
            // 
            // Refresh
            // 
            this.Refresh.Image = global::SourceCode.Properties.Resources.refresh_icon;
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(126, 22);
            this.Refresh.Text = "Refresh";
            this.Refresh.ToolTipText = "Refresh";
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(576, 51);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.richTextBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseDown);
            // 
            // ColorSyntax
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(576, 51);
            this.Controls.Add(this.richTextBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.sourceMainMenu;
            this.Name = "ColorSyntax";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Color Syntax Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColorSyntax_FormClosing);
            this.cmsSourceCode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FlickerFreeRichEditTextBox richTextBox1;
        private System.Windows.Forms.MenuItem EditCodeMenu;
        private System.Windows.Forms.ContextMenuStrip cmsSourceCode;
        private System.Windows.Forms.ToolStripMenuItem Undo;
        private System.Windows.Forms.ToolStripMenuItem Cut;
        private System.Windows.Forms.ToolStripMenuItem Copy;
        private System.Windows.Forms.ToolStripMenuItem Paste;
        private System.Windows.Forms.ToolStripMenuItem Delete;
        private System.Windows.Forms.ToolStripMenuItem SelectAll;
        private System.Windows.Forms.ToolStripMenuItem Font;
        private System.Windows.Forms.ToolStripMenuItem Forcolor;
        private System.Windows.Forms.ToolStripMenuItem Backcolor;
        private System.Windows.Forms.ToolStripMenuItem Refresh;
        private System.Windows.Forms.MenuItem UpdateMenu;
        private System.Windows.Forms.MenuItem DeleteMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem Compare;
        private System.Windows.Forms.ToolStripMenuItem CodeWithFile;
        private System.Windows.Forms.ToolStripMenuItem FileWithFile;
    }
 }
