namespace ScdMergeWizard.Formz
{
    partial class TransformationHelpForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransformationHelpForm));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxUseMultipleTimes = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxSourceColumnDefined = new System.Windows.Forms.PictureBox();
            this.textBoxOnInsert = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxOnUpdate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxOnDelete = new System.Windows.Forms.TextBox();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUseMultipleTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSourceColumnDefined)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Can be used multiple times?";
            // 
            // pictureBoxUseMultipleTimes
            // 
            this.pictureBoxUseMultipleTimes.Location = new System.Drawing.Point(189, 22);
            this.pictureBoxUseMultipleTimes.Name = "pictureBoxUseMultipleTimes";
            this.pictureBoxUseMultipleTimes.Size = new System.Drawing.Size(22, 22);
            this.pictureBoxUseMultipleTimes.TabIndex = 1;
            this.pictureBoxUseMultipleTimes.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Needs source column defined?";
            // 
            // pictureBoxSourceColumnDefined
            // 
            this.pictureBoxSourceColumnDefined.Location = new System.Drawing.Point(189, 62);
            this.pictureBoxSourceColumnDefined.Name = "pictureBoxSourceColumnDefined";
            this.pictureBoxSourceColumnDefined.Size = new System.Drawing.Size(22, 22);
            this.pictureBoxSourceColumnDefined.TabIndex = 3;
            this.pictureBoxSourceColumnDefined.TabStop = false;
            // 
            // textBoxOnInsert
            // 
            this.textBoxOnInsert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOnInsert.Location = new System.Drawing.Point(32, 113);
            this.textBoxOnInsert.Multiline = true;
            this.textBoxOnInsert.Name = "textBoxOnInsert";
            this.textBoxOnInsert.ReadOnly = true;
            this.textBoxOnInsert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOnInsert.Size = new System.Drawing.Size(633, 67);
            this.textBoxOnInsert.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(29, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "On Insert:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 390);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(694, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label4
            // 
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(29, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "On Update:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxOnUpdate
            // 
            this.textBoxOnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOnUpdate.Location = new System.Drawing.Point(32, 205);
            this.textBoxOnUpdate.Multiline = true;
            this.textBoxOnUpdate.Name = "textBoxOnUpdate";
            this.textBoxOnUpdate.ReadOnly = true;
            this.textBoxOnUpdate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOnUpdate.Size = new System.Drawing.Size(633, 67);
            this.textBoxOnUpdate.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(29, 286);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "On Delete:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxOnDelete
            // 
            this.textBoxOnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOnDelete.Location = new System.Drawing.Point(32, 302);
            this.textBoxOnDelete.Multiline = true;
            this.textBoxOnDelete.Name = "textBoxOnDelete";
            this.textBoxOnDelete.ReadOnly = true;
            this.textBoxOnDelete.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOnDelete.Size = new System.Drawing.Size(633, 67);
            this.textBoxOnDelete.TabIndex = 9;
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDesc.Location = new System.Drawing.Point(236, 21);
            this.textBoxDesc.Multiline = true;
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.ReadOnly = true;
            this.textBoxDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDesc.Size = new System.Drawing.Size(429, 67);
            this.textBoxDesc.TabIndex = 11;
            // 
            // TransformationHelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 412);
            this.Controls.Add(this.textBoxDesc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxOnDelete);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxOnUpdate);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxOnInsert);
            this.Controls.Add(this.pictureBoxSourceColumnDefined);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBoxUseMultipleTimes);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TransformationHelpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TransformationHelpForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUseMultipleTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSourceColumnDefined)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxUseMultipleTimes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxSourceColumnDefined;
        private System.Windows.Forms.TextBox textBoxOnInsert;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxOnUpdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxOnDelete;
        private System.Windows.Forms.TextBox textBoxDesc;
    }
}