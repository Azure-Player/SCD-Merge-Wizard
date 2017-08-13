namespace ScdMergeWizard.Pages
{
    partial class Page99_Query
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
            this.myRichTextBox1 = new MergeWizard.Components.MyRichTextBox();
            this.buttonOpenInSSMS = new System.Windows.Forms.Button();
            this.buttonCopyToClipboard = new System.Windows.Forms.Button();
            this.buttonSaveQuery = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openInSSMSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ScdMergeWizard.Properties.Resources.sql_query;
            // 
            // myRichTextBox1
            // 
            this.myRichTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myRichTextBox1.EnableEditing = false;
            this.myRichTextBox1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myRichTextBox1.LineNumbersColor = System.Drawing.Color.Orange;
            this.myRichTextBox1.Location = new System.Drawing.Point(27, 85);
            this.myRichTextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.myRichTextBox1.Name = "myRichTextBox1";
            this.myRichTextBox1.Size = new System.Drawing.Size(396, 207);
            this.myRichTextBox1.TabIndex = 2;
            // 
            // buttonOpenInSSMS
            // 
            this.buttonOpenInSSMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenInSSMS.Location = new System.Drawing.Point(439, 85);
            this.buttonOpenInSSMS.Name = "buttonOpenInSSMS";
            this.buttonOpenInSSMS.Size = new System.Drawing.Size(109, 23);
            this.buttonOpenInSSMS.TabIndex = 3;
            this.buttonOpenInSSMS.Text = "Open in SSMS";
            this.buttonOpenInSSMS.UseVisualStyleBackColor = true;
            this.buttonOpenInSSMS.Click += new System.EventHandler(this.buttonOpenInSSMS_Click);
            // 
            // buttonCopyToClipboard
            // 
            this.buttonCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopyToClipboard.Location = new System.Drawing.Point(439, 114);
            this.buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            this.buttonCopyToClipboard.Size = new System.Drawing.Size(109, 23);
            this.buttonCopyToClipboard.TabIndex = 4;
            this.buttonCopyToClipboard.Text = "Copy to Clipboard";
            this.buttonCopyToClipboard.UseVisualStyleBackColor = true;
            this.buttonCopyToClipboard.Click += new System.EventHandler(this.buttonCopyToClipboard_Click);
            // 
            // buttonSaveQuery
            // 
            this.buttonSaveQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveQuery.Location = new System.Drawing.Point(439, 143);
            this.buttonSaveQuery.Name = "buttonSaveQuery";
            this.buttonSaveQuery.Size = new System.Drawing.Size(109, 23);
            this.buttonSaveQuery.TabIndex = 5;
            this.buttonSaveQuery.Text = "Save Query";
            this.buttonSaveQuery.UseVisualStyleBackColor = true;
            this.buttonSaveQuery.Click += new System.EventHandler(this.buttonSaveQuery_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInSSMSToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem,
            this.saveQueryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(172, 70);
            // 
            // openInSSMSToolStripMenuItem
            // 
            this.openInSSMSToolStripMenuItem.Name = "openInSSMSToolStripMenuItem";
            this.openInSSMSToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.openInSSMSToolStripMenuItem.Text = "Open in SSMS";
            this.openInSSMSToolStripMenuItem.Click += new System.EventHandler(this.buttonOpenInSSMS_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to Clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.buttonCopyToClipboard_Click);
            // 
            // saveQueryToolStripMenuItem
            // 
            this.saveQueryToolStripMenuItem.Name = "saveQueryToolStripMenuItem";
            this.saveQueryToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.saveQueryToolStripMenuItem.Text = "Save Query";
            this.saveQueryToolStripMenuItem.Click += new System.EventHandler(this.buttonSaveQuery_Click);
            // 
            // Page99_Query
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSaveQuery);
            this.Controls.Add(this.buttonCopyToClipboard);
            this.Controls.Add(this.buttonOpenInSSMS);
            this.Controls.Add(this.myRichTextBox1);
            this.Description = "Generated query based on your settings";
            this.Name = "Page99_Query";
            this.Title = "Query";
            this.Controls.SetChildIndex(this.myRichTextBox1, 0);
            this.Controls.SetChildIndex(this.buttonOpenInSSMS, 0);
            this.Controls.SetChildIndex(this.buttonCopyToClipboard, 0);
            this.Controls.SetChildIndex(this.buttonSaveQuery, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MergeWizard.Components.MyRichTextBox myRichTextBox1;
        private System.Windows.Forms.Button buttonOpenInSSMS;
        private System.Windows.Forms.Button buttonCopyToClipboard;
        private System.Windows.Forms.Button buttonSaveQuery;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openInSSMSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveQueryToolStripMenuItem;
    }
}
