using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ScdMergeWizard.Formz
{
    public partial class ConnectDbForm : Form
    {
        SqlConnection _cnn = null;
        string _ConnectionString = "";

        public ConnectDbForm(string connectionString)
        {
            InitializeComponent();
            txtLoginWindows.Text = Environment.UserDomainName + @"\" + Environment.UserName;
            cmbAuthentication.SelectedIndex = 0;    //Windows Auth
            txtServerName.Text = "localhost";
            comboBox1.SelectedIndex = 0;
            RestoreSettings(connectionString);
        }

        private void RestoreSettings(string connectionString)
        {
            List<string> parts = new List<string>();
            parts.AddRange(connectionString.Split(';'));
            foreach (string p in parts.Where(n => n.StartsWith("Data Source=")))
                txtServerName.Text = p.Split('=')[1];
            foreach (string p in parts.Where(n => n.StartsWith("Initial Catalog=")))
                cmbDatabase.Text = p.Split('=')[1];
            if (parts.FindIndex(n => n.Contains("Integrated Security=False")) < 0)
                cmbAuthentication.SelectedIndex = 0;
            else
            {
                cmbAuthentication.SelectedIndex = 1;
                foreach (string p in parts.Where(n => n.StartsWith("User ID=")))
                    txtLoginSQL.Text = p.Split('=')[1];
                foreach (string p in parts.Where(n => n.StartsWith("Password=")))
                    txtPassword.Text = p.Split('=')[1];
            }
        }

        private void cmbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isSqlMode = cmbAuthentication.SelectedIndex == 1;
            lblLogin.Enabled = isSqlMode;
            lblPassword.Enabled = isSqlMode;
            if (!isSqlMode) chkRememberPwd.Checked = false;
            chkRememberPwd.Enabled = isSqlMode;
            txtLoginSQL.Enabled = true;
            txtLoginSQL.Visible = isSqlMode;
            txtLoginWindows.Visible = !isSqlMode;
            txtPassword.Enabled = isSqlMode;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
            cb.UserID = txtLoginSQL.Text;
            cb.IntegratedSecurity = cmbAuthentication.SelectedIndex == 0;
            cb.Password = txtPassword.Text;
            cb.DataSource = txtServerName.Text;
            cb.InitialCatalog = cmbDatabase.Text;
            try
            {
                _ConnectionString = cb.ConnectionString;
                _cnn = new SqlConnection(cb.ConnectionString);
                _cnn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public SqlConnection Connection
        {
            get { return _cnn; } 
        }

        public string ConnectionString
        {
            get { return _ConnectionString; }
        }

        private void txtServerName_TextChanged(object sender, EventArgs e)
        {
            cmbDatabase.Items.Clear();
        }

        private void cmbDatabase_DropDown(object sender, EventArgs e)
        {
            DataTable t = new DataTable();
            btnConnect_Click(sender, e);
            if (_cnn.State == ConnectionState.Open && cmbDatabase.Items.Count == 0)
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [name] FROM sys.databases ORDER BY [name];", _cnn);
                da.Fill(t);
                foreach (DataRow row in t.Rows)
                {
                    cmbDatabase.Items.Add(row[0].ToString());
                }
            }
        }

        private void ConnectDbForm_Load(object sender, EventArgs e)
        {

        }

        private void chkRememberPwd_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
