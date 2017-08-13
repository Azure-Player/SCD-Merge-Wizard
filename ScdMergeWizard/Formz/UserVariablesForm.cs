using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ScdMergeWizard.Formz
{
    public partial class UserVariablesForm : Form
    {
        private string _sqlToShow;
        private string _sqlToExec;

        public UserVariablesForm(string sqlToShow, string sqlToExec)
        {
            InitializeComponent();

            this._sqlToShow = sqlToShow;
            this._sqlToExec = sqlToExec;
        }

        protected override void OnLoad(EventArgs e)
        {
            myRichTextBox1.RichTextBox.Text = _sqlToShow;
            populateResultGrid();

            toolStripStatusLabel1.Text = dataGridView1.Rows.Count.ToString() + " user variables defined";
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            splitContainer1.SplitterDistance = this.Width / 2;
        }

        private void populateResultGrid()
        {
            try
            {
                dataGridView1.Rows.Clear();

                OleDbCommand cmd = GlobalVariables.SourceConnection.GetConn().CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = _sqlToExec;

                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string s = rdr[1].ToString();
                    dataGridView1.Rows.Add(rdr[0].ToString(), rdr[1].ToString());
                }
                rdr.Close();
            }
            catch { }
        }
    }
}
