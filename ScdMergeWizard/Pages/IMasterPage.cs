using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScdMergeWizard.Pages
{
    public interface IMasterPage
    {
        bool IsShowable();
        void OnPageEntering();
        PageLeavingEventArgs OnPageLeaving(PageLeavingEventArgs e);
    }

    public class PageLeavingEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
        public List<PageError> PageErrors = new List<PageError>();
    }

    public class PageError
    {
        public Control Control;
        public string ErrorMessage;
    }
}
