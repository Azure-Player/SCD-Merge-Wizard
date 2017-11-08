using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using ScdMergeWizard.Pages;
using ScdMergeWizard.Database;
using ScdMergeWizard.Components;
using ScdMergeWizard.EditMenu;
using System.Data.OleDb;
using System.Xml;
using ScdMergeWizard.Recent;
using ScdMergeWizard.ExcHandling;
using System.IO;
using ScdMergeWizard;
using ScdMergeWizard.Formz;

namespace ScdMergeWizard
{
    public partial class MainForm : Form
    {

        //public delegate void FileNameChangedEventHandler();
        //public static event FileNameChangedEventHandler FileNameChanged;

        private string _fileName;
        private string FileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    setTitleText();
                    /*
                    if (FileNameChanged != null)
                        FileNameChanged();
                     * */
                }
            }
        }

        private Page10_SourceConnection pageSourceConnection = null;
        private Page11_TargetConnection pageTargetConnection = null;
        private Page50_Options pageOptions = null;

        public MainForm()
        {
            InitializeComponent();

            pageSourceConnection = new Page10_SourceConnection();
            pageTargetConnection = new Page11_TargetConnection();
            pageOptions = new Page50_Options();

            thePageNavigator1.AddPage(new Page01_Welcome());
            thePageNavigator1.AddPage(new Page02_Agreement());
            thePageNavigator1.AddPage(pageSourceConnection);
            thePageNavigator1.AddPage(pageTargetConnection);
            // History table connection, when implemented
            thePageNavigator1.AddPage(new Page20_UserVariables());
            thePageNavigator1.AddPage(new Page30_Transformations());
            thePageNavigator1.AddPage(new Page80_Audit());
            thePageNavigator1.AddPage(new Page40_CheckSourceDataBusinessKeys());
            thePageNavigator1.AddPage(pageOptions);
            thePageNavigator1.AddPage(new Page99_Query());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            setTitleText();

            GlobalVariables.ProjectModified += new GlobalVariables.ProjectModifiedEventHandler(GlobalVariables_ProjectModified);

            EditMenuManager emm = new EditMenuManager();
            emm.ConnectMenus(editToolStripMenuItem);

            loadRecentFiles();

            GlobalVariables.IsProjectModified = false;
        }

        private void setTitleText()
        {
            this.Text = "SCD Merge Wizard " + Assembly.GetEntryAssembly().GetName().Version;
            if (!string.IsNullOrEmpty(FileName))
                this.Text += " [" + FileName + "]";
            if (GlobalVariables.IsProjectModified)
                this.Text += "*";
        }

        void GlobalVariables_ProjectModified()
        {
            setTitleText();
        }

        private void thePageNavigator1_CancelRequested()
        {
            this.Close();
        }


        #region Save / Load
        private void save(bool isSaveAs)
        {
            if (string.IsNullOrEmpty(FileName) || isSaveAs)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = "mwpxml";
                sfd.Filter = "SCD Merge Wizard Project Files|*.mwpxml";
                sfd.RestoreDirectory = true;
                sfd.Title = "Save Project";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileName = sfd.FileName;
                }
                else
                    return;

                if (string.IsNullOrEmpty(FileName))
                {
                    return;
                }
            }

            using (XmlWriter writer = XmlWriter.Create(FileName))
            {
                writer.WriteStartElement("ScdMergeWizard");

                writer.WriteStartElement("SourceConnection");
                writer.WriteElementString("ConnectionString", pageSourceConnection.rtbSrcConnStr.Text);
                writer.WriteElementString("IsTableOrView", pageSourceConnection.rbIsTableOrView.Checked.ToString());
                writer.WriteElementString("TableOrViewName", pageSourceConnection.cbxSrcTableOrViewName.Text);
                writer.WriteElementString("CommandText", "<![CDATA[" + pageSourceConnection.rtbCommandText.Text + "]]>");
                writer.WriteEndElement();

                writer.WriteStartElement("TargetConnection");
                writer.WriteElementString("ConnectionString", pageTargetConnection.rtbTgtConnStr.Text);
                writer.WriteElementString("TableOrViewName", pageTargetConnection.cbxTgtTableOrViewName.Text);
                writer.WriteEndElement();


                writer.WriteStartElement("UserVariables");
                for (int i = 0; i < GlobalVariables.UserColumnsDefinitions.Count; i++)
                {
                    MyUserVariable uv = GlobalVariables.UserColumnsDefinitions[i];

                    writer.WriteStartElement("UserVariable_" + i.ToString());

                    writer.WriteElementString("Name", uv.Name);
                    writer.WriteElementString("DataType", uv.DataType);
                    writer.WriteElementString("Value", "<![CDATA[" + uv.Definition + "]]>");

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();


                writer.WriteStartElement("Options");

                writer.WriteElementString("RecordsOnTargetNotFoundOnSourceMode", GlobalVariables.Options.RecordsOnTargetNotFoundOnSourceMode.ToString());

                writer.WriteElementString("IgnoreDatabasePrefix", GlobalVariables.Options.IgnoreDatabasePrefix.ToString());
                writer.WriteElementString("ShowExtendedComments", GlobalVariables.Options.ShowExtendedComments.ToString());
                writer.WriteElementString("SCD2VersionNumberMode", GlobalVariables.Options.SCD2VersionNumberMode.ToString());
                writer.WriteElementString("SCD13UpdateMode", GlobalVariables.Options.SCD13UpdateMode.ToString());
                writer.WriteElementString("ComparisonMethod", GlobalVariables.Options.ComparisonMethod.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("ColumnMappings");

                for (int i = 0; i < GlobalVariables.ColumnMappings.Count; i++)
                {
                    ColumnMapping cm = GlobalVariables.ColumnMappings[i];

                    writer.WriteStartElement("ColumnMapping_" + i.ToString());

                    writer.WriteElementString("SourceColumn", cm.SourceColumn);
                    writer.WriteElementString("TransformationType", cm.TransformationCode.ToString());
                    writer.WriteElementString("TargetDatabaseType", cm.TargetDatabaseType.ToString());
                    writer.WriteElementString("TargetColumn", cm.TargetColumn);
                    //writer.WriteElementString("Value1Column", cm.Value1Column);
                    //writer.WriteElementString("Value2Column", cm.Value2Column);
                    writer.WriteElementString("CustomInsertValue", cm.CustomInsertValue);
                    writer.WriteElementString("CustomUpdateValue", cm.CustomUpdateValue);
                    writer.WriteElementString("CustomDeleteValue", cm.CustomDeleteValue);

                    writer.WriteElementString("ColumnCompareMethod", cm.ColumnCompareMethod.ToString());

                    writer.WriteEndElement();
                }

                writer.WriteEndElement(); // Mappings

                writer.WriteEndElement(); // MERGER
            }

            RecentFilesManager.Add(FileName);
            GlobalVariables.IsProjectModified = false;
        }

        private bool saveCurrentProjectIfModified()
        {
            if (GlobalVariables.IsProjectModified)
            {
                DialogResult dr = MessageBox.Show("Project you are currently working on has been changed. Would you like to save it before continuing?", "Save project?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    save(false);
                }
                else if (dr == System.Windows.Forms.DialogResult.No)
                    return true;
                else
                    return false;
            }
            return true;
        }

        private void openProject()
        {
            if (!saveCurrentProjectIfModified())
                return;

            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.DefaultExt = "xml";
                ofd.Filter = "SCD Merge Wizard Project Files|*.mwpxml";
                ofd.RestoreDirectory = true;
                ofd.Title = "Open Project";

                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ReadFile(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MyExceptionHandler.NewEx(ex);
            }
        }

        public void ReadFile(string fileName)
        {
            XmlNode node;
            string src, tgt, val;
            StringBuilder sbLoadingErrors = new StringBuilder();

            if (!File.Exists(fileName))
            {
                MessageBox.Show("File does not exists!");
                return;
            }

            if (!thePageNavigator1.GotoBeginning())
                return;

            GlobalVariables.LoadedColumnMappings.Clear();
            GlobalVariables.LoadedUserColumnsDefinitions.Clear();
            src = tgt = val = string.Empty;
            //trfType = ETransformationType.SKIP;

            pageSourceConnection.rtbSrcConnStr.Text = string.Empty;
            pageSourceConnection.cbxSrcTableOrViewName.Text = string.Empty;
            pageTargetConnection.rtbTgtConnStr.Text = string.Empty;
            pageTargetConnection.cbxTgtTableOrViewName.Text = string.Empty;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);

                // Source Connection
                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/SourceConnection/ConnectionString");
                if (node == null)
                    node = doc.DocumentElement.SelectSingleNode("/MERGER/SourceConnection/ConnectionString");
                if (node != null)
                    pageSourceConnection.rtbSrcConnStr.Text = node.InnerText;
                else
                    sbLoadingErrors.AppendLine("Cannot read Source ConnectionString");


                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/SourceConnection/IsTableOrView");
                if (node == null)
                    node = doc.DocumentElement.SelectSingleNode("/MERGER/SourceConnection/IsTableOrView");
                if (node != null)
                {
                    pageSourceConnection.rbIsTableOrView.Checked = (node.InnerText == "True");
                    pageSourceConnection.rbIsCommandText.Checked = (node.InnerText != "True");
                }
                else
                    sbLoadingErrors.AppendLine("Cannot read IsTableOrView");

                if (pageSourceConnection.rbIsTableOrView.Checked)
                {
                    GlobalVariables.SourceConnection = DbHelper.CreateConnection(pageSourceConnection.rtbSrcConnStr.Text);
                    pageSourceConnection.cbxSrcTableOrViewName.AddItems(DbHelper.GetTablesViewsAndSynonyms(GlobalVariables.SourceConnection));
                }


                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/SourceConnection/TableOrViewName");
                if (node == null)
                    node = doc.DocumentElement.SelectSingleNode("/MERGER/SourceConnection/TableOrView");
                if (node != null)
                    pageSourceConnection.cbxSrcTableOrViewName.Text = node.InnerText;
                else
                    sbLoadingErrors.AppendLine("Cannot read Source TableOrViewName");


                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/SourceConnection/CommandText");
                if (node == null)
                    node = doc.DocumentElement.SelectSingleNode("/MERGER/SourceConnection/CommandText");
                if (node != null)
                    pageSourceConnection.rtbCommandText.Text = node.InnerText.Replace("<![CDATA[", "").Replace("]]>", "");
                else
                    sbLoadingErrors.AppendLine("Cannot read CommandText");


                // Target Connection
                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/TargetConnection/ConnectionString");
                if (node == null)
                    node = doc.DocumentElement.SelectSingleNode("/MERGER/TargetConnection/ConnectionString");
                if (node != null)
                    pageTargetConnection.rtbTgtConnStr.Text = node.InnerText;
                else
                    sbLoadingErrors.AppendLine("Cannot read Target ConnectionString");


                GlobalVariables.TargetConnection = DbHelper.CreateConnection(pageTargetConnection.rtbTgtConnStr.Text);
                pageTargetConnection.cbxTgtTableOrViewName.AddItems(DbHelper.GetTablesViewsAndSynonyms(GlobalVariables.TargetConnection));


                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/TargetConnection/TableOrViewName");
                if (node == null)
                    node = doc.DocumentElement.SelectSingleNode("/MERGER/TargetConnection/TableOrView");
                if (node != null)
                    pageTargetConnection.cbxTgtTableOrViewName.Text = node.InnerText;
                else
                    sbLoadingErrors.AppendLine("Cannot read TableOrViewName");


                // User Variables
                foreach (XmlNode n in doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/UserVariables").ChildNodes)
                {
                    MyUserVariable uv = new MyUserVariable();

                    node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/UserVariables/{0}/Name", n.Name));
                    uv.Name = node.InnerText;

                    node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/UserVariables/{0}/DataType", n.Name));
                    uv.DataType = node.InnerText;

                    node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/UserVariables/{0}/Value", n.Name));
                    uv.Definition = node.InnerText;

                    GlobalVariables.LoadedUserColumnsDefinitions.Add(uv);
                }


                // Options
                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/Options/RecordsOnTargetNotFoundOnSourceMode");
                if (node != null)
                {
                    GlobalVariables.Options.RecordsOnTargetNotFoundOnSourceMode = (ERecordsOnTargetNotFoundOnSourceMode)Enum.Parse(typeof(ERecordsOnTargetNotFoundOnSourceMode), node.InnerText);
                }
                else
                {
                    node = doc.DocumentElement.SelectSingleNode("/MERGER/Options/RecordsOnTargetNotFoundOnSourceEx");
                    if (node != null)
                    {
                        if (node.InnerText.Equals("UpdateDateTo"))
                            GlobalVariables.Options.RecordsOnTargetNotFoundOnSourceMode = ERecordsOnTargetNotFoundOnSourceMode.UpdateTargetField;
                        else if (node.InnerText.Equals("PhysicallyDelete"))
                            GlobalVariables.Options.RecordsOnTargetNotFoundOnSourceMode = ERecordsOnTargetNotFoundOnSourceMode.PhysicallyDelete;
                        else if (node.InnerText.Equals("DoNothing"))
                            GlobalVariables.Options.RecordsOnTargetNotFoundOnSourceMode = ERecordsOnTargetNotFoundOnSourceMode.DoNothing;
                    }
                    else
                        sbLoadingErrors.AppendLine("Cannot read RecordsOnTargetNotFoundOnSourceMode");
                }

                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/Options/IgnoreDatabasePrefix");
                if (node == null)
                    node = doc.DocumentElement.SelectSingleNode("/MERGER/Options/IgnoreDatabasePrefix");
                if (node != null)
                    GlobalVariables.Options.IgnoreDatabasePrefix = (node.InnerText == "True");
                else
                    sbLoadingErrors.AppendLine("Cannot read IgnoreDatabasePrefix");


                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/Options/ShowExtendedComments");
                if (node == null)
                    node = doc.DocumentElement.SelectSingleNode("/MERGER/Options/ShowExtendedComments");
                if (node != null)
                    GlobalVariables.Options.ShowExtendedComments = (node.InnerText == "True");
                else
                    sbLoadingErrors.AppendLine("Cannot read ShowExtendedComments");

                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/Options/SCD2VersionNumberMode");
                if (node != null)
                {
                    GlobalVariables.Options.SCD2VersionNumberMode = (ESCD2VersionNumberMode)Enum.Parse(typeof(ESCD2VersionNumberMode), node.InnerText);
                }
                else
                    sbLoadingErrors.AppendLine("Cannot read VersionNumberResetOnSCD2");

                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/Options/SCD13UpdateMode");
                if (node != null)
                {
                    GlobalVariables.Options.SCD13UpdateMode = (ESCD13UpdateMode)Enum.Parse(typeof(ESCD13UpdateMode), node.InnerText);
                }
                else
                    sbLoadingErrors.AppendLine("Cannot read SCD13UpdateMode");

                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/Options/ComparisonMethod");
                if (node != null)
                {
                    GlobalVariables.Options.ComparisonMethod = (EComparisonMethod)Enum.Parse(typeof(EComparisonMethod), node.InnerText);
                }
                else
                    sbLoadingErrors.AppendLine("Cannot read ComparisonMethod");

                // Column Mappings
                node = doc.DocumentElement.SelectSingleNode("/ScdMergeWizard/ColumnMappings");
                if (node != null)
                {
                    foreach (XmlNode n in node.ChildNodes)
                    {
                        node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/ColumnMappings/{0}/SourceColumn", n.Name));
                        var srcc = node.InnerText;

                        node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/ColumnMappings/{0}/TransformationType", n.Name));
                        var trfc = (ETransformationCode)Enum.Parse(typeof(ETransformationCode), node.InnerText);

                        ETargetDatabaseType tgtdbt;
                        node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/ColumnMappings/{0}/TargetDatabaseType", n.Name));
                        if (node != null)
                            tgtdbt = (ETargetDatabaseType)Enum.Parse(typeof(ETargetDatabaseType), node.InnerText);
                        else
                            tgtdbt = ETargetDatabaseType.TARGET;

                        node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/ColumnMappings/{0}/TargetColumn", n.Name));
                        var tgtc = node.InnerText;

                        string val1c = string.Empty, val2c = string.Empty, cins = string.Empty, cupd = string.Empty, cdel = string.Empty;
                        // Old version
                        node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/ColumnMappings/{0}/Value1Column", n.Name));
                        if (node != null)
                            val1c = node.InnerText;

                        node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/ColumnMappings/{0}/Value2Column", n.Name));
                        if (node != null)
                            val2c = node.InnerText;

                        // New version

                        node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/ColumnMappings/{0}/CustomInsertValue", n.Name));
                        if (node != null)
                            cins = node.InnerText;

                        node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/ColumnMappings/{0}/CustomUpdateValue", n.Name));
                        if (node != null)
                            cupd = node.InnerText;

                        node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/ColumnMappings/{0}/CustomDeleteValue", n.Name));
                        if (node != null)
                            cdel = node.InnerText;

                        EColumnCompareMethod ccm;
                        node = doc.DocumentElement.SelectSingleNode(string.Format("/ScdMergeWizard/ColumnMappings/{0}/ColumnCompareMethod", n.Name));
                        if (node != null)
                            ccm = (EColumnCompareMethod)Enum.Parse(typeof(EColumnCompareMethod), node.InnerText);
                        else
                            ccm = EColumnCompareMethod.Default;


                        if (string.IsNullOrEmpty(cins) && !string.IsNullOrEmpty(val1c) && (trfc == ETransformationCode.SCD2_IS_ACTIVE || trfc == ETransformationCode.SCD3_PREVIOUS_VALUE || trfc == ETransformationCode.CREATED_DATE))
                            cins = val1c;
                        if (string.IsNullOrEmpty(cupd) && !string.IsNullOrEmpty(val1c) && (trfc == ETransformationCode.SCD2_DATE_FROM || trfc == ETransformationCode.SCD2_DATE_TO || trfc == ETransformationCode.SCD3_DATE_FROM || trfc == ETransformationCode.MODIFIED_DATE))
                            cupd = val1c;
                        if (string.IsNullOrEmpty(cdel) && !string.IsNullOrEmpty(val1c) && (trfc == ETransformationCode.DELETED_DATE || trfc == ETransformationCode.IS_DELETED))
                            cdel = val1c;

                        if (string.IsNullOrEmpty(cins) && !string.IsNullOrEmpty(val2c) && (trfc == ETransformationCode.SCD2_DATE_FROM || trfc == ETransformationCode.SCD2_DATE_TO || trfc == ETransformationCode.SCD3_DATE_FROM || trfc == ETransformationCode.MODIFIED_DATE || trfc == ETransformationCode.DELETED_DATE || trfc == ETransformationCode.IS_DELETED))
                            cins = val2c;
                        if (string.IsNullOrEmpty(cupd) && !string.IsNullOrEmpty(val2c) && (trfc == ETransformationCode.SCD2_IS_ACTIVE))
                            cupd = val2c;

                        GlobalVariables.LoadedColumnMappings.Add(new ColumnMapping(srcc, tgtdbt, tgtc, trfc, cins, cupd, cdel, ccm));
                    }
                }
                else
                {
                    node = doc.DocumentElement.SelectSingleNode("/MERGER/Mappings");
                    if (node != null)
                    {
                        foreach (XmlNode n in node.ChildNodes)
                        {
                            node = doc.DocumentElement.SelectSingleNode(string.Format("/MERGER/Mappings/{0}/Source", n.Name));
                            var srcc = node.InnerText;

                            node = doc.DocumentElement.SelectSingleNode(string.Format("/MERGER/Mappings/{0}/Transformation", n.Name));

                            ETransformationCode tt = ETransformationCode.SKIP;
                            try
                            {
                                tt = (ETransformationCode)Enum.Parse(typeof(ETransformationCode), node.InnerText);
                            }
                            catch { }

                            var trfc = tt;

                            node = doc.DocumentElement.SelectSingleNode(string.Format("/MERGER/Mappings/{0}/Target", n.Name));
                            var tgtc = node.InnerText;

                            GlobalVariables.LoadedColumnMappings.Add(new ColumnMapping(srcc, ETargetDatabaseType.TARGET, tgtc, trfc, string.Empty, string.Empty, string.Empty, EColumnCompareMethod.Default));
                        }
                    }
                    else
                        sbLoadingErrors.AppendLine("Cannot load ColumnMappings");
                }

                RecentFilesManager.Add(fileName);
                loadRecentFiles();
                GlobalVariables.IsProjectModified = false;
            }
            catch (Exception ex)
            {
                MyExceptionHandler.NewEx(ex);
            }

            FileName = fileName;
        }
        #endregion

        #region Recent Files
        void loadRecentFiles()
        {
            recentFilesToolStripMenuItem.DropDownItems.Clear();
            foreach (string file in RecentFilesManager.Read())
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem();
                tsmi.Text = file;
                tsmi.Click += new EventHandler(tsmi_Click);
                recentFilesToolStripMenuItem.DropDownItems.Add(tsmi);
            }
            recentFilesToolStripMenuItem.DropDownItems.Add(toolStripSeparatorRecentFiles);
            recentFilesToolStripMenuItem.DropDownItems.Add(clearRecentFilesHistoryToolStripMenuItem);
        }

        void tsmi_Click(object sender, EventArgs e)
        {
            if (saveCurrentProjectIfModified())
                ReadFile(((ToolStripMenuItem)sender).Text);
        }

        private void clearRecentFilesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete Recent Files History", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                RecentFilesManager.Clear();
                loadRecentFiles();
            }
        }
        #endregion Recent Files

        #region Drag/Drop Events
        private void MainForm1_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the Dataformat of the data can be accepted
            // (we only accept file drops from Explorer, etc.)
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
            {
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
                return;
            }


            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (fileList.Length != 1)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            if (!fileList[0].EndsWith(".mwpxml"))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
        }



        private void MainForm1_DragDrop(object sender, DragEventArgs e)
        {
            // Extract the data from the DataObject-Container into a string list
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (saveCurrentProjectIfModified())
            {
                ReadFile(FileList[0]);
            }
        }

        #endregion Drag/Drop Events

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save(false);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProject();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save(true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saveCurrentProjectIfModified())
                e.Cancel = true;
        }

        private void viewOnlineHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/SQLPlayer/SCD-Merge-Wizard/wiki/");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                this.Hide();
                new ExitForm().ShowDialog();
            }
            else
            {
                //...
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }

        private void thePageNavigator1_CurrentPageChanged(int CurrentPageIndex)
        {
            saveToolStripMenuItem.Enabled =(CurrentPageIndex > 5);
            saveAsToolStripMenuItem.Enabled = (CurrentPageIndex > 5);
        }
    }
}
