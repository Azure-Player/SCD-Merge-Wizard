namespace ScdMergeWizard.Pages
{
    partial class Page20_UserVariables
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvUserFields = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeleteVariable = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonTestSql = new System.Windows.Forms.Button();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.buttonDeleteRow = new System.Windows.Forms.Button();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDefinition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserFields)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ScdMergeWizard.Properties.Resources.Debug_Variable_icon;
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBox1.Size = new System.Drawing.Size(100, 73);
            // 
            // dgvUserFields
            // 
            this.dgvUserFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUserFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colDataType,
            this.colDefinition});
            this.dgvUserFields.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvUserFields.Location = new System.Drawing.Point(27, 142);
            this.dgvUserFields.Margin = new System.Windows.Forms.Padding(4);
            this.dgvUserFields.Name = "dgvUserFields";
            this.dgvUserFields.Size = new System.Drawing.Size(587, 256);
            this.dgvUserFields.TabIndex = 4;
            this.dgvUserFields.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvUserFields_CellMouseDown);
            this.dgvUserFields.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserFields_CellValueChanged);
            this.dgvUserFields.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvUserFields_RowsAdded);
            this.dgvUserFields.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvUserFields_RowsRemoved);
            this.dgvUserFields.SelectionChanged += new System.EventHandler(this.dgvUserFields_SelectionChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteVariable});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(182, 28);
            // 
            // toolStripMenuItemDeleteVariable
            // 
            this.toolStripMenuItemDeleteVariable.Image = global::ScdMergeWizard.Properties.Resources.the_delete_icon;
            this.toolStripMenuItemDeleteVariable.Name = "toolStripMenuItemDeleteVariable";
            this.toolStripMenuItemDeleteVariable.Size = new System.Drawing.Size(181, 24);
            this.toolStripMenuItemDeleteVariable.Text = "Delete Variable";
            this.toolStripMenuItemDeleteVariable.Click += new System.EventHandler(this.buttonDeleteRow_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 107);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Define your variables:";
            // 
            // buttonTestSql
            // 
            this.buttonTestSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTestSql.Location = new System.Drawing.Point(636, 142);
            this.buttonTestSql.Margin = new System.Windows.Forms.Padding(4);
            this.buttonTestSql.Name = "buttonTestSql";
            this.buttonTestSql.Size = new System.Drawing.Size(100, 28);
            this.buttonTestSql.TabIndex = 0;
            this.buttonTestSql.Text = "Test";
            this.buttonTestSql.UseVisualStyleBackColor = true;
            this.buttonTestSql.Click += new System.EventHandler(this.buttonTestSql_Click);
            // 
            // buttonPreview
            // 
            this.buttonPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPreview.Location = new System.Drawing.Point(636, 177);
            this.buttonPreview.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(100, 28);
            this.buttonPreview.TabIndex = 6;
            this.buttonPreview.Text = "Preview...";
            this.buttonPreview.UseVisualStyleBackColor = true;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // buttonDeleteRow
            // 
            this.buttonDeleteRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteRow.Location = new System.Drawing.Point(636, 332);
            this.buttonDeleteRow.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDeleteRow.Name = "buttonDeleteRow";
            this.buttonDeleteRow.Size = new System.Drawing.Size(100, 65);
            this.buttonDeleteRow.TabIndex = 7;
            this.buttonDeleteRow.Text = "Delete Selected Variable";
            this.buttonDeleteRow.UseVisualStyleBackColor = true;
            this.buttonDeleteRow.Click += new System.EventHandler(this.buttonDeleteRow_Click);
            // 
            // colName
            // 
            this.colName.HeaderText = "Variable Name";
            this.colName.Name = "colName";
            this.colName.Width = 200;
            // 
            // colDataType
            // 
            this.colDataType.HeaderText = "Data Type";
            this.colDataType.Name = "colDataType";
            this.colDataType.Width = 110;
            // 
            // colDefinition
            // 
            this.colDefinition.HeaderText = "Definition";
            this.colDefinition.Name = "colDefinition";
            this.colDefinition.Width = 350;
            // 
            // Page20_UserVariables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDeleteRow);
            this.Controls.Add(this.buttonPreview);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonTestSql);
            this.Controls.Add(this.dgvUserFields);
            this.Description = "Define custom variables which can be used in the transformations";
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Page20_UserVariables";
            this.Size = new System.Drawing.Size(779, 446);
            this.Title = "User Variables";
            this.Controls.SetChildIndex(this.dgvUserFields, 0);
            this.Controls.SetChildIndex(this.buttonTestSql, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.buttonPreview, 0);
            this.Controls.SetChildIndex(this.buttonDeleteRow, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserFields)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUserFields;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonTestSql;
        private System.Windows.Forms.Button buttonPreview;
        private System.Windows.Forms.Button buttonDeleteRow;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteVariable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDefinition;
    }
}
