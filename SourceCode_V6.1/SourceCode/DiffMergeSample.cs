using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using FastColoredTextBoxNS;
using SourceCode.DiffMergeStuffs;
using System.Reflection;

namespace SourceCode
{
    delegate void TextBoxRefresh(FastColoredTextBox ftb);
    delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);
    public partial class DiffMergeSample : Form
    {
        int updating;
        int delayTime = 10;
        public static int progress = 0;
        public static float i = 0;
        public static float total = 0;
        Style greenStyle;
        Style redStyle;
        public static BackgroundWorker compareCode;
        private string[] source_code = null;
        private string fileName = string.Empty;
        public DiffMergeSample()
        {
            InitializeComponent();
            compareCode = new BackgroundWorker();
            compareCode.WorkerReportsProgress = true;
            compareCode.WorkerSupportsCancellation = true;
            compareCode.DoWork += new DoWorkEventHandler(compareCode_DoWork);
            compareCode.ProgressChanged +=new ProgressChangedEventHandler(compareCode_ProgressChanged);
            compareCode.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(compareCode_RunWorkerCompleted);
            greenStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Lime)));
            redStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Red)));
        }

        public DiffMergeSample(string fileName, string[] source_code)
        {
            InitializeComponent();
            if (!(source_code == null) && !string.IsNullOrEmpty(fileName))
            {
                btFirst.Visible = false;
                tbFirstFile.Visible = false;
                label2.Visible = false;
                label3.Text = "Select file";
                this.fileName = fileName;
                this.source_code = source_code;
            }
            compareCode = new BackgroundWorker();
            compareCode.WorkerReportsProgress = true;
            compareCode.WorkerSupportsCancellation = true;
            compareCode.DoWork += new DoWorkEventHandler(compareCode_DoWork);
            compareCode.ProgressChanged += new ProgressChangedEventHandler(compareCode_ProgressChanged);
            compareCode.RunWorkerCompleted += new RunWorkerCompletedEventHandler(compareCode_RunWorkerCompleted);
            greenStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Lime)));
            redStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Red)));
        }

        private void btSecond_Click(object sender, EventArgs e)
        {
            if (ofdFile.ShowDialog() == DialogResult.OK)
                tbSecondFile.Text = ofdFile.FileName;
        }

        private void btFirst_Click(object sender, EventArgs e)
        {
            if (ofdFile.ShowDialog() == DialogResult.OK)
                tbFirstFile.Text = ofdFile.FileName;
        }

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }

        void tb_VisibleRangeChanged(object sender, EventArgs e)
        {
            if (updating > 0)
                return;

            var vPos = (sender as FastColoredTextBox).VerticalScroll.Value;
            var curLine = (sender as FastColoredTextBox).Selection.Start.iLine;

            if (sender == fctb2)
                UpdateScroll(fctb1, vPos, curLine);
            else
                UpdateScroll(fctb2, vPos, curLine);
            if (this.fctb1.InvokeRequired)
            {
                TextBoxRefresh refresh = new TextBoxRefresh(this.Refresh);
                this.Invoke(refresh, fctb1);
            }
            else
            {
               fctb1.Refresh();
            }
            if (this.fctb2.InvokeRequired)
            {
                TextBoxRefresh refresh = new TextBoxRefresh(this.Refresh);
                this.Invoke(refresh, fctb2);
            }
            else
            {
                fctb2.Refresh();
            }
        }

        public void Refresh(FastColoredTextBox rtb)
        {
            rtb.Refresh();
        }
        void UpdateScroll(FastColoredTextBox tb, int vPos, int curLine)
        {
            if (updating > 0)
                return;
            //
            BeginUpdate();
            //
            if (vPos <= tb.VerticalScroll.Maximum)
            {
                tb.VerticalScroll.Value = vPos;
                tb.UpdateScrollbars();
            }

            if (curLine < tb.LinesCount)
                tb.Selection = new Range(tb, 0, curLine, 0, curLine);
            //
            EndUpdate();
        }

        private void EndUpdate()
        {
            updating--;
        }

        private void BeginUpdate()
        {
            updating++;
        }

        private void btCompare_Click(object sender, EventArgs e)
        {
            try
            {
                if (source_code == null && string.IsNullOrEmpty(fileName))
                {
                    if (!File.Exists(tbFirstFile.Text) && !File.Exists(tbSecondFile.Text))
                    {
                        MessageBox.Show(this, "Please select a valid file", "Invalid file");
                        return;
                    }
                    else
                    {
                        if (compareCode.IsBusy)
                        {
                            btCompare.Enabled = false;
                            lblStatus.Text = "Cancelling...";
                            compareCode.CancelAsync();
                        }
                        else
                        {
                            btCompare.Text = "Cancel";
                            lblStatus.Text = "Running...";
                            lblFirstFile.Text = Path.GetFileName(tbFirstFile.Text);
                            lblSecondFile.Text = Path.GetFileName(tbSecondFile.Text);
                            SetControlPropertyThreadSafe(lblStatus, "Text", string.Empty);
                            colorProgressBar1.Value = 0;
                            lblPercentage.Text = "0%";
                            i = 0;
                            total = 0;
                            compareCode.RunWorkerAsync();
                        }
                    }
                }
                else
                {
                    if (compareCode.IsBusy)
                    {
                        btCompare.Enabled = false;
                        lblStatus.Text = "Cancelling...";
                        compareCode.CancelAsync();
                    }
                    else
                    {
                        btCompare.Text = "Cancel";
                        lblStatus.Text = "Running...";
                        lblFirstFile.Text = fileName;
                        lblSecondFile.Text = Path.GetFileName(tbSecondFile.Text);
                        SetControlPropertyThreadSafe(lblStatus, "Text", string.Empty);
                        colorProgressBar1.Value = 0;
                        lblPercentage.Text = "0%";
                        i = 0;
                        total = 0;
                        compareCode.RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex) { }
        }


        private void compareCode_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                if (worker.CancellationPending)
                {
                    System.Threading.Thread.Sleep(1200);
                    e.Cancel = true; ;
                    return;
                }
                else
                {
                    CompareCode(sender, e);
                }
            }
            catch (Exception ex) { }
        }

        private void compareCode_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                string value = string.Empty;
                value = (string)e.UserState;
                SetControlPropertyThreadSafe(lblStatus, "Text", value);
                colorProgressBar1.Value = e.ProgressPercentage;
                lblPercentage.Text = e.ProgressPercentage + "%";
            }
            catch (Exception ex) { }
        }

        private void compareCode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btCompare.Text = "Compare";
            btCompare.Enabled = true;

            try
            {
                if (e.Error != null)
                {
                    MessageBox.Show("ERROR", e.Error.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (e.Cancelled)
                {
                    lblStatus.Text = "Cancelled...";
                }
                else
                {
                    lblStatus.Text = "Completed...";
                    if (colorProgressBar1.Value != colorProgressBar1.Maximum)
                    {
                        colorProgressBar1.Value = colorProgressBar1.Maximum;
                        lblPercentage.Text = "100%";
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void CompareCode(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int totalLines = 0;
            string[] file1;
            string[] file2;
            Lines source1, source2;
            if (worker.CancellationPending)
            {
                System.Threading.Thread.Sleep(1200);
                e.Cancel = true; ;
                return;
            }
            else
            {
                fctb1.Invoke((MethodInvoker)delegate()
                {
                    fctb1.Clear();
                });

                fctb2.Invoke((MethodInvoker)delegate()
                {
                    fctb2.Clear();
                });
                if (source_code == null && string.IsNullOrEmpty(fileName))
                    file1 = File.ReadAllLines(tbFirstFile.Text);
                else
                    file1 = source_code;
                file2 = File.ReadAllLines(tbSecondFile.Text);
                totalLines = file1.Length + file2.Length;
                DiffMergeSample.total += (float)(totalLines);
                if (source_code == null && string.IsNullOrEmpty(fileName))
                    source1 = Lines.Load(tbFirstFile.Text, sender, e, false, null);
                else
                    source1 = Lines.Load(tbFirstFile.Text, sender, e, true, source_code);
                source2 = Lines.Load(tbSecondFile.Text, sender, e, false, null);
                if (Path.GetExtension(tbFirstFile.Text).ToLower() == ".cs")
                {
                    fctb1.Invoke((MethodInvoker)delegate()
                    {
                        fctb1.Language = Language.CSharp;
                    });
                    fctb2.Invoke((MethodInvoker)delegate()
                    {
                        fctb2.Language = Language.CSharp;
                    });
                }
                else
                {
                    fctb1.Invoke((MethodInvoker)delegate()
                    {
                        fctb1.Language = Language.Custom;
                    });
                    fctb2.Invoke((MethodInvoker)delegate()
                    {
                        fctb2.Language = Language.Custom;
                    });
                }
                source1.Merge(source2, sender,e);
                BeginUpdate();
                Process(source1, sender, e);
                EndUpdate();
            }
        }

        private void Process(Lines lines,object sender, DoWorkEventArgs e)//work2
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            DiffMergeSample.total += lines.Count;
            foreach(var line in lines)
            {
                if (worker.CancellationPending)
                {
                    System.Threading.Thread.Sleep(1200);
                    e.Cancel = true; ;
                    break;
                }
                else
                {
                    DiffMergeSample.progress = (int)((DiffMergeSample.i / DiffMergeSample.total) * 100);
                    worker.ReportProgress(DiffMergeSample.progress,line);
                    System.Threading.Thread.Sleep(delayTime);
                    DiffMergeSample.i++;
                    switch (line.state)
                    {
                        case DiffType.None:
                            fctb1.AppendText(line.line + Environment.NewLine);
                            fctb2.AppendText(line.line + Environment.NewLine);
                            break;
                        case DiffType.Inserted:
                            fctb1.AppendText(Environment.NewLine);
                            fctb2.AppendText(line.line + Environment.NewLine, greenStyle);
                            break;
                        case DiffType.Deleted:
                            fctb1.AppendText(line.line + Environment.NewLine, redStyle);
                            fctb2.AppendText(Environment.NewLine);
                            break;
                    }
                    if (line.subLines != null)
                        Process(line.subLines, sender, e);
                }
            }
        }

        private void DiffMergeSample_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (compareCode.IsBusy)
            {
                compareCode.CancelAsync();
            }
        }

        private void DiffMergeSample_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (compareCode.IsBusy)
            {
                compareCode.CancelAsync();
            }
        }
    }

    #region Merge stuffs

    namespace DiffMergeStuffs
    {
        public class SimpleDiff<T>
        {
            //private int delayTime = 10;
            private IList<T> _left;
            private IList<T> _right;
            private int[,] _matrix;
            private bool _matrixCreated;
            private int _preSkip;
            private int _postSkip;

            private Func<T, T, bool> _compareFunc;

            public SimpleDiff(IList<T> left, IList<T> right)
            {
                _left = left;
                _right = right;

                InitializeCompareFunc();
            }

            public event EventHandler<DiffEventArgs<T>> LineUpdate;

            public TimeSpan ElapsedTime { get; private set; }

            /// <summary>
            /// This is the sole public method and it initializes
            /// the LCS matrix the first time it's called, and 
            /// proceeds to fire a series of LineUpdate events
            /// </summary>
            public void RunDiff(object sender, DoWorkEventArgs e)
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                if (worker.CancellationPending)
                {
                    System.Threading.Thread.Sleep(1200);
                    e.Cancel = true; ;
                    return;
                }
                else
                {
                    if (!_matrixCreated)
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        CalculatePreSkip(sender,e);
                        CalculatePostSkip(sender,e);
                        CreateLCSMatrix(sender,e);
                        sw.Stop();
                        this.ElapsedTime = sw.Elapsed;
                    }

                    for (int i = 0; i < _preSkip; i++)
                    {
                        FireLineUpdate(DiffType.None, i, -1);
                    }

                    int totalSkip = _preSkip + _postSkip;
                    ShowDiff(_left.Count - totalSkip, _right.Count - totalSkip);

                    int leftLen = _left.Count;
                    for (int i = _postSkip; i > 0; i--)
                    {
                        FireLineUpdate(DiffType.None, leftLen - i, -1);
                    }
                }
            }

            /// <summary>
            /// This method is an optimization that
            /// skips matching elements at the end of the 
            /// two arrays being diff'ed.
            /// Care's taken so that this will never
            /// overlap with the pre-skip.
            /// </summary>
            private void CalculatePostSkip(object sender, DoWorkEventArgs e)
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                
                if (worker.CancellationPending)
                {
                    System.Threading.Thread.Sleep(1200);
                    e.Cancel = true; ;
                    return;
                }
                {
                    int leftLen = _left.Count;
                    int rightLen = _right.Count;
                    while (_postSkip < leftLen && _postSkip < rightLen &&
                           _postSkip < (leftLen - _preSkip) &&
                           _compareFunc(_left[leftLen - _postSkip - 1], _right[rightLen - _postSkip - 1]))
                    {
                        _postSkip++;
                        //DiffMergeSample.progress = (int)((DiffMergeSample.i / DiffMergeSample.total) * 100);
                        //worker.ReportProgress(DiffMergeSample.progress);
                        //System.Threading.Thread.Sleep(delayTime);
                        //DiffMergeSample.i++;
                    }
                }
            }

            /// <summary>
            /// This method is an optimization that
            /// skips matching elements at the start of
            /// the arrays being diff'ed
            /// </summary>
            private void CalculatePreSkip(object sender, DoWorkEventArgs e)
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                if (worker.CancellationPending)
                {
                    System.Threading.Thread.Sleep(1200);
                    e.Cancel = true; ;
                    return;
                }
                else
                {
                    int leftLen = _left.Count;
                    int rightLen = _right.Count;
                    while (_preSkip < leftLen && _preSkip < rightLen &&
                           _compareFunc(_left[_preSkip], _right[_preSkip]))
                    {
                        //DiffMergeSample.progress = (int)((DiffMergeSample.i / DiffMergeSample.total) * 100);
                        //worker.ReportProgress(DiffMergeSample.progress);
                        //System.Threading.Thread.Sleep(delayTime);
                        //DiffMergeSample.i++;
                        _preSkip++;
                    }
                }
            }

            /// <summary>
            /// This traverses the elements using the LCS matrix
            /// and fires appropriate events for added, subtracted, 
            /// and unchanged lines.
            /// It's recursively called till we run out of items.
            /// </summary>
            /// <param name="leftIndex"></param>
            /// <param name="rightIndex"></param>
            private void ShowDiff(int leftIndex, int rightIndex)
            {
                if (leftIndex > 0 && rightIndex > 0 &&
                    _compareFunc(_left[_preSkip + leftIndex - 1], _right[_preSkip + rightIndex - 1]))
                {
                    ShowDiff(leftIndex - 1, rightIndex - 1);
                    FireLineUpdate(DiffType.None, _preSkip + leftIndex - 1, -1);
                }
                else
                {
                    if (rightIndex > 0 &&
                        (leftIndex == 0 ||
                         _matrix[leftIndex, rightIndex - 1] >= _matrix[leftIndex - 1, rightIndex]))
                    {
                        ShowDiff(leftIndex, rightIndex - 1);
                        FireLineUpdate(DiffType.Inserted, -1, _preSkip + rightIndex - 1);
                    }
                    else if (leftIndex > 0 &&
                             (rightIndex == 0 ||
                              _matrix[leftIndex, rightIndex - 1] < _matrix[leftIndex - 1, rightIndex]))
                    {
                        ShowDiff(leftIndex - 1, rightIndex);
                        FireLineUpdate(DiffType.Deleted, _preSkip + leftIndex - 1, -1);
                    }
                }

            }

            /// <summary>
            /// This is the core method in the entire class,
            /// and uses the standard LCS calculation algorithm.
            /// </summary>
            private void CreateLCSMatrix(object sender, DoWorkEventArgs e)
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                if (worker.CancellationPending)
                {
                    System.Threading.Thread.Sleep(1200);
                    e.Cancel = true; ;
                    return;
                }
                {
                    int totalSkip = _preSkip + _postSkip;
                    if (totalSkip >= _left.Count || totalSkip >= _right.Count)
                        return;

                    // We only create a matrix large enough for the
                    // unskipped contents of the diff'ed arrays
                    _matrix = new int[_left.Count - totalSkip + 1, _right.Count - totalSkip + 1];

                    for (int i = 1; i <= _left.Count - totalSkip; i++)
                    {
                        // Simple optimization to avoid this calculation
                        // inside the outer loop (may have got JIT optimized 
                        // but my tests showed a minor improvement in speed)
                        int leftIndex = _preSkip + i - 1;

                        // Again, instead of calculating the adjusted index inside
                        // the loop, I initialize it under the assumption that
                        // incrementing will be a faster operation on most CPUs
                        // compared to addition. Again, this may have got JIT
                        // optimized but my tests showed a minor speed difference.
                        for (int j = 1, rightIndex = _preSkip + 1; j <= _right.Count - totalSkip; j++, rightIndex++)
                        {
                            _matrix[i, j] = _compareFunc(_left[leftIndex], _right[rightIndex - 1])
                                                ? _matrix[i - 1, j - 1] + 1
                                                : Math.Max(_matrix[i, j - 1], _matrix[i - 1, j]);
                            //DiffMergeSample.progress = (int)((DiffMergeSample.i / DiffMergeSample.total) * 100);
                            //worker.ReportProgress(DiffMergeSample.progress);
                            //System.Threading.Thread.Sleep(delayTime);
                            //DiffMergeSample.i++;
                        }
                    }

                    _matrixCreated = true;
                }
            }

            private void FireLineUpdate(DiffType diffType, int leftIndex, int rightIndex)
            {
                var local = this.LineUpdate;

                if (local == null)
                    return;

                T lineValue = leftIndex >= 0 ? _left[leftIndex] : _right[rightIndex];

                local(this, new DiffEventArgs<T>(diffType, lineValue, leftIndex, rightIndex));
            }

            private void InitializeCompareFunc()
            {
                // Special case for String types
                if (typeof (T) == typeof (String))
                {
                    _compareFunc = StringCompare;
                }
                else
                {
                    _compareFunc = DefaultCompare;
                }
            }

            /// <summary>
            /// This comparison is specifically
            /// for strings, and was nearly thrice as 
            /// fast as the default comparison operation.
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            private bool StringCompare(T left, T right)
            {
                return Object.Equals(left, right);
            }

            private bool DefaultCompare(T left, T right)
            {
                return left.Equals(right);
            }
        }

        [Flags]
        public enum DiffType
        {
            None = 0,
            Inserted = 1,
            Deleted = 2
        }

        public class DiffEventArgs<T> : EventArgs
        {
            public DiffType DiffType { get; set; }

            public T LineValue { get; private set; }
            public int LeftIndex { get; private set; }
            public int RightIndex { get; private set; }

            public DiffEventArgs(DiffType diffType, T lineValue, int leftIndex, int rightIndex)
            {
                this.DiffType = diffType;
                this.LineValue = lineValue;
                this.LeftIndex = leftIndex;
                this.RightIndex = rightIndex;
            }
        }

        /// <summary>
        /// Line of file
        /// </summary>
        public class Line
        {
            /// <summary>
            /// Source string
            /// </summary>
            public readonly string line;
            private int delayTime = 10;
            /// <summary>
            /// Inserted strings
            /// </summary>
            public Lines subLines;

            /// <summary>
            /// Line state
            /// </summary>
            public DiffType state;

            public Line(string line)
            {
                this.line = line;
            }

            /// <summary>
            /// Equals
            /// </summary>
            public override bool Equals(object obj)
            {
                return Object.Equals(line, ((Line) obj).line);
            }

            public static bool operator ==(Line line1, Line line2)
            {
                return Object.Equals(line1.line, line2.line);
            }

            public static bool operator !=(Line line1, Line line2)
            {
                return !Object.Equals(line1.line, line2.line);
            }

            public override string ToString()
            {
                return line;
            }
        }

        /// <summary>
        /// File as list of lines
        /// </summary>
        public class Lines : List<Line>, IEquatable<Lines>
        {
            //эта строка нужна для хранения строк, вставленных в самом начале, до первой строки исходного файла
            private Line fictiveLine = new Line("===fictive line===") {state = DiffType.Deleted};
            private static int delayTime = 10;
            public Lines()
            {
            }


            public Lines(int capacity)
                : base(capacity)
            {
            }

            public Line this[int i]
            {
                get
                {
                    if (i == -1) return fictiveLine;
                    return base[i];
                }

                set
                {
                    if (i == -1) fictiveLine = value;
                    base[i] = value;
                }
            }

            /// <summary>
            /// Load from file
            /// </summary>
            public static Lines Load(string fileName,object sender,DoWorkEventArgs e,bool IsCode,string[] code,params Encoding[] enc)//work1
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                Encoding tempEnc;
                string[] lineArray;
                if (enc.Length > 0)
                {
                    tempEnc = enc[0];
                }
                else
                {
                    tempEnc = Encoding.Default;
                }
                Lines lines = new Lines();
                if (IsCode)
                {
                    lineArray = code;
                }
                else
                {
                    lineArray = File.ReadAllLines(fileName, tempEnc);
                }
                foreach (var line in lineArray)
                {
                    if (worker.CancellationPending)
                    {
                        System.Threading.Thread.Sleep(1200);
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        lines.Add(new Line(line));
                        DiffMergeSample.progress = (int)((DiffMergeSample.i / DiffMergeSample.total) * 100);
                        worker.ReportProgress(DiffMergeSample.progress,line);
                        System.Threading.Thread.Sleep(delayTime);
                        DiffMergeSample.i++;
                    }
                }

                return lines;
            }

            /// <summary>
            /// Merge lines
            /// </summary>
            public void Merge(Lines lines, object sender, DoWorkEventArgs args)//work3
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                if (worker.CancellationPending)
                {
                    System.Threading.Thread.Sleep(1200);
                    args.Cancel = true; ;
                    return;
                }
                {
                    SimpleDiff<Line> diff = new SimpleDiff<Line>(this, lines);
                    int iLine = -1;

                    diff.LineUpdate += (o, e) =>
                                           {
                                               if (e.DiffType == DiffType.Inserted)
                                               {
                                                   if (this[iLine].subLines == null)
                                                       this[iLine].subLines = new Lines();
                                                   e.LineValue.state = DiffType.Inserted;
                                                   this[iLine].subLines.Add(e.LineValue);
                                               }
                                               else
                                               {
                                                   iLine++;
                                                   this[iLine].state = e.DiffType;
                                                   if (iLine > 0 &&
                                                       this[iLine - 1].state == DiffType.Deleted &&
                                                       this[iLine - 1].subLines == null &&
                                                       e.DiffType == DiffType.None)
                                                       this[iLine - 1].subLines = new Lines();
                                               }
                                           };
                    diff.RunDiff(sender,args);
                }
            }

            /// <summary>
            /// Clone
            /// </summary>
            public Lines Clone()
            {
                Lines result = new Lines(this.Count);
                foreach (var line in this)
                    result.Add(new Line(line.line));

                return result;
            }

            /// <summary>
            /// Is lines equal?
            /// </summary>
            public bool Equals(Lines other)
            {
                if (Count != other.Count)
                    return false;
                for (int i = 0; i < Count; i++)
                    if (this[i] != other[i])
                        return false;
                return true;
            }

            /// <summary>
            /// Transform tree to list
            /// </summary>
            public Lines Expand()
            {
                return Expand(-1, Count - 1);
            }

            /// <summary>
            /// Transform tree to list
            /// </summary>
            public Lines Expand(int from, int to)
            {
                Lines result = new Lines();
                for (int i = from; i <= to; i++)
                {
                    if (this[i].state != DiffType.Deleted)
                        result.Add(this[i]);
                    if (this[i].subLines != null)
                        result.AddRange(this[i].subLines.Expand());
                }

                return result;
            }
        }

        /// <summary>
        /// Строка, содержащая несколько конфликтных версий
        /// </summary>
        public class ConflictedLine : Line
        {
            public readonly Lines version1;
            public readonly Lines version2;

            public ConflictedLine(Lines version1, Lines version2)
                : base("?")
            {
                this.version1 = version1;
                this.version2 = version2;
            }
        }
    }
    #endregion
}
