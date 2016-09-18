using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logger;
using SourceCodeDAL;
using SourceCodeBL;
using EmailAPI;

namespace SourceCode
{
    public partial class ForgotPassword : Form
    {
        string userIdOrEmail = string.Empty;
        string userId;
        string emailId;
        string password;
        string userName;
        SourceDataAccessLayer sourceDal = null;
        SendMail mail = null;
        Users user = null;
        DataTable dtUsers;
        StringBuilder message;
        public ForgotPassword()
        {
            InitializeComponent();
            sourceDal = new SourceDataAccessLayer();
            mail = new SendMail();
            if(lblVerificationResult.Visible)
            lblVerificationResult.Visible = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (VerifyUser(txtEmailOrUserName.Text.Trim()))
            {
                if (dtUsers.Rows.Count > 0)
                {
                    message = new StringBuilder();
                    userId = dtUsers.Rows[0]["USER_ID"].ToString().Trim();
                    emailId = dtUsers.Rows[0]["EMAIL_ID"].ToString().Trim();
                    password = dtUsers.Rows[0]["USER_PASSWORD"].ToString().Trim();
                    userName = dtUsers.Rows[0]["FIRST_NAME"].ToString().Trim() + " " +
                               dtUsers.Rows[0]["FIRST_NAME"].ToString().Trim();
                    message.AppendLine(string.Format("UserId : {0}",userId));
                    message.AppendLine(string.Format("UserName : {0}", userName));
                    message.AppendLine(string.Format("EmailId : {0}", emailId));
                    message.AppendLine(string.Format("Password : {0}", password));
                    if (SendEmail(message))
                    {
                        lblVerificationResult.Visible = true;
                        lblVerificationResult.Text = string.Format("Hello ! {0} Verificatin successful!\n"+
                                                                   "An email containing your account password\n"+
                                                                   " has been sent to your email {1}",userName,emailId);
                        lblVerificationResult.ForeColor = System.Drawing.Color.Green;
                    }
                }
            }
            else
            {
                lblVerificationResult.Visible = true;
                lblVerificationResult.Text = "Verificatin failed!";
                lblVerificationResult.ForeColor = System.Drawing.Color.Red;
            }
        }
        private bool VerifyUser(string userId)
        {
            user = new Users();
            user.UserId = userId;
            bool success = false;
            dtUsers = new DataTable();
            dtUsers = sourceDal.ForgotUserPassword(user, out success);
            return success;
        }
        private bool SendEmail(StringBuilder sbMessage)
        {
            bool success = false;
            SendMail.RecepientData msgData = new SendMail.RecepientData();
            msgData.RecepientEmail = emailId;
            msgData.RecepientName = userName;
            msgData.Subject = "Password recovery email.";
            msgData.MessageContent = sbMessage.ToString();
            success = mail.SendMessage(SendMail.MailType.TextEmail, msgData);
            return success;
        }
    }
}
