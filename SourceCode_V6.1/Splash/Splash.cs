using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Associate;

namespace SourceCode
{
    public partial class Splash : Form
    {
        //file path
        public string path = null;


        public Splash()
        {
            InitializeComponent();

            //Tasks
            //Starting
            tasks.Text = "Starting...";
            bigApp.Text = "SOURCE CODE";
            Thread.Sleep(1000);
            //start timer
            splashtime.Start();
        }

        public bool Isminimized = false;

        //Close Application
        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Minimize Application
        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Isminimized = true;
        }
        //Mouse hover and leave effects
        private void close_MouseHover(object sender, EventArgs e)
        {
            close.ForeColor = Color.Silver;
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            close.ForeColor = Color.White;
        }

        private void minimize_MouseHover(object sender, EventArgs e)
        {
            minimize.ForeColor = Color.Silver;
        }

        private void minimize_MouseLeave(object sender, EventArgs e)
        {
            minimize.ForeColor = Color.White;
        }

        //Show MainForm(Form1)
        public void frmNewFormThread()
        {
            var frmNewForm = new Form1();
            if (Isminimized == true)
            {
                frmNewForm.WindowState = FormWindowState.Minimized;
            }
            else
            {
                frmNewForm.WindowState = FormWindowState.Maximized;
            }

            if(path != null)
            {
                frmNewForm.OpenFile(path);
            }
            Application.Run(frmNewForm);
            frmNewForm.Focus();
        }
       private void splashtime_Tick(object sender, EventArgs e)
        {
            colorProgressBar1.Increment(5);
            if (colorProgressBar1.Value == colorProgressBar1.Maximum)
            {
                splashtime.Stop();
                var newThread = new System.Threading.Thread(frmNewFormThread);
                newThread.SetApartmentState(System.Threading.ApartmentState.STA);
                newThread.Start();
                this.Close();
            }
        }

        private void Splash_FormClosed(object sender, FormClosedEventArgs e)
        {

        }


        private void Splash_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Splash_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
