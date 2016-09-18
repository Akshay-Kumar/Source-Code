using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logger;
using System.Data;

namespace SourceCode
{
    #region AutoResizeDataGridview
    public class AutoResizeDataGridview
    {
        #region Methods
        /// <summary>
        /// public static void ResizeGrid(DataGridView dataGrid, ref int prevWidth)
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="prevWidth"></param>
        public static void ResizeGrid(DataGridView dataGrid, ref int prevWidth)
        {
            int fixedWidth = 0;
            int mul = 0;
            int columnWidth = 0;
            int total = 0;
            try
            {
                if (prevWidth == 0)
                    prevWidth = dataGrid.Width;
                if (prevWidth == dataGrid.Width)
                    return;

                if (dataGrid.RowHeadersVisible)
                {
                    fixedWidth = dataGrid.RowHeadersWidth + 2;
                }
                if (prevWidth != fixedWidth)
                {
                    mul = 100 * (dataGrid.Width - fixedWidth)/(prevWidth - fixedWidth);
                }
                DataGridViewColumn lastVisibleCol = null;
                for (int i = 0; i < dataGrid.ColumnCount; i++)
                    if (dataGrid.Columns[i].Visible)
                    {
                        columnWidth = (dataGrid.Columns[i].Width * mul + 50) / 100;
                        dataGrid.Columns[i].Width = Math.Max(columnWidth, dataGrid.Columns[i].MinimumWidth);
                        total += dataGrid.Columns[i].Width;
                        lastVisibleCol = dataGrid.Columns[i];
                    }
                if (lastVisibleCol == null)
                    return;
                columnWidth = (dataGrid.Width - total) +
                (lastVisibleCol.Width - fixedWidth);
                lastVisibleCol.Width = Math.Max(columnWidth, lastVisibleCol.MinimumWidth);
                prevWidth = dataGrid.Width;
            }
            catch (Exception exHandler)
            {
                ErrorLog.ErrorRoutine(exHandler);
            }
        }
        #endregion
    }
    #endregion
}
