using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScdMergeWizard.Formz
{
    public partial class ExitForm : Form
    {
        private int secondsPast = 0;
        private const int DURATION = 7;

        public ExitForm()
        {
            InitializeComponent();

            buttonClose.Text = "Close (" + (DURATION - secondsPast) + ")";
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            secondsPast++;

            if(DURATION - secondsPast > 0)
                buttonClose.Text = "Close (" + (DURATION - secondsPast) + ")";
            else
                buttonClose.Text = "Close";

            timer1.Enabled = DURATION - secondsPast > 0;
            buttonClose.Enabled = DURATION - secondsPast == 0;
        }

        private void ExitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (secondsPast < DURATION)
                e.Cancel = true;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void urlClicked(EUrlType urlType)
        {
            string url = "";

            switch (urlType)
            {
                case EUrlType.DONATE: url = @"https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=BYBDVE5392VGW"; break;
                case EUrlType.FACEBOOK: url = @"https://www.facebook.com/ScdMergeWizard"; break;
                case EUrlType.LINKEDIN: url = @"http://www.linkedin.com"; break;
                case EUrlType.REVIEW: url = @"https://github.com/SQLPlayer/SCD-Merge-Wizard/"; break;
                case EUrlType.TWEET: url = @"https://www.twitter.com/share?url=https%3A%2F%2Fbit.ly%2FSCDMrgWiz&text=%23SCDMergeWizard Check out this great application for generating Slowly Changing Dimension MERGE statement: "; break;
            };

            if(!string.IsNullOrEmpty(url))
                System.Diagnostics.Process.Start(url);
            secondsPast = DURATION;
            Close();
        }

        private void pictureBoxDonate_Click(object sender, EventArgs e)
        {
            urlClicked(EUrlType.DONATE);
        }

        private void pictureBoxFacebook_Click(object sender, EventArgs e)
        {
            urlClicked(EUrlType.FACEBOOK);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            urlClicked(EUrlType.REVIEW);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            urlClicked(EUrlType.LINKEDIN);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            urlClicked(EUrlType.TWEET);
        }

        
    }

    public enum EUrlType
    {
        DONATE,
        FACEBOOK,
        LINKEDIN,
        REVIEW,
        TWEET
    }
}
