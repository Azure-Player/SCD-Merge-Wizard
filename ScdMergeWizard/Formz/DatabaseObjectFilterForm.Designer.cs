namespace ScdMergeWizard.Formz
{
    partial class DatabaseObjectFilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseObjectFilterForm));
            this.checkBoxTables = new System.Windows.Forms.CheckBox();
            this.checkBoxViews = new System.Windows.Forms.CheckBox();
            this.checkBoxSynonyms = new System.Windows.Forms.CheckBox();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBoxTables
            // 
            this.checkBoxTables.Checked = true;
            this.checkBoxTables.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTables.Image = global::ScdMergeWizard.Properties.Resources.TableHS;
            this.checkBoxTables.Location = new System.Drawing.Point(35, 79);
            this.checkBoxTables.Name = "checkBoxTables";
            this.checkBoxTables.Size = new System.Drawing.Size(83, 24);
            this.checkBoxTables.TabIndex = 3;
            this.checkBoxTables.Text = "Tables";
            this.checkBoxTables.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.checkBoxTables.UseVisualStyleBackColor = true;
            // 
            // checkBoxViews
            // 
            this.checkBoxViews.Checked = true;
            this.checkBoxViews.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxViews.Image = global::ScdMergeWizard.Properties.Resources.view;
            this.checkBoxViews.Location = new System.Drawing.Point(141, 79);
            this.checkBoxViews.Name = "checkBoxViews";
            this.checkBoxViews.Size = new System.Drawing.Size(76, 24);
            this.checkBoxViews.TabIndex = 4;
            this.checkBoxViews.Text = "Views";
            this.checkBoxViews.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.checkBoxViews.UseVisualStyleBackColor = true;
            // 
            // checkBoxSynonyms
            // 
            this.checkBoxSynonyms.Checked = true;
            this.checkBoxSynonyms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSynonyms.Image = global::ScdMergeWizard.Properties.Resources.synonym;
            this.checkBoxSynonyms.Location = new System.Drawing.Point(247, 79);
            this.checkBoxSynonyms.Name = "checkBoxSynonyms";
            this.checkBoxSynonyms.Size = new System.Drawing.Size(96, 24);
            this.checkBoxSynonyms.TabIndex = 5;
            this.checkBoxSynonyms.Text = "Synonyms";
            this.checkBoxSynonyms.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.checkBoxSynonyms.UseVisualStyleBackColor = true;
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(110, 18);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(233, 20);
            this.textBoxFilter.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Object name filter:";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(268, 126);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(106, 126);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Show following objects:";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(187, 126);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 7;
            this.buttonClear.Text = "Reset";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // DatabaseObjectFilterForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(369, 168);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFilter);
            this.Controls.Add(this.checkBoxSynonyms);
            this.Controls.Add(this.checkBoxViews);
            this.Controls.Add(this.checkBoxTables);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatabaseObjectFilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Object Filter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxTables;
        private System.Windows.Forms.CheckBox checkBoxViews;
        private System.Windows.Forms.CheckBox checkBoxSynonyms;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonClear;

    }
}