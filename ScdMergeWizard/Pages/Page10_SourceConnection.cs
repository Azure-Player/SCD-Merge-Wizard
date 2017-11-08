using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScdMergeWizard.Components;
using ScdMergeWizard.Database;
using ScdMergeWizard.Formz;

namespace ScdMergeWizard.Pages
{
    public partial class Page10_SourceConnection : TheMasterPage, IMasterPage
    {
        public Page10_SourceConnection()
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
            if (GlobalVariables.SourceConnection == null)
                GlobalVariables.SourceConnection = DbHelper.CreateConnection(rtbSrcConnStr.Text);

            if (GlobalVariables.SourceConnection == null)
            {
                e.Cancel = true;
                e.PageErrors.Add(new PageError { Control = rtbSrcConnStr, ErrorMessage = "Source connection cannot be established" });
            }
            if (GlobalVariables.SourceConnection != null && !GlobalVariables.SourceConnection.IsConnectionOpened())
            {
                e.Cancel = true;
                e.PageErrors.Add(new PageError { Control = rtbSrcConnStr, ErrorMessage = "Source connection cannot be opened" });
            }

            if (rbIsTableOrView.Checked && cbxSrcTableOrViewName.SelectedIndex < 0)
            {
                e.Cancel = true;
                e.PageErrors.Add(new PageError { Control = cbxSrcTableOrViewName, ErrorMessage = "Select source table or view" });
            }
            if (rbIsCommandText.Checked && string.IsNullOrEmpty(rtbCommandText.Text))
            {
                e.Cancel = true;
                e.PageErrors.Add(new PageError { Control = rtbCommandText, ErrorMessage = "Enter source command text" });
            }

            if (!e.Cancel)
            {
                string commandText;
                if (rbIsTableOrView.Checked)
                    commandText = "SELECT * FROM " + cbxSrcTableOrViewName.Text;
                else
                    commandText = rtbCommandText.Text;


                var sc = DbHelper.GetColumns(GlobalVariables.SourceConnection, commandText);
                if (sc != null)
                    GlobalVariables.SourceColumns = sc.ToList();

                GlobalVariables.SourceObjectName = cbxSrcTableOrViewName.Text;
                GlobalVariables.SourceIsTableOrViewMode = rbIsTableOrView.Checked;
                GlobalVariables.SourceCommandText = rtbCommandText.Text;
            }
            return e;
        }

        private void rtbSrcConnStr_TextChanged(object sender, EventArgs e)
        {
            GlobalVariables.IsProjectModified = true;
            if (GlobalVariables.SourceConnection != null && GlobalVariables.SourceConnection.IsConnectionOpened())
                GlobalVariables.SourceConnection.GetConn().Close();
        }

        private void rbTableOrView_CheckedChanged(object sender, EventArgs e)
        {
            cbxSrcTableOrViewName.Enabled = rbIsTableOrView.Checked;
            GlobalVariables.IsProjectModified = true;
        }

        private void cbxSrcTables_TextChanged(object sender, EventArgs e)
        {
            GlobalVariables.IsProjectModified = true;
            buttonPreviewData.Enabled = !string.IsNullOrEmpty(cbxSrcTableOrViewName.Text);
        }

        private void rbCommandText_CheckedChanged(object sender, EventArgs e)
        {
            rtbCommandText.Enabled = rbIsCommandText.Checked;
            GlobalVariables.IsProjectModified = true;
        }

        private void rtbCommandText_TextChanged(object sender, EventArgs e)
        {
            GlobalVariables.IsProjectModified = true;
        }

        private void cbxSrcTables_DropDown(object sender, EventArgs e)
        {
            if (GlobalVariables.SourceConnection == null)
                GlobalVariables.SourceConnection = DbHelper.CreateConnection(rtbSrcConnStr.Text);

            cbxSrcTableOrViewName.AddItems(DbHelper.GetTablesViewsAndSynonyms(GlobalVariables.SourceConnection));
        }

        private void rtbSrcConnStr_Leave(object sender, EventArgs e)
        {
            GlobalVariables.SourceConnection = DbHelper.CreateConnection(rtbSrcConnStr.Text);
        }

        private void buttonPreviewData_Click(object sender, EventArgs e)
        {
            string commandText;

            if (GlobalVariables.SourceConnection == null)
                GlobalVariables.SourceConnection = DbHelper.CreateConnection(rtbSrcConnStr.Text);

            if (rbIsTableOrView.Checked)
                    commandText = "SELECT * FROM " + cbxSrcTableOrViewName.Text;
                else
                    commandText = rtbCommandText.Text;

            var form = new DataPreviewForm(GlobalVariables.SourceConnection, commandText);
            form.ShowDialog();
        }

        private void cbxSrcTableOrViewName_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonPreviewData.Enabled = cbxSrcTableOrViewName.SelectedIndex != -1;
            GlobalVariables.IsProjectModified = true;
        }

        private void buttonEditConnectionString_Click(object sender, EventArgs e)
        {
            var res = DbHelper.GetBuiltConnectionString(rtbSrcConnStr.Text);
            if (res != null)
            {
                if (res != rtbSrcConnStr.Text)
                    GlobalVariables.SourceConnection = null;
                rtbSrcConnStr.Text = res;
            }
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            var ff = new DatabaseObjectFilterForm();

            ff.ShowTables = this.cbxSrcTableOrViewName.ShowTables;
            ff.ShowViews = this.cbxSrcTableOrViewName.ShowViews;
            ff.ShowSynonyms = this.cbxSrcTableOrViewName.ShowSynonyms;
            ff.FilterText = this.cbxSrcTableOrViewName.FilterText;

            if (ff.ShowDialog() == DialogResult.OK)
            {
                this.cbxSrcTableOrViewName.ShowTables = ff.ShowTables;
                this.cbxSrcTableOrViewName.ShowViews = ff.ShowViews;
                this.cbxSrcTableOrViewName.ShowSynonyms = ff.ShowSynonyms;
                this.cbxSrcTableOrViewName.FilterText = ff.FilterText;
            }

            labelFilter.Text = this.cbxSrcTableOrViewName.GetFilterText();
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
            ConnectDbForm ff = new ConnectDbForm(rtbSrcConnStr.Text);
            ff.ShowDialog();
            if (ff.DialogResult == DialogResult.OK)
            {
                rtbSrcConnStr.Text = ff.ConnectionString;
                GlobalVariables.SourceConnection = new Database.MyAdoDbConnection(ff.Connection);
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

        private void RefreshCnnButtonLogic()
        {
            tsmiEditNew.Checked = GlobalVariables.SourceConnection is MyAdoDbConnection;
            tsmiEditOld.Checked = GlobalVariables.SourceConnection is MyOleDbConnection;
        }

    }
}
