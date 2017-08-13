using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using ScdMergeWizard.Database;
using ScdMergeWizard.ExcHandling;

namespace ScdMergeWizard.Pages
{
    public partial class Page40_CheckSourceDataBusinessKeys : TheMasterPage, IMasterPage
    {
        private const int MAX_ROWS = 1000;

        public Page40_CheckSourceDataBusinessKeys()
        {
            InitializeComponent();
        }

        public bool IsShowable()
        {
            return true;
        }

        public void OnPageEntering()
        {
            label2.Text = string.Format("If any duplicate business keys are found, they will be shown in this table (first {0}):", MAX_ROWS);
        }

        public PageLeavingEventArgs OnPageLeaving(PageLeavingEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                e.Cancel = true;
                e.PageErrors.Add(new PageError { Control = dataGridView1, ErrorMessage = "Duplicate Business Keys found!" });
            }

            return e;
        }

        private void checkUsingSourceQuery()
        {
            string commandText = "";
            var initialCursor = this.Cursor;
            object[] meta = new object[GlobalVariables.ColumnMappings.Count(cm=>cm.TransformationCode == ETransformationCode.BUSINESS_KEY) + 1];
            
            this.Cursor = Cursors.WaitCursor;

            try
            {
                DbCommand cmd = GlobalVariables.SourceConnection.GetConn().CreateCommand();

                string businessKeysCsv = string.Join(",", GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode == ETransformationCode.BUSINESS_KEY).Select(c => c.SourceColumn).ToArray());
                commandText += string.Format("WITH cte as ( SELECT {0} FROM {1} ) SELECT {0}, COUNT(*) AS [COUNT] FROM cte GROUP BY {0} HAVING COUNT(*) > 1", businessKeysCsv, GlobalVariables.SourceObjectName);
                cmd.CommandText = commandText;

                DbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();

                    if (reader.FieldCount > 0)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            DataGridViewColumn col = new DataGridViewColumn();
                            DataGridViewCell cell = new DataGridViewTextBoxCell();
                            col.CellTemplate = cell;
                            col.Name = reader.GetName(i);
                            dataGridView1.Columns.Add(col);
                        }

                        int rowsCount = 0;
                        while (reader.Read())
                        {
                            reader.GetValues(meta);
                            dataGridView1.Rows.Add(meta);

                            rowsCount++;
                            if (rowsCount >= MAX_ROWS)
                                break;
                        }
                        reader.Close();

                        MessageBox.Show("Unfortunately, there are some duplicate Business Keys found. Check table for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Congratulations, no duplicate Business Keys found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }

            }
            catch (Exception ex)
            {
                this.Cursor = initialCursor;
                MyExceptionHandler.NewEx(ex);
            }

            this.Cursor = initialCursor;
        }

        private void checkUsingDotNetDataTable()
        {
            string commandText;

            try
            {
                if (GlobalVariables.SourceIsTableOrViewMode == true)
                    commandText = "SELECT * FROM " + GlobalVariables.SourceObjectName;
                else
                    commandText = GlobalVariables.SourceCommandText;

                DbDataAdapter adapter = GlobalVariables.SourceConnection.CreateAdapter(commandText); // new DbDataAdapter(commandText, GlobalVariables.SourceConnection.GetConn());
                DataTable dt = new DataTable();
                adapter.Fill(dt);


                string[] columnNames = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode == ETransformationCode.BUSINESS_KEY).Select(c => c.SourceColumn.Substring(1, c.SourceColumn.Length - 2)).ToArray();
                DataView dv = new DataView(dt);

                //getting distinct values for group column
                DataTable dtGroup = dv.ToTable(true, columnNames);

                //adding column for the row count
                dtGroup.Columns.Add("Count", typeof(int));

                //looping thru distinct values for the group, counting
                foreach (DataRow dr in dtGroup.Rows)
                {
                    string filter = "";

                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(filter))
                            filter += " AND ";

                        MyDbColumn c1 = GlobalVariables.SourceColumns.Find(sc => sc.ColumnName.Equals("[" + columnNames[i] + "]"));

                        filter += c1.ColumnName + " = ";

                        if (!c1.IsNumeric)
                            filter += "'";

                        filter += dr[columnNames[i]].ToString();

                        if (!c1.IsNumeric)
                            filter += "'";
                    }

                    string func = string.Format("Count({0})", GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode != ETransformationCode.SKIP && cm.TransformationCode != ETransformationCode.BUSINESS_KEY && cm.IsSourceColumnDefined).First().SourceColumn);

                    dr["Count"] = dt.Compute(func, filter);
                }

                dt.Dispose();


                DataRow[] rows = (from r in dtGroup.AsEnumerable()
                                  where r.Field<int>("Count") > 1
                                  select r).ToArray();

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                if (rows.Length > 0)
                {
                    for (int i = 0; i < rows[0].Table.Columns.Count; i++)
                    {
                        DataGridViewColumn col = new DataGridViewColumn();
                        DataGridViewCell cell = new DataGridViewTextBoxCell();
                        col.CellTemplate = cell;
                        col.Name = rows[0].Table.Columns[i].ColumnName;
                        dataGridView1.Columns.Add(col);
                    }

                    foreach (var row in rows)
                    {
                        dataGridView1.Rows.Add(row.ItemArray);
                    }

                    MessageBox.Show("Unfortunately, there are some duplicate Business Keys found. Check table for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Congratulations, no duplicate Business Keys found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                dataGridView1.Rows.Clear();
                MyExceptionHandler.NewEx(ex);
            }
        }

        private void buttonCheckDuplicateBusinessKeys_Click(object sender, EventArgs e)
        {
            buttonCheckDuplicateBusinessKeys.Enabled = false;
            buttonClearResults.Enabled = false;
            if (radioButtonCheckUsingSourceQuery.Checked)
                checkUsingSourceQuery();
            else
                checkUsingDotNetDataTable();
            buttonCheckDuplicateBusinessKeys.Enabled = true;
            buttonClearResults.Enabled = true;
        }

        private void buttonClearResults_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}
