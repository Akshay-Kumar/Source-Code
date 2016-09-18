using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SourceCode
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //with args(user open file with the program)
            if (args != null && args.Length > 0)
            {
                string fileName = args[0];
                //Check file exists
                if (File.Exists(fileName))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Splash splash = new Splash();
                    splash.path = fileName;
                    Application.Run(splash);
                }
                //The file does not exist
                else
                {
                    MessageBox.Show("The file does not exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Splash());
                }
            }
            //without args
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Splash());
            }
        }
    }
}
