using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScdMergeWizard.Pages
{
    public partial class Page02_Agreement : TheMasterPage, IMasterPage
    {
        public Page02_Agreement()
        {
            InitializeComponent();
        }



        public bool IsShowable()
        {
            return false;
        }

        public void OnPageEntering()
        {
            
        }

        public PageLeavingEventArgs OnPageLeaving(PageLeavingEventArgs e)
        {
            return e;
        }
    }
}
