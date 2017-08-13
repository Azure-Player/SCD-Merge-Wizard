using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScdMergeWizard.EditMenu
{
	/// <summary>
	/// A utility class for accessing the Win32 API.
	/// </summary>
	public class Win32API
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct GETTEXTLENGTHEX
		{
			public Int32 uiFlags;
			public Int32 uiCodePage;
		}

		public const int WM_USER = 0x400;

		public const int EM_CUT = 0x300;
		public const int EM_COPY = 0x301;
		public const int EM_PASTE = 0x302;
		public const int EM_CLEAR = 0x303;
		public const int EM_UNDO = 0x304;

        public const int EM_CANUNDO = 0xC6;
        public const int EM_CANPASTE = WM_USER + 50;
        public const int EM_GETTEXTLENGTHEX = WM_USER + 95;

		/// Windows API SendMessage functions
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
		[DllImport("user32.dll", EntryPoint="SendMessage", CharSet=CharSet.Auto )]
		public static extern int SendMessage( IntPtr hWnd, int Msg, ref GETTEXTLENGTHEX wParam, IntPtr lParam);

		// Return the handle of the window that has the focus.
		[DllImport("user32.dll")]
		public static extern IntPtr GetFocus();

		/// Windows API GetParent function
		[DllImport("user32", SetLastError = true)]
		public extern static IntPtr GetParent(IntPtr hwnd);

		// The constructor.  Not used because all methods are static.
		public Win32API(){}


		// Return the Framework control associated with the specified handle.
		public static Control GetFrameworkControl(IntPtr hControl)
		{
			Control rv = null;

			if( hControl.ToInt32() != 0 )
			{
				rv = Control.FromHandle(hControl);
				// Try the parent, since with a ComboBox, we get a 
				// handle to an inner control.
				if( rv == null )
					rv = GetFrameworkControl(GetParent(hControl));
			}
			return rv;
		}

		// Edit commands for the inner textbox in the ComboBox control.
		public static void Undo(IntPtr hEdit) {SendMessage(hEdit, EM_UNDO, 0, 0);}
		public static void Cut(IntPtr hEdit){SendMessage(hEdit, EM_CUT, 0, 0);}
		public static void Copy(IntPtr hEdit){SendMessage(hEdit, EM_COPY, 0, 0);}
		public static void Paste(IntPtr hEdit){SendMessage(hEdit, EM_PASTE, 0, 0);}
		public static bool CanUndo(IntPtr hEdit){return SendMessage(hEdit, EM_CANUNDO, 0, 0) != 0;}

		// Determine whether there is any format that can be pasted into a rich text box.
		public static bool CanPasteAnyFormat(IntPtr hRichText){return SendMessage(hRichText, EM_CANPASTE, 0,0) != 0;}
		// Determine the length of a control's Text.  Required since using the 
		// RichTextBox .Length property wipes out the Undo/Redo buffer.
		public static int GetTextLength(IntPtr hControl)
		{
			GETTEXTLENGTHEX lpGTL = new GETTEXTLENGTHEX();
			lpGTL.uiFlags = 0;
			lpGTL.uiCodePage = 1200;			// Unicode
			return SendMessage(hControl, EM_GETTEXTLENGTHEX, ref lpGTL, IntPtr.Zero);
		}
	}
}
