namespace ScdMergeWizard.Pages
{
    partial class Page11_TargetConnection
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
            this.rtbTgtConnStr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxTgtTableOrViewName = new ScdMergeWizard.Components.ImageComboBox();
            this.buttonPreviewData = new System.Windows.Forms.Button();
            this.buttonEditConnectionString = new System.Windows.Forms.Button();
            this.labelFilter = new System.Windows.Forms.Label();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.btnEditCnn = new ExoticControls.SplitButton();
            this.cmsEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditOld = new System.Windows.Forms.ToolStripMenuItem();
            this.lilCopyFromSource = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cmsEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ScdMergeWizard.Properties.Resources.database_arrow_left_icon;
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            // 
            // rtbTgtConnStr
            // 
            this.rtbTgtConnStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbTgtConnStr.Location = new System.Drawing.Point(23, 96);
            this.rtbTgtConnStr.Multiline = true;
            this.rtbTgtConnStr.Name = "rtbTgtConnStr";
            this.rtbTgtConnStr.Size = new System.Drawing.Size(442, 53);
            this.rtbTgtConnStr.TabIndex = 6;
            this.rtbTgtConnStr.TextChanged += new System.EventHandler(this.rtbTgtConnStr_TextChanged);
            this.rtbTgtConnStr.Leave += new System.EventHandler(this.rtbTgtConnStr_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "OLE DB Connection string:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Table, View or Synonym:";
            // 
            // cbxTgtTableOrViewName
            // 
            this.cbxTgtTableOrViewName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTgtTableOrViewName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbxTgtTableOrViewName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTgtTableOrViewName.FormattingEnabled = true;
            this.cbxTgtTableOrViewName.Location = new System.Drawing.Point(23, 178);
            this.cbxTgtTableOrViewName.Name = "cbxTgtTableOrViewName";
            this.cbxTgtTableOrViewName.Size = new System.Drawing.Size(408, 21);
            this.cbxTgtTableOrViewName.TabIndex = 8;
            this.cbxTgtTableOrViewName.DropDown += new System.EventHandler(this.myComboBox1_DropDown);
            this.cbxTgtTableOrViewName.SelectedIndexChanged += new System.EventHandler(this.cbxTgtTableOrViewName_SelectedIndexChanged);
            this.cbxTgtTableOrViewName.TextChanged += new System.EventHandler(this.myComboBox1_TextChanged);
            // 
            // buttonPreviewData
            // 
            this.buttonPreviewData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPreviewData.Enabled = false;
            this.buttonPreviewData.Location = new System.Drawing.Point(471, 176);
            this.buttonPreviewData.Name = "buttonPreviewData";
            this.buttonPreviewData.Size = new System.Drawing.Size(75, 23);
            this.buttonPreviewData.TabIndex = 9;
            this.buttonPreviewData.Text = "Preview...";
            this.buttonPreviewData.UseVisualStyleBackColor = true;
            this.buttonPreviewData.Click += new System.EventHandler(this.buttonPreviewData_Click);
            // 
            // buttonEditConnectionString
            // 
            this.buttonEditConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditConnectionString.Location = new System.Drawing.Point(471, 96);
            this.buttonEditConnectionString.Name = "buttonEditConnectionString";
            this.buttonEditConnectionString.Size = new System.Drawing.Size(75, 23);
            this.buttonEditConnectionString.TabIndex = 10;
            this.buttonEditConnectionString.Text = "Edit";
            this.buttonEditConnectionString.UseVisualStyleBackColor = true;
            this.buttonEditConnectionString.Click += new System.EventHandler(this.buttonEditConnectionString_Click);
            // 
            // labelFilter
            // 
            this.labelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilter.Location = new System.Drawing.Point(176, 156);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(255, 15);
            this.labelFilter.TabIndex = 18;
            this.labelFilter.Text = "Filter: (none)";
            this.labelFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonFilter
            // 
            this.buttonFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFilter.Image = global::ScdMergeWizard.Properties.Resources.Filter2HS;
            this.buttonFilter.Location = new System.Drawing.Point(437, 176);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(28, 23);
            this.buttonFilter.TabIndex = 19;
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // btnEditCnn
            // 
            this.btnEditCnn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditCnn.ContextMenuStrip = this.cmsEdit;
            this.btnEditCnn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditCnn.ImageKey = "Normal";
            this.btnEditCnn.Location = new System.Drawing.Point(472, 96);
            this.btnEditCnn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEditCnn.Name = "btnEditCnn";
            this.btnEditCnn.Size = new System.Drawing.Size(74, 28);
            this.btnEditCnn.TabIndex = 20;
            this.btnEditCnn.Text = "Edit";
            this.btnEditCnn.UseVisualStyleBackColor = true;
            this.btnEditCnn.ButtonClick += new System.EventHandler(this.btnEditCnn_ButtonClick);
            // 
            // cmsEdit
            // 
            this.cmsEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditNew,
            this.tsmiEditOld});
            this.cmsEdit.Name = "cmsEdit";
            this.cmsEdit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmsEdit.ShowCheckMargin = true;
            this.cmsEdit.ShowImageMargin = false;
            this.cmsEdit.Size = new System.Drawing.Size(267, 48);
            // 
            // tsmiEditNew
            // 
            this.tsmiEditNew.Checked = true;
            this.tsmiEditNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiEditNew.Name = "tsmiEditNew";
            this.tsmiEditNew.Size = new System.Drawing.Size(266, 22);
            this.tsmiEditNew.Text = "Edit (.Net Framework for SQL Server)";
            this.tsmiEditNew.Click += new System.EventHandler(this.tsmiEditNew_Click);
            // 
            // tsmiEditOld
            // 
            this.tsmiEditOld.Name = "tsmiEditOld";
            this.tsmiEditOld.Size = new System.Drawing.Size(266, 22);
            this.tsmiEditOld.Text = "Edit (OLE DB Connection)";
            this.tsmiEditOld.Click += new System.EventHandler(this.tsmiEditOld_Click);
            // 
            // lilCopyFromSource
            // 
            this.lilCopyFromSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lilCopyFromSource.AutoSize = true;
            this.lilCopyFromSource.Location = new System.Drawing.Point(302, 80);
            this.lilCopyFromSource.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lilCopyFromSource.Name = "lilCopyFromSource";
            this.lilCopyFromSource.Size = new System.Drawing.Size(166, 13);
            this.lilCopyFromSource.TabIndex = 21;
            this.lilCopyFromSource.TabStop = true;
            this.lilCopyFromSource.Text = "Copy from the Source Connection";
            this.lilCopyFromSource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lilCopyFromSource_LinkClicked);
            // 
            // Page11_TargetConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lilCopyFromSource);
            this.Controls.Add(this.btnEditCnn);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.buttonEditConnectionString);
            this.Controls.Add(this.buttonPreviewData);
            this.Controls.Add(this.cbxTgtTableOrViewName);
            this.Controls.Add(this.rtbTgtConnStr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Description = "Define your data destination (target)";
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Page11_TargetConnection";
            this.Size = new System.Drawing.Size(580, 311);
            this.Title = "Target Connection";
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.rtbTgtConnStr, 0);
            this.Controls.SetChildIndex(this.cbxTgtTableOrViewName, 0);
            this.Controls.SetChildIndex(this.buttonPreviewData, 0);
            this.Controls.SetChildIndex(this.buttonEditConnectionString, 0);
            this.Controls.SetChildIndex(this.labelFilter, 0);
            this.Controls.SetChildIndex(this.buttonFilter, 0);
            this.Controls.SetChildIndex(this.btnEditCnn, 0);
            this.Controls.SetChildIndex(this.lilCopyFromSource, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cmsEdit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonPreviewData;
        public System.Windows.Forms.TextBox rtbTgtConnStr;
        public Components.ImageComboBox cbxTgtTableOrViewName;
        private System.Windows.Forms.Button buttonEditConnectionString;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.Button buttonFilter;
        private ExoticControls.SplitButton btnEditCnn;
        private System.Windows.Forms.ContextMenuStrip cmsEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditNew;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditOld;
        private System.Windows.Forms.LinkLabel lilCopyFromSource;
    }
}
