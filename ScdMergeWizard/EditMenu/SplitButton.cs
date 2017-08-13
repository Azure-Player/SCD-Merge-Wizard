using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace ExoticControls
{
	/// <summary>
	/// Note.  This control has two challanges to overcome
	/// 1	allow the client to bind to the drop down click events
	/// 2	have design time support.  This design time support will
	///	be covered in a subsequent article
	/// </summary>
	public partial class SplitButton : Button
	{
		#region Fields

		/// <summary>
		/// Toggle between button-part click as action causing vs. click 
		/// to displays the dropdown list.
		/// </summary>
		private bool _AlwaysDropDown = false;

		/// <summary>
		/// Mouse hover causes the image of the split-part to change to hover 
		/// image and drop down occurrs on button click.
		/// </summary>
		private bool _AlwaysHoverChange = false;

		/// <summary>
		/// One potential problem with drop down menu items is the fact that 
		/// you do not have an after the fact indication to which item was 
		/// pressed.  This is an attempt to come up with such a solution.
		/// 
		/// After the user selected an option from the drop down, this flag 
		/// will instruct the software weather to switch the meaning of the 
		/// button to the just clicked menu item or to keep the default, original
		/// value.
		/// </summary>
		private bool _persistDropDownName = false;

		private bool _CalculateSplitRect = true;
		private bool _FillSplitHeight = true;
		private int _SplitHeight = 0;
		private int _SplitWidth = 0;

		/// <summary>
		/// Store the 4 possible image names (5 image states).  _HoverImage and 
		/// _FocusedImage share the same image state.
		/// </summary>
		private string _NormalImage;
		private string _HoverImage;
		private string _ClickedImage;
		private string _DisabledImage;
		private string _FocusedImage;

		/// <summary>
		/// Images are housed here
		/// </summary>
		private ImageList _DefaultSplitImages;

		/// <summary>
		/// A dictionary allowing the events to be tied to the drop down list.
		/// 
		/// The first generic type, string, is the identifier of the event the key 
		/// and we will make it the text display of the drop down item.  The second 
		/// generic type, EventHandler<EventArgs> is the EventHandler for the event.
		/// 
		/// This is the mechanism through which we keep the control's interface to 
		/// its client without exposing the ContextMenuStrip itself.
		/// </summary>
		private Dictionary<string, EventHandler> _dropDownsEventHandlers = new Dictionary<string, EventHandler>();

		/// <summary>
		/// I am using the timers to keep track of the open/close state of the
		/// drop-down menu.
		/// </summary>
		static private readonly DateTime ZeroTime = new DateTime(0);
		private DateTime ClosedTime = ZeroTime;

		#endregion

		#region Events

		[Browsable(true)]
		[Category("Action")]
		[Description("Occurs when the button part of the SplitButton is clicked.")]
		public event EventHandler ButtonClick;

		#endregion

		#region Construction

		public SplitButton()
		{
			InitializeComponent();
		}

		#endregion

		#region Helper Methods

		private void InitDefaultSplitImages()
		{
			_NormalImage = "Normal";
			_HoverImage = "Hover";
			_ClickedImage = "Clicked";
			_DisabledImage = "Disabled";
			_FocusedImage = "Hover";
			ImageKey = "Normal";
			InitDefaultSplitImages(false);
		}

		private void InitDefaultSplitImages(bool refresh)
		{
			if (string.IsNullOrEmpty(_NormalImage)) _NormalImage = "Normal";
			if (string.IsNullOrEmpty(_HoverImage)) _HoverImage = "Hover";
			if (string.IsNullOrEmpty(_ClickedImage)) _ClickedImage = "Clicked";
			if (string.IsNullOrEmpty(_DisabledImage)) _DisabledImage = "Disabled";
			if (string.IsNullOrEmpty(_FocusedImage)) _FocusedImage = "Hover";

			if (_DefaultSplitImages == null)
				_DefaultSplitImages = new ImageList();

			if (_DefaultSplitImages.Images.Count == 0 || refresh)
			{
				if (_DefaultSplitImages.Images.Count > 0)
					_DefaultSplitImages.Images.Clear();

				try
				{
					int w;		// Width
					int h;		// Height

					if (!_CalculateSplitRect && _SplitWidth > 0)
						w = _SplitWidth;
					else
						w = 18;

					if (!CalculateSplitRect && SplitHeight > 0)
						h = SplitHeight;
					else
						h = Height;
					h -= 8;

					_DefaultSplitImages.ImageSize = new Size(w, h);

					//
					// Middles
					//
					int mw = w / 2 + (w % 2);
					int mh = h / 2;

					//
					// Draw images and place them in the _DefaultSplitImages
					// class.
					//
					using (SolidBrush fBrush = new SolidBrush(ForeColor))
					{
						//
						// Normal image
						//
						Bitmap imgN = new Bitmap(w, h);
						using (Graphics g = Graphics.FromImage(imgN))
						{
							g.CompositingQuality = CompositingQuality.HighQuality;
							g.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, h - 2));
							g.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, h));
							g.FillPolygon(fBrush, new Point[] { new Point(mw - 2, mh - 1), new Point(mw + 3, mh - 1), new Point(mw, mh + 2) });
						}
						_DefaultSplitImages.Images.Add(_NormalImage, imgN);

						//
						// Hover image
						//
						Bitmap imgH = new Bitmap(w, h);
						using (Graphics g = Graphics.FromImage(imgH))
						{
							g.CompositingQuality = CompositingQuality.HighQuality;
							g.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, h - 2));
							g.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, h));
							g.FillPolygon(fBrush, new Point[] { new Point(mw - 3, mh - 2), new Point(mw + 4, mh - 2), new Point(mw, mh + 2) });
						}
						_DefaultSplitImages.Images.Add(_HoverImage, imgH);

						//
						// Clicked image
						//
						Bitmap imgC = new Bitmap(w, h);
						using (Graphics g = Graphics.FromImage(imgC))
						{
							g.CompositingQuality = CompositingQuality.HighQuality;
							g.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, h - 2));
							g.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, h));
							g.FillPolygon(fBrush, new Point[] { new Point(mw - 2, mh - 1), new Point(mw + 3, mh - 1), new Point(mw, mh + 2) });
						}
						_DefaultSplitImages.Images.Add(_ClickedImage, imgC);

						//
						// Focused image
						//
						Bitmap imgF = new Bitmap(w, h);
						using (Graphics g = Graphics.FromImage(imgF))
						{
							g.CompositingQuality = CompositingQuality.HighQuality;
							g.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, h - 2));
							g.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, h));
							g.FillPolygon(fBrush, new Point[] { new Point(mw - 2, mh - 1), new Point(mw + 3, mh - 1), new Point(mw, mh + 2) });
						}
						_DefaultSplitImages.Images.Add(_FocusedImage, imgF);
					}

					//
					// Disabled image.  Gets a different brush
					//
					using (SolidBrush sBrush = new SolidBrush(SystemColors.GrayText))
					{
						Bitmap imgD = new Bitmap(w, h);
						using (Graphics g = Graphics.FromImage(imgD))
						{
							g.CompositingQuality = CompositingQuality.HighQuality;
							g.DrawLine(SystemPens.GrayText, new Point(1, 1), new Point(1, h - 2));
							g.FillPolygon(sBrush, new Point[] { new Point(mw - 2, mh - 1), new Point(mw + 3, mh - 1), new Point(mw, mh + 2) });
						}
						_DefaultSplitImages.Images.Add(_DisabledImage, imgD);
					}
				}
				catch (Exception ex)
				{
					// eat up the exception
					System.Diagnostics.Debug.WriteLine(string.Format("Exception in InitDefaultSplitImages(refresh:={0}).  Exception = {1}", refresh, ex.ToString()));
				}
			}
		}

		private void SetSplitImage(string imageName)
		{
			if (imageName != null && ImageList != null && ImageList.Images.ContainsKey(imageName))
			{
				ImageKey = imageName;
			}
		}

		private bool IsMouseInSplit()
		{
			Rectangle splitRect = GetImageRect(_NormalImage);

			if (!_CalculateSplitRect)
			{
				splitRect.Width = _SplitWidth;
				splitRect.Height = _SplitHeight;
			}

			return splitRect.Contains(PointToClient(MousePosition));
		}

		private Rectangle GetImageRect(string imageKey)
		{
			Image currImg = GetImage(imageKey);
			if (currImg == null)
				return Rectangle.Empty;

			int x = 0;						// Composing the return rectangle
			int y = 0;						// Composing the return rectagle
			int w = currImg.Width + 1;
			int h = currImg.Height + 1;

			if (w > Width)
				w = Width;

			if (h > Width)
				h = Width;

			switch (ImageAlign)
			{
				//
				//	Top alignment
				//
				case ContentAlignment.TopLeft:
					x = 0;
					y = 0;
					break;

				case ContentAlignment.TopCenter:
					x = (Width - w) / 2;
					y = 0;
					x += (Width - w) % 2;
					break;

				case ContentAlignment.TopRight:
					x = Width - w;
					y = 0;
					break;

				//
				// Middle alignment
				//
				case ContentAlignment.MiddleLeft:
					x = 0;
					y = (Height - h) / 2;
					y += (Height - h) % 2;
					break;

				case ContentAlignment.MiddleCenter:
					x = (Width - w) / 2;
					x += (Width - w) % 2;
					y = (Height - h) / 2;
					y += (Height - h) % 2;
					break;

				case ContentAlignment.MiddleRight:
					x = Width - w;
					y = (Height - h) / 2;
					y += (Height - h) % 2;
					break;

				//
				// Bottom
				//
				case ContentAlignment.BottomLeft:
					x = 0;
					y = Height - h;
					y += (Height - h) % 2;
					break;

				case ContentAlignment.BottomCenter:
					x = (Width - w) / 2;
					x += (Width - w) % 2;
					y = Height - h;
					break;

				case ContentAlignment.BottomRight:
					x = Width - w;
					y = Height - h;
					break;
			}

			if (_FillSplitHeight && h < Height)
				h = Height;

			if (x > 0)
				x -= 1;

			if (y > 0)
				y -= 1;

			return new Rectangle(x, y, w, h);
		}

		private Image GetImage(string imageName)
		{
			if (ImageList != null && ImageList.Images.ContainsKey(imageName))
			{
				return ImageList.Images[imageName];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Notice the NoInlining decoration of the method as a mechanism for 
		/// preventing the optimized compiler from optimizing this call away.
		/// See article for further discussion.
		/// </summary>
		/// <param name="evHadlr"></param>
		/// <param name="ea"></param>
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void EventFire(EventHandler evntHndlr, EventArgs ea)
		{
			// Make sure that the handler has methods bound to it.
			if (evntHndlr == null)
				return;

			// Iterate through the methods attached to the handler:
			// 1	If an exception is thrown, swallow it
			// 2	Make sure that Contorl-Invoke is used if appropriate.
			int i = 0;
			foreach (Delegate del in evntHndlr.GetInvocationList())
			{
				try
				{
					//
					//	syncr is more than likely a control through it could be 
					// any class that supports ISynchronizeInvoke interface
					//
					ISynchronizeInvoke syncr = del.Target as ISynchronizeInvoke;
					if (syncr == null)
					{
						//
						// If del.Target does not represent a control (or a class that 
						// requires synchronization) then invoke the event as usual.
						//
						evntHndlr.DynamicInvoke(new object[] { this, ea });
					}
					else if (syncr.InvokeRequired)
					{
						//
						// syncr represents a control and invoke is required so 
						// use the syncr's invoke (or Control-invoke).
						//
						syncr.Invoke(evntHndlr, new object[] { this, ea });
					}
					else
					{
						//
						// syncr represents a control but invoke on the control 
						// is not required.  This means that we are on the UI thread
						// of that control.
						//
						evntHndlr.DynamicInvoke(new object[] { this, ea });
					}
				}
				catch (Exception ex)
				{
					//
					// Eat the exception
					//
					System.Diagnostics.Debug.WriteLine(string.Format("SplitButton failed delegate call {0}.  Exception {1}", i, ex.ToString()));
				}

				++i;
			}
		}

		/// <summary>
		/// Upon a click on the split part of the button the default behavior 
		/// depends on the current state:
		/// 1 if state of the drop-down is closed then the click will simply 
		///   open the drop down menu
		/// 2 if the state is open, then the click on the split part will first 
		///   close the drop down menu then reopen it.
		/// 
		/// Therefore, if the drop-down was open, to begin with, the time between 
		/// close and re-open is very short.  On my machine it is between 25 - 218 
		/// milli-seconds.  As such we can capture this time and prevent a reopen.
		///
		/// I was tempted create a private member variable, 
		/// _isDropDownMenuShowing, and a corresponding public Property,
		/// IsDropDownMenuShowing.  Then use it to Show/Close the 
		/// ConextMenuStrip's drop-down menu
		/// like so:
		///		if (_isDropDownMenuShowing)
		///			ContextMenuStrip.Close();
		///		else
		///			ContextMenuStrip.Show(this, new Point(0, Height));
		///
		/// It turns out to be a bad idea.  Let's review the callback sequence in 
		/// the two scenarios:
		/// Scenario 1 menu-drop down is closed (not showing)
		///		1	_isDropDownMenuShowing == false
		///		2	OnMouseUp() is fired and therefore ContextMenuStrip.Show() will 
		///			show the drop-down.
		/// all is well--this is the desired result.
		///
		/// Scenario 2 menu-drop down is open and client clicked on 
		/// the triangle icon
		///		1	_isDropDownMenuShowing == true
		///		2	system first closes the drop down calling the
		///			SplitButtonDropDown_Closed()
		///		3	now: _isDropDownMenuShowing == false
		///		4	system calls OnMouseUp() which will run the ContextMenuStrip.Show() 
		///			function -- NOT DESIRED BEHAVIOR
		///		5	system calls SplitButtonDropDown_Opening()
		///		6	now: _isDropDownMenuShowing == true
		///
		/// Therefore we need to use the timer here too and I deem it
		/// too risky to use such a member/property _isDropDownMenuShowing/
		/// IsDropDownMenuShowing.  
		/// </summary>
		/// <returns></returns>
		private bool IsTooSoonAfterCloseMenuDropDown()
		{
			const int TOO_CLOSE_IN_MILLISECONDS = 300;

			//
			//	The drop-down is open or no timer started therefore
			// we are not too close to the Close of the drop-down
			// menu.
			//
			if (ClosedTime == ZeroTime)
				return false;

			if (DateTime.Now.Subtract(ClosedTime).Milliseconds < TOO_CLOSE_IN_MILLISECONDS)
			{
				return true;
			}

			return false;
		}

		#endregion

		#region Properties Exposing States

		[Category("Split Button")]
		[Description("Indicates whether the SplitButton always shows the drop down menu even when the button part of the SplitButton is clicked.")]
		[DefaultValue(false)]
		public bool AlwaysDropDown
		{
			get { return _AlwaysDropDown; }
			set { _AlwaysDropDown = value; }
		}

		[Category("Split Button")]
		[Description("Indicates whether the SplitButton always shows the Hover image status in the split part even when the button part of the SplitButton is hovered.")]
		[DefaultValue(false)]
		public bool AlwaysHoverChange
		{
			get { return _AlwaysHoverChange; }
			set { _AlwaysHoverChange = value; }
		}

		[Category("Split Button")]
		[Description("After the user selects an action from the drop down, this flag will dictate weather to change the button's look and feel to persist this selection or keep the button look and feel unaffected.")]
		[DefaultValue(false)]
		public bool PersistDropDownName
		{
			get { return _persistDropDownName; }
			set { _persistDropDownName = value; }
		}

		[Category("Split Button")]
		[Description("Indicates whether the split rectangle must be calculated (basing on Split image size)")]
		[DefaultValue(true)]
		public bool CalculateSplitRect
		{
			get { return _CalculateSplitRect; }
			set
			{
				bool flag1 = _CalculateSplitRect;
				_CalculateSplitRect = value;

				if (flag1 != _CalculateSplitRect)
				{
					if (_SplitWidth > 0 && _SplitHeight > 0)
					{
						InitDefaultSplitImages(true);
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [fill split height].
		/// </summary>
		/// <value><c>true</c> if [fill split height]; otherwise, <c>false</c>.</value>
		[Category("Split Button")]
		[Description("Indicates whether the split height must be filled to the button height even if the split image height is lower.")]
		[DefaultValue(true)]
		public bool FillSplitHeight
		{
			get { return _FillSplitHeight; }
			set { _FillSplitHeight = value; }
		}

		[Category("Split Button")]
		[Description("The split height (ignored if CalculateSplitRect is setted to true).")]
		[DefaultValue(0)]
		public int SplitHeight
		{
			get { return _SplitHeight; }
			set
			{
				_SplitHeight = value;

				if (!_CalculateSplitRect)
				{
					if (_SplitWidth > 0 && _SplitHeight > 0)
					{
						InitDefaultSplitImages(true);
					}
				}
			}
		}

		[Category("Split Button")]
		[Description("The split width (ignored if CalculateSplitRect is setted to true).")]
		[DefaultValue(0)]
		public int SplitWidth
		{
			get { return _SplitWidth; }
			set
			{
				_SplitWidth = value;

				if (!_CalculateSplitRect)
				{
					if (_SplitWidth > 0 && _SplitHeight > 0)
					{
						InitDefaultSplitImages(true);
					}
				}
			}
		}

		[Category("Split Button Images")]
		[Description("The Normal status image name in the ImageList, corresponding to the image name.")]
		[DefaultValue("Normal")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(ImageKeyConverter))]
		public string NormalImage
		{
			get { return _NormalImage; }
			set { _NormalImage = value; }
		}

		[Category("Split Button Images")]
		[Description("The Hover status image name in the ImageList.")]
		[DefaultValue("Hover")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(ImageKeyConverter))]
		public string HoverImage
		{
			get { return _HoverImage; }
			set { _HoverImage = value; }
		}

		[Category("Split Button Images")]
		[Description("The Clicked status image name in the ImageList.")]
		[DefaultValue("Clicked")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(ImageKeyConverter))]
		public string ClickedImage
		{
			get { return _ClickedImage; }
			set { _ClickedImage = value; }
		}

		[Category("Split Button Images")]
		[Description("The Disabled status image name in the ImageList.")]
		[DefaultValue("Disabled")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(ImageKeyConverter))]
		public string DisabledImage
		{
			get { return _DisabledImage; }
			set { _DisabledImage = value; }
		}

		[Category("Split Button Images")]
		[Description("The Focused status image name in the ImageList.")]
		[DefaultValue("Hover")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(ImageKeyConverter))]
		public string FocusedImage
		{
			get { return _FocusedImage; }
			set { _FocusedImage = value; }
		}

		#endregion

		#region Overridable Methods

		protected override void OnCreateControl()
		{
			InitDefaultSplitImages();

			if (ImageList == null)
				ImageList = _DefaultSplitImages;

			if (Enabled)
				SetSplitImage(_NormalImage);
			else
				SetSplitImage(_DisabledImage);

			base.OnCreateControl();
		}

		protected override void OnMouseMove(MouseEventArgs mevent)
		{
			if (Enabled)
			{
				if (_AlwaysDropDown || _AlwaysHoverChange || IsMouseInSplit())
				{
					SetSplitImage(_HoverImage);
				}
				else
				{
					SetSplitImage(_NormalImage);
				}
			}

			base.OnMouseMove(mevent);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			if (Enabled)
				SetSplitImage(_NormalImage);

			base.OnMouseLeave(e);
		}

		protected override void OnMouseDown(MouseEventArgs mevent)
		{
			if (Enabled)
			{
				if (_AlwaysDropDown || IsMouseInSplit())
				{
					SetSplitImage(_ClickedImage);
				}
				else
				{
					SetSplitImage(_NormalImage);
				}
			}

			base.OnMouseDown(mevent);
		}

		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (Enabled)
			{
				if (_AlwaysDropDown || _AlwaysHoverChange || IsMouseInSplit())
				{
					SetSplitImage(_HoverImage);

					if (ContextMenuStrip != null && ContextMenuStrip.Items.Count > 0)
					{
						if (!IsTooSoonAfterCloseMenuDropDown())
							ContextMenuStrip.Show(this, new Point(0, Height));
					}
				}
				else
				{
					SetSplitImage(_NormalImage);
				}
			}

			base.OnMouseUp(mevent);
		}

		protected override void OnEnabledChanged(EventArgs e)
		{
			if (Enabled)
			{
				if (IsMouseInSplit())
					SetSplitImage(_HoverImage);
				else
					SetSplitImage(_NormalImage);
			}
			else
			{
				SetSplitImage(_DisabledImage);
			}

			base.OnEnabledChanged(e);
		}

		protected override void OnGotFocus(EventArgs e)
		{
			if (Enabled)
				SetSplitImage(_FocusedImage);

			base.OnGotFocus(e);
		}

		protected override void OnLostFocus(EventArgs e)
		{
			if (Enabled)
				SetSplitImage(_NormalImage);

			base.OnLostFocus(e);
		}

		protected override void OnClick(EventArgs e)
		{
			//
			// Regular mouse button click occurs whenever the button is clicked
			//
			base.OnClick(e);

			if (!IsMouseInSplit() && !_AlwaysDropDown)
			{
				//
				// If we are not in the split part of the button, namely we are in the 
				// Button-Part of the button then fire the ButtonClick event.
				//
				// See text writeup for the logic behind using
				// this EventFire() function as opposed to the "customary:"
				//		if (ButtonClick != null)
				//			ButtonClick(this, e);
				//
				EventFire(ButtonClick, e);
			}
		}

		#endregion

		#region Additional Interface Methods

		/// <summary>
		/// Purpose: Clears the dropdown items and clears the EventHandler list
		/// corresponding to these items.
		/// </summary>
		public void ClearDropDownItems()
		{
			SplitButtonDropDown.Items.Clear();
			_dropDownsEventHandlers = new Dictionary<string, EventHandler>();
		}

		/// <summary>
		/// Purpose: Add an item to the drop down menu and bind an appropriate
		/// the event handler.  
		/// </summary>
		/// <param name="name">the text display of the drop down item</param>
		/// <param name="handler">
		/// The event handler for that drop down item.  The client will provide 
		/// a function attached to the handler with signature: 
		///		"private void [methodname](object sender, EventArgs e)
		/// </param>
		public void AddDropDownItemAndHandle(string text, EventHandler handler)
		{
			// Add item to menu
			SplitButtonDropDown.Items.Add(text);

			//
			// Add handler
			// Note.  This choice means that if we have two menu items sharing the
			// same display text the first display will dictate the behavior of both
			// items.  If this is intentional you may call the Add functions from
			// within the client of the button as follows:
			//			splitButton1.AddDropDownItemAndHandle("display 1", Handler1);
			//			...
			//			splitButton1.AddDropDownItemAndHandle("display 1", null);
			//
			if (!_dropDownsEventHandlers.ContainsKey(text))
			    _dropDownsEventHandlers.Add(text, handler);
		}

		#endregion

		#region Internal Events Handling

		/// <summary>
		/// Note the implementation where the one event, e (the input drop-down 
		/// event), is being translated into another, adaptorEvent which is the 
		/// event bound by the client.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SplitButtonDropDown_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			//
			//	Close the drop down first
			//
			SplitButtonDropDown.Close();

			//
			// Translate the ItemClicked, event that was just fired, to the event the user bound
			// its handling to which is in _dropDownsEventHandlers[<name of the drop down>]
			//
			string textDisplay = e.ClickedItem.Text;
			EventHandler adaptorEvent = _dropDownsEventHandlers[textDisplay];

			//
			// Fire the new event
			//
			EventFire(adaptorEvent, EventArgs.Empty);

			//
			// If persisting is necessary, change the display of the button 
			// and rebind the ButtonClick event
			//
			if (_persistDropDownName)
			{
				Text = textDisplay;
				ButtonClick = adaptorEvent;
			}
		}

		/// <summary>
		/// Set the time when the menu drop-down was closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SplitButtonDropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e)
		{
			// Start timer
			ClosedTime = DateTime.Now;
		}

		/// <summary>
		/// Clear the time when the menu drop-down was closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SplitButtonDropDown_Opening(object sender, CancelEventArgs e)
		{
			if (IsTooSoonAfterCloseMenuDropDown())
			{
				//
				// We can either use the following line
				//		e.Cancel = true;
				// to prevent the menu drop-down from showing.  Or else, we 
				// can use the the same timer logic in the the OnMouseUp() 
				// call back.
				//
			}

			//
			// In effect we close the timer.  Either the menu is shown or closed
			// but we no longer need the timer.
			//
			ClosedTime = ZeroTime;
		}

		#endregion
	}
}
