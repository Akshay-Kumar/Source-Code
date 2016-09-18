namespace SourceCode
{
    partial class ForgotPassword
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
            this.txtEmailOrUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancelSubmit = new System.Windows.Forms.Button();
            this.lblVerificationResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtEmailOrUserName
            // 
            this.txtEmailOrUserName.Location = new System.Drawing.Point(143, 32);
            this.txtEmailOrUserName.Name = "txtEmailOrUserName";
            this.txtEmailOrUserName.Size = new System.Drawing.Size(219, 20);
            this.txtEmailOrUserName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Name or Email";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(143, 74);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancelSubmit
            // 
            this.btnCancelSubmit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelSubmit.Location = new System.Drawing.Point(247, 74);
            this.btnCancelSubmit.Name = "btnCancelSubmit";
            this.btnCancelSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnCancelSubmit.TabIndex = 3;
            this.btnCancelSubmit.Text = "Cancel";
            this.btnCancelSubmit.UseVisualStyleBackColor = true;
            // 
            // lblVerificationResult
            // 
            this.lblVerificationResult.AutoSize = true;
            this.lblVerificationResult.Location = new System.Drawing.Point(140, 112);
            this.lblVerificationResult.Name = "lblVerificationResult";
            this.lblVerificationResult.Size = new System.Drawing.Size(92, 13);
            this.lblVerificationResult.TabIndex = 4;
            this.lblVerificationResult.Text = "Verification Result";
            this.lblVerificationResult.Visible = false;
            // 
            // ForgotPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelSubmit;
            this.ClientSize = new System.Drawing.Size(446, 162);
            this.Controls.Add(this.lblVerificationResult);
            this.Controls.Add(this.btnCancelSubmit);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmailOrUserName);
            this.MaximumSize = new System.Drawing.Size(462, 200);
            this.Name = "ForgotPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forgot Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmailOrUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancelSubmit;
        private System.Windows.Forms.Label lblVerificationResult;
    }
}