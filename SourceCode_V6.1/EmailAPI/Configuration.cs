using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Logger;

namespace EmailAPI
{
    public class Configuration
    {
        static string userName = GetConfig(CREDENTIALS.USERNAME);
        static string password = GetConfig(CREDENTIALS.PASSWORD);
        static string hostname = GetConfig(CREDENTIALS.HOSTNAME);
        static string port = GetConfig(CREDENTIALS.PORT);

        public static string UserName
        {
            get { return userName; }
        }

        public static string Password
        {
            get { return password; }
        }

        public static string Hostname
        {
            get { return hostname; }
        }

        public static string Port
        {
            get { return port; }
        }

        private static string GetConfig(string key)
        {
            string value = string.Empty;
            try
            {
                switch (key)
                {
                    case CREDENTIALS.USERNAME: value = Properties.Settings.Default.userName;
                        break;
                    case CREDENTIALS.PASSWORD: value = Properties.Settings.Default.password;
                        break;
                    case CREDENTIALS.HOSTNAME: value = Properties.Settings.Default.hostname;
                        break;
                    case CREDENTIALS.PORT: value = Properties.Settings.Default.port;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return value;
        }
    }

    internal class CREDENTIALS
    {
        internal const string USERNAME = "userName";
        internal const string PASSWORD = "password";
        internal const string HOSTNAME = "hostname";
        internal const string PORT = "port";
    }
}
