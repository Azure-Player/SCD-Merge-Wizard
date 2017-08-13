using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MergeWizard.Components
{

    

    public partial class MyRichTextBox : UserControl
    {
        public RichTextBox RichTextBox
        { get { return richTextBox1; } }

        public bool EnableEditing
        {
            get { return !richTextBox1.ReadOnly; }
            set { richTextBox1.ReadOnly = !value; }
        }

        public Color LineNumbersColor
        {
            get { return numberLabel.ForeColor; }
            set { numberLabel.ForeColor = value; }
        }


        public delegate void RichTextBoxTextChangedHandler(object sender, EventArgs e);
        public event RichTextBoxTextChangedHandler TextChanged;


        public MyRichTextBox()
        {
            InitializeComponent();

            numberLabel.ForeColor = LineNumbersColor;

            richTextBox1.TextChanged += new EventHandler(richTextBox1_TextChanged);
        }

        void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
            {
                TextChanged(sender, e);
            }
            //TextChanged2 += new RichTextBoxTextChangedHandler(MyRichTextBox_TextChanged2);
        }

        private void UpdateLineNumbersEvent(object sender, EventArgs e)
        {
            //move location of numberLabel for amount 
            //of pixels caused by scrollbar
            int d = richTextBox1.GetPositionFromCharIndex(0).Y %
                                      (richTextBox1.Font.Height + 1);
            numberLabel.Location = new Point(0, d);

            updateNumberLabel();

            richTextBox1.Refresh();
        }


        private void updateNumberLabel()
        {
            //we get index of first visible char and 
            //number of first visible line
            Point pos = new Point(0, 0);
            int firstIndex = richTextBox1.GetCharIndexFromPosition(pos);
            int firstLine = richTextBox1.GetLineFromCharIndex(firstIndex);

            //now we get index of last visible char 
            //and number of last visible line
            pos.X = ClientRectangle.Width;
            pos.Y = ClientRectangle.Height;
            int lastIndex = richTextBox1.GetCharIndexFromPosition(pos);
            int lastLine = richTextBox1.GetLineFromCharIndex(lastIndex);

            //this is point position of last visible char, we'll 
            //use its Y value for calculating numberLabel size
            pos = richTextBox1.GetPositionFromCharIndex(lastIndex);

            //finally, renumber label
            numberLabel.Text = "";
            for (int i = firstLine; i < lastLine + 1; i++)
            {
                numberLabel.Text += string.Format("{0,8}", i + 1 + Environment.NewLine);
            }

        }
    }
}
