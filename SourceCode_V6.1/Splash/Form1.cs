using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using SourceCode;

namespace Associate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        public void OpenFile(string filePath)
        {

    openStatus.Text = "Open file: " + filePath;
          Thread.Sleep(1000);

            string file = File.ReadAllText(filePath);
            richTextBox1.Text = file;
            openStatus.Text = "Ready";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
