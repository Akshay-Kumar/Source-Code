using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OC.Windows.Forms;
using System.Data;

namespace FileExplorer
{
    internal enum eCategory
    { 
        Library,
        Book,
        Page
    }

    public partial class Explorer : Form
    {
        public Explorer(DataTable dtDirectory)
        {
            InitializeComponent();

            ocTreeView1.DrawMode = TreeViewDrawMode.OwnerDrawAll;

            ensureDefaultImageIndex(ocTreeView1);
            loadTree(ocTreeView1, dtDirectory);
            ocTreeView1.Nodes[0].ExpandAll();
        }
        public Explorer()
        {
            InitializeComponent();

            ocTreeView1.DrawMode = TreeViewDrawMode.OwnerDrawAll;

            ensureDefaultImageIndex(ocTreeView1);
            loadTree(ocTreeView1);
            ocTreeView1.Nodes[0].ExpandAll();
        }
        // !!! ensure default imageindices are invalid !!!
        // ,otherwise treeview assigns first image to all nodes with unspecified (or -1) imageindices.
        // (Setting -1 does not work, nor does specifying empty or invalid ImageKey).
        // Set ImageIndex to >= current ImageList length.
        // Redo after any change in ImageList.Length or re-assignment of ImageList.
        // Caveat: ImageIndex will return ImageList.Images.Count -1;
        private void ensureDefaultImageIndex(TreeView tree)
        {
            if (tree.ImageList != null)
            {
                tree.ImageIndex = tree.ImageList.Images.Count;
                tree.SelectedImageIndex = tree.ImageList.Images.Count;
            }
        }

        private void loadTree(TreeView tree)
        {
            TreeNode tnLib; TreeNode tnBook; TreeNode tnPage;
            const int iLib = 3; const int iBook = 3; const int iPage = 3;

            tree.BeginUpdate();

            tree.Nodes.Clear();

            for (int i = 0; i < iLib; i++)
            {
                tnLib = tree.Nodes.Add(eCategory.Library + ":" + i.ToString(),
                    eCategory.Library.ToString() + i.ToString(),
                    idx(0, i), idx(1, i));

                for (int j = 0; j < iBook; j++)
                {
                    tnBook = tnLib.Nodes.Add(eCategory.Book + ":" + j.ToString(),
                        eCategory.Book.ToString() + j.ToString(),
                        idx(2, j), idx(3, j));

                    for (int k = 0; k < iPage; k++)
                    {
                        tnPage = tnBook.Nodes.Add(eCategory.Page + ":" + k.ToString(),
                            eCategory.Page.ToString() + k.ToString(),
                            ocTreeView.NOIMAGE, ocTreeView.NOIMAGE);
                    }
                }
            }
            tree.EndUpdate();
           
        }
        private void loadTree(TreeView tree,DataTable dtDirectory)
        {
            TreeNode tnLib; /*TreeNode tnBook;*/ TreeNode tnPage;
            int iLib = 3; int iPage = 3;
            string dir = string.Empty; string pg = string.Empty;
            DataView dirView = new DataView(dtDirectory);
            DataTable distinctDirs = new DataTable();
            distinctDirs = dirView.ToTable(true, "Directory");
            iLib = distinctDirs.Rows.Count;

            tree.BeginUpdate();

            for (int i = 0; i < iLib; i++)
            {
                dir = distinctDirs.Rows[i]["Directory"].ToString().Trim();
                tnLib = tree.Nodes.Add
                    (
                    dir,
                    dir,
                    0, 
                    1
                    );

                DataView pgView = new DataView(dtDirectory);
                pgView.RowFilter = "Directory = '" + dir + "'";
                DataTable distinctPgs = new DataTable();
                distinctPgs = pgView.ToTable(true, "File");
                iPage = distinctPgs.Rows.Count;

                for (int k = 0; k < iPage; k++)
                {
                    pg = distinctPgs.Rows[k]["File"].ToString().Trim();
                    tnPage = tnLib.Nodes.Add
                        (
                        pg,
                        pg,
                        4,
                        4
                        );
                }
            }

            tree.EndUpdate();
        }

        // no image for even indices
        private int idx(int iImg, int iNode)
        {
            return (iNode % 2 == 0) ? ocTreeView.NOIMAGE : iImg;
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            if (ocTreeView1 == null) return;

            if (sender.Equals(chkCheckBoxes))
            {
                ocTreeView1.CheckBoxes = chkCheckBoxes.Checked;
            }
            else if (sender.Equals(chkShowLines))
            {
                ocTreeView1.ShowLines = chkShowLines.Checked;
            }
            else if (sender.Equals(chkShowRootLines))
            {
                ocTreeView1.ShowRootLines = chkShowRootLines.Checked;
            }
            else if (sender.Equals(chkShowPlusMinus))
            {
                ocTreeView1.ShowPlusMinus = chkShowPlusMinus.Checked;
            }
            else if (sender.Equals(chkOwnerDraw))
            {
                ocTreeView1.DrawMode = chkOwnerDraw.Checked ? 
                    TreeViewDrawMode.OwnerDrawAll : TreeViewDrawMode.Normal;
            }
            else if (sender.Equals(chkImageList))
            {
                if (chkImageList.Checked)
                {
                    ocTreeView1.ImageList = imageList24;
                }
                else
                {
                    ocTreeView1.ImageList = imageList16;
                }

                nudItemHeight.Minimum = new decimal(ocTreeView1.ImageList.ImageSize.Height);
                nudItemHeight.Maximum = 2 * nudItemHeight.Minimum;
                nudIndent.Minimum = new decimal(ocTreeView1.ImageList.ImageSize.Width + 3);
                nudIndent.Maximum = 2 * nudIndent.Minimum;
            }
        }

        private void nudItemHeight_ValueChanged(object sender, EventArgs e)
        {
            if (ocTreeView1 == null) return;
            ensureDemo();
            ocTreeView1.ItemHeight = (int)nudItemHeight.Value;
        }

        private void nudIndent_ValueChanged(object sender, EventArgs e)
        {
            if (ocTreeView1 == null) return;
            ocTreeView1.Indent = (int)nudIndent.Value;
        }

        private void ensureDemo()
        {
            // Unless a child node is selected, treeview collapses all nodes, if height is set
            // annoying in demo
            if (ocTreeView1.SelectedNode != null & ocTreeView1.SelectedNode.Level > 0)
            {
                return;
            }

            if (ocTreeView1.SelectedNode == null)
            {
                ocTreeView1.SelectedNode = ocTreeView1.Nodes[0];
            }

            ocTreeView1.SelectedNode = ocTreeView1.SelectedNode.FirstNode;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Explorer());
        }

        private void ocTreeView1_DoubleClick(object sender, EventArgs e)
        {
            string message = ProcessAPI.ProcStartargs("notepad", ocTreeView1.SelectedNode.Text);
        }
    }
}