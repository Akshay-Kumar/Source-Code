using System.Windows.Forms;

namespace SplashScreen
{
	partial class SplashScreen : Form
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblTimeRemaining = new System.Windows.Forms.Label();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tasks = new System.Windows.Forms.Label();
            this.bigApp = new System.Windows.Forms.Label();
            this.appMini = new System.Windows.Forms.Label();
            this.minimize = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblStatus.Location = new System.Drawing.Point(71, 125);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(279, 14);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.Transparent;
            this.pnlStatus.Location = new System.Drawing.Point(70, 147);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(279, 24);
            this.pnlStatus.TabIndex = 1;
            this.pnlStatus.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeRemaining.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTimeRemaining.Location = new System.Drawing.Point(71, 178);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(279, 22);
            this.lblTimeRemaining.TabIndex = 2;
            this.lblTimeRemaining.Text = "Time remaining";
            this.lblTimeRemaining.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Wingdings", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "1";
            // 
            // tasks
            // 
            this.tasks.AutoSize = true;
            this.tasks.BackColor = System.Drawing.Color.Transparent;
            this.tasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tasks.ForeColor = System.Drawing.Color.White;
            this.tasks.Location = new System.Drawing.Point(14, 215);
            this.tasks.Name = "tasks";
            this.tasks.Size = new System.Drawing.Size(136, 29);
            this.tasks.TabIndex = 9;
            this.tasks.Text = "current task";
            // 
            // bigApp
            // 
            this.bigApp.AutoSize = true;
            this.bigApp.BackColor = System.Drawing.Color.Transparent;
            this.bigApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bigApp.ForeColor = System.Drawing.Color.White;
            this.bigApp.Location = new System.Drawing.Point(23, 55);
            this.bigApp.Name = "bigApp";
            this.bigApp.Size = new System.Drawing.Size(389, 55);
            this.bigApp.TabIndex = 8;
            this.bigApp.Text = "Your Application";
            // 
            // appMini
            // 
            this.appMini.AutoSize = true;
            this.appMini.BackColor = System.Drawing.Color.Transparent;
            this.appMini.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.appMini.ForeColor = System.Drawing.Color.White;
            this.appMini.Location = new System.Drawing.Point(32, 5);
            this.appMini.Name = "appMini";
            this.appMini.Size = new System.Drawing.Size(130, 18);
            this.appMini.TabIndex = 7;
            this.appMini.Text = "Your Application";
            // 
            // minimize
            // 
            this.minimize.AutoSize = true;
            this.minimize.BackColor = System.Drawing.Color.Transparent;
            this.minimize.Font = new System.Drawing.Font("Webdings", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.minimize.ForeColor = System.Drawing.Color.White;
            this.minimize.Location = new System.Drawing.Point(371, 1);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(36, 26);
            this.minimize.TabIndex = 12;
            this.minimize.Text = "0";
            this.minimize.Click += new System.EventHandler(this.minimize_Click);
            // 
            // close
            // 
            this.close.AutoSize = true;
            this.close.BackColor = System.Drawing.Color.Transparent;
            this.close.Font = new System.Drawing.Font("Webdings", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.close.ForeColor = System.Drawing.Color.White;
            this.close.Location = new System.Drawing.Point(406, 1);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(36, 26);
            this.close.TabIndex = 11;
            this.close.Text = "r";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // SplashScreen
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImage = global::SourceCode.Properties.Resources.officesplash_6;
            this.ClientSize = new System.Drawing.Size(449, 257);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tasks);
            this.Controls.Add(this.bigApp);
            this.Controls.Add(this.appMini);
            this.Controls.Add(this.lblTimeRemaining);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label lblTimeRemaining;
		private System.Windows.Forms.Timer UpdateTimer;
		private System.Windows.Forms.Panel pnlStatus;
        private Label label1;
        private Label tasks;
        private Label bigApp;
        private Label appMini;
        private Label minimize;
        private Label close;
	}
}