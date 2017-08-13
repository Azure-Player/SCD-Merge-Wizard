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
    public partial class ThePageNavigator : UserControl
    {

        public delegate void CancelRequestedEventHandler();
        public event CancelRequestedEventHandler CancelRequested;

        public delegate void CurrentPageChangedEventHandler(int CurrentPageIndex);
        public event CurrentPageChangedEventHandler CurrentPageChanged;

        


        private List<IMasterPage> _pages = new List<IMasterPage>();
        private int _currentPageIndex = -1;
        private int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set
            {
                if (_currentPageIndex != value)
                {
                    _currentPageIndex = value;
                    onCurrentIndexChanged();
                }
            }
        }

        private void onCurrentIndexChanged()
        {
            buttonCancel.Text = (_pages.Count > 0 && CurrentPageIndex == _pages.Count - 1) ? "Finish" : "Cancel";
            CurrentPageChanged(CurrentPageIndex);
        }


        public ThePageNavigator()
        {
            InitializeComponent();

            setNextEnabled();
            setBackEnabled();
        }

        public void AddPage(IMasterPage p)
        {
            _pages.Add(p);

            if (_pages.Count == 1)
            {
                showPage((TheMasterPage)p);
                CurrentPageIndex = 0;
            }

            onCurrentIndexChanged();
            setNextEnabled();
            setBackEnabled();
        }


        private void showPage(TheMasterPage p)
        {
            panelPageHolder.Controls.Clear();
            panelPageHolder.Controls.Add(p);
            p.Dock = DockStyle.Fill;

            setBackEnabled();
            setNextEnabled();
        }

        public bool GotoBeginning()
        {
            if (CurrentPageIndex > 2)
            {
                if (MessageBox.Show("You will be returned to the Source Connection page", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    CurrentPageIndex = 2;
                    showPage((TheMasterPage)_pages[CurrentPageIndex]);
                    return true;
                }
                else
                    return false;
            }
            return true;
        }

        private void setNextEnabled()
        {
            buttonNext.Enabled = getNextShowablePageIndex() != -1;
        }

        private int getNextShowablePageIndex()
        {
            int idx = -1;
            for (int i = CurrentPageIndex + 1; i < _pages.Count; i++)
            {
                if (_pages[i].IsShowable())
                {
                    idx = i;
                    break;
                }
            }
            return idx;
        }


        private void setBackEnabled()
        {
            buttonBack.Enabled = getPreviousShowablePageIndex() != -1;
        }

        private int getPreviousShowablePageIndex()
        {
            int idx = -1;
            for (int i = CurrentPageIndex - 1; i >= 0; i--)
            {
                if (_pages[i].IsShowable())
                {
                    idx = i;
                    break;
                }
            }
            return idx;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            var plea = new PageLeavingEventArgs();

            errorProvider1.Clear();

            var res = _pages[CurrentPageIndex].OnPageLeaving(plea);

            if (!res.Cancel)
            {
                CurrentPageIndex = getNextShowablePageIndex();
                _pages[CurrentPageIndex].OnPageEntering();
                showPage((TheMasterPage)_pages[CurrentPageIndex]);
            }
            else
            {
                for (int i = res.PageErrors.Count - 1; i >= 0; i--)
                //foreach (PageError pe in res.PageErrors)
                {
                    errorProvider1.SetIconAlignment(res.PageErrors[i].Control, ErrorIconAlignment.MiddleLeft);
                    errorProvider1.SetError(res.PageErrors[i].Control, !string.IsNullOrEmpty(res.PageErrors[i].ErrorMessage) ? res.PageErrors[i].ErrorMessage : "Error");
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = getPreviousShowablePageIndex();
            hideCurrentPage();
            showPage((TheMasterPage)_pages[CurrentPageIndex]);
        }

        private void hideCurrentPage()
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (CancelRequested != null)
                CancelRequested();
        }
    }
}
