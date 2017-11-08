using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScdMergeWizard.Database;
using ScdMergeWizard.Formz;

namespace ScdMergeWizard.Pages
{
    public partial class Page11_TargetConnection : TheMasterPage, IMasterPage
    {
        public Page11_TargetConnection()
        {
            InitializeComponent();
        }

        public bool IsShowable()
        {
            return true;
        }

        public void OnPageEntering()
        {
            RefreshCnnButtonLogic();
        }

        public PageLeavingEventArgs OnPageLeaving(PageLeavingEventArgs e)
        {
            if(GlobalVariables.TargetConnection == null)
                GlobalVariables.TargetConnection = DbHelper.CreateConnection(rtbTgtConnStr.Text);

            if (GlobalVariables.TargetConnection == null)
            {
                e.Cancel = true;
                e.PageErrors.Add(new PageError { Control = rtbTgtConnStr, ErrorMessage = "Target connection cannot be established." });
            }

            if (GlobalVariables.TargetConnection != null && !GlobalVariables.TargetConnection.IsConnectionOpened())
            {
                e.Cancel = true;
                e.PageErrors.Add(new PageError { Control = rtbTgtConnStr, ErrorMessage = "Target connection cannot be opened." });
            }

            if (string.IsNullOrEmpty(cbxTgtTableOrViewName.Text))
            {
                e.Cancel = true;
                e.PageErrors.Add(new PageError { Control = cbxTgtTableOrViewName, ErrorMessage = "You must select source table or view" });
            }


            if (!e.Cancel)
            {
                string commandText = "SELECT * FROM " + cbxTgtTableOrViewName.Text;
                var tc = DbHelper.GetColumns(GlobalVariables.TargetConnection, commandText);
                if (tc != null)
                    GlobalVariables.TargetColumns = tc.ToList();

                GlobalVariables.TargetObjectName = cbxTgtTableOrViewName.Text;
            }

            return e;
        }

        private void rtbTgtConnStr_Leave(object sender, EventArgs e)
        {
            GlobalVariables.TargetConnection = DbHelper.CreateConnection(rtbTgtConnStr.Text);
        }

        private void rtbTgtConnStr_TextChanged(object sender, EventArgs e)
        {
            GlobalVariables.IsProjectModified = true;
            if (GlobalVariables.TargetConnection != null && GlobalVariables.TargetConnection.IsConnectionOpened())
                GlobalVariables.TargetConnection.GetConn().Close();
        }

        private void myComboBox1_TextChanged(object sender, EventArgs e)
        {
            GlobalVariables.IsProjectModified = true;
            buttonPreviewData.Enabled = !string.IsNullOrEmpty(cbxTgtTableOrViewName.Text);
        }

        private void myComboBox1_DropDown(object sender, EventArgs e)
        {
            if (GlobalVariables.TargetConnection == null)
                GlobalVariables.TargetConnection = DbHelper.CreateConnection(rtbTgtConnStr.Text);

            cbxTgtTableOrViewName.AddItems(DbHelper.GetTablesViewsAndSynonyms(GlobalVariables.TargetConnection));
        }

        private void buttonPreviewData_Click(object sender, EventArgs e)
        {
            string commandText;

            if (GlobalVariables.TargetConnection == null)
                GlobalVariables.TargetConnection = DbHelper.CreateConnection(rtbTgtConnStr.Text);

            commandText = "SELECT * FROM " + cbxTgtTableOrViewName.Text;

            var form = new DataPreviewForm(GlobalVariables.TargetConnection, commandText);
            form.ShowDialog();
        }

        private void cbxTgtTableOrViewName_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonPreviewData.Enabled = cbxTgtTableOrViewName.SelectedIndex != -1;
            GlobalVariables.IsProjectModified = true;
        }

        private void buttonEditConnectionString_Click(object sender, EventArgs e)
        {
            var res = DbHelper.GetBuiltConnectionString(rtbTgtConnStr.Text);
            if (res != null)
                rtbTgtConnStr.Text = res;
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            var ff = new DatabaseObjectFilterForm();

            ff.ShowTables = this.cbxTgtTableOrViewName.ShowTables;
            ff.ShowViews = this.cbxTgtTableOrViewName.ShowViews;
            ff.ShowSynonyms = this.cbxTgtTableOrViewName.ShowSynonyms;
            ff.FilterText = this.cbxTgtTableOrViewName.FilterText;

            if (ff.ShowDialog() == DialogResult.OK)
            {
                this.cbxTgtTableOrViewName.ShowTables = ff.ShowTables;
                this.cbxTgtTableOrViewName.ShowViews = ff.ShowViews;
                this.cbxTgtTableOrViewName.ShowSynonyms = ff.ShowSynonyms;
                this.cbxTgtTableOrViewName.FilterText = ff.FilterText;
            }

            labelFilter.Text = this.cbxTgtTableOrViewName.GetFilterText();
        }

        private void tsmiEditOld_Click(object sender, EventArgs e)
        {
            tsmiEditOld.Checked = true;
            tsmiEditNew.Checked = false;
            btnEditCnn_ButtonClick(sender, e);
        }

        private void tsmiEditNew_Click(object sender, EventArgs e)
        {
            tsmiEditNew.Checked = true;
            tsmiEditOld.Checked = false;
            btnEditCnn_ButtonClick(sender, e);
        }

        private void buttonEditNewConnectionString_Click(object sender, EventArgs e)
        {
            ConnectDbForm ff = new ConnectDbForm(rtbTgtConnStr.Text);
            ff.ShowDialog();
            if (ff.DialogResult == DialogResult.OK)
            {
                rtbTgtConnStr.Text = ff.ConnectionString;
                GlobalVariables.TargetConnection = new Database.MyAdoDbConnection(ff.Connection);
            }
        }

        private void btnEditCnn_ButtonClick(object sender, EventArgs e)
        {
            if (tsmiEditOld.Checked)
            {
                buttonEditConnectionString_Click(sender, e);
            }
            else
            {
                buttonEditNewConnectionString_Click(sender, e);
            }
        }

        private void lilCopyFromSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rtbTgtConnStr.Text = String.Empty;
            rtbTgtConnStr.Text = GlobalVariables.SourceConnection.GetConnectionString();
            GlobalVariables.TargetConnection = GlobalVariables.SourceConnection.Clone();
            RefreshCnnButtonLogic();
        }

        private void RefreshCnnButtonLogic()
        {
            tsmiEditNew.Checked = GlobalVariables.TargetConnection is MyAdoDbConnection;
            tsmiEditOld.Checked = GlobalVariables.TargetConnection is MyOleDbConnection;
        }

    }
}
