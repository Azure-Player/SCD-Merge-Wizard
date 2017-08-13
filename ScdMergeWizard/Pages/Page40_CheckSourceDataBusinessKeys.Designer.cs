namespace ScdMergeWizard.Pages
{
    partial class Page40_CheckSourceDataBusinessKeys
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Page40_CheckSourceDataBusinessKeys));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonCheckDuplicateBusinessKeys = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonClearResults = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonCheckUsingDotNetTable = new System.Windows.Forms.RadioButton();
            this.radioButtonCheckUsingSourceQuery = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ScdMergeWizard.Properties.Resources.database_check_icon;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(23, 284);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(662, 128);
            this.dataGridView1.TabIndex = 2;
            // 
            // buttonCheckDuplicateBusinessKeys
            // 
            this.buttonCheckDuplicateBusinessKeys.Image = global::ScdMergeWizard.Properties.Resources.base_checkmark_32;
            this.buttonCheckDuplicateBusinessKeys.Location = new System.Drawing.Point(286, 30);
            this.buttonCheckDuplicateBusinessKeys.Name = "buttonCheckDuplicateBusinessKeys";
            this.buttonCheckDuplicateBusinessKeys.Size = new System.Drawing.Size(106, 40);
            this.buttonCheckDuplicateBusinessKeys.TabIndex = 3;
            this.buttonCheckDuplicateBusinessKeys.Text = "Check";
            this.buttonCheckDuplicateBusinessKeys.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCheckDuplicateBusinessKeys.UseVisualStyleBackColor = true;
            this.buttonCheckDuplicateBusinessKeys.Click += new System.EventHandler(this.buttonCheckDuplicateBusinessKeys_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(347, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "If any duplicate business keys are found, they will be shown in this table:";
            // 
            // buttonClearResults
            // 
            this.buttonClearResults.Image = global::ScdMergeWizard.Properties.Resources.Delete_black_32x32;
            this.buttonClearResults.Location = new System.Drawing.Point(398, 30);
            this.buttonClearResults.Name = "buttonClearResults";
            this.buttonClearResults.Size = new System.Drawing.Size(101, 40);
            this.buttonClearResults.TabIndex = 7;
            this.buttonClearResults.Text = "Clear Results";
            this.buttonClearResults.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonClearResults.UseVisualStyleBackColor = true;
            this.buttonClearResults.Click += new System.EventHandler(this.buttonClearResults_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonCheckUsingDotNetTable);
            this.groupBox1.Controls.Add(this.buttonClearResults);
            this.groupBox1.Controls.Add(this.buttonCheckDuplicateBusinessKeys);
            this.groupBox1.Controls.Add(this.radioButtonCheckUsingSourceQuery);
            this.groupBox1.Location = new System.Drawing.Point(23, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 81);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Check Options";
            // 
            // radioButtonCheckUsingDotNetTable
            // 
            this.radioButtonCheckUsingDotNetTable.AutoSize = true;
            this.radioButtonCheckUsingDotNetTable.Location = new System.Drawing.Point(20, 53);
            this.radioButtonCheckUsingDotNetTable.Name = "radioButtonCheckUsingDotNetTable";
            this.radioButtonCheckUsingDotNetTable.Size = new System.Drawing.Size(106, 17);
            this.radioButtonCheckUsingDotNetTable.TabIndex = 1;
            this.radioButtonCheckUsingDotNetTable.Text = "Using .NET table";
            this.radioButtonCheckUsingDotNetTable.UseVisualStyleBackColor = true;
            // 
            // radioButtonCheckUsingSourceQuery
            // 
            this.radioButtonCheckUsingSourceQuery.AutoSize = true;
            this.radioButtonCheckUsingSourceQuery.Checked = true;
            this.radioButtonCheckUsingSourceQuery.Location = new System.Drawing.Point(20, 30);
            this.radioButtonCheckUsingSourceQuery.Name = "radioButtonCheckUsingSourceQuery";
            this.radioButtonCheckUsingSourceQuery.Size = new System.Drawing.Size(244, 17);
            this.radioButtonCheckUsingSourceQuery.TabIndex = 0;
            this.radioButtonCheckUsingSourceQuery.TabStop = true;
            this.radioButtonCheckUsingSourceQuery.Text = "Using SELECT against source (recommended)";
            this.radioButtonCheckUsingSourceQuery.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(23, 89);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(565, 71);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // Page40_CheckSourceDataBusinessKeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Description = "Check your input (source) data for duplicate keys";
            this.Name = "Page40_CheckSourceDataBusinessKeys";
            this.Size = new System.Drawing.Size(719, 438);
            this.Title = "Business Key Check";
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonCheckDuplicateBusinessKeys;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonClearResults;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonCheckUsingDotNetTable;
        private System.Windows.Forms.RadioButton radioButtonCheckUsingSourceQuery;
        private System.Windows.Forms.TextBox textBox1;
    }
}
