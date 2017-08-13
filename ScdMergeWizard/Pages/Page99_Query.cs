using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ScdMergeWizard.ExcHandling;

namespace ScdMergeWizard.Pages
{
    public partial class Page99_Query : TheMasterPage, IMasterPage
    {
        public Page99_Query()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public bool IsShowable()
        {
            return true;
        }

        public void OnPageEntering()
        {
            myRichTextBox1.RichTextBox.Text = MyQueryBuilder.GetQuery();
        }

        public PageLeavingEventArgs OnPageLeaving(PageLeavingEventArgs e)
        {
            return e;
        }

        private void buttonOpenInSSMS_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = Path.GetTempPath() + "SCD Merge Wizard";
                if (!Directory.Exists(fileName))
                    Directory.CreateDirectory(fileName);

                fileName = Path.GetTempPath() + "SCD Merge Wizard\\ScdMergeWizard_" + Guid.NewGuid() + ".sql";
                File.WriteAllLines(fileName, myRichTextBox1.RichTextBox.Lines);

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "ssms.exe";
                startInfo.Arguments = "-e \"" + fileName + "\"";
                process.StartInfo = startInfo;
                process.Start();
            }
            catch (Exception ex)
            {
                MyExceptionHandler.NewEx(ex);
            }
        }

        private void buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(myRichTextBox1.RichTextBox.Text))
                Clipboard.SetText(myRichTextBox1.RichTextBox.Text);
        }

        private void buttonSaveQuery_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "sql";
            sfd.Filter = "SQL Files|*.sql";
            sfd.RestoreDirectory = true;
            sfd.Title = "Save SCD Merge Query";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    File.WriteAllLines(sfd.FileName, myRichTextBox1.RichTextBox.Lines);
                    MessageBox.Show("Query saved successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MyExceptionHandler.NewEx(ex);
                }
            }
        }

       
    }
}

