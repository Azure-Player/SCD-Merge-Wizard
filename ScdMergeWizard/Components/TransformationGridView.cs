using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScdMergeWizard.EditMenu;
using ScdMergeWizard.Database;
using ScdMergeWizard.ExcHandling;
using System.Drawing;
using System.Data;
using ScdMergeWizard.Formz;

namespace ScdMergeWizard.Components
{
    public class TransformationGridView : DataGridView, ISupportsEdit
    {
        private DataGridViewImageColumn colMappingStatus;
        private DataGridViewComboBoxColumn colSourceColumn;
        private DataGridViewComboBoxColumn colTransformation;
        private DataGridViewTextBoxColumn colTargetColumn;
        private DataGridViewTextBoxColumn colTargetDatabaseTypeColumn;
        private DataGridViewComboBoxColumn colCustomInsertValue;
        private DataGridViewComboBoxColumn colCustomUpdateValue;
        private DataGridViewComboBoxColumn colCustomDeleteValue;
        private DataGridViewComboBoxColumn colColumnCompareMethod;

        private ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem toolStripMenuItemDeleteRow;
        private ToolStripMenuItem toolStripMenuItemHelp;
        private ToolStripMenuItem toolStripMenuItemCopyCustomColumns;
        private DataGridViewTextBoxColumn colMappingInfo;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem toolStripMenuItemCopy;
        private ToolStripMenuItem toolStripMenuItemCut;
        private ToolStripMenuItem toolStripMenuItemPaste;
        private ToolStripSeparator toolStripSeparator1;

        private CopyRow _copyRow = null;


        public delegate void MappingsChangedEventHandler(MappingsChangedEventArgs e);
        public event MappingsChangedEventHandler MappingsChanged;

        public TransformationGridView()
        {
            InitializeComponent();
        }


        public void Initialize()
        {
            this.Columns.Clear();
            this.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colMappingStatus,
            this.colSourceColumn,
            this.colTransformation,
            this.colTargetDatabaseTypeColumn,
            this.colTargetColumn,
            this.colCustomInsertValue,
            this.colCustomUpdateValue,
            this.colCustomDeleteValue,
            this.colColumnCompareMethod,
            this.colMappingInfo});

            this.colSourceColumn.DataSource = getSourceColumns(null);
            this.colTransformation.DataSource = getTransformationColumns(null);
            this.colCustomInsertValue.DataSource = new[] { "" }.Union(GlobalVariables.SourceColumns.Select(sc => sc.ColumnName)).Union(GlobalVariables.UserColumns.Select(uc => uc.ColumnName)).ToArray();
            this.colCustomUpdateValue.DataSource = new[] { "" }.Union(GlobalVariables.SourceColumns.Select(sc => sc.ColumnName)).Union(GlobalVariables.UserColumns.Select(uc => uc.ColumnName)).ToArray();
            this.colCustomDeleteValue.DataSource = new[] { "" }.Union(GlobalVariables.UserColumns.Select(uc => uc.ColumnName)).ToArray();
            this.colColumnCompareMethod.DataSource = Enum.GetNames(typeof(EColumnCompareMethod));

            initiallyPopulateGrid();

            if (GlobalVariables.LoadedColumnMappings != null && GlobalVariables.LoadedColumnMappings.Count > 0)
                setLoadedMappings();

            doMapping();

