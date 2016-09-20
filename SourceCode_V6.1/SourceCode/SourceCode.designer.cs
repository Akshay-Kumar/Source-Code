namespace SourceCode
{
    partial class SourceCode
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Fetch All Users");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Add User");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Edit User");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Manager Users", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Fetch All Roles");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Fetch Tabs By Role");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Add Role");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Edit Role");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Delete Role");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Manage Roles", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9});
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceCode));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSourceCode = new System.Windows.Forms.TabPage();
            this.sourceCodeStatusStrip = new System.Windows.Forms.StatusStrip();
            this.sourceCodeToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsDDBtnUserName = new System.Windows.Forms.ToolStripDropDownButton();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsBtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvPrograms = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSearchBox = new System.Windows.Forms.TextBox();
            this.ccbCodeLanguage = new CheckComboBoxTest.CheckedComboBox();
            this.btnSearchCode = new System.Windows.Forms.Button();
            this.tabAddSourceCode = new System.Windows.Forms.TabPage();
            this.addSourceCodeToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsBtnSaveSource = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnAddSourceCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnEditSourceCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnDocumentMap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnMarker = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnViewLog = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnCompareCode_Click = new System.Windows.Forms.ToolStripButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnCancelSource = new System.Windows.Forms.Button();
            this.txtProgramName = new System.Windows.Forms.TextBox();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.btnSaveSource = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.usersToolStrip = new System.Windows.Forms.ToolStrip();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExportUser = new System.Windows.Forms.Button();
            this.dgvUserData = new System.Windows.Forms.DataGridView();
            this.tabRoles = new System.Windows.Forms.TabPage();
            this.rolesToolStrip = new System.Windows.Forms.ToolStrip();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.grpBoxRights = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.grpBoxRoles = new System.Windows.Forms.GroupBox();
            this.dgvRoleData = new System.Windows.Forms.DataGridView();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.aboutToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tabHelp = new System.Windows.Forms.TabPage();
            this.helpToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tabArchive = new System.Windows.Forms.TabPage();
            this.archiveToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsBtnProcessMonitor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnViewDeletedPrograms = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSendMail = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnLogViewer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.dgvDeletedPrograms = new System.Windows.Forms.DataGridView();
            this.tabRolePermissions = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblPermissions = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tvRolesToControls = new System.Windows.Forms.TreeView();
            this.RoleList = new System.Windows.Forms.ListBox();
            this.tvPanelControls = new System.Windows.Forms.TreeView();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.PanelList = new System.Windows.Forms.ListBox();
            this.PanelControlsList = new System.Windows.Forms.ListBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.Disabled = new System.Windows.Forms.CheckBox();
            this.Invisible = new System.Windows.Forms.CheckBox();
            this.rolePermissionToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsBtnRefreshPanels = new System.Windows.Forms.ToolStripButton();
            this.logoutLinkLbl = new System.Windows.Forms.LinkLabel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.exportToExcelFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.linkLogLbl = new System.Windows.Forms.LinkLabel();
            this.sourceCodeTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.lblUser = new AutoEllipsis.LabelEllipsis();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabSourceCode.SuspendLayout();
            this.sourceCodeToolStrip.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrograms)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabAddSourceCode.SuspendLayout();
            this.addSourceCodeToolStrip.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserData)).BeginInit();
            this.tabRoles.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpBoxRights.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.grpBoxRoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoleData)).BeginInit();
            this.tabAbout.SuspendLayout();
            this.aboutToolStrip.SuspendLayout();
            this.tabHelp.SuspendLayout();
            this.helpToolStrip.SuspendLayout();
            this.tabArchive.SuspendLayout();
            this.archiveToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeletedPrograms)).BeginInit();
            this.tabRolePermissions.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.rolePermissionToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabSourceCode);
            this.tabControl1.Controls.Add(this.tabAddSourceCode);
            this.tabControl1.Controls.Add(this.tabUsers);
            this.tabControl1.Controls.Add(this.tabRoles);
            this.tabControl1.Controls.Add(this.tabAbout);
            this.tabControl1.Controls.Add(this.tabHelp);
            this.tabControl1.Controls.Add(this.tabArchive);
            this.tabControl1.Controls.Add(this.tabRolePermissions);
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 26);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(728, 435);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabSourceCode
            // 
            this.tabSourceCode.Controls.Add(this.sourceCodeStatusStrip);
            this.tabSourceCode.Controls.Add(this.sourceCodeToolStrip);
            this.tabSourceCode.Controls.Add(this.groupBox4);
            this.tabSourceCode.Controls.Add(this.groupBox3);
            this.tabSourceCode.Location = new System.Drawing.Point(4, 22);
            this.tabSourceCode.Name = "tabSourceCode";
            this.tabSourceCode.Size = new System.Drawing.Size(720, 409);
            this.tabSourceCode.TabIndex = 0;
            this.tabSourceCode.Text = "SourceCode";
            this.tabSourceCode.UseVisualStyleBackColor = true;
            // 
            // sourceCodeStatusStrip
            // 
            this.sourceCodeStatusStrip.Location = new System.Drawing.Point(0, 387);
            this.sourceCodeStatusStrip.Name = "sourceCodeStatusStrip";
            this.sourceCodeStatusStrip.Size = new System.Drawing.Size(720, 22);
            this.sourceCodeStatusStrip.TabIndex = 3;
            // 
            // sourceCodeToolStrip
            // 
            this.sourceCodeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsDDBtnUserName,
            this.tsBtnRefresh,
            this.toolStripButton2});
            this.sourceCodeToolStrip.Location = new System.Drawing.Point(0, 0);
            this.sourceCodeToolStrip.Name = "sourceCodeToolStrip";
            this.sourceCodeToolStrip.Size = new System.Drawing.Size(720, 25);
            this.sourceCodeToolStrip.TabIndex = 2;
            this.sourceCodeToolStrip.Text = "toolStrip1";
            // 
            // tsDDBtnUserName
            // 
            this.tsDDBtnUserName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsDDBtnUserName.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePasswordToolStripMenuItem,
            this.viewProfileToolStripMenuItem,
            this.editProfileToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.tsDDBtnUserName.Image = global::SourceCode.Properties.Resources.group_icon;
            this.tsDDBtnUserName.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDDBtnUserName.Name = "tsDDBtnUserName";
            this.tsDDBtnUserName.Size = new System.Drawing.Size(90, 22);
            this.tsDDBtnUserName.Text = "userName";
            this.tsDDBtnUserName.ToolTipText = "User Name";
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = global::SourceCode.Properties.Resources.Alarm_Padlock_icon;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.changePasswordToolStripMenuItem.Text = "change password";
            this.changePasswordToolStripMenuItem.ToolTipText = "change password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // viewProfileToolStripMenuItem
            // 
            this.viewProfileToolStripMenuItem.Image = global::SourceCode.Properties.Resources.Actions_office_chart_pie_icon;
            this.viewProfileToolStripMenuItem.Name = "viewProfileToolStripMenuItem";
            this.viewProfileToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.viewProfileToolStripMenuItem.Text = "view profile";
            this.viewProfileToolStripMenuItem.ToolTipText = "view profile";
            this.viewProfileToolStripMenuItem.Click += new System.EventHandler(this.viewProfileToolStripMenuItem_Click);
            // 
            // editProfileToolStripMenuItem
            // 
            this.editProfileToolStripMenuItem.Image = global::SourceCode.Properties.Resources.marker_icon;
            this.editProfileToolStripMenuItem.Name = "editProfileToolStripMenuItem";
            this.editProfileToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.editProfileToolStripMenuItem.Text = "edit profile";
            this.editProfileToolStripMenuItem.ToolTipText = "edit profile";
            this.editProfileToolStripMenuItem.Click += new System.EventHandler(this.editProfileToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Image = global::SourceCode.Properties.Resources.Alarm_Error_icon;
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.logoutToolStripMenuItem.Text = "logout";
            this.logoutToolStripMenuItem.ToolTipText = "logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // tsBtnRefresh
            // 
            this.tsBtnRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnRefresh.Image = global::SourceCode.Properties.Resources.refresh_icon;
            this.tsBtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRefresh.Name = "tsBtnRefresh";
            this.tsBtnRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsBtnRefresh.ToolTipText = "Refresh";
            this.tsBtnRefresh.Click += new System.EventHandler(this.tsBtnRefresh_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::SourceCode.Properties.Resources.Lock_2_icon;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Manage gridview columns";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.dgvPrograms);
            this.groupBox4.Location = new System.Drawing.Point(8, 71);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(704, 313);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // dgvPrograms
            // 
            this.dgvPrograms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPrograms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.dgvPrograms.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPrograms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPrograms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPrograms.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPrograms.Location = new System.Drawing.Point(3, 12);
            this.dgvPrograms.Name = "dgvPrograms";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPrograms.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPrograms.Size = new System.Drawing.Size(698, 295);
            this.dgvPrograms.TabIndex = 0;
            this.dgvPrograms.DataSourceChanged += new System.EventHandler(this.dgvPrograms_DataSourceChanged);
            this.dgvPrograms.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPrograms_CellContentClick);
            this.dgvPrograms.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPrograms_CellDoubleClick);
            this.dgvPrograms.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvPrograms_DataBindingComplete);
            this.dgvPrograms.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvPrograms_MouseClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtSearchBox);
            this.groupBox3.Controls.Add(this.ccbCodeLanguage);
            this.groupBox3.Controls.Add(this.btnSearchCode);
            this.groupBox3.Location = new System.Drawing.Point(8, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(704, 51);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // txtSearchBox
            // 
            this.txtSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchBox.Location = new System.Drawing.Point(171, 16);
            this.txtSearchBox.Name = "txtSearchBox";
            this.txtSearchBox.Size = new System.Drawing.Size(241, 20);
            this.txtSearchBox.TabIndex = 3;
            this.txtSearchBox.TextChanged += new System.EventHandler(this.txtSearchBox_TextChanged);
            this.txtSearchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchBox_KeyDown);
            // 
            // ccbCodeLanguage
            // 
            this.ccbCodeLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ccbCodeLanguage.CheckOnClick = true;
            this.ccbCodeLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ccbCodeLanguage.DropDownHeight = 1;
            this.ccbCodeLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ccbCodeLanguage.FormattingEnabled = true;
            this.ccbCodeLanguage.IntegralHeight = false;
            this.ccbCodeLanguage.Location = new System.Drawing.Point(420, 16);
            this.ccbCodeLanguage.Name = "ccbCodeLanguage";
            this.ccbCodeLanguage.Size = new System.Drawing.Size(80, 21);
            this.ccbCodeLanguage.TabIndex = 2;
            this.ccbCodeLanguage.ValueSeparator = ", ";
            this.ccbCodeLanguage.DropDownClosed += new System.EventHandler(this.ccbCodeLanguage_DropDownClosed);
            // 
            // btnSearchCode
            // 
            this.btnSearchCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchCode.Location = new System.Drawing.Point(508, 16);
            this.btnSearchCode.Name = "btnSearchCode";
            this.btnSearchCode.Size = new System.Drawing.Size(75, 23);
            this.btnSearchCode.TabIndex = 1;
            this.btnSearchCode.Text = "Search";
            this.btnSearchCode.UseVisualStyleBackColor = true;
            this.btnSearchCode.Click += new System.EventHandler(this.btnSearchCode_Click);
            // 
            // tabAddSourceCode
            // 
            this.tabAddSourceCode.Controls.Add(this.addSourceCodeToolStrip);
            this.tabAddSourceCode.Controls.Add(this.groupBox6);
            this.tabAddSourceCode.Controls.Add(this.richTextBox1);
            this.tabAddSourceCode.Location = new System.Drawing.Point(4, 22);
            this.tabAddSourceCode.Name = "tabAddSourceCode";
            this.tabAddSourceCode.Size = new System.Drawing.Size(720, 409);
            this.tabAddSourceCode.TabIndex = 1;
            this.tabAddSourceCode.Text = "AddSourceCode";
            this.tabAddSourceCode.UseVisualStyleBackColor = true;
            // 
            // addSourceCodeToolStrip
            // 
            this.addSourceCodeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnSaveSource,
            this.toolStripSeparator8,
            this.tsBtnAddSourceCode,
            this.toolStripSeparator4,
            this.tsBtnEditSourceCode,
            this.toolStripSeparator6,
            this.tsBtnEditor,
            this.toolStripSeparator7,
            this.tsBtnDocumentMap,
            this.toolStripSeparator5,
            this.tsBtnMarker,
            this.toolStripSeparator10,
            this.tsBtnViewLog,
            this.toolStripSeparator2,
            this.tsBtnCompareCode_Click});
            this.addSourceCodeToolStrip.Location = new System.Drawing.Point(0, 0);
            this.addSourceCodeToolStrip.Name = "addSourceCodeToolStrip";
            this.addSourceCodeToolStrip.Size = new System.Drawing.Size(720, 25);
            this.addSourceCodeToolStrip.TabIndex = 2;
            this.addSourceCodeToolStrip.Text = "toolStrip1";
            // 
            // tsBtnSaveSource
            // 
            this.tsBtnSaveSource.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSaveSource.Enabled = false;
            this.tsBtnSaveSource.Image = global::SourceCode.Properties.Resources.Programming_Save_icon;
            this.tsBtnSaveSource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSaveSource.Name = "tsBtnSaveSource";
            this.tsBtnSaveSource.Size = new System.Drawing.Size(23, 22);
            this.tsBtnSaveSource.ToolTipText = "Save Source code";
            this.tsBtnSaveSource.Click += new System.EventHandler(this.tsBtnSaveSource_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnAddSourceCode
            // 
            this.tsBtnAddSourceCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnAddSourceCode.Image = global::SourceCode.Properties.Resources.Add_icon;
            this.tsBtnAddSourceCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAddSourceCode.Name = "tsBtnAddSourceCode";
            this.tsBtnAddSourceCode.Size = new System.Drawing.Size(23, 22);
            this.tsBtnAddSourceCode.ToolTipText = "Add Source code";
            this.tsBtnAddSourceCode.Click += new System.EventHandler(this.tsBtnAddSourceCode_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnEditSourceCode
            // 
            this.tsBtnEditSourceCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnEditSourceCode.Enabled = false;
            this.tsBtnEditSourceCode.Image = global::SourceCode.Properties.Resources.Code_Window_icon1;
            this.tsBtnEditSourceCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnEditSourceCode.Name = "tsBtnEditSourceCode";
            this.tsBtnEditSourceCode.Size = new System.Drawing.Size(23, 22);
            this.tsBtnEditSourceCode.ToolTipText = "Edit Source code";
            this.tsBtnEditSourceCode.Click += new System.EventHandler(this.tsBtnEditSourceCode_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnEditor
            // 
            this.tsBtnEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnEditor.Image = global::SourceCode.Properties.Resources.Apps_accessories_text_editor_icon1;
            this.tsBtnEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnEditor.Name = "tsBtnEditor";
            this.tsBtnEditor.Size = new System.Drawing.Size(23, 22);
            this.tsBtnEditor.ToolTipText = "Code editor";
            this.tsBtnEditor.Click += new System.EventHandler(this.tsBtnEditor_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnDocumentMap
            // 
            this.tsBtnDocumentMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnDocumentMap.Image = global::SourceCode.Properties.Resources.Document_Flow_Chart_icon;
            this.tsBtnDocumentMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDocumentMap.Name = "tsBtnDocumentMap";
            this.tsBtnDocumentMap.Size = new System.Drawing.Size(23, 22);
            this.tsBtnDocumentMap.ToolTipText = "Document map";
            this.tsBtnDocumentMap.Click += new System.EventHandler(this.tsBtnDocumentMap_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnMarker
            // 
            this.tsBtnMarker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnMarker.Image = global::SourceCode.Properties.Resources.marker_icon;
            this.tsBtnMarker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnMarker.Name = "tsBtnMarker";
            this.tsBtnMarker.Size = new System.Drawing.Size(23, 22);
            this.tsBtnMarker.ToolTipText = "Marker";
            this.tsBtnMarker.Click += new System.EventHandler(this.tsBtnMarker_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnViewLog
            // 
            this.tsBtnViewLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnViewLog.Image = global::SourceCode.Properties.Resources.action_log_icon;
            this.tsBtnViewLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnViewLog.Name = "tsBtnViewLog";
            this.tsBtnViewLog.Size = new System.Drawing.Size(23, 22);
            this.tsBtnViewLog.ToolTipText = "View log";
            this.tsBtnViewLog.Click += new System.EventHandler(this.tsBtnViewLog_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnCompareCode_Click
            // 
            this.tsBtnCompareCode_Click.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnCompareCode_Click.Image = global::SourceCode.Properties.Resources.copy_icon;
            this.tsBtnCompareCode_Click.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnCompareCode_Click.Name = "tsBtnCompareCode_Click";
            this.tsBtnCompareCode_Click.Size = new System.Drawing.Size(23, 22);
            this.tsBtnCompareCode_Click.ToolTipText = "Compare codes";
            this.tsBtnCompareCode_Click.Click += new System.EventHandler(this.tsBtnCompareCode_Click_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.btnCancelSource);
            this.groupBox6.Controls.Add(this.txtProgramName);
            this.groupBox6.Controls.Add(this.cbLanguage);
            this.groupBox6.Controls.Add(this.btnSaveSource);
            this.groupBox6.Location = new System.Drawing.Point(8, 315);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(704, 86);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            // 
            // btnCancelSource
            // 
            this.btnCancelSource.Location = new System.Drawing.Point(503, 19);
            this.btnCancelSource.Name = "btnCancelSource";
            this.btnCancelSource.Size = new System.Drawing.Size(75, 23);
            this.btnCancelSource.TabIndex = 7;
            this.btnCancelSource.Text = "Cancel";
            this.btnCancelSource.UseVisualStyleBackColor = true;
            this.btnCancelSource.Click += new System.EventHandler(this.btnCancelSource_Click);
            // 
            // txtProgramName
            // 
            this.txtProgramName.Location = new System.Drawing.Point(11, 19);
            this.txtProgramName.Name = "txtProgramName";
            this.txtProgramName.Size = new System.Drawing.Size(319, 20);
            this.txtProgramName.TabIndex = 5;
            // 
            // cbLanguage
            // 
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(337, 18);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(71, 21);
            this.cbLanguage.TabIndex = 4;
            this.cbLanguage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbLanguage_MouseClick);
            // 
            // btnSaveSource
            // 
            this.btnSaveSource.Enabled = false;
            this.btnSaveSource.Location = new System.Drawing.Point(422, 19);
            this.btnSaveSource.Name = "btnSaveSource";
            this.btnSaveSource.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSource.TabIndex = 0;
            this.btnSaveSource.Text = "Save";
            this.btnSaveSource.UseVisualStyleBackColor = true;
            this.btnSaveSource.Click += new System.EventHandler(this.btnSaveSource_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(4, 28);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(713, 288);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.richTextBox1.Enter += new System.EventHandler(this.richTextBox1_Enter);
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            this.richTextBox1.Leave += new System.EventHandler(this.richTextBox1_Leave);
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.usersToolStrip);
            this.tabUsers.Controls.Add(this.groupBox5);
            this.tabUsers.Controls.Add(this.groupBox1);
            this.tabUsers.Location = new System.Drawing.Point(4, 22);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Size = new System.Drawing.Size(720, 409);
            this.tabUsers.TabIndex = 2;
            this.tabUsers.Text = "Users";
            this.tabUsers.UseVisualStyleBackColor = true;
            // 
            // usersToolStrip
            // 
            this.usersToolStrip.Location = new System.Drawing.Point(0, 0);
            this.usersToolStrip.Name = "usersToolStrip";
            this.usersToolStrip.Size = new System.Drawing.Size(720, 25);
            this.usersToolStrip.TabIndex = 2;
            this.usersToolStrip.Text = "toolStrip5";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.treeView1);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(530, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(182, 378);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Manage Users";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(6, 19);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "nodeFetchAllUsers";
            treeNode1.Text = "Fetch All Users";
            treeNode2.Name = "nodeAddUser";
            treeNode2.Text = "Add User";
            treeNode3.Name = "nodeEditUser";
            treeNode3.Text = "Edit User";
            treeNode4.Name = "nodeManageUsers";
            treeNode4.Text = "Manager Users";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView1.Size = new System.Drawing.Size(170, 350);
            this.treeView1.TabIndex = 2;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 378);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Users List";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnExportUser, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.dgvUserData, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(5, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 397F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(505, 356);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // btnExportUser
            // 
            this.btnExportUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportUser.Location = new System.Drawing.Point(402, 330);
            this.btnExportUser.Name = "btnExportUser";
            this.btnExportUser.Size = new System.Drawing.Size(100, 23);
            this.btnExportUser.TabIndex = 2;
            this.btnExportUser.Text = "Export to excel";
            this.btnExportUser.UseVisualStyleBackColor = true;
            this.btnExportUser.Click += new System.EventHandler(this.btnExportUser_Click);
            // 
            // dgvUserData
            // 
            this.dgvUserData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUserData.Location = new System.Drawing.Point(3, 3);
            this.dgvUserData.Name = "dgvUserData";
            this.dgvUserData.Size = new System.Drawing.Size(499, 321);
            this.dgvUserData.TabIndex = 0;
            this.dgvUserData.DataSourceChanged += new System.EventHandler(this.dgvUserData_DataSourceChanged);
            this.dgvUserData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserData_CellClick);
            this.dgvUserData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserData_CellDoubleClick_1);
            this.dgvUserData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvUserData_DataBindingComplete);
            // 
            // tabRoles
            // 
            this.tabRoles.Controls.Add(this.rolesToolStrip);
            this.tabRoles.Controls.Add(this.groupBox2);
            this.tabRoles.Controls.Add(this.grpBoxRights);
            this.tabRoles.Controls.Add(this.grpBoxRoles);
            this.tabRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabRoles.Location = new System.Drawing.Point(4, 22);
            this.tabRoles.Name = "tabRoles";
            this.tabRoles.Size = new System.Drawing.Size(720, 409);
            this.tabRoles.TabIndex = 3;
            this.tabRoles.Text = "Roles";
            this.tabRoles.UseVisualStyleBackColor = true;
            // 
            // rolesToolStrip
            // 
            this.rolesToolStrip.Location = new System.Drawing.Point(0, 0);
            this.rolesToolStrip.Name = "rolesToolStrip";
            this.rolesToolStrip.Size = new System.Drawing.Size(720, 25);
            this.rolesToolStrip.TabIndex = 5;
            this.rolesToolStrip.Text = "toolStrip4";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Location = new System.Drawing.Point(530, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 376);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Manage Roles";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.treeView2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(170, 354);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeView2
            // 
            this.treeView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView2.Location = new System.Drawing.Point(3, 3);
            this.treeView2.Name = "treeView2";
            treeNode5.Name = "nodeFetchAllRoles";
            treeNode5.Text = "Fetch All Roles";
            treeNode6.Name = "nodeFetchTabsByRole";
            treeNode6.Text = "Fetch Tabs By Role";
            treeNode7.Name = "nodeAddRole";
            treeNode7.Text = "Add Role";
            treeNode8.Name = "nodeEditRole";
            treeNode8.Text = "Edit Role";
            treeNode9.Name = "nodeDeleteRole";
            treeNode9.Text = "Delete Role";
            treeNode10.Name = "nodeManageRoles";
            treeNode10.Text = "Manage Roles";
            this.treeView2.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10});
            this.treeView2.Size = new System.Drawing.Size(164, 348);
            this.treeView2.TabIndex = 13;
            this.treeView2.DoubleClick += new System.EventHandler(this.treeView2_DoubleClick);
            // 
            // grpBoxRights
            // 
            this.grpBoxRights.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxRights.Controls.Add(this.btnExport);
            this.grpBoxRights.Controls.Add(this.tableLayoutPanel5);
            this.grpBoxRights.Controls.Add(this.tableLayoutPanel3);
            this.grpBoxRights.Location = new System.Drawing.Point(8, 244);
            this.grpBoxRights.Name = "grpBoxRights";
            this.grpBoxRights.Size = new System.Drawing.Size(513, 157);
            this.grpBoxRights.TabIndex = 3;
            this.grpBoxRights.TabStop = false;
            this.grpBoxRights.Text = "Rights";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(385, 19);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(105, 23);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "Export To Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.checkedListBox1, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(33, 19);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(247, 101);
            this.tableLayoutPanel5.TabIndex = 11;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(3, 3);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(241, 94);
            this.checkedListBox1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.btnCancel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnEdit, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(33, 126);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(247, 29);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(85, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 22);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(3, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 22);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // grpBoxRoles
            // 
            this.grpBoxRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxRoles.Controls.Add(this.dgvRoleData);
            this.grpBoxRoles.Location = new System.Drawing.Point(8, 25);
            this.grpBoxRoles.Name = "grpBoxRoles";
            this.grpBoxRoles.Size = new System.Drawing.Size(513, 220);
            this.grpBoxRoles.TabIndex = 0;
            this.grpBoxRoles.TabStop = false;
            this.grpBoxRoles.Text = "Roles";
            // 
            // dgvRoleData
            // 
            this.dgvRoleData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRoleData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoleData.Location = new System.Drawing.Point(6, 16);
            this.dgvRoleData.Name = "dgvRoleData";
            this.dgvRoleData.Size = new System.Drawing.Size(501, 198);
            this.dgvRoleData.TabIndex = 4;
            this.dgvRoleData.DataSourceChanged += new System.EventHandler(this.dgvRoleData_DataSourceChanged);
            this.dgvRoleData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRoleData_CellClick);
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.aboutToolStrip);
            this.tabAbout.Location = new System.Drawing.Point(4, 22);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(720, 409);
            this.tabAbout.TabIndex = 4;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // aboutToolStrip
            // 
            this.aboutToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.aboutToolStrip.Location = new System.Drawing.Point(0, 0);
            this.aboutToolStrip.Name = "aboutToolStrip";
            this.aboutToolStrip.Size = new System.Drawing.Size(720, 25);
            this.aboutToolStrip.TabIndex = 0;
            this.aboutToolStrip.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::SourceCode.Properties.Resources.Very_Basic_About_icon2;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tabHelp
            // 
            this.tabHelp.Controls.Add(this.helpToolStrip);
            this.tabHelp.Location = new System.Drawing.Point(4, 22);
            this.tabHelp.Name = "tabHelp";
            this.tabHelp.Size = new System.Drawing.Size(720, 409);
            this.tabHelp.TabIndex = 5;
            this.tabHelp.Text = "Help";
            this.tabHelp.UseVisualStyleBackColor = true;
            // 
            // helpToolStrip
            // 
            this.helpToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.helpToolStrip.Location = new System.Drawing.Point(0, 0);
            this.helpToolStrip.Name = "helpToolStrip";
            this.helpToolStrip.Size = new System.Drawing.Size(720, 25);
            this.helpToolStrip.TabIndex = 0;
            this.helpToolStrip.Text = "toolStrip3";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(69, 22);
            this.toolStripLabel1.Text = "Help Topics";
            // 
            // tabArchive
            // 
            this.tabArchive.Controls.Add(this.archiveToolStrip);
            this.tabArchive.Controls.Add(this.dgvDeletedPrograms);
            this.tabArchive.Location = new System.Drawing.Point(4, 22);
            this.tabArchive.Name = "tabArchive";
            this.tabArchive.Size = new System.Drawing.Size(720, 409);
            this.tabArchive.TabIndex = 6;
            this.tabArchive.Text = "Archive";
            this.tabArchive.UseVisualStyleBackColor = true;
            // 
            // archiveToolStrip
            // 
            this.archiveToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnProcessMonitor,
            this.toolStripSeparator1,
            this.tsBtnViewDeletedPrograms,
            this.toolStripSeparator3,
            this.tsBtnSendMail,
            this.toolStripSeparator9,
            this.tsBtnLogViewer,
            this.toolStripSeparator11,
            this.toolStripButton3,
            this.toolStripSeparator12,
            this.toolStripButton4});
            this.archiveToolStrip.Location = new System.Drawing.Point(0, 0);
            this.archiveToolStrip.Name = "archiveToolStrip";
            this.archiveToolStrip.Size = new System.Drawing.Size(720, 25);
            this.archiveToolStrip.TabIndex = 5;
            this.archiveToolStrip.Text = "toolStrip1";
            // 
            // tsBtnProcessMonitor
            // 
            this.tsBtnProcessMonitor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnProcessMonitor.Image = global::SourceCode.Properties.Resources.process_accept_icon;
            this.tsBtnProcessMonitor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnProcessMonitor.Name = "tsBtnProcessMonitor";
            this.tsBtnProcessMonitor.Size = new System.Drawing.Size(23, 22);
            this.tsBtnProcessMonitor.ToolTipText = "Process monitor";
            this.tsBtnProcessMonitor.Click += new System.EventHandler(this.tsBtnProcessMonitor_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnViewDeletedPrograms
            // 
            this.tsBtnViewDeletedPrograms.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnViewDeletedPrograms.Image = global::SourceCode.Properties.Resources.Start_Up_Items_icon;
            this.tsBtnViewDeletedPrograms.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnViewDeletedPrograms.Name = "tsBtnViewDeletedPrograms";
            this.tsBtnViewDeletedPrograms.Size = new System.Drawing.Size(23, 22);
            this.tsBtnViewDeletedPrograms.ToolTipText = "View deleted programs";
            this.tsBtnViewDeletedPrograms.Click += new System.EventHandler(this.tsBtnViewDeletedPrograms_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnSendMail
            // 
            this.tsBtnSendMail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSendMail.Image = global::SourceCode.Properties.Resources.email_send_icon;
            this.tsBtnSendMail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSendMail.Name = "tsBtnSendMail";
            this.tsBtnSendMail.Size = new System.Drawing.Size(23, 22);
            this.tsBtnSendMail.ToolTipText = "Send mail";
            this.tsBtnSendMail.Click += new System.EventHandler(this.tsBtnSendMail_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnLogViewer
            // 
            this.tsBtnLogViewer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnLogViewer.Image = global::SourceCode.Properties.Resources.action_log_icon;
            this.tsBtnLogViewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnLogViewer.Name = "tsBtnLogViewer";
            this.tsBtnLogViewer.Size = new System.Drawing.Size(23, 22);
            this.tsBtnLogViewer.ToolTipText = "View log";
            this.tsBtnLogViewer.Click += new System.EventHandler(this.tsBtnLogViewer_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::SourceCode.Properties.Resources.folder_open_document_icon;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // dgvDeletedPrograms
            // 
            this.dgvDeletedPrograms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDeletedPrograms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDeletedPrograms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDeletedPrograms.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDeletedPrograms.Location = new System.Drawing.Point(4, 28);
            this.dgvDeletedPrograms.Name = "dgvDeletedPrograms";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDeletedPrograms.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDeletedPrograms.Size = new System.Drawing.Size(713, 378);
            this.dgvDeletedPrograms.TabIndex = 1;
            this.dgvDeletedPrograms.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDeletedPrograms_CellContentClick);
            // 
            // tabRolePermissions
            // 
            this.tabRolePermissions.Controls.Add(this.groupBox8);
            this.tabRolePermissions.Controls.Add(this.groupBox7);
            this.tabRolePermissions.Controls.Add(this.Cancel);
            this.tabRolePermissions.Controls.Add(this.btnSaveSettings);
            this.tabRolePermissions.Controls.Add(this.Disabled);
            this.tabRolePermissions.Controls.Add(this.Invisible);
            this.tabRolePermissions.Controls.Add(this.rolePermissionToolStrip);
            this.tabRolePermissions.Location = new System.Drawing.Point(4, 22);
            this.tabRolePermissions.Name = "tabRolePermissions";
            this.tabRolePermissions.Padding = new System.Windows.Forms.Padding(3);
            this.tabRolePermissions.Size = new System.Drawing.Size(720, 409);
            this.tabRolePermissions.TabIndex = 7;
            this.tabRolePermissions.Text = "RolePermissions";
            this.tabRolePermissions.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.lblPermissions);
            this.groupBox8.Controls.Add(this.btnRemove);
            this.groupBox8.Controls.Add(this.btnAdd);
            this.groupBox8.Controls.Add(this.tvRolesToControls);
            this.groupBox8.Controls.Add(this.RoleList);
            this.groupBox8.Controls.Add(this.tvPanelControls);
            this.groupBox8.Location = new System.Drawing.Point(309, 31);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(323, 369);
            this.groupBox8.TabIndex = 13;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Role Permissions";
            // 
            // lblPermissions
            // 
            this.lblPermissions.AutoSize = true;
            this.lblPermissions.Location = new System.Drawing.Point(7, 221);
            this.lblPermissions.Name = "lblPermissions";
            this.lblPermissions.Size = new System.Drawing.Size(35, 13);
            this.lblPermissions.TabIndex = 17;
            this.lblPermissions.Text = "label1";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(123, 104);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(45, 23);
            this.btnRemove.TabIndex = 16;
            this.btnRemove.Text = "<<";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(123, 75);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(45, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = ">>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tvRolesToControls
            // 
            this.tvRolesToControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvRolesToControls.CheckBoxes = true;
            this.tvRolesToControls.Location = new System.Drawing.Point(174, 27);
            this.tvRolesToControls.Name = "tvRolesToControls";
            this.tvRolesToControls.ShowNodeToolTips = true;
            this.tvRolesToControls.Size = new System.Drawing.Size(143, 328);
            this.tvRolesToControls.TabIndex = 14;
            this.tvRolesToControls.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvRolesToControls_AfterCheck);
            // 
            // RoleList
            // 
            this.RoleList.FormattingEnabled = true;
            this.RoleList.Location = new System.Drawing.Point(6, 27);
            this.RoleList.Name = "RoleList";
            this.RoleList.Size = new System.Drawing.Size(111, 186);
            this.RoleList.Sorted = true;
            this.RoleList.TabIndex = 13;
            this.RoleList.SelectedValueChanged += new System.EventHandler(this.RoleList_SelectedValueChanged);
            // 
            // tvPanelControls
            // 
            this.tvPanelControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvPanelControls.Location = new System.Drawing.Point(6, 243);
            this.tvPanelControls.Name = "tvPanelControls";
            this.tvPanelControls.Size = new System.Drawing.Size(162, 112);
            this.tvPanelControls.TabIndex = 12;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox7.Controls.Add(this.PanelList);
            this.groupBox7.Controls.Add(this.PanelControlsList);
            this.groupBox7.Location = new System.Drawing.Point(6, 31);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(291, 369);
            this.groupBox7.TabIndex = 12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Panel Controls";
            // 
            // PanelList
            // 
            this.PanelList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PanelList.FormattingEnabled = true;
            this.PanelList.Location = new System.Drawing.Point(8, 26);
            this.PanelList.Name = "PanelList";
            this.PanelList.Size = new System.Drawing.Size(120, 329);
            this.PanelList.Sorted = true;
            this.PanelList.TabIndex = 4;
            this.PanelList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelList_MouseUp);
            // 
            // PanelControlsList
            // 
            this.PanelControlsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelControlsList.FormattingEnabled = true;
            this.PanelControlsList.Location = new System.Drawing.Point(151, 26);
            this.PanelControlsList.Name = "PanelControlsList";
            this.PanelControlsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.PanelControlsList.Size = new System.Drawing.Size(132, 329);
            this.PanelControlsList.Sorted = true;
            this.PanelControlsList.TabIndex = 3;
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.Location = new System.Drawing.Point(638, 146);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 9;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSettings.Location = new System.Drawing.Point(638, 117);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSettings.TabIndex = 8;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // Disabled
            // 
            this.Disabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Disabled.AutoSize = true;
            this.Disabled.Location = new System.Drawing.Point(638, 75);
            this.Disabled.Name = "Disabled";
            this.Disabled.Size = new System.Drawing.Size(67, 17);
            this.Disabled.TabIndex = 7;
            this.Disabled.Text = "Disabled";
            this.Disabled.UseVisualStyleBackColor = true;
            // 
            // Invisible
            // 
            this.Invisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Invisible.AutoSize = true;
            this.Invisible.Location = new System.Drawing.Point(638, 52);
            this.Invisible.Name = "Invisible";
            this.Invisible.Size = new System.Drawing.Size(64, 17);
            this.Invisible.TabIndex = 6;
            this.Invisible.Text = "Invisible";
            this.Invisible.UseVisualStyleBackColor = true;
            // 
            // rolePermissionToolStrip
            // 
            this.rolePermissionToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnRefreshPanels});
            this.rolePermissionToolStrip.Location = new System.Drawing.Point(3, 3);
            this.rolePermissionToolStrip.Name = "rolePermissionToolStrip";
            this.rolePermissionToolStrip.Size = new System.Drawing.Size(714, 25);
            this.rolePermissionToolStrip.TabIndex = 0;
            this.rolePermissionToolStrip.Text = "toolStrip1";
            // 
            // tsBtnRefreshPanels
            // 
            this.tsBtnRefreshPanels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnRefreshPanels.Image = global::SourceCode.Properties.Resources.refresh_icon;
            this.tsBtnRefreshPanels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRefreshPanels.Name = "tsBtnRefreshPanels";
            this.tsBtnRefreshPanels.Size = new System.Drawing.Size(23, 22);
            this.tsBtnRefreshPanels.ToolTipText = "Refresh";
            this.tsBtnRefreshPanels.Click += new System.EventHandler(this.tsBtnRefreshPanels_Click);
            // 
            // logoutLinkLbl
            // 
            this.logoutLinkLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logoutLinkLbl.AutoSize = true;
            this.logoutLinkLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logoutLinkLbl.Location = new System.Drawing.Point(684, 3);
            this.logoutLinkLbl.Name = "logoutLinkLbl";
            this.logoutLinkLbl.Size = new System.Drawing.Size(40, 13);
            this.logoutLinkLbl.TabIndex = 1;
            this.logoutLinkLbl.TabStop = true;
            this.logoutLinkLbl.Text = "Logout";
            this.logoutLinkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.logoutLinkLbl_LinkClicked);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(550, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(114, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // exportToExcelFileDialog
            // 
            this.exportToExcelFileDialog.DefaultExt = "xls";
            this.exportToExcelFileDialog.FileName = "Untitled";
            this.exportToExcelFileDialog.Filter = "Excel Worksheets (*.xls)|*.xls";
            // 
            // linkLogLbl
            // 
            this.linkLogLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLogLbl.AutoSize = true;
            this.linkLogLbl.Location = new System.Drawing.Point(501, 4);
            this.linkLogLbl.Name = "linkLogLbl";
            this.linkLogLbl.Size = new System.Drawing.Size(46, 13);
            this.linkLogLbl.TabIndex = 5;
            this.linkLogLbl.TabStop = true;
            this.linkLogLbl.Text = "view log";
            this.linkLogLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLogLbl_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::SourceCode.Properties.Resources.Logos_Google_Web_Search_icon2;
            this.pictureBox1.Location = new System.Drawing.Point(664, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.Location = new System.Drawing.Point(9, 3);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(714, 25);
            this.miniToolStrip.TabIndex = 0;
            // 
            // lblUser
            // 
            this.lblUser.AutoEllipsis = AutoEllipsis.EllipsisFormat.None;
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(9, 7);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(117, 13);
            this.lblUser.TabIndex = 6;
            this.lblUser.Text = "UserName                    ";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // SourceCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 461);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.linkLogLbl);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.logoutLinkLbl);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SourceCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SourceCode";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SourceCode_FormClosed);
            this.Resize += new System.EventHandler(this.SourceCode_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabSourceCode.ResumeLayout(false);
            this.tabSourceCode.PerformLayout();
            this.sourceCodeToolStrip.ResumeLayout(false);
            this.sourceCodeToolStrip.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrograms)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabAddSourceCode.ResumeLayout(false);
            this.tabAddSourceCode.PerformLayout();
            this.addSourceCodeToolStrip.ResumeLayout(false);
            this.addSourceCodeToolStrip.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabUsers.ResumeLayout(false);
            this.tabUsers.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserData)).EndInit();
            this.tabRoles.ResumeLayout(false);
            this.tabRoles.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpBoxRights.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.grpBoxRoles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoleData)).EndInit();
            this.tabAbout.ResumeLayout(false);
            this.tabAbout.PerformLayout();
            this.aboutToolStrip.ResumeLayout(false);
            this.aboutToolStrip.PerformLayout();
            this.tabHelp.ResumeLayout(false);
            this.tabHelp.PerformLayout();
            this.helpToolStrip.ResumeLayout(false);
            this.helpToolStrip.PerformLayout();
            this.tabArchive.ResumeLayout(false);
            this.tabArchive.PerformLayout();
            this.archiveToolStrip.ResumeLayout(false);
            this.archiveToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeletedPrograms)).EndInit();
            this.tabRolePermissions.ResumeLayout(false);
            this.tabRolePermissions.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.rolePermissionToolStrip.ResumeLayout(false);
            this.rolePermissionToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSourceCode;
        private System.Windows.Forms.TabPage tabAddSourceCode;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.TabPage tabRoles;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.TabPage tabHelp;
        private System.Windows.Forms.GroupBox grpBoxRoles;
        private System.Windows.Forms.GroupBox grpBoxRights;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.LinkLabel logoutLinkLbl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridView dgvUserData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.DataGridView dgvRoleData;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.SaveFileDialog exportToExcelFileDialog;
        private System.Windows.Forms.Button btnExportUser;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSearchCode;
        private System.Windows.Forms.TextBox txtSearchBox;
        private CheckComboBoxTest.CheckedComboBox ccbCodeLanguage;
        private System.Windows.Forms.DataGridView dgvPrograms;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.LinkLabel linkLogLbl;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnSaveSource;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.TextBox txtProgramName;
        private System.Windows.Forms.Button btnCancelSource;
        private System.Windows.Forms.TabPage tabArchive;
        private AutoEllipsis.LabelEllipsis lblUser;
        private System.Windows.Forms.ToolTip sourceCodeTooltip;
        private System.Windows.Forms.DataGridView dgvDeletedPrograms;
        private System.Windows.Forms.ToolStrip archiveToolStrip;
        private System.Windows.Forms.ToolStripButton tsBtnProcessMonitor;
        private System.Windows.Forms.ToolStripButton tsBtnViewDeletedPrograms;
        private System.Windows.Forms.ToolStripButton tsBtnSendMail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStrip addSourceCodeToolStrip;
        private System.Windows.Forms.ToolStripButton tsBtnDocumentMap;
        private System.Windows.Forms.ToolStripButton tsBtnEditor;
        private System.Windows.Forms.ToolStripButton tsBtnMarker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsBtnAddSourceCode;
        private System.Windows.Forms.ToolStripButton tsBtnEditSourceCode;
        private System.Windows.Forms.ToolStripButton tsBtnSaveSource;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsBtnViewLog;
        private System.Windows.Forms.ToolStripButton tsBtnLogViewer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStrip sourceCodeToolStrip;
        private System.Windows.Forms.ToolStrip rolesToolStrip;
        private System.Windows.Forms.ToolStrip aboutToolStrip;
        private System.Windows.Forms.ToolStrip helpToolStrip;
        private System.Windows.Forms.ToolStrip usersToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton tsDDBtnUserName;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnCompareCode_Click;
        private System.Windows.Forms.ToolStripButton tsBtnRefresh;
        private System.Windows.Forms.StatusStrip sourceCodeStatusStrip;
        private System.Windows.Forms.TabPage tabRolePermissions;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TreeView tvRolesToControls;
        private System.Windows.Forms.ListBox RoleList;
        private System.Windows.Forms.TreeView tvPanelControls;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ListBox PanelList;
        private System.Windows.Forms.ListBox PanelControlsList;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.CheckBox Disabled;
        private System.Windows.Forms.CheckBox Invisible;
        private System.Windows.Forms.ToolStrip rolePermissionToolStrip;
        private System.Windows.Forms.ToolStrip miniToolStrip;
        private System.Windows.Forms.ToolStripButton tsBtnRefreshPanels;
        private System.Windows.Forms.Label lblPermissions;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
    }
}

