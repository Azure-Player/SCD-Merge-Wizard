using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ScdMergeWizard.Components
{
    public sealed class ImageComboBoxItem
    {
        public string Value { get; set; }

        public Image Image { get; set; }

        public ImageComboBoxItem()
            : this("")
        { }

        public ImageComboBoxItem(string val)
        {
            try
            {
                Value = val;

                Image = new Bitmap(16, 16);
                using (Graphics g = Graphics.FromImage(Image))
                {
                    using (Brush b = new SolidBrush(Color.FromName(val)))
                    {
                        g.DrawRectangle(Pens.White, 2, 2, Image.Width, Image.Height);
                        g.FillRectangle(b, 2, 2, Image.Width - 1, Image.Height - 1);
                    }
                }
            }
            catch
            { }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
