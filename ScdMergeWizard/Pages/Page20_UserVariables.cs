using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using ScdMergeWizard.Database;
using ScdMergeWizard.Formz;

namespace ScdMergeWizard.Pages
{
    public partial class Page20_UserVariables : TheMasterPage, IMasterPage
    {
        public Page20_UserVariables()
        {
            InitializeComponent();
        }

        public bool IsShowable()
        {
            return true;
        }

        public void OnPageEntering()
        {
            // Init Loaded User variables from file...
            if (GlobalVariables.LoadedUserColumnsDefinitions.Count > 0)
            {
                dgvUserFields.Rows.Clear();
                foreach (MyUserVariable uv in GlobalVariables.LoadedUserColumnsDefinitions)
                    dgvUserFields.Rows.Add(uv.Name, uv.DataType, uv.Definition.Replace("<![CDATA[", "").Replace("]]>", ""));

                GlobalVariables.LoadedUserColumnsDefinitions.Clear();
            }

            // If there are no fields
            if (dgvUserFields.Rows.Count == 1) // Only new row
            {
                dgvUserFields.Rows.Add("CurrentDateTime", "datetime", "cast(getdate() as datetime)");
                dgvUserFields.Rows.Add("MinDateTime", "datetime", "cast('1900-01-01' as datetime)");
                dgvUserFields.Rows.Add("MinDateTime2", "datetime2", "cast('0001-01-01' as datetime2)");
                dgvUserFields.Rows.Add("MaxDateTime", "datetime", "cast('9999-12-31' as datetime)");
                dgvUserFields.Rows.Add("NullDateTime", "datetime", "cast(null as datetime)");
                dgvUserFields.Rows.Add("BooleanTrue", "bit", "cast(1 as bit)");
                dgvUserFields.Rows.Add("BooleanFalse", "bit", "cast(0 as bit)");
                dgvUserFields.Rows.Add("StringYes", "char(3)", "'Yes'");
                dgvUserFields.Rows.Add("StringNo", "char(2)", "'No'");

                dgvUserFields.Rows.Add("StringActive", "varchar(20)", "'Active'");
                dgvUserFields.Rows.Add("StringNotActive", "varchar(20)", "'Not Active'");

                dgvUserFields.Rows.Add("StringDeleted", "varchar(20)", "'Deleted'");
                dgvUserFields.Rows.Add("StringNotDeleted", "varchar(20)", "'Not Deleted'");

                dgvUserFields.Rows.Add("StringNull", "varchar(10)", "cast(null as varchar)");
            }

            GlobalVariables.IsProjectModified = false;
        }

        private string getSQL(bool showResults)
        {
            string sql = string.Empty;

            if (dgvUserFields.Rows.Count > 0)
            {
                for (int i = 0; i < dgvUserFields.Rows.Count; i++)
                {
                    if (!dgvUserFields.Rows[i].IsNewRow)
                        sql += string.Format("DECLARE @{0} {1}{2}", dgvUserFields.Rows[i].Cells["colName"].Value, dgvUserFields.Rows[i].Cells["colDataType"].Value, Environment.NewLine);
                }

                sql += Environment.NewLine + "SELECT";
                for (int i = 0; i < dgvUserFields.Rows.Count; i++)
                {
                    if (!dgvUserFields.Rows[i].IsNewRow)
                    {
                        if (i > 0)
                            sql += ",";
                        sql += string.Format("{0}\t@{1} = {2}", Environment.NewLine, dgvUserFields.Rows[i].Cells["colName"].Value, dgvUserFields.Rows[i].Cells["colDefinition"].Value);
                    }
                }

                if (showResults)
                {
                    sql += Environment.NewLine;
                    for (int i = 0; i < dgvUserFields.Rows.Count; i++)
                    {
                        
                        if (!dgvUserFields.Rows[i].IsNewRow)
                        {
                            if (i > 0)
                                sql += "UNION ALL" + Environment.NewLine;
                            sql += string.Format("SELECT '@{0}' as [Name], cast(@{0} as varchar) as [Value]", dgvUserFields.Rows[i].Cells["colName"].Value, dgvUserFields.Rows[i].Cells["colDefinition"].Value);
                        }
                    }

                }
            }
            return sql;
        }

