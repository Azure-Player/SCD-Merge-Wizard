namespace ScdMergeWizard.Pages
{
    partial class Page10_SourceConnection
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
            this.rtbSrcConnStr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbCommandText = new System.Windows.Forms.RichTextBox();
            this.rbIsCommandText = new System.Windows.Forms.RadioButton();
            this.rbIsTableOrView = new System.Windows.Forms.RadioButton();
            this.cbxSrcTableOrViewName = new ScdMergeWizard.Components.ImageComboBox();
            this.buttonPreviewData = new System.Windows.Forms.Button();
            this.buttonEditConnectionString = new System.Windows.Forms.Button();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.labelFilter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ScdMergeWizard.Properties.Resources.database_arrow_right_icon;
            // 
            // rtbSrcConnStr
            // 
            this.rtbSrcConnStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbSrcConnStr.Location = new System.Drawing.Point(23, 96);
            this.rtbSrcConnStr.Multiline = true;
            this.rtbSrcConnStr.Name = "rtbSrcConnStr";
            this.rtbSrcConnStr.Size = new System.Drawing.Size(450, 53);
            this.rtbSrcConnStr.TabIndex = 9;
            this.rtbSrcConnStr.TextChanged += new System.EventHandler(this.rtbSrcConnStr_TextChanged);
            this.rtbSrcConnStr.Leave += new System.EventHandler(this.rtbSrcConnStr_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "OLE DB Connection string:";
            // 
            // rtbCommandText
            // 
            this.rtbCommandText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbCommandText.Enabled = false;
            this.rtbCommandText.Location = new System.Drawing.Point(23, 228);
            this.rtbCommandText.Name = "rtbCommandText";
            this.rtbCommandText.Size = new System.Drawing.Size(531, 80);
            this.rtbCommandText.TabIndex = 12;
            this.rtbCommandText.Text = "";
            this.rtbCommandText.TextChanged += new System.EventHandler(this.rtbCommandText_TextChanged);
            // 
            // rbIsCommandText
            // 
            this.rbIsCommandText.AutoSize = true;
            this.rbIsCommandText.Location = new System.Drawing.Point(23, 205);
            this.rbIsCommandText.Name = "rbIsCommandText";
            this.rbIsCommandText.Size = new System.Drawing.Size(95, 17);
            this.rbIsCommandText.TabIndex = 11;
            this.rbIsCommandText.Text = "Command text:";
            this.rbIsCommandText.UseVisualStyleBackColor = true;
            this.rbIsCommandText.CheckedChanged += new System.EventHandler(this.rbCommandText_CheckedChanged);
            // 
            // rbIsTableOrView
            // 
            this.rbIsTableOrView.AutoSize = true;
            this.rbIsTableOrView.Checked = true;
            this.rbIsTableOrView.Location = new System.Drawing.Point(23, 155);
            this.rbIsTableOrView.Name = "rbIsTableOrView";
            this.rbIsTableOrView.Size = new System.Drawing.Size(142, 17);
            this.rbIsTableOrView.TabIndex = 10;
            this.rbIsTableOrView.TabStop = true;
            this.rbIsTableOrView.Text = "Table, View or Synonym:";
            this.rbIsTableOrView.UseVisualStyleBackColor = true;
            this.rbIsTableOrView.CheckedChanged += new System.EventHandler(this.rbTableOrView_CheckedChanged);
            // 
            // cbxSrcTableOrViewName
            // 
            this.cbxSrcTableOrViewName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSrcTableOrViewName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbxSrcTableOrViewName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSrcTableOrViewName.FormattingEnabled = true;
            this.cbxSrcTableOrViewName.Location = new System.Drawing.Point(23, 178);
            this.cbxSrcTableOrViewName.Name = "cbxSrcTableOrViewName";
            this.cbxSrcTableOrViewName.Size = new System.Drawing.Size(416, 21);
            this.cbxSrcTableOrViewName.TabIndex = 13;
            this.cbxSrcTableOrViewName.DropDown += new System.EventHandler(this.cbxSrcTables_DropDown);
            this.cbxSrcTableOrViewName.SelectedIndexChanged += new System.EventHandler(this.cbxSrcTableOrViewName_SelectedIndexChanged);
            this.cbxSrcTableOrViewName.TextChanged += new System.EventHandler(this.cbxSrcTables_TextChanged);
            // 
            // buttonPreviewData
            // 
            this.buttonPreviewData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPreviewData.Location = new System.Drawing.Point(479, 176);
            this.buttonPreviewData.Name = "buttonPreviewData";
            this.buttonPreviewData.Size = new System.Drawing.Size(75, 23);
            this.buttonPreviewData.TabIndex = 14;
            this.buttonPreviewData.Text = "Preview...";
            this.buttonPreviewData.UseVisualStyleBackColor = true;
            this.buttonPreviewData.Click += new System.EventHandler(this.buttonPreviewData_Click);
            // 
            // buttonEditConnectionString
            // 
            this.buttonEditConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditConnectionString.Location = new System.Drawing.Point(479, 96);
            this.buttonEditConnectionString.Name = "buttonEditConnectionString";
            this.buttonEditConnectionString.Size = new System.Drawing.Size(75, 23);
            this.buttonEditConnectionString.TabIndex = 15;
            this.buttonEditConnectionString.Text = "Edit";
            this.buttonEditConnectionString.UseVisualStyleBackColor = true;
            this.buttonEditConnectionString.Click += new System.EventHandler(this.buttonEditConnectionString_Click);
            // 
            // buttonFilter
            // 
            this.buttonFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFilter.Image = global::ScdMergeWizard.Properties.Resources.Filter2HS;
            this.buttonFilter.Location = new System.Drawing.Point(445, 176);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(28, 23);
            this.buttonFilter.TabIndex = 16;
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // labelFilter
            // 
            this.labelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilter.Location = new System.Drawing.Point(184, 157);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(255, 15);
            this.labelFilter.TabIndex = 17;
            this.labelFilter.Text = "Filter: (none)";
            this.labelFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Page10_SourceConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.buttonEditConnectionString);
            this.Controls.Add(this.buttonPreviewData);
            this.Controls.Add(this.cbxSrcTableOrViewName);
            this.Controls.Add(this.rtbSrcConnStr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbCommandText);
            this.Controls.Add(this.rbIsCommandText);
            this.Controls.Add(this.rbIsTableOrView);
            this.Description = "Define the source of your data";
            this.Name = "Page10_SourceConnection";
            this.Size = new System.Drawing.Size(588, 339);
            this.Title = "Source Connection";
            this.Controls.SetChildIndex(this.rbIsTableOrView, 0);
            this.Controls.SetChildIndex(this.rbIsCommandText, 0);
            this.Controls.SetChildIndex(this.rtbCommandText, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.rtbSrcConnStr, 0);
            this.Controls.SetChildIndex(this.cbxSrcTableOrViewName, 0);
            this.Controls.SetChildIndex(this.buttonPreviewData, 0);
            this.Controls.SetChildIndex(this.buttonEditConnectionString, 0);
            this.Controls.SetChildIndex(this.buttonFilter, 0);
            this.Controls.SetChildIndex(this.labelFilter, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPreviewData;
        public System.Windows.Forms.TextBox rtbSrcConnStr;
        public System.Windows.Forms.RadioButton rbIsTableOrView;
        public Components.ImageComboBox cbxSrcTableOrViewName;
        public System.Windows.Forms.RichTextBox rtbCommandText;
        public System.Windows.Forms.RadioButton rbIsCommandText;
        private System.Windows.Forms.Button buttonEditConnectionString;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.Label labelFilter;
    }
}
