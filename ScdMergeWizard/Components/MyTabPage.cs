using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ScdMergeWizard.Pages;

namespace ScdMergeWizard.Components
{
    public class MyTabPage : TabPage
    {
        private Label l;


        public delegate bool IsShowableEventHandler();
        public event IsShowableEventHandler IsShowable;

        public delegate void PageEnteringEventHandler(PageEnteringEventArgs e);
        public event PageEnteringEventHandler PageEntering;

        public delegate void PageLeavingEventHandler(PageLeavingEventArgs e);
        public event PageLeavingEventHandler PageLeaving;

        public MyTabPage()
        {
            l = new Label();
            l.AutoSize = false;
            l.Size = new System.Drawing.Size(100, 40);
            l.Dock = DockStyle.Top;
            l.TextAlign = ContentAlignment.MiddleLeft;

            Font f = this.Font;

            l.Font = new Font(f.FontFamily, 16);
            l.Text = this.Text ?? "AAA";
            this.Controls.Add(l);
        }

        public void SetTitle()
        {
            l.Text = "  " + this.Text;
        }

        public virtual bool OnIsShowable()
        {
            if (IsShowable != null)
                return IsShowable();
            return false;
        }

        public virtual PageEnteringEventArgs OnPageEntering(PageEnteringEventArgs e)
        {
            if (PageEntering != null)
                PageEntering(e);

            return e;
        }

        public virtual PageLeavingEventArgs OnPageLeaving(PageLeavingEventArgs e)
        {
            if (PageLeaving != null)
                PageLeaving(e);

            return e;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }

    public class PageLeavingEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
        public List<PageError> PageErrors = new List<PageError>();
    }

    public class PageEnteringEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
        public string ErrorMessage { get; set; }
    }

}
