using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using ScdMergeWizard.Database;

namespace ScdMergeWizard.Formz
{
    public partial class DatabaseObjectFilterForm : Form
    {
        public DatabaseObjectFilterForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {

        }

        public bool ShowTables
        {
            get { return checkBoxTables.Checked; }
            set { checkBoxTables.Checked = value; }
        }

        public bool ShowViews
        {
            get
            {
                return checkBoxViews.Checked;
            }
            set { checkBoxViews.Checked = value; }
        }

        public bool ShowSynonyms
        {
            get
            {
                return checkBoxSynonyms.Checked;
            }
            set { checkBoxSynonyms.Checked = value; }
        }

        public string FilterText
        { get { return textBoxFilter.Text; } set { textBoxFilter.Text = value; } }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ShowTables = ShowViews = ShowSynonyms = true;
            FilterText = string.Empty;
        }
    }
}
