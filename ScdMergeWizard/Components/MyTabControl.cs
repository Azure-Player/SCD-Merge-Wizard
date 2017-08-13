using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ScdMergeWizard.Pages;

namespace ScdMergeWizard.Components
{
    public class MyTabControl : TabControl
    {
        private ErrorProvider _errorProvider;

        public bool IsNextAvailable
        {
            get
            {
                return getNextShowablePageIndex() != -1;
            }
        }

        public bool IsBackAvailable { get { return getPreviousShowablePageIndex() != -1; } }

        public MyTabControl()
        {
            _errorProvider = new ErrorProvider();
            _errorProvider.BlinkRate = 0;
        }

        protected override void WndProc(ref Message m)
        {
            // Hide tabs by trapping the TCM_ADJUSTRECT message
            if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
            else base.WndProc(ref m);
        }

        private int getNextShowablePageIndex()
        {
            int idx = -1;
            for (int i = this.SelectedIndex + 1; i < this.TabPages.Count; i++)
            {
                if (((MyTabPage)this.TabPages[i]).OnIsShowable())
                {
                    idx = i;
                    break;
                }
            }
            return idx;
        }

        private int getPreviousShowablePageIndex()
        {
            int idx = -1;
            for (int i = this.SelectedIndex - 1; i >= 0; i--)
            {
                if (((MyTabPage)this.TabPages[i]).OnIsShowable())
                {
                    idx = i;
                    break;
                }
            }
            return idx;
        }

        public void NextTab()
        {
            if (IsNextAvailable)
            {
                _errorProvider.Clear();
                var res = ((MyTabPage)this.SelectedTab).OnPageLeaving(new Components.PageLeavingEventArgs());

                if (!res.Cancel)
                {
                    this.SelectedIndex = getNextShowablePageIndex();
                    ((MyTabPage)this.SelectedTab).OnPageEntering(new Components.PageEnteringEventArgs());
                }
                else
                {
                    foreach (PageError pe in res.PageErrors)
                    {
                        _errorProvider.SetIconAlignment(pe.Control, ErrorIconAlignment.MiddleLeft);
                        _errorProvider.SetError(pe.Control, !string.IsNullOrEmpty(pe.ErrorMessage) ? pe.ErrorMessage : "Error");
                    }
                }
                //MessageBox.Show(res.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PreviousTab()
        {
            if (IsBackAvailable)
                this.SelectedIndex = getPreviousShowablePageIndex();
        }
    }
}