        private Exception testSQL()
        {
            OleDbCommand cmd = GlobalVariables.SourceConnection.GetConn().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = getSQL(false);

            try
            {
                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        public PageLeavingEventArgs OnPageLeaving(PageLeavingEventArgs e)
        {
            List<string> variableValues = new List<string>();

            GlobalVariables.UserColumns.Clear();
            GlobalVariables.UserColumnsDefinitions.Clear();

            Exception ex = testSQL();

            if (ex != null)
            {
                e.Cancel = true;
                e.PageErrors.Add(new PageError { Control = dgvUserFields, ErrorMessage = string.Format("System Fields are not defined properly! Use TSQL standards.{0}{0}Error:{0}{1}", Environment.NewLine, ex.Message) });
                return e;
            }

            OleDbCommand cmd = GlobalVariables.SourceConnection.GetConn().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = getSQL(true);

            OleDbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                variableValues.Add(rdr[1].ToString());
            }
            rdr.Close();

            for(int i=0; i<dgvUserFields.Rows.Count-1; i++)
            {
                if (!dgvUserFields.Rows[i].IsNewRow)
                {
                    GlobalVariables.UserColumns.Add(new MyDbColumn { ColumnName = "@" + dgvUserFields.Rows[i].Cells["colName"].Value });
                    GlobalVariables.UserColumnsDefinitions.Add(new MyUserVariable { Name = dgvUserFields.Rows[i].Cells["colName"].Value.ToString(), ColumnName = "@" + dgvUserFields.Rows[i].Cells["colName"].Value, DataType = dgvUserFields.Rows[i].Cells["colDataType"].Value.ToString(), Definition = dgvUserFields.Rows[i].Cells["colDefinition"].Value.ToString(), Value = variableValues[i].ToUpper() });
                }
            }

            GlobalVariables.UserColumnsDefsQuery = getSQL(false);

            return e;
        }

        private void buttonTestSql_Click(object sender, EventArgs e)
        {
            Exception ex = testSQL();
            if (ex == null)
                MessageBox.Show("Variables are well defined", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Variables are not defined well", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            var form = new UserVariablesForm(getSQL(false), getSQL(true));
            form.ShowDialog();
        }

        private void buttonDeleteRow_Click(object sender, EventArgs e)
        {
            if (dgvUserFields.SelectedCells.Count > 0)
            {
                List<string> varNames = new List<string>();

                foreach (int i in getSelectedRowsIndexes())
                {
                    string s = dgvUserFields.Rows[i].Cells[0].Value as String;
                    if (s != null)
                        varNames.Add(s);
                }

                if (MessageBox.Show(string.Format("Are you sure to delete following {1} variables:{2}{2}{0}", varNames.Aggregate((current, next) => current + Environment.NewLine + next), varNames.Count, Environment.NewLine), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    foreach (string varName in varNames)
                    {
                        for (int i = 0; i < dgvUserFields.Rows.Count; i++)
                        {
                            if(varName.Equals(dgvUserFields.Rows[i].Cells[0].Value.ToString()))
                            {
                                dgvUserFields.Rows.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
            }
            else
                MessageBox.Show(string.Format("There are no selected cells"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private int[] getSelectedRowsIndexes()
        {
            List<int> selRows = new List<int>();
            foreach (DataGridViewCell selCell in dgvUserFields.SelectedCells)
            {
                selRows.Add(selCell.RowIndex);
            }

            return (from sr in selRows.Distinct().ToArray()
                         orderby sr ascending
                         select sr).ToArray();
        }

        private void dgvUserFields_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            GlobalVariables.IsProjectModified = true;
        }

        private void dgvUserFields_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            GlobalVariables.IsProjectModified = true;
        }

        private void dgvUserFields_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            GlobalVariables.IsProjectModified = true;
        }

        private void dgvUserFields_SelectionChanged(object sender, EventArgs e)
        {
            buttonDeleteRow.Enabled = dgvUserFields.SelectedCells.Count > 0 && !dgvUserFields.Rows[dgvUserFields.CurrentCell.RowIndex].IsNewRow;

            if(getSelectedRowsIndexes().Length > 1)
                buttonDeleteRow.Text = string.Format("Delete Selected {0} Variables", getSelectedRowsIndexes().Length);
            else
                buttonDeleteRow.Text = "Delete Selected Variable";
        }

        private void dgvUserFields_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.ColumnIndex >= 0 && e.RowIndex >= 0)
                dgvUserFields.CurrentCell = dgvUserFields.Rows[e.RowIndex].Cells[e.ColumnIndex];
        }       
    }
}
