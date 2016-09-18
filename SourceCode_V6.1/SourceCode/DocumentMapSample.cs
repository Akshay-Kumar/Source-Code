using System;
using System.Drawing;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Drawing.Drawing2D;

namespace SourceCode
{
    public partial class DocumentMapSample : Form
    {
        public DocumentMapSample(string sourceCode)
        {
            InitializeComponent();
            fctb.Clear();
            fctb.Text = sourceCode.Trim();
        }
    }
}
