using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Printing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using UtilitPackage;
using SourceCodeDAL;

namespace SourceCode
{
    //delegate declared
    public delegate void SetSourceCodeDelegate(string code);
    public delegate void UpdateSourceCode(string code);
    public delegate void SetEvents(bool flag);
    public delegate void DeleteCode();
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class ColorSyntax : System.Windows.Forms.Form
    {
        private int lastChar = 0;
        private bool populating = true;
        public static bool ADD_SOURCECODE = false;
        public static bool dirtyForm = false;
        private bool fileSaved = false;
        private static string existingCode = string.Empty;
        public SetSourceCodeDelegate SetSourceCode;
        public UpdateSourceCode UpdateCode;
        public SetEvents CustomEvents;
        public DeleteCode DeleteSourceCode;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private SyntaxReader TheSyntaxReader = new SyntaxReader("SourceCodeSyntex.syntax");
        const int DEFAULT_BUFFER_SIZE = 4000;
        WordAndPosition[] TheBuffer;
        #region Constructor
        public ColorSyntax(string sourceCodeText, string userId)
        {
            InitializeComponent();
            if (ADD_SOURCECODE)
            {
                this.Text = TABS.ADD_SOURCE_CODE;
                OpenMenu.Visible = true;
                SaveMenu.Text = BUTTONLABEL.ADD;
                EditCodeMenu.Visible = false;
                DeleteMenu.Visible = false;
                richTextBox1.ReadOnly = false;
            }
            else
            {
                this.Text = GlobalClass.CurrentProgramName;
                OpenMenu.Visible = false;
                SaveMenu.Text = BUTTONLABEL.SAVE;
                richTextBox1.ReadOnly = true;
            }
            if (userId.Trim() == Login.GetUserSessionData().UserData.UserId.ToString().Trim()
                && !ADD_SOURCECODE)
            {
                EditCodeMenu.Visible = true;
                DeleteMenu.Visible = true;
                fileSaved = true;
            }
            else
            {
                EditCodeMenu.Visible = false;
                DeleteMenu.Visible = false;
                fileSaved = true;
            }
            existingCode = sourceCodeText.Trim();
            richTextBox1.Clear();
            richTextBox1.Text = sourceCodeText.Trim();
            richTextBox1.SelectAll();
            richTextBox1.Refresh();
            MakeColorSyntaxForAllText(richTextBox1.Text.Trim());
        }
        #endregion
        private void SetWordBufferSize(int size)
        {
            TheBuffer = new WordAndPosition[size];
        }
        #region WordAndPosition
        struct WordAndPosition
        {
            public string Word;
            public int Position;
            public int Length;
            public override string ToString()
            {
                string s = "Word = " + Word + ", Position = " + Position + ", Length = " + Length + "\n";
                return s;
            }
        };
        #endregion
        private bool TestComment(string s)
        {
            bool isComment = false;
            string testString = s.Trim();
            if ((testString.Length >= 2) && (testString[0] == '/') && (testString[1] == '/'))
            {
                isComment = true;
            }
            else
            {
                isComment = false;
            }
            return isComment;
        }
        public string GetRichTextBoxContent()
        {
            return this.richTextBox1.Text.Trim();
        }
        private bool TestString(string s)
        {
            bool isString = false;
            string testString = s.Trim();
            if ((testString.Length >= 2) && (testString[0] == '"') && (testString[testString.Length - 1] == '"'))
            {
                isString = true;
            }
            else
            {
                isString = false;
            }
            return isString;
        }
        private int ParseLine(string s)
        {
            TheBuffer.Initialize();
            int count = 0;
            Regex r = new Regex(@"\w+|[^A-Za-z0-9_ \f\t\v]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m;

            for (m = r.Match(s); m.Success; m = m.NextMatch())
            {
                TheBuffer[count].Word = m.Value;
                TheBuffer[count].Position = m.Index;
                TheBuffer[count].Length = m.Length;
                count++;
            }
            return count;
        }

        private Color Lookup(string s)
        {
            Color theColor = Color.Black;

            if (TheSyntaxReader.IsClassKeyword(s))
            {
                theColor = COLOR.CLASS_COLOR;
            }

            if (TheSyntaxReader.IsKeyword(s))
            {
                theColor = COLOR.KEYWORD_COLOR;
            }
            if (TheSyntaxReader.IsComment(s))
            {
                theColor = COLOR.COMMENT_COLOR;
            }
            if (TheSyntaxReader.IsFunction(s))
            {
                theColor = COLOR.KEYWORD_COLOR;
            }
            if (TheSyntaxReader.IsString(s))
            {
                theColor = COLOR.STRING_COLOR;
            }
            return theColor;
        }
        private void richTextBox1_TextChanged(object sender, System.EventArgs e)
        {
            if (populating)
                return;
            if (richTextBox1.Text.Length > DEFAULT_BUFFER_SIZE)
            {
                SetWordBufferSize(richTextBox1.Text.Length + 100);
            }
            else
            {
                SetWordBufferSize(DEFAULT_BUFFER_SIZE);
            }
            FlickerFreeRichEditTextBox._Paint = false;
            MakeColorSyntaxForCurrentLine();
            FlickerFreeRichEditTextBox._Paint = true;
            if (ADD_SOURCECODE)
            {
                if (existingCode != richTextBox1.Text.Trim())
                {
                    fileSaved = false;
                }
                if (!fileSaved)
                {
                    dirtyForm = true;
                }
                else
                {
                    dirtyForm = false;
                }
                SetSourceCode(richTextBox1.Text.Trim());
            }
            else if (UpdateMenu.Enabled == false && !ADD_SOURCECODE)
            {
                if (existingCode != richTextBox1.Text.Trim())
                {
                    dirtyForm = true;
                    fileSaved = false;
                    this.Text = GlobalClass.CurrentProgramName + "*";
                }
                else
                {
                    dirtyForm = false;
                }
                UpdateMenu.Enabled = true;
                UpdateMenu.Visible = true;
            }
        }

        private void MakeColorSyntaxForCurrentLine()
        {
            #region SuntexColoring
            if (richTextBox1.Text.Trim().Length > 0)
            {
                int length = richTextBox1.Text.Trim().Length;
                if (length > DEFAULT_BUFFER_SIZE)
                {
                    SetWordBufferSize(length + 100);
                }
                else
                {
                    SetWordBufferSize(DEFAULT_BUFFER_SIZE);
                }
                int CurrentSelectionStart = richTextBox1.SelectionStart;
                int CurrentSelectionLength = richTextBox1.SelectionLength;
                // find start of line
                int pos = CurrentSelectionStart;
                int pos2 = CurrentSelectionStart;
                while ((pos > 0) && (richTextBox1.Text[pos - 1] != '\n'))
                {
                    pos--;
                }
                while ((pos2 < richTextBox1.Text.Length) &&
                        (richTextBox1.Text[pos2] != '\n'))
                {
                    pos2++;
                }
                string s = richTextBox1.Text.Substring(pos, pos2 - pos);
                if (TestComment(s))
                {
                    richTextBox1.Select(pos, pos2 - pos);
                    richTextBox1.SelectionColor = COLOR.COMMENT_COLOR;
                }
                else if (TestString(s))
                {
                    richTextBox1.Select(pos, pos2 - pos);
                    richTextBox1.SelectionColor = COLOR.STRING_COLOR;
                }
                else
                {
                    string previousWord = "";
                    int count = ParseLine(s);
                    for (int i = 0; i < count; i++)
                    {
                        WordAndPosition wp = TheBuffer[i];

                        // check for comment
                        if (wp.Word == "/" && previousWord == "/")
                        {
                            // color until end of line
                            int posCommentStart = wp.Position - 1;
                            int posCommentEnd = pos2;
                            while (wp.Word != "\n" && i < count)
                            {
                                wp = TheBuffer[i];
                                i++;
                            }
                            i--;
                            posCommentEnd = pos2;
                            richTextBox1.Select(posCommentStart + pos, posCommentEnd - (posCommentStart + pos));
                            richTextBox1.SelectionColor = COLOR.COMMENT_COLOR;

                        }
                        // check for string
                        else if (wp.Word == "\"")
                        {
                            // color until end of line
                            int posStringStart = wp.Position;
                            int posStringEnd = i;
                            i++;
                            wp = TheBuffer[i];//string
                            while (wp.Word != "\"" && i < count)
                            {
                                i++;
                                wp = TheBuffer[i];
                            }
                            posStringEnd = wp.Position + 1;
                            richTextBox1.Select(posStringStart, posStringEnd - posStringStart);
                            richTextBox1.SelectionColor = COLOR.STRING_COLOR;
                        }
                        else
                        {
                            Color c = Lookup(wp.Word);
                            richTextBox1.Select(wp.Position + pos, wp.Length);
                            richTextBox1.SelectionColor = c;
                        }

                        previousWord = wp.Word;

                    }

                }

                if (CurrentSelectionStart >= 0)
                {
                    richTextBox1.Select(CurrentSelectionStart, CurrentSelectionLength);
                }
            }
            #endregion
        }

        private void MakeColorSyntaxForAllText(string s)
        {
            populating = true;
            int CurrentSelectionStart = richTextBox1.SelectionStart;
            int CurrentSelectionLength = richTextBox1.SelectionLength;
            if (s.Length > DEFAULT_BUFFER_SIZE)
            {
                SetWordBufferSize(s.Length + 100);
            }
            else
            {
                SetWordBufferSize(DEFAULT_BUFFER_SIZE);
            }
            int count = ParseLine(s);
            string previousWord = "";
            for (int i = 0; i < count; i++)
            {
                WordAndPosition wp = TheBuffer[i];

                // check for comment
                if (wp.Word == "/" && previousWord == "/")
                {
                    // color until end of line
                    int posCommentStart = wp.Position - 1;
                    int posCommentEnd = i;
                    while (wp.Word != "\n" && i < count)
                    {
                        wp = TheBuffer[i];
                        i++;
                    }
                    i--;
                    posCommentEnd = wp.Position;
                    richTextBox1.Select(posCommentStart, posCommentEnd - posCommentStart);
                    richTextBox1.SelectionColor = COLOR.COMMENT_COLOR;
                }
                // check for multiline comments
                else if (wp.Word == "/" && TheBuffer[i + 1].Word == "*")
                {
                    // color until end of line
                    int posStringStart = wp.Position;
                    int posStringEnd = i;
                    i = i + 2;
                    wp = TheBuffer[i];//string
                    while (wp.Word != "*" && TheBuffer[i + 1].Word != "/" && i < count)
                    {
                        i++;
                        wp = TheBuffer[i];
                    }
                    posStringEnd = wp.Position + 1;
                    richTextBox1.Select(posStringStart, posStringEnd - posStringStart);
                    richTextBox1.SelectionColor = COLOR.COMMENT_COLOR;
                }
                // check for string
                else if (wp.Word == "\"")
                {
                    // color until end of line
                    int posStringStart = wp.Position;
                    int posStringEnd = i;
                    i++;
                    wp = TheBuffer[i];//string
                    while (wp.Word != "\"" && i < count)
                    {
                        i++;
                        wp = TheBuffer[i];
                    }
                    posStringEnd = wp.Position + 1;
                    richTextBox1.Select(posStringStart, posStringEnd - posStringStart);
                    richTextBox1.SelectionColor = COLOR.STRING_COLOR;
                }
                else
                {
                    Color c = Lookup(wp.Word);
                    richTextBox1.Select(wp.Position, wp.Length);
                    richTextBox1.SelectionColor = c;
                }
                previousWord = wp.Word;
            }
            if (CurrentSelectionStart >= 0)
                richTextBox1.Select(CurrentSelectionStart, CurrentSelectionLength);
            populating = false;
        }

        private void ExitMenu_Click(object sender, System.EventArgs e)
        {
            if (ADD_SOURCECODE)
            {
                CustomEvents(true);
                if (!fileSaved)
                {
                    dirtyForm = true;
                }
                else
                {
                    dirtyForm = false;
                }
            }
            this.Close();
        }

        private void OpenMenu_Click(object sender, System.EventArgs e)
        {
            if (openSourceCodeFileDialog.ShowDialog() == DialogResult.OK)
            {
                bool textOverWrite = false;
                string oldCode = string.Empty;
                if (string.IsNullOrEmpty(richTextBox1.Text.Trim()) && ADD_SOURCECODE)
                {
                    fileSaved = false;
                    textOverWrite = false;
                }
                else if (!string.IsNullOrEmpty(richTextBox1.Text.Trim()) && ADD_SOURCECODE)
                {
                    fileSaved = false;
                    textOverWrite = true;
                    oldCode = richTextBox1.Text.Trim();
                }
                populating = true;
                FileStream fs = new FileStream(openSourceCodeFileDialog.FileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string s = sr.ReadToEnd();
                existingCode = s;
                sr.Close();
                fs.Close();
                richTextBox1.Text = string.Empty;
                richTextBox1.Text = s.Trim();
                richTextBox1.SelectAll();
                richTextBox1.Refresh();
                FlickerFreeRichEditTextBox._Paint = false;
                MakeColorSyntaxForAllText(richTextBox1.Text.Trim());
                FlickerFreeRichEditTextBox._Paint = true;
                if (ADD_SOURCECODE)
                {
                    CustomEvents(true);
                    if (!fileSaved)
                    {
                        dirtyForm = true;
                    }
                    else
                    {
                        dirtyForm = false;
                    }
                    if (textOverWrite)
                    {
                        DialogResult result = MessageBox.Show(this, "You have not saved the current data. Do you want to save the code before opening another document?", "Save current data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            richTextBox1.Clear();
                            richTextBox1.Text = oldCode;
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    SetSourceCode(richTextBox1.Text.Trim());
                }
            }
        }
        private void SaveMenu_Click(object sender, System.EventArgs e)
        {
            if (SaveMenu.Text == BUTTONLABEL.SAVE)
            {
                if (saveCodeFileDialog.ShowDialog() == DialogResult.OK && saveCodeFileDialog.FileName.Length > 0)
                {
                    if (saveCodeFileDialog.FileName.Contains(saveCodeFileDialog.DefaultExt))
                    {
                        richTextBox1.SaveFile(saveCodeFileDialog.FileName, RichTextBoxStreamType.RichNoOleObjs);
                    }
                    else
                    {
                        string fileName = saveCodeFileDialog.FileName;
                        string fileExt = Path.GetExtension(fileName);
                        string fileText = richTextBox1.Text;
                        switch (fileExt)
                        {
                            case FILE_EXTENSION.C:
                            case FILE_EXTENSION.CPP:
                            case FILE_EXTENSION.CS:
                            case FILE_EXTENSION.JAVA:
                            case FILE_EXTENSION.SQL:
                            case FILE_EXTENSION.TXT:
                                SaveFileInSpecificFormat(saveCodeFileDialog, fileText);
                                break;
                            case FILE_EXTENSION.PDF:
                                SaveAsPdf(saveCodeFileDialog, fileText);
                                break;
                            default:
                                MessageBox.Show("PLEASE INPUT A PROPER FILE EXTENSION\nSUPPORTED EXTENSIONS (.c, .cpp, .cs, .java, .txt, .sql, .pdf) !", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                    }
                    if (File.Exists(saveCodeFileDialog.FileName))
                    {
                        MessageBox.Show("FILE SAVED SUCCESSFULLY !", "SUCCESS_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR IN SAVING FILE !", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("PLEASE ENTER A FILENAME TO SAVE FILE !", "ERROR_MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (SaveMenu.Text == BUTTONLABEL.ADD)
            {
                if (ADD_SOURCECODE)
                {
                    CustomEvents(true);
                    fileSaved = true;
                    if (!fileSaved)
                    {
                        dirtyForm = true;
                    }
                    else
                    {
                        dirtyForm = false;
                    }
                    SetSourceCode(richTextBox1.Text.Trim());
                    this.Close();
                }
            }
        }

        private void SaveAsPdf(SaveFileDialog saveFileDialog, string fileText)
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
            doc.Open();
            doc.Add(new iTextSharp.text.Paragraph(fileText));
            doc.Close();
        }

        private void SaveFileInSpecificFormat(SaveFileDialog saveFileDialog, string fileText)
        {
            FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(fileText);
            sw.Close();
            fs.Close();
        }
        private void PrintPreviewMenu_Click(object sender, System.EventArgs e)
        {
            Print();
        }

        private void Print()
        {
            if (printPreviewSourceCodeDialog.ShowDialog() == DialogResult.OK)
            {
                PrintDocument doc = new PrintDocument();
                PrintDialog pd = new PrintDialog();
                PrintPreviewDialog ppd = new PrintPreviewDialog();
                ppd.Document = doc;
                pd.Document = doc;
                doc.PrintPage += new PrintPageEventHandler(this.printSourceCodeDocument_PrintPage);
                if (ppd.ShowDialog() == DialogResult.OK)
                {
                    if (pd.ShowDialog() == DialogResult.OK)
                    {
                        doc.Print();
                    }
                }
            }
        }
        private int DrawEditControl(Graphics g, int lastChar)
        {
            // draw the control by selecting each character and deterimining its color
            int xPos = 10;
            int yPos = 40;
            int kMargin = 50;
            for (int c = lastChar; c < richTextBox1.Text.Length; c++)
            {
                richTextBox1.Select(c, 1);
                char nextChar = richTextBox1.Text[c];
                Color theColor = richTextBox1.SelectionColor;
                System.Drawing.Font theFont = richTextBox1.SelectionFont;
                int height = theFont.Height;
                if (nextChar == '\n')
                {
                    yPos += (height + 3);  // add to height on return characters
                    xPos = 10;
                    int paperHeight = printSourceCodeDocument.PrinterSettings.DefaultPageSettings.PaperSize.Height;
                    if (yPos > paperHeight - kMargin)
                        return c;
                }
                else if ((nextChar == ' ') || (nextChar == '\t'))
                {
                    xPos += theFont.Height / 2;
                }
                else
                {
                    g.DrawString(nextChar.ToString(), theFont, new SolidBrush(theColor), new Point(xPos, yPos));
                    SizeF thesize = g.MeasureString(nextChar.ToString(), theFont);
                    xPos += (int)thesize.Width - 5;
                }
            }

            return -1;
        }

        private void menuItem3_Click(object sender, System.EventArgs e)
        {
            MakeColorSyntaxForAllText(richTextBox1.Text);
        }

        private void PrintMenu_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void EditCodeMenu_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = false;
            existingCode = richTextBox1.Text.Trim();
            if (!ADD_SOURCECODE)
            {
                UpdateMenu.Enabled = false;
            }
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            int SelectionIndex = richTextBox1.SelectionStart;
            int SelectionCount = richTextBox1.SelectionLength;
            richTextBox1.Text = richTextBox1.Text.Remove(SelectionIndex, SelectionCount);
            richTextBox1.SelectionStart = SelectionIndex;
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void Font_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog.Font;
            }
        }

        private void Forcolor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = colorDialog.Color;
            }
        }

        private void Backcolor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.BackColor = colorDialog.Color;
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            MakeColorSyntaxForAllText(richTextBox1.Text);
        }

        private void cmsSourceCode_Opening(object sender, CancelEventArgs e)
        {
            // Disable Undo if CanUndo property returns false
            if (richTextBox1.CanUndo)
            {
                cmsSourceCode.Items["Undo"].Enabled = true;
            }
            else
            {
                cmsSourceCode.Items["Undo"].Enabled = false;
            }

            // Disable Cut, Copy and Delete if any text is not selected in TextBox
            if (richTextBox1.SelectedText.Length == 0)
            {
                cmsSourceCode.Items["Cut"].Enabled = false;
                cmsSourceCode.Items["Copy"].Enabled = false;
                cmsSourceCode.Items["Delete"].Enabled = false;
            }
            else
            {
                cmsSourceCode.Items["Cut"].Enabled = true;
                cmsSourceCode.Items["Copy"].Enabled = true;
                cmsSourceCode.Items["Delete"].Enabled = true;
            }
            // Disable Paste if Clipboard does not contains text
            if (Clipboard.ContainsText())
            {
                cmsSourceCode.Items["Paste"].Enabled = true;
            }
            else
            {
                cmsSourceCode.Items["Paste"].Enabled = false;
            }
            // Disable Select All if TextBox is blank
            if (richTextBox1.Text.Length == 0)
            {
                cmsSourceCode.Items["SelectAll"].Enabled = false;
            }
            else
            {
                cmsSourceCode.Items["SelectAll"].Enabled = true;
            }
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsSourceCode.Show(richTextBox1, e.Location);
            }
        }

        private void UpdateMenu_Click(object sender, EventArgs e)
        {
            string codeTxt = richTextBox1.Text.Trim();
            existingCode = codeTxt;
            UpdateCode(codeTxt);
            richTextBox1.ReadOnly = true;
            UpdateMenu.Enabled = false;
            UpdateMenu.Visible = false;
            fileSaved = true;
            if (ADD_SOURCECODE)
            {
                CustomEvents(true);
                if (!fileSaved)
                {
                    dirtyForm = true;
                }
                else
                {
                    dirtyForm = false;
                }
            }
            this.Text = GlobalClass.CurrentProgramName;
        }

        private void DeleteMenu_Click(object sender, EventArgs e)
        {
            DeleteSourceCode();
            richTextBox1.Clear();
            fileSaved = true;
            if (ADD_SOURCECODE)
            {
                CustomEvents(true);
                if (!fileSaved)
                {
                    dirtyForm = true;
                }
                else
                {
                    dirtyForm = false;
                }
            }
        }

        private void ColorSyntax_FormClosing(object sender, FormClosingEventArgs e)
        {
            dirtyForm = !fileSaved;
            if (ADD_SOURCECODE)
            {
                if (!string.IsNullOrEmpty(richTextBox1.Text.Trim()))
                {
                    if (!fileSaved)
                    {
                        DialogResult result = MessageBox.Show(this, "You have not saved the current data. Do you want to save the code before closing the current window?", "Save current data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            richTextBox1.Text = string.Empty;
                        }
                    }
                }
            }
            else
            {
                if (!fileSaved)
                {
                    DialogResult result = MessageBox.Show(this, "You have not saved the current data. Do you want to save the code before closing the current window?", "Save current data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void printSourceCodeDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            lastChar = DrawEditControl(g, lastChar);
            if (lastChar != -1)
                e.HasMorePages = true;
            else
            {
                e.HasMorePages = false;
                lastChar = 0;
            }
        }

        private void CodeWithFile_Click(object sender, EventArgs e)
        {
            string[] codeArray = richTextBox1.Lines;
            string fileName = GlobalClass.CurrentProgramName;
            string extension = string.Empty;
            switch (GlobalClass.CurrentLangName)
            {
                case "C": extension = ".c";
                    break;
                case "C#": extension = ".cs";
                    break;
                case "JAVA": extension = ".java";
                    break;
                case "HTML": extension = ".html";
                    break;
                case "PEARL": extension = ".pl";
                    break;
                case "PYTHON": extension = ".py";
                    break;
                case "C++": extension = ".cpp";
                    break;
                case "VB.NET": extension = ".vb";
                    break;
                case "JAVASCRIPT": extension = ".js";
                    break;
            }
            fileName += extension;
            DiffMergeSample diffMerge = new DiffMergeSample(fileName, codeArray);
            diffMerge.Show();
        }

        private void FileWithFile_Click(object sender, EventArgs e)
        {
            DiffMergeSample diffMerge = new DiffMergeSample();
            diffMerge.Show();
        }
    }
    public class COLOR
    {
        public static Color CLASS_COLOR = Color.SkyBlue;
        public static Color KEYWORD_COLOR = Color.Blue;
        public static Color COMMENT_COLOR = Color.Green;
        public static Color STRING_COLOR = Color.Brown;
    }
    public class FILE_EXTENSION
    {
        public const string TXT = ".txt";
        public const string CPP = ".cpp";
        public const string CS = ".cs";
        public const string JAVA = ".java";
        public const string C = ".c";
        public const string PDF = ".pdf";
        public const string SQL = ".sql";
    }
}

