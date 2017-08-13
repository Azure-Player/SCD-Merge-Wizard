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
    public partial class TheMasterPage : UserControl
    {
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                labelTitle.Text = value;
            }
        }

        private string _desc;
        public string Description
        {
            get
            {
                return _desc;
            }
            set
            {
                _desc = value;
                labelDescription.Text = value;
            }
        }

        public string PageNavigationError = string.Empty;

        public TheMasterPage()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (pictureBox1.Image == null)
                pictureBox1.Image = Properties.Resources.SQL;
        }

        /*
        public virtual bool IsShowable()
        {
            throw new NotImplementedException();
        }

        public virtual void OnPageEntering()
        {
            throw new NotImplementedException();
        }

        public virtual PageLeavingEventArgs OnPageLeaving(PageLeavingEventArgs e)
        {
            throw new NotImplementedException();
        }
         * */
    }

    
    
}
