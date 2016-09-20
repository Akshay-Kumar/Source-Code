namespace FileExplorer
{
    partial class Explorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Explorer));
            this.imageList16 = new System.Windows.Forms.ImageList(this.components);
            this.nudItemHeight = new System.Windows.Forms.NumericUpDown();
            this.nudIndent = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkShowLines = new System.Windows.Forms.CheckBox();
            this.chkShowRootLines = new System.Windows.Forms.CheckBox();
            this.chkShowPlusMinus = new System.Windows.Forms.CheckBox();
            this.chkCheckBoxes = new System.Windows.Forms.CheckBox();
            this.chkImageList = new System.Windows.Forms.CheckBox();
            this.chkOwnerDraw = new System.Windows.Forms.CheckBox();
            this.imageList24 = new System.Windows.Forms.ImageList(this.components);
            this.ocTreeView1 = new OC.Windows.Forms.ocTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIndent)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList16
            // 
            this.imageList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16.ImageStream")));
            this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList16.Images.SetKeyName(0, "icoLibrary");
            this.imageList16.Images.SetKeyName(1, "selLibrary");
            this.imageList16.Images.SetKeyName(2, "icoBook");
            this.imageList16.Images.SetKeyName(3, "selBook");
            this.imageList16.Images.SetKeyName(4, "icoPage");
            // 
            // nudItemHeight
            // 
            this.nudItemHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudItemHeight.Location = new System.Drawing.Point(76, 222);
            this.nudItemHeight.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudItemHeight.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudItemHeight.Name = "nudItemHeight";
            this.nudItemHeight.Size = new System.Drawing.Size(40, 20);
            this.nudItemHeight.TabIndex = 1;
            this.nudItemHeight.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudItemHeight.ValueChanged += new System.EventHandler(this.nudItemHeight_ValueChanged);
            // 
            // nudIndent
            // 
            this.nudIndent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudIndent.Location = new System.Drawing.Point(182, 222);
            this.nudIndent.Maximum = new decimal(new int[] {
            38,
            0,
            0,
            0});
            this.nudIndent.Minimum = new decimal(new int[] {
            19,
            0,
            0,
            0});
            this.nudIndent.Name = "nudIndent";
            this.nudIndent.Size = new System.Drawing.Size(40, 20);
            this.nudIndent.TabIndex = 2;
            this.nudIndent.Value = new decimal(new int[] {
            19,
            0,
            0,
            0});
            this.nudIndent.ValueChanged += new System.EventHandler(this.nudIndent_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "ItemHeight";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Indent";
            // 
            // chkShowLines
            // 
            this.chkShowLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowLines.AutoSize = true;
            this.chkShowLines.Checked = true;
            this.chkShowLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowLines.Location = new System.Drawing.Point(15, 248);
            this.chkShowLines.Name = "chkShowLines";
            this.chkShowLines.Size = new System.Drawing.Size(78, 17);
            this.chkShowLines.TabIndex = 5;
            this.chkShowLines.Text = "ShowLines";
            this.chkShowLines.UseVisualStyleBackColor = true;
            this.chkShowLines.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkShowRootLines
            // 
            this.chkShowRootLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowRootLines.AutoSize = true;
            this.chkShowRootLines.Checked = true;
            this.chkShowRootLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowRootLines.Location = new System.Drawing.Point(15, 271);
            this.chkShowRootLines.Name = "chkShowRootLines";
            this.chkShowRootLines.Size = new System.Drawing.Size(101, 17);
            this.chkShowRootLines.TabIndex = 6;
            this.chkShowRootLines.Text = "ShowRootLines";
            this.chkShowRootLines.UseVisualStyleBackColor = true;
            this.chkShowRootLines.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkShowPlusMinus
            // 
            this.chkShowPlusMinus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowPlusMinus.AutoSize = true;
            this.chkShowPlusMinus.Checked = true;
            this.chkShowPlusMinus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowPlusMinus.Location = new System.Drawing.Point(15, 294);
            this.chkShowPlusMinus.Name = "chkShowPlusMinus";
            this.chkShowPlusMinus.Size = new System.Drawing.Size(101, 17);
            this.chkShowPlusMinus.TabIndex = 7;
            this.chkShowPlusMinus.Text = "ShowPlusMinus";
            this.chkShowPlusMinus.UseVisualStyleBackColor = true;
            this.chkShowPlusMinus.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkCheckBoxes
            // 
            this.chkCheckBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCheckBoxes.AutoSize = true;
            this.chkCheckBoxes.Location = new System.Drawing.Point(142, 248);
            this.chkCheckBoxes.Name = "chkCheckBoxes";
            this.chkCheckBoxes.Size = new System.Drawing.Size(86, 17);
            this.chkCheckBoxes.TabIndex = 8;
            this.chkCheckBoxes.Text = "CheckBoxes";
            this.chkCheckBoxes.UseVisualStyleBackColor = true;
            this.chkCheckBoxes.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkImageList
            // 
            this.chkImageList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkImageList.AutoSize = true;
            this.chkImageList.Location = new System.Drawing.Point(142, 271);
            this.chkImageList.Name = "chkImageList";
            this.chkImageList.Size = new System.Drawing.Size(88, 17);
            this.chkImageList.TabIndex = 9;
            this.chkImageList.Text = "24 bit images";
            this.chkImageList.UseVisualStyleBackColor = true;
            this.chkImageList.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkOwnerDraw
            // 
            this.chkOwnerDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkOwnerDraw.AutoSize = true;
            this.chkOwnerDraw.Checked = true;
            this.chkOwnerDraw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOwnerDraw.Location = new System.Drawing.Point(142, 294);
            this.chkOwnerDraw.Name = "chkOwnerDraw";
            this.chkOwnerDraw.Size = new System.Drawing.Size(82, 17);
            this.chkOwnerDraw.TabIndex = 10;
            this.chkOwnerDraw.Text = "OwnerDraw";
            this.chkOwnerDraw.UseVisualStyleBackColor = true;
            this.chkOwnerDraw.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // imageList24
            // 
            this.imageList24.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList24.ImageStream")));
            this.imageList24.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList24.Images.SetKeyName(0, "icoLibrary");
            this.imageList24.Images.SetKeyName(1, "selLibrary");
            this.imageList24.Images.SetKeyName(2, "icoBook");
            this.imageList24.Images.SetKeyName(3, "selBook");
            this.imageList24.Images.SetKeyName(4, "icoPage");
            // 
            // ocTreeView1
            // 
            this.ocTreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ocTreeView1.BackColor = System.Drawing.SystemColors.Info;
            this.ocTreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.ocTreeView1.FullRowSelect = true;
            this.ocTreeView1.ImageIndex = 0;
            this.ocTreeView1.ImageList = this.imageList16;
            this.ocTreeView1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.ocTreeView1.Location = new System.Drawing.Point(0, 0);
            this.ocTreeView1.Name = "ocTreeView1";
            this.ocTreeView1.SelectedImageIndex = 0;
            this.ocTreeView1.Size = new System.Drawing.Size(234, 216);
            this.ocTreeView1.TabIndex = 0;
            this.ocTreeView1.DoubleClick += new System.EventHandler(this.ocTreeView1_DoubleClick);
            // 
            // Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 320);
            this.Controls.Add(this.chkOwnerDraw);
            this.Controls.Add(this.chkImageList);
            this.Controls.Add(this.chkCheckBoxes);
            this.Controls.Add(this.chkShowPlusMinus);
            this.Controls.Add(this.chkShowRootLines);
            this.Controls.Add(this.chkShowLines);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudIndent);
            this.Controls.Add(this.nudItemHeight);
            this.Controls.Add(this.ocTreeView1);
            this.DoubleBuffered = true;
            this.Name = "Explorer";
            this.ShowIcon = false;
            this.Text = "Explorer";
            ((System.ComponentModel.ISupportInitialize)(this.nudItemHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIndent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList16;
        private OC.Windows.Forms.ocTreeView ocTreeView1;
        private System.Windows.Forms.NumericUpDown nudItemHeight;
        private System.Windows.Forms.NumericUpDown nudIndent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkShowLines;
        private System.Windows.Forms.CheckBox chkShowRootLines;
        private System.Windows.Forms.CheckBox chkShowPlusMinus;
        private System.Windows.Forms.CheckBox chkCheckBoxes;
        private System.Windows.Forms.CheckBox chkImageList;
        private System.Windows.Forms.CheckBox chkOwnerDraw;
        private System.Windows.Forms.ImageList imageList24;
    }
}

