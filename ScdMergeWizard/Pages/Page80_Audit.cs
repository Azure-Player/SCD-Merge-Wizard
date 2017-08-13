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
    public partial class Page80_Audit : TheMasterPage, IMasterPage
    {
        public Page80_Audit()
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
