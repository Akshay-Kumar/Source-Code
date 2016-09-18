using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Logger;

namespace EmailAPI
{
    public class FormatEmail
    {
        public static string FormatMessageBody(string userName,string content)
        {
            StreamReader reader = null;
            string template = string.Empty;
            try
            {
                string cwd = System.IO.Directory.GetCurrentDirectory();
                
                if (cwd.EndsWith("\\bin\\Debug"))
                {
                    cwd = cwd.Replace("\\bin\\Debug", "");
                }
                reader = new StreamReader(cwd+ "\\Template.html");
                template = reader.ReadToEnd();
                template = template.Replace("images/logo.png", "cid:logo");
                template = template.Replace("images/6@2x.png", "cid:id1");
                template = template.Replace("images/pics.png", "cid:pics");
                template = template.Replace("images/yes.gif", "cid:yes");
                template = template.Replace("images/no.gif", "cid:no");
                template = template.Replace("images/facebook.png", "cid:facebook");
                template = template.Replace("images/twitter.png", "cid:twitter");
                template = template.Replace("images/pinterest.png", "cid:pinterest");
                template = template.Replace("@USER_NAME", userName);
                template = template.Replace("@MESSAGE_BODY", content);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return template;
        }
    }
}
