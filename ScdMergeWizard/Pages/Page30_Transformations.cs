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
    public partial class Page30_Transformations : TheMasterPage, IMasterPage
    {
        public Page30_Transformations()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            transformationGridView1.Initialize();
        }

        public bool IsShowable()
        {
            return true;
        }

        public void OnPageEntering()
        {
            GlobalVariables.TargetColumns.Sort(delegate(MyDbColumn c1, MyDbColumn c2) { return c1.ColumnName.CompareTo(c2.ColumnName); });
            //GlobalVariables.ColumnMappings.Clear();
            transformationGridView1.refreshDataSources();
        }

        public PageLeavingEventArgs OnPageLeaving(PageLeavingEventArgs e)
        {
            var result = transformationGridView1.GetMappingResults();

            if (result.Messages.Any(m => m.MessageType == Components.EMappingMessageType.ERROR))
            {
                e.Cancel = true;
                e.PageErrors.Add(new PageError { Control = panel2, ErrorMessage = result.Messages.First(m => m.MessageType == Components.EMappingMessageType.ERROR).MessageText });
            }
            return e;
        }

        private void transformationGridView1_MappingsChanged(Components.MappingsChangedEventArgs e)
        {
            int imgIdx = 0;
            listView1.Items.Clear();
            foreach (var msg in e.Messages)
            {
                switch (msg.MessageType)
                {
                    case Components.EMappingMessageType.INFO: imgIdx = 0; break;
                    case Components.EMappingMessageType.WARNING: imgIdx = 1; break;
                    case Components.EMappingMessageType.ERROR: imgIdx = 2; break;
                };
                listView1.Items.Add(msg.MessageText, imgIdx);
            }
        }

        private void buttonStatistics_Click(object sender, EventArgs e)
        {
            new TransformationStatsForm().ShowDialog();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("All your current mappings will be lost.\r\nAre you sure?", "Reset mappings", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                GlobalVariables.ColumnMappings.Clear();
                transformationGridView1.Initialize();
                GlobalVariables.TargetColumns.Sort(delegate(MyDbColumn c1, MyDbColumn c2) { return c1.ColumnName.CompareTo(c2.ColumnName); });
            }
        }

        private void SCD1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("All your current mappings will be lost and replaced with SCD1 value.\r\nAre you sure?", "Setting SCD1", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                transformationGridView1.makeAll_SCD1();
            }
        }

    }
}
