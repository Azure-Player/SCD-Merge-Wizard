namespace ScdMergeWizard.Pages
{
    partial class Page30_Transformations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Page30_Transformations));
            this.transformationGridView1 = new ScdMergeWizard.Components.TransformationGridView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colError = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.buttonStatistics = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.SCD1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transformationGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // transformationGridView1
            // 
            this.transformationGridView1.AllowUserToAddRows = false;
            this.transformationGridView1.AllowUserToDeleteRows = false;
            this.transformationGridView1.AllowUserToOrderColumns = true;
            this.transformationGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transformationGridView1.Location = new System.Drawing.Point(0, 0);
            this.transformationGridView1.Name = "transformationGridView1";
            this.transformationGridView1.Size = new System.Drawing.Size(467, 98);
            this.transformationGridView1.TabIndex = 2;
            this.transformationGridView1.MappingsChanged += new ScdMergeWizard.Components.TransformationGridView.MappingsChangedEventHandler(this.transformationGridView1_MappingsChanged);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colError});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView1.Location = new System.Drawing.Point(0, 98);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(467, 93);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // colError
            // 
            this.colError.Text = "Message";
            this.colError.Width = 400;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "109_AllAnnotations_Info_16x16_72.png");
            this.imageList1.Images.SetKeyName(1, "109_AllAnnotations_Warning_16x16_72.png");
            this.imageList1.Images.SetKeyName(2, "exclamation_red.png");
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.transformationGridView1);
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Location = new System.Drawing.Point(26, 114);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(467, 191);
            this.panel2.TabIndex = 4;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 95);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(467, 3);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // buttonStatistics
            // 
            this.buttonStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStatistics.Location = new System.Drawing.Point(512, 114);
            this.buttonStatistics.Name = "buttonStatistics";
            this.buttonStatistics.Size = new System.Drawing.Size(75, 23);
            this.buttonStatistics.TabIndex = 5;
            this.buttonStatistics.Text = "Statistics";
            this.buttonStatistics.UseVisualStyleBackColor = true;
            this.buttonStatistics.Click += new System.EventHandler(this.buttonStatistics_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Transformations (right click on the row to see options):";
            // 
            // button1
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(512, 152);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // SCD1
            // 
            this.SCD1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SCD1.Location = new System.Drawing.Point(512, 189);
            this.SCD1.Name = "SCD1";
            this.SCD1.Size = new System.Drawing.Size(75, 23);
            this.SCD1.TabIndex = 8;
            this.SCD1.Text = "SCD1";
            this.SCD1.UseVisualStyleBackColor = true;
            this.SCD1.Click += new System.EventHandler(this.SCD1_Click);
            // 
            // Page30_Transformations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SCD1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStatistics);
            this.Controls.Add(this.panel2);
            this.Description = "Define column transformations based on your business needs";
            this.Name = "Page30_Transformations";
            this.Size = new System.Drawing.Size(605, 330);
            this.Title = "Transformations";
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.buttonStatistics, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnReset, 0);
            this.Controls.SetChildIndex(this.SCD1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transformationGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.TransformationGridView transformationGridView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colError;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonStatistics;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button SCD1;
    }
}
