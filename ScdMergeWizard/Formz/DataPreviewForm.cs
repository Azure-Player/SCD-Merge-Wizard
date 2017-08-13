using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using ScdMergeWizard.Database;

namespace ScdMergeWizard.Formz
{
    public partial class DataPreviewForm : Form
    {

        private MyBaseDbConnection _conn;
        private string _sql;

        private const int MAX_DATA_ROWS_COUNT = 1000;

        public DataPreviewForm(MyBaseDbConnection conn, string sql)
        {
            InitializeComponent();

            this._conn = conn;
            this._sql = sql;
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.Text = this.Text + " (up to " + MAX_DATA_ROWS_COUNT.ToString() + " records)";
                DbDataAdapter dataAdapter = _conn.CreateAdapter(_sql);
                DbCommandBuilder commandBuilder = _conn.CreateCommandBuilder(dataAdapter);

                dataGridView1.DataError += dataGridView1_DataError;

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(0, MAX_DATA_ROWS_COUNT, table);
                //dataGridView1.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                // you can make it grid readonly.
                dataGridView1.ReadOnly = true;
                // finally bind the data to the grid
                dataGridView1.DataSource = table;
            }
            catch
            { }
        }

        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
