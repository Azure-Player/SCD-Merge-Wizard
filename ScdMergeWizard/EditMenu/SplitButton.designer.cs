namespace ExoticControls
{
	partial class SplitButton
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplitButton));
			this.SplitButtonDropDown = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SplitButtonDemoToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SplitButtonImages = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// SplitButtonDropDown
			// 
			this.SplitButtonDropDown.Name = "SplitButtonDropDown";
			this.SplitButtonDropDown.Size = new System.Drawing.Size(189, 76);
			this.SplitButtonDropDown.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.SplitButtonDropDown_Closed);
			this.SplitButtonDropDown.Opening += new System.ComponentModel.CancelEventHandler(this.SplitButtonDropDown_Opening);
			this.SplitButtonDropDown.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.SplitButtonDropDown_ItemClicked);
			// 
			// SplitButtonImages
			// 
			this.SplitButtonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SplitButtonImages.ImageStream")));
			this.SplitButtonImages.TransparentColor = System.Drawing.Color.Transparent;
			this.SplitButtonImages.Images.SetKeyName(0, "Normal");
			this.SplitButtonImages.Images.SetKeyName(1, "Hover");
			this.SplitButtonImages.Images.SetKeyName(2, "Clicked");
			this.SplitButtonImages.Images.SetKeyName(3, "Disabled");
			// 
			// SplitButton
			// 
			this.ContextMenuStrip = this.SplitButtonDropDown;
			this.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ImageKey = "Normal";
			this.ImageList = this.SplitButtonImages;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip SplitButtonDemoToolTip;
		private System.Windows.Forms.ImageList SplitButtonImages;
		private System.Windows.Forms.ContextMenuStrip SplitButtonDropDown;
	}
}