            updateComponentDependingOnTransformation();
        }

        public void refreshDataSources()
        {
            this.colCustomInsertValue.DataSource = new[] { "" }.Union(GlobalVariables.SourceColumns.Select(sc => sc.ColumnName)).Union(GlobalVariables.UserColumns.Select(uc => uc.ColumnName)).ToArray();
            this.colCustomUpdateValue.DataSource = new[] { "" }.Union(GlobalVariables.SourceColumns.Select(sc => sc.ColumnName)).Union(GlobalVariables.UserColumns.Select(uc => uc.ColumnName)).ToArray();
            this.colCustomDeleteValue.DataSource = new[] { "" }.Union(GlobalVariables.UserColumns.Select(uc => uc.ColumnName)).ToArray();
            foreach (DataGridViewRow row in this.Rows)
            {
                row.Cells["colCustomInsertValue"].Value = (GlobalVariables.SourceColumns.Union(GlobalVariables.UserColumns).Select(sc => sc.ColumnName).Contains(row.Cells["colCustomInsertValue"].Value.ToString())) ? row.Cells["colCustomInsertValue"].Value : string.Empty;
                row.Cells["colCustomUpdateValue"].Value = (GlobalVariables.SourceColumns.Union(GlobalVariables.UserColumns).Select(sc => sc.ColumnName).Contains(row.Cells["colCustomUpdateValue"].Value.ToString())) ? row.Cells["colCustomUpdateValue"].Value : string.Empty;
                row.Cells["colCustomDeleteValue"].Value = (GlobalVariables.UserColumns.Select(sc => sc.ColumnName).Contains(row.Cells["colCustomDeleteValue"].Value.ToString())) ? row.Cells["colCustomDeleteValue"].Value : string.Empty;
            }
        }

        private void setLoadedMappings()
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                string tgt = (row.Cells["colTargetColumn"].Value != null) ? row.Cells["colTargetColumn"].Value.ToString() : string.Empty;

                ColumnMapping lcm = GlobalVariables.LoadedColumnMappings.Where(cm => cm.TargetColumn == tgt).FirstOrDefault();
                if (lcm != null)
                {
                    row.Cells["colSourceColumn"].Value = (GlobalVariables.SourceColumns.Select(sc => sc.ColumnName).Contains(lcm.SourceColumn)) ? lcm.SourceColumn : string.Empty;
                    row.Cells["colTransformation"].Value = GlobalVariables.Transformations.Find(t => t.Code.Equals(lcm.TransformationCode)).Name;
                    row.Cells["colCustomInsertValue"].Value = (GlobalVariables.SourceColumns.Union(GlobalVariables.UserColumns).Select(sc => sc.ColumnName).Contains(lcm.CustomInsertValue)) ? lcm.CustomInsertValue : string.Empty;
                    row.Cells["colCustomUpdateValue"].Value = (GlobalVariables.SourceColumns.Union(GlobalVariables.UserColumns).Select(sc => sc.ColumnName).Contains(lcm.CustomUpdateValue)) ? lcm.CustomUpdateValue : string.Empty;
                    row.Cells["colCustomDeleteValue"].Value = (GlobalVariables.UserColumns.Select(sc => sc.ColumnName).Contains(lcm.CustomDeleteValue)) ? lcm.CustomDeleteValue : string.Empty;

                    row.Cells["colColumnCompareMethod"].Value = lcm.ColumnCompareMethod.ToString();
                }
            }
        }

        public void makeAll_SCD1()
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                string tgt = (row.Cells["colTargetColumn"].Value != null) ? row.Cells["colTargetColumn"].Value.ToString() : string.Empty;

                row.Cells["colTransformation"].Value = GlobalVariables.Transformations.Find(t => t.Code.Equals(ETransformationCode.SCD1)).Name;
            }
            doMapping();
        }



        public void initiallyPopulateGrid()
        {
            if (this.Rows.Count == 0)
            {
                foreach (MyDbColumn col in GlobalVariables.TargetColumns)
                {
                    MyDbColumn sourceCol = GlobalVariables.SourceColumns.Find(sc => sc.ColumnName.ToUpper().Equals(col.ColumnName.ToUpper()));

                    this.Rows.Add(
                        Properties.Resources.arrow_skip,
                        (sourceCol != null) ? sourceCol.ColumnName : string.Empty,
                        GlobalVariables.Transformations.Find(tt => tt.Code == ETransformationCode.SKIP).Name,
                        ETargetDatabaseType.TARGET.ToString(),
                        col.ColumnName,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        EColumnCompareMethod.Default.ToString(),
                        string.Empty
                        );
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.colMappingStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.colSourceColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colTransformation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colTargetDatabaseTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTargetColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomInsertValue = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colCustomUpdateValue = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colCustomDeleteValue = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colColumnCompareMethod = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colMappingInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemCopyCustomColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // colMappingStatus
            // 
            this.colMappingStatus.HeaderText = "";
            this.colMappingStatus.Name = "colMappingStatus";
            this.colMappingStatus.ReadOnly = true;
            this.colMappingStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colMappingStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colMappingStatus.Width = 20;
            // 
            // colSourceColumn
            // 
            this.colSourceColumn.HeaderText = "Source Column";
            this.colSourceColumn.Name = "colSourceColumn";
            this.colSourceColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSourceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colSourceColumn.Width = 150;
            // 
            // colTransformation
            // 
            this.colTransformation.HeaderText = "Transformation";
            this.colTransformation.Name = "colTransformation";
            this.colTransformation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colTransformation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colTransformation.Width = 150;
            // 
            // colTargetDatabaseTypeColumn
            // 
            this.colTargetDatabaseTypeColumn.HeaderText = "T";
            this.colTargetDatabaseTypeColumn.Name = "colTargetDatabaseTypeColumn";
            this.colTargetDatabaseTypeColumn.ReadOnly = true;
            this.colTargetDatabaseTypeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colTargetDatabaseTypeColumn.Visible = false;
            this.colTargetDatabaseTypeColumn.Width = 10;
            // 
            // colTargetColumn
            // 
            this.colTargetColumn.HeaderText = "Target Column";
            this.colTargetColumn.Name = "colTargetColumn";
            this.colTargetColumn.ReadOnly = true;
            this.colTargetColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colTargetColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colTargetColumn.Width = 150;
            // 
            // colCustomInsertValue
            // 
            this.colCustomInsertValue.HeaderText = "On Insert";
            this.colCustomInsertValue.Name = "colCustomInsertValue";
            this.colCustomInsertValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCustomInsertValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colCustomInsertValue.Width = 150;
            // 
            // colCustomUpdateValue
            // 
            this.colCustomUpdateValue.HeaderText = "On Update";
            this.colCustomUpdateValue.Name = "colCustomUpdateValue";
            this.colCustomUpdateValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCustomUpdateValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colCustomUpdateValue.Width = 150;
            // 
            // colCustomDeleteValue
            // 
            this.colCustomDeleteValue.HeaderText = "On Delete";
            this.colCustomDeleteValue.Name = "colCustomDeleteValue";
            this.colCustomDeleteValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCustomDeleteValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colCustomDeleteValue.Width = 150;
            // 
            // colColumnCompareMethod
            // 
            this.colColumnCompareMethod.HeaderText = "Column Compare Method";
            this.colColumnCompareMethod.Name = "colColumnCompareMethod";
            this.colColumnCompareMethod.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colColumnCompareMethod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colColumnCompareMethod.Visible = false;
            this.colColumnCompareMethod.Width = 180;
            // 
            // colMappingInfo
            // 
            this.colMappingInfo.HeaderText = "Mapping Info";
            this.colMappingInfo.Name = "colMappingInfo";
            this.colMappingInfo.ReadOnly = true;
            this.colMappingInfo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colMappingInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colMappingInfo.Width = 300;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemCut,
            this.toolStripMenuItemPaste,
            this.toolStripSeparator1,
            this.toolStripMenuItemCopyCustomColumns,
            this.toolStripMenuItemDeleteRow,
            this.toolStripSeparator2,
            this.toolStripMenuItemHelp});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(186, 148);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Image = global::ScdMergeWizard.Properties.Resources.CopyHS;
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemCopy.Text = "Copy";
            this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
            // 
            // toolStripMenuItemCut
            // 
            this.toolStripMenuItemCut.Image = global::ScdMergeWizard.Properties.Resources.CutHS;
            this.toolStripMenuItemCut.Name = "toolStripMenuItemCut";
            this.toolStripMenuItemCut.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemCut.Text = "Cut";
            this.toolStripMenuItemCut.Click += new System.EventHandler(this.toolStripMenuItemCut_Click);
            // 
            // toolStripMenuItemPaste
            // 
            this.toolStripMenuItemPaste.Image = global::ScdMergeWizard.Properties.Resources.PasteHS;
            this.toolStripMenuItemPaste.Name = "toolStripMenuItemPaste";
            this.toolStripMenuItemPaste.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemPaste.Text = "Paste";
            this.toolStripMenuItemPaste.Click += new System.EventHandler(this.toolStripMenuItemPaste_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(182, 6);
            // 
            // toolStripMenuItemCopyCustomColumns
            // 
            this.toolStripMenuItemCopyCustomColumns.Image = global::ScdMergeWizard.Properties.Resources.CopyHS;
            this.toolStripMenuItemCopyCustomColumns.Name = "toolStripMenuItemCopyCustomColumns";
            this.toolStripMenuItemCopyCustomColumns.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemCopyCustomColumns.Text = "Copy Event Columns";
            this.toolStripMenuItemCopyCustomColumns.ToolTipText = "Copy selected transformation event columns to all transformations with same code";
            this.toolStripMenuItemCopyCustomColumns.Click += new System.EventHandler(this.toolStripMenuItemCopyCustomColumns_Click);
            // 
            // toolStripMenuItemDeleteRow
            // 
            this.toolStripMenuItemDeleteRow.Image = global::ScdMergeWizard.Properties.Resources.DeleteHS;
            this.toolStripMenuItemDeleteRow.Name = "toolStripMenuItemDeleteRow";
            this.toolStripMenuItemDeleteRow.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemDeleteRow.Text = "Delete";
            this.toolStripMenuItemDeleteRow.ToolTipText = "Clear source, transformation and event columns";
            this.toolStripMenuItemDeleteRow.Click += new System.EventHandler(this.toolStripMenuItemResetTransformation_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(182, 6);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.Image = global::ScdMergeWizard.Properties.Resources._109_AllAnnotations_Help_16x16_72;
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemHelp.Text = "Transformation Help";
            this.toolStripMenuItemHelp.Click += new System.EventHandler(this.toolStripMenuItemHelp_Click);
            // 
            // TransformationGridView
            // 
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }


        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            DataGridViewComboBoxCell cell = this[e.ColumnIndex, e.RowIndex] as DataGridViewComboBoxCell;
            string oldCellValue = (cell.Value != null) ? cell.Value.ToString() : null;

            if (e.ColumnIndex == 1)
                cell.DataSource = getSourceColumns(oldCellValue);
            else if (e.ColumnIndex == 2)
                cell.DataSource = getTransformationColumns(oldCellValue);
            else
                base.OnCellBeginEdit(e);

            updateComponentDependingOnTransformation();
        }


        object getSourceColumns(string col)
        {
            List<string> columns = new List<string>();

            foreach (var c in GlobalVariables.SourceColumns)
                columns.Add(c.ColumnName);

            var allButScd3 = GlobalVariables.ColumnMappings
                        .Where(m => m.TransformationCode != ETransformationCode.SCD3_CURRENT_VALUE &&
                                m.TransformationCode != ETransformationCode.SCD3_DATE_FROM &&
                                m.TransformationCode != ETransformationCode.SCD3_ORIGINAL_FIRST_VALUE &&
                                m.TransformationCode != ETransformationCode.SCD3_PREVIOUS_VALUE &&
                                m.TransformationCode != ETransformationCode.SKIP)
                        .GroupBy(cm => cm.SourceColumn)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric);

            foreach (var c in allButScd3.Where(m => m.Count > 0))
            {
                columns.Remove(c.Metric);
            }

            var onlyScd3 = GlobalVariables.ColumnMappings
                        .Where(m => m.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE ||
                                m.TransformationCode == ETransformationCode.SCD3_DATE_FROM ||
                                m.TransformationCode == ETransformationCode.SCD3_ORIGINAL_FIRST_VALUE ||
                                m.TransformationCode == ETransformationCode.SCD3_PREVIOUS_VALUE)
                        .GroupBy(cm => cm.SourceColumn)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric);

            foreach (var c in onlyScd3.Where(m => m.Count >= 4))
            {
                columns.Remove(c.Metric);
            }

            if (!string.IsNullOrEmpty(col) && !columns.Contains(col))
                columns.Add(col);


            if (!columns.Contains(string.Empty))
                columns.Add(string.Empty);

            columns.Sort();
            return columns;
        }

        object getTransformationColumns(string col)
        {
            List<string> columns = new List<string>();

            foreach (var c in GlobalVariables.Transformations)
                columns.Add(c.Name);

            var ott = GlobalVariables.Transformations.Where(t => t.UseOnlyOnce).Select(tt => tt.Code);


            var oneTimeTransformations = GlobalVariables.ColumnMappings
                        .Where(m => ott.Contains(m.TransformationCode))
                        .GroupBy(cm => cm.TransformationCode)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric);

            foreach (var c in oneTimeTransformations.Where(m => m.Count == 1))
            {
                columns.Remove(GlobalVariables.Transformations.Find(t => t.Code == c.Metric).Name);
            }

            if (!string.IsNullOrEmpty(col) && !columns.Contains(col))
                columns.Add(col);

            columns.Sort();

            return columns;
        }

        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            cellValueChanged();
        }



        private void cellValueChanged()
        {
            doMapping();
            updateComponentDependingOnTransformation();
            GlobalVariables.IsProjectModified = true;
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            MyExceptionHandler.NewEx(e.Exception);
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);

            updateComponentDependingOnTransformation();
        }

        private void updateComponentDependingOnTransformation()
        {
            if (this.CurrentCell != null && this.CurrentCell.RowIndex >= 0)
            {
                MyTransformation trf = GlobalVariables.Transformations.Find(t => t.Name.Equals(this.Rows[this.CurrentCell.RowIndex].Cells["colTransformation"].Value.ToString()));

                Font f = this.Font;

                this.Columns["colSourceColumn"].HeaderCell.Style.Font = new Font(f.FontFamily, f.Size, (trf.HasSourceColumn) ? FontStyle.Regular : FontStyle.Strikeout);
                this.Columns["colCustomInsertValue"].HeaderCell.Style.Font = new Font(f.FontFamily, f.Size, (trf.HasOnInsertColumn) ? FontStyle.Regular : FontStyle.Strikeout);
                this.Columns["colCustomUpdateValue"].HeaderCell.Style.Font = new Font(f.FontFamily, f.Size, (trf.HasOnUpdateColumn) ? FontStyle.Regular : FontStyle.Strikeout);
                this.Columns["colCustomDeleteValue"].HeaderCell.Style.Font = new Font(f.FontFamily, f.Size, (trf.HasOnDeleteColumn) ? FontStyle.Regular : FontStyle.Strikeout);
            }
        }

        private MyTransformation getSelectedTransformation()
        {
            if (this.CurrentCell != null && this.CurrentCell.RowIndex != -1)
                return GlobalVariables.Transformations.Find(t => t.Name.Equals(this.Rows[this.CurrentCell.RowIndex].Cells["colTransformation"].Value.ToString()));
            return null;
        }
        private string getSelectedSourceColumn()
        {
            if (this.CurrentCell != null)
                return (this.Rows[this.CurrentCell.RowIndex].Cells["colSourceColumn"].Value != null) ? this.Rows[this.CurrentCell.RowIndex].Cells["colSourceColumn"].Value.ToString() : string.Empty;
            return string.Empty;
        }
        private ETargetDatabaseType getSelectedTargetDatabaseType()
        {
            if (this.CurrentCell != null)
                return (ETargetDatabaseType)Enum.Parse(typeof(ETargetDatabaseType), this.Rows[this.CurrentCell.RowIndex].Cells["colTargetDatabaseTypeColumn"].Value.ToString());
            return ETargetDatabaseType.UNDEF;
        }
        private string getSelectedTargetColumn()
        {
            if (this.CurrentCell != null)
                return (this.Rows[this.CurrentCell.RowIndex].Cells["colTargetColumn"].Value != null) ? this.Rows[this.CurrentCell.RowIndex].Cells["colTargetColumn"].Value.ToString() : string.Empty;
            return string.Empty;
        }
        private string getSelectedCustomInsertValue()
        {
            if (this.CurrentCell != null)
                return (this.Rows[this.CurrentCell.RowIndex].Cells["colCustomInsertValue"].Value != null) ? this.Rows[this.CurrentCell.RowIndex].Cells["colCustomInsertValue"].Value.ToString() : string.Empty;
            return string.Empty;
        }
        private string getSelectedCustomUpdateValue()
        {
            if (this.CurrentCell != null)
                return (this.Rows[this.CurrentCell.RowIndex].Cells["colCustomUpdateValue"].Value != null) ? this.Rows[this.CurrentCell.RowIndex].Cells["colCustomUpdateValue"].Value.ToString() : string.Empty;
            return string.Empty;
        }
        private string getSelectedCustomDeleteValue()
        {
            if (this.CurrentCell != null)
                return (this.Rows[this.CurrentCell.RowIndex].Cells["colCustomDeleteValue"].Value != null) ? this.Rows[this.CurrentCell.RowIndex].Cells["colCustomDeleteValue"].Value.ToString() : string.Empty;
            return string.Empty;
        }

        private MappingsChangedEventArgs doMapping()
        {
            MappingsChangedEventArgs e = new MappingsChangedEventArgs();
            bool rowError;
            string s;

            GlobalVariables.ColumnMappings.Clear();

            foreach (DataGridViewRow row in this.Rows)
            {
                row.Cells["colMappingInfo"].Value = string.Empty;
                rowError = false;

                MyTransformation trf = GlobalVariables.Transformations.Find(t => t.Name.Equals(row.Cells["colTransformation"].Value.ToString()));
                ETargetDatabaseType tdbt = (ETargetDatabaseType)Enum.Parse(typeof(ETargetDatabaseType), row.Cells["colTargetDatabaseTypeColumn"].Value.ToString());
                string src = (row.Cells["colSourceColumn"].Value != null) ? row.Cells["colSourceColumn"].Value.ToString() : string.Empty;
                string tgt = (row.Cells["colTargetColumn"].Value != null) ? row.Cells["colTargetColumn"].Value.ToString() : string.Empty;


                string customInsertValue = (row.Cells["colCustomInsertValue"].Value != null) ? row.Cells["colCustomInsertValue"].Value.ToString() : string.Empty;
                string customUpdateValue = (row.Cells["colCustomUpdateValue"].Value != null) ? row.Cells["colCustomUpdateValue"].Value.ToString() : string.Empty;
                string customDeleteValue = (row.Cells["colCustomDeleteValue"].Value != null) ? row.Cells["colCustomDeleteValue"].Value.ToString() : string.Empty;

                EColumnCompareMethod ccm = (EColumnCompareMethod)Enum.Parse(typeof(EColumnCompareMethod), row.Cells["colColumnCompareMethod"].Value.ToString());

                //if (trf.Code != ETransformationType.SKIP && trf.Code != ETransformationType.UNDEF)
                {
                    if (trf.HasSourceColumn && string.IsNullOrEmpty(src))
                    {
                        rowError = true;
                        s = tgt + ": Source Column must be defined for '" + trf.Name + "' transformation";
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, s));
                        row.Cells["colMappingInfo"].Value = s;
                        row.Cells["colMappingStatus"].Value = Properties.Resources.exclamation_red;
                    }
                    if (!trf.HasSourceColumn && !string.IsNullOrEmpty(src))
                    {
                        rowError = true;
                        s = tgt + ": Source Column cannot be defined for '" + trf.Name + "' transformation";
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, s));
                        row.Cells["colMappingInfo"].Value = s;
                        row.Cells["colMappingStatus"].Value = Properties.Resources.exclamation_red;
                    }


                    if (trf.HasOnDeleteColumn && string.IsNullOrEmpty(customDeleteValue))
                    {
                        rowError = true;
                        s = tgt + ": On Delete column must be defined for '" + trf.Name + "' transformation";
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, s));
                        row.Cells["colMappingInfo"].Value = s;
                        row.Cells["colMappingStatus"].Value = Properties.Resources.exclamation_red;
                    }
                    if (!trf.HasOnDeleteColumn && !string.IsNullOrEmpty(customDeleteValue))
                    {
                        rowError = true;
                        s = tgt + ": On delete column cannot be defined for '" + trf.Name + "' transformation";
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, s));
                        row.Cells["colMappingInfo"].Value = s;
                        row.Cells["colMappingStatus"].Value = Properties.Resources.exclamation_red;
                    }


                    if (trf.HasOnUpdateColumn && string.IsNullOrEmpty(customUpdateValue))
                    {
                        rowError = true;
                        s = tgt + ": On Update column must be defined for '" + trf.Name + "' transformation";
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, s));
                        row.Cells["colMappingInfo"].Value = s;
                        row.Cells["colMappingStatus"].Value = Properties.Resources.exclamation_red;
                    }
                    if (!trf.HasOnUpdateColumn && !string.IsNullOrEmpty(customUpdateValue))
                    {
                        rowError = true;
                        s = tgt + ": On Update column cannot be defined for '" + trf.Name + "' transformation";
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, s));
                        row.Cells["colMappingInfo"].Value = s;
                        row.Cells["colMappingStatus"].Value = Properties.Resources.exclamation_red;
                    }

                    if (trf.HasOnInsertColumn && string.IsNullOrEmpty(customInsertValue))
                    {
                        rowError = true;
                        s = tgt + ": On Insert column must be defined for '" + trf.Name + "' transformation";
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, s));
                        row.Cells["colMappingInfo"].Value = s;
                        row.Cells["colMappingStatus"].Value = Properties.Resources.exclamation_red;
                    }
                    if (!trf.HasOnInsertColumn && !string.IsNullOrEmpty(customInsertValue))
                    {
                        rowError = true;
                        s = tgt + ": On Insert column cannot be defined for '" + trf.Name + "' transformation";
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, s));
                        row.Cells["colMappingInfo"].Value = s;
                        row.Cells["colMappingStatus"].Value = Properties.Resources.exclamation_red;
                    }

                    if ((trf.Code == ETransformationCode.SCD2_DATE_TO || trf.Code == ETransformationCode.SCD2_IS_ACTIVE || trf.Code == ETransformationCode.IS_DELETED)
                         && !string.IsNullOrEmpty(customInsertValue) && !string.IsNullOrEmpty(customUpdateValue)
                         && GlobalVariables.UserColumnsDefinitions.Find(uc => uc.ColumnName.Equals(customInsertValue)).Value != null
                         && GlobalVariables.UserColumnsDefinitions.Find(uc => uc.ColumnName.Equals(customUpdateValue)).Value != null
                         && GlobalVariables.UserColumnsDefinitions.Find(uc => uc.ColumnName.Equals(customInsertValue)).Value.Equals(GlobalVariables.UserColumnsDefinitions.Find(uc => uc.ColumnName.Equals(customUpdateValue)).Value)
                        )
                    {
                        rowError = true;
                        s = tgt + ": On Insert and On Update columns must have different values. Current values are same : '" + GlobalVariables.UserColumnsDefinitions.Find(uc => uc.ColumnName.Equals(customInsertValue)).Value + "'";
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, s));
                        row.Cells["colMappingInfo"].Value = s;
                        row.Cells["colMappingStatus"].Value = Properties.Resources.exclamation_red;
                    }


                    if (!rowError)
                    {
                        GlobalVariables.ColumnMappings.Add(new ColumnMapping(src, tdbt, tgt, trf.Code, customInsertValue, customUpdateValue, customDeleteValue, ccm));
                        row.Cells["colMappingInfo"].Value = "OK";

                        row.Cells["colMappingStatus"].Value = trf.Image;
                        /*

                        switch (trf.Code)
                        {
                            case ETransformationCode.BUSINESS_KEY: row.Cells["colMappingStatus"].Value = Properties.Resources.key; break;

                            case ETransformationCode.CREATED_DATE: row.Cells["colMappingStatus"].Value = Properties.Resources.date_add; break;
                            case ETransformationCode.DELETED_DATE: row.Cells["colMappingStatus"].Value = Properties.Resources.date_delete; break;
                            case ETransformationCode.MODIFIED_DATE: row.Cells["colMappingStatus"].Value = Properties.Resources.date_edit; break;
                            

                            case ETransformationCode.IS_DELETED: row.Cells["colMappingStatus"].Value = Properties.Resources.the_delete_icon; break;
                            

                            case ETransformationCode.SCD3_DATE_FROM: row.Cells["colMappingStatus"].Value = Properties.Resources.date_next; break;

                            case ETransformationCode.SCD0: row.Cells["colMappingStatus"].Value = Properties.Resources.Numbers_0_icon; break;
                            case ETransformationCode.SCD1: row.Cells["colMappingStatus"].Value = Properties.Resources.Numbers_1_icon; break;
                            case ETransformationCode.SCD2: row.Cells["colMappingStatus"].Value = Properties.Resources.Numbers_2_icon; break;
                            case ETransformationCode.SCD2_DATE_FROM: row.Cells["colMappingStatus"].Value = Properties.Resources.date_next; break;
                            case ETransformationCode.SCD2_DATE_TO: row.Cells["colMappingStatus"].Value = Properties.Resources.date_previous; break;
                            case ETransformationCode.SCD2_IS_ACTIVE: row.Cells["colMappingStatus"].Value = Properties.Resources.check; break;

                            case ETransformationCode.SCD3_CURRENT_VALUE: row.Cells["colMappingStatus"].Value = Properties.Resources.Numbers_3_icon; break;
                            case ETransformationCode.SCD3_ORIGINAL_FIRST_VALUE: row.Cells["colMappingStatus"].Value = Properties.Resources.Numbers_3_icon; break;
                            case ETransformationCode.SCD3_PREVIOUS_VALUE: row.Cells["colMappingStatus"].Value = Properties.Resources.Numbers_3_icon; break;

                            case ETransformationCode.VERSION_NUMBER: row.Cells["colMappingStatus"].Value = Properties.Resources.old_versions; break;

                            case ETransformationCode.SKIP: row.Cells["colMappingStatus"].Value = Properties.Resources.arrow_skip; break;
                            default: row.Cells["colMappingStatus"].Value = Properties.Resources._109_AllAnnotations_Help_16x16_72; break;
                        }*/
                    }
                }
            }

            if (!GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.BUSINESS_KEY))
            {
                e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, "No business keys defined"));
            }

            if (!GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD0) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD1) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD2) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD3_ORIGINAL_FIRST_VALUE) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD3_PREVIOUS_VALUE))
            {
                e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, "No SCD transformations defined"));
            }

            if (GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD2) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD2_DATE_FROM))
            {
                e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR,
string.Format("'{0}' transformation defined, but '{1}' was not",
                        TrfType2Name(ETransformationCode.SCD2), TrfType2Name(ETransformationCode.SCD2_DATE_FROM))
                ));
            }

            if (GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD2) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD2_DATE_TO) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD2_IS_ACTIVE))
            {
                e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, string.Format("'{0}' transformation defined, but '{1}' or '{2}' (or both) was/were not",
                    TrfType2Name(ETransformationCode.SCD2), TrfType2Name(ETransformationCode.SCD2_DATE_TO), TrfType2Name(ETransformationCode.SCD2_IS_ACTIVE))
                ));
            }


            var allButScd3 = from cm2 in GlobalVariables.ColumnMappings
                               .Where(c => c.TransformationCode != ETransformationCode.SCD3_CURRENT_VALUE &&
                                        c.TransformationCode != ETransformationCode.SCD3_DATE_FROM &&
                                        c.TransformationCode != ETransformationCode.SCD3_ORIGINAL_FIRST_VALUE &&
                                        c.TransformationCode != ETransformationCode.SCD3_PREVIOUS_VALUE &&
                                        c.TransformationCode != ETransformationCode.SKIP)
                             group cm2 by new
                             {
                                 cm2.SourceColumn,
                                 TransformationType = cm2.TransformationCode
                             } into grp
                             select new ConsolidatedColumnMapping()
                             {
                                 SourceColumnName = grp.Key.SourceColumn,
                                 TransformationType = grp.Key.TransformationType,
                                 Count = grp.Count()
                             };

            var onlyScd3 = from cm2 in GlobalVariables.ColumnMappings
                               .Where(c => c.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE ||
                                        c.TransformationCode == ETransformationCode.SCD3_DATE_FROM ||
                                        c.TransformationCode == ETransformationCode.SCD3_ORIGINAL_FIRST_VALUE ||
                                        c.TransformationCode == ETransformationCode.SCD3_PREVIOUS_VALUE)
                           group cm2 by new
                           {
                               cm2.SourceColumn,
                               TransformationType = cm2.TransformationCode
                           } into grp
                           select new ConsolidatedColumnMapping()
                           {
                               SourceColumnName = grp.Key.SourceColumn,
                               TransformationType = grp.Key.TransformationType,
                               Count = grp.Count()
                           };


            foreach (ConsolidatedColumnMapping ccm1 in allButScd3)
            {
                foreach (ConsolidatedColumnMapping ccm2 in onlyScd3)
                {
                    if (ccm1.SourceColumnName.Equals(ccm2.SourceColumnName))
                    {
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, string.Format("Source column cannot be combined between SCD3 and other type of transformation : {0} [{1}], {2} [{3}]", ccm1.SourceColumnName, TrfType2Name(ccm1.TransformationType), ccm2.SourceColumnName, TrfType2Name(ccm2.TransformationType))));
                    }
                }
            }

            foreach (ConsolidatedColumnMapping ccm in allButScd3.Union(onlyScd3))
            {
                if (ccm.Count > 1)
                    e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, string.Format("Source column cannot be used more than once for same transformation : {0} [{1}] is used {2} times", ccm.SourceColumnName, TrfType2Name(ccm.TransformationType), ccm.Count)));
            }



            foreach (ColumnMapping cm in GlobalVariables.ColumnMappings.Where(c => c.TransformationCode == ETransformationCode.SCD3_PREVIOUS_VALUE))
            {
                if (!GlobalVariables.ColumnMappings.Any(q => q.SourceColumn.Equals(cm.SourceColumn) && q.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE))
                    e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, string.Format("Cannot use '{0}' transformation without '{1}' transformation on the same Source Column", TrfType2Name(ETransformationCode.SCD3_PREVIOUS_VALUE), TrfType2Name(ETransformationCode.SCD3_CURRENT_VALUE))));
            }

            if (GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD3_DATE_FROM) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD3_ORIGINAL_FIRST_VALUE) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD3_PREVIOUS_VALUE))
            {
                e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, string.Format("Cannot use '{0}' transformation without at least one SCD3 transformation on the same Source Column", TrfType2Name(ETransformationCode.SCD3_DATE_FROM))));
            }


            if (GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD2_DATE_FROM) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD2))
            {
                e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, string.Format("Cannot use '{0}' transformation without '{1}' transformation(s)", TrfType2Name(ETransformationCode.SCD2_DATE_FROM), TrfType2Name(ETransformationCode.SCD2))));
            }

            if (GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD2_DATE_TO) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD2))
            {
                e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, string.Format("Cannot use '{0}' transformation without '{1}' transformation(s)", TrfType2Name(ETransformationCode.SCD2_DATE_TO), TrfType2Name(ETransformationCode.SCD2))));
            }

            if (GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD2_IS_ACTIVE) &&
                !GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.SCD2))
            {
                e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, string.Format("Cannot use '{0}' transformation without '{1}' transformation(s)", TrfType2Name(ETransformationCode.SCD2_IS_ACTIVE), TrfType2Name(ETransformationCode.SCD2))));
            }


            var ott = GlobalVariables.Transformations.Where(t => t.UseOnlyOnce).Select(tt => tt.Code);

            var oneTimeTransformations = GlobalVariables.ColumnMappings
                        .Where(m => ott.Contains(m.TransformationCode))
                        .GroupBy(cm => cm.TransformationCode)
                        .Select(group => new
                        {
                            Transformation = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Transformation);

            foreach (var c in oneTimeTransformations.Where(m => m.Count > 1))
            {
                e.Messages.Add(new MappingChangedMessage(EMappingMessageType.ERROR, string.Format("Cannot use '{0}' transformation more than once", TrfType2Name(c.Transformation))));
            }

            // Check data types
            foreach (ColumnMapping cm in GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode != ETransformationCode.SCD3_DATE_FROM))
            {
                MyDbColumn src = GlobalVariables.SourceColumns.Find(c => c.ColumnName.Equals(cm.SourceColumn));
                MyDbColumn tgt = GlobalVariables.TargetColumns.Find(c => c.ColumnName.Equals(cm.TargetColumn));

                if (src != null && tgt != null)
                {
                    if (src.DataType != tgt.DataType)
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.WARNING, string.Format("Error may occur when converting data type '{0}' into '{1}' for '{2}' and '{3}' columns", src.DataType, tgt.DataType, src.ColumnName, tgt.ColumnName)));
                    if (src.ColumnSize > tgt.ColumnSize)
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.WARNING, string.Format("Truncation may occur when trying to convert column with size {2} into {3}, between '{0}' and '{1}' columns", src.ColumnName, tgt.ColumnName, src.ColumnSize, tgt.ColumnSize)));
                    if (src.AllowDBNull.HasValue && src.AllowDBNull.Value && tgt.AllowDBNull.HasValue && !tgt.AllowDBNull.Value)
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.WARNING, string.Format("Error may occur when trying to convert nullable column '{0}' into non-nullable column '{1}'", src.ColumnName, tgt.ColumnName, src.ColumnSize, tgt.ColumnSize)));
                    if (src.IsLong && !tgt.IsLong)
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.WARNING, string.Format("Error may occur when trying to convert long data type of column '{0}' into non-long column '{1}'", src.ColumnName, tgt.ColumnName, src.ColumnSize, tgt.ColumnSize)));
                    if (!src.IsUnique && tgt.IsUnique)
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.WARNING, string.Format("Error may occur when trying to convert non-unique column '{0}' into unique column '{1}'", src.ColumnName, tgt.ColumnName)));
                    if (src.NumericPrecision != tgt.NumericPrecision)
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.WARNING, string.Format("Error may occur when trying to convert column '{0}' with numeric precision {1} into column '{2}' with numeric precision {3}", src.ColumnName, src.NumericPrecision, tgt.ColumnName, tgt.NumericPrecision)));
                    if (src.NumericScale != tgt.NumericScale)
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.WARNING, string.Format("Error may occur when trying to convert column '{0}' with numeric scale {1} into column '{2}' with numeric scale {3}", src.ColumnName, src.NumericScale, tgt.ColumnName, tgt.NumericScale)));
                    if (src.ProviderType != tgt.ProviderType)
                        e.Messages.Add(new MappingChangedMessage(EMappingMessageType.WARNING, string.Format("Error may occur when trying to convert column '{0}' with provider type {1} into column '{2}' with provider type {3}", src.ColumnName, src.ProviderType, tgt.ColumnName, tgt.ProviderType)));
                }
            }

            // 

            if (MappingsChanged != null)
            {
                MappingsChanged(e);
            }

            return e;
        }

        public MappingsChangedEventArgs GetMappingResults()
        {
            return doMapping();
        }

        #region Magic Edit
        public bool UndoVisible
        {
            get { return false; }
        }

        public bool CanUndo
        {
            get { return false; }
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public bool RedoVisible
        {
            get { return false; }
        }

        public bool CanRedo
        {
            get { return false; }
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public bool CanCut
        {
            get { return true; }
        }

        public void Cut()
        {
            Copy();
            deleteRowData(false);
        }

        public bool CanCopy
        {
            get { return true; }
        }

        public void Copy()
        {
            if (this.CurrentCell != null && this.CurrentCell.RowIndex != -1)
            {
                this._copyRow = new CopyRow()
                {
                    TransformationName = this.Rows[this.CurrentCell.RowIndex].Cells["colTransformation"].Value.ToString(),
                    CustomInsertValue = this.Rows[this.CurrentCell.RowIndex].Cells["colCustomInsertValue"].Value.ToString(),
                    CustomUpdateValue = this.Rows[this.CurrentCell.RowIndex].Cells["colCustomUpdateValue"].Value.ToString(),
                    CustomDeleteValue = this.Rows[this.CurrentCell.RowIndex].Cells["colCustomDeleteValue"].Value.ToString()
                };
            }
        }

        public bool CanPaste
        {
            get { return _copyRow != null; }
        }

        public void Paste()
        {
            if (_copyRow != null)
            {
                this.Rows[this.CurrentCell.RowIndex].Cells["colTransformation"].Value = _copyRow.TransformationName;
                this.Rows[this.CurrentCell.RowIndex].Cells["colCustomInsertValue"].Value = _copyRow.CustomInsertValue;
                this.Rows[this.CurrentCell.RowIndex].Cells["colCustomUpdateValue"].Value = _copyRow.CustomUpdateValue;
                this.Rows[this.CurrentCell.RowIndex].Cells["colCustomDeleteValue"].Value = _copyRow.CustomDeleteValue;

                cellValueChanged();
            }
        }

        public bool CanSelectAll
        {
            get { return false; }
        }

        public bool CanDelete
        {
            get { return true; }
        }

        public void Delete()
        {
            deleteRowData(true);
        }

        public bool CanShowProperties
        {
            get { return false; }
        }

        public void ShowProperties()
        {
            throw new NotImplementedException();
        }
        #endregion Magic Edit


        public string TrfType2Name(ETransformationCode tt)
        {
            return GlobalVariables.Transformations.Find(t => t.Code == tt).Name;
        }

        private void toolStripMenuItemResetTransformation_Click(object sender, EventArgs e)
        {
            deleteRowData(true);
        }


        private void deleteRowData(bool askBeforeDelete)
        {
            if (!askBeforeDelete || askBeforeDelete && MessageBox.Show(string.Format("Are you sure you want Reset selected transformation (Source, Transformation and Value columns) on target column '{0}'", getSelectedTargetColumn()), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Rows[this.CurrentCell.RowIndex].Cells["colSourceColumn"].Value = string.Empty;
                this.Rows[this.CurrentCell.RowIndex].Cells["colTransformation"].Value = GlobalVariables.Transformations.Find(t => t.Code == ETransformationCode.SKIP).Name;
                this.Rows[this.CurrentCell.RowIndex].Cells["colCustomInsertValue"].Value = string.Empty;
                this.Rows[this.CurrentCell.RowIndex].Cells["colCustomUpdateValue"].Value = string.Empty;
                this.Rows[this.CurrentCell.RowIndex].Cells["colCustomDeleteValue"].Value = string.Empty;

                GlobalVariables.ColumnMappings.RemoveAll(m => m.TargetColumn == getSelectedTargetColumn());
                doMapping();
                GlobalVariables.IsProjectModified = true;
            }
        }

        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                this.CurrentCell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
            else
                base.OnCellMouseDown(e);
        }

        private void toolStripMenuItemHelp_Click(object sender, EventArgs e)
        {
            MyTransformation trf = getSelectedTransformation();

            if (trf == null)
                MessageBox.Show("Transformation not selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                TransformationHelpForm thf = new TransformationHelpForm(trf);
                thf.ShowDialog();

                /*

                string title = string.Format("{0} [{1}] Help", trf.Name, trf.Code);
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(string.Format("Transformation {0} [{1}] properties:", trf.Name, trf.Code));
                sb.AppendLine();
                sb.AppendLine(string.Format(" - {0} be used multimple times", trf.UseOnlyOnce ? "Cannot" : "Can"));
                sb.AppendLine();
                sb.AppendLine(string.Format(" - Source column {0} to be defined", trf.HasSourceColumn ? "has" : "does not have"));

                if (!string.IsNullOrEmpty(trf.OnInsertColumnDesc))
                {
                    sb.AppendLine();
                    sb.AppendLine(string.Format(" - On Insert column:{1}{0}", trf.OnInsertColumnDesc, Environment.NewLine));
                }
                if (!string.IsNullOrEmpty(trf.OnUpdateColumnDesc))
                {
                    sb.AppendLine();
                    sb.AppendLine(string.Format(" - On Update column:{1}{0}", trf.OnUpdateColumnDesc, Environment.NewLine));
                }
                if (!string.IsNullOrEmpty(trf.OnDeleteColumnDesc))
                {
                    sb.AppendLine();
                    sb.AppendLine(string.Format(" - On Delete column:{1}{0}", trf.OnDeleteColumnDesc, Environment.NewLine));
                }
                MessageBox.Show(sb.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                 * */
            }
        }

        private void toolStripMenuItemCopyCustomColumns_Click(object sender, EventArgs e)
        {
            string s = string.Format("Copy event columns to all {0} transformations?", getSelectedTransformation().Name);
            if (MessageBox.Show(s, "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                MyTransformation selectedTransformation = getSelectedTransformation();
                int currentRow = this.CurrentCell.RowIndex;
                int modifiedRowsCount = 0;

                for (int i = 0; i < this.Rows.Count; i++)
                {
                    if (i != currentRow)
                    {
                        var currentTransformation = GlobalVariables.Transformations.Find(t => t.Name.Equals(this.Rows[i].Cells["colTransformation"].Value.ToString()));

                        if (selectedTransformation.Code == currentTransformation.Code)
                        {
                            this.Rows[i].Cells["colCustomInsertValue"].Value = getSelectedCustomInsertValue();
                            this.Rows[i].Cells["colCustomUpdateValue"].Value = getSelectedCustomUpdateValue();
                            this.Rows[i].Cells["colCustomDeleteValue"].Value = getSelectedCustomDeleteValue();


                            modifiedRowsCount++;
                        }
                    }
                }

                cellValueChanged();

                MessageBox.Show(string.Format("{0} rows modified", modifiedRowsCount), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            toolStripMenuItemCopyCustomColumns.Text = string.Format("Copy Event Columns to all '{0}' transformations", getSelectedTransformation().Name);

            toolStripMenuItemCopy.Enabled = CanCopy;
            toolStripMenuItemCut.Enabled = CanCut;
            toolStripMenuItemPaste.Enabled = CanPaste;
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void toolStripMenuItemPaste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void toolStripMenuItemCut_Click(object sender, EventArgs e)
        {
            Cut();
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1 && this.Columns[e.ColumnIndex].Name.StartsWith("colCustom"))
            {
                Font drawFont = this.Columns[e.ColumnIndex].HeaderCell.Style.Font;
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                string text = this.Columns[e.ColumnIndex].HeaderText;

                Image img = Properties.Resources.Event16x16;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                Rectangle rec = new Rectangle(e.CellBounds.X, e.CellBounds.Y + 2, 16, 16);

                e.Graphics.DrawImage(img, rec);

                Rectangle rec2 = new Rectangle(e.CellBounds.X + 18, e.CellBounds.Y + 5, e.CellBounds.Width, e.CellBounds.Height);
                e.Graphics.DrawString(text, drawFont, drawBrush, rec2);
                e.Handled = true;

            }
            else
                base.OnCellPainting(e);
        }
    }

    public class MappingsChangedEventArgs : EventArgs
    {
        public List<MappingChangedMessage> Messages = new List<MappingChangedMessage>();
    }

    public class MappingChangedMessage
    {
        public EMappingMessageType MessageType;
        public string MessageText;

        public MappingChangedMessage(EMappingMessageType mmt, string text)
        {
            this.MessageType = mmt;
            this.MessageText = text;
        }
    }


    public class ConsolidatedColumnMapping
    {
        public string SourceColumnName;
        public ETransformationCode TransformationType;
        public int Count;
    }

    public class CopyRow
    {
        public string TransformationName;
        public string CustomInsertValue;
        public string CustomUpdateValue;
        public string CustomDeleteValue;
    }


    public enum EMappingMessageType
    {
        ERROR,
        WARNING,
        INFO
    }

}
