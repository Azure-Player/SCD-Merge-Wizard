using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScdMergeWizard.Formz
{
    public partial class TransformationStatsForm : Form
    {
        public TransformationStatsForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            var ds = GlobalVariables.ColumnMappings.GroupBy(cm => cm.TransformationCode)
                        .Select(group => new
                        {
                            Image = GlobalVariables.Transformations.Find(t => t.Code == group.Key).Image,
                            Transformation = GlobalVariables.Transformations.Find(t=>t.Code == group.Key).Name,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Transformation);
            dataGridView1.DataSource = ds.ToArray();
            dataGridView1.Refresh();
            dataGridView1.Columns[0].Width = 20;
            dataGridView1.Columns[0].HeaderText = "";
            dataGridView1.Columns[0].Resizable = DataGridViewTriState.False;

            toolStripStatusLabel1.Text = "Disctinct transformations: " + GlobalVariables.ColumnMappings.Select(c => c.TransformationCode).Distinct().Count().ToString();
            toolStripStatusLabel2.Text = "Total transformations: " + GlobalVariables.ColumnMappings.Select(c => c.TransformationCode).Count().ToString();
        }
    }
}
