using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ScdMergeWizard.Database;
using System.Data;

namespace ScdMergeWizard.Components
{
    public class ImageComboBox : ComboBox
    {
        public bool ShowTables = true;
        public bool ShowViews = true;
        public bool ShowSynonyms = true;
        public string FilterText = string.Empty;

        private List<ImageComboBoxItem> _imagedItems = new List<ImageComboBoxItem>();

        public ImageComboBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        public void ClearFilters()
        {
            this.ShowTables = true;
            this.ShowViews = true;
            this.ShowSynonyms = true;
            this.FilterText = string.Empty;
        }

        public string GetFilterText()
        {
            List<string> s = new List<string>();

            if (ShowTables && ShowViews && ShowSynonyms && string.IsNullOrEmpty(FilterText))
                return "Filter: (none)";

            if (ShowTables)
                s.Add("'tables'");
            if (ShowViews)
                s.Add("'views'");
            if (ShowSynonyms)
                s.Add("'synonyms'");
            if (!string.IsNullOrEmpty(FilterText))
                s.Add("containing '" + FilterText + "'");

            return "Filter: " + string.Join(", ", s.ToArray());
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            try
            {
                e.DrawBackground();

                e.DrawFocusRectangle();

                if (e.Index >= 0 && e.Index < Items.Count)
                {
                    ImageComboBoxItem item = (ImageComboBoxItem)Items[e.Index];
                    e.Graphics.DrawImage(item.Image, e.Bounds.Left + 2, e.Bounds.Top + 2);
                    e.Graphics.DrawString(item.Value, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + item.Image.Width + 2, e.Bounds.Top + 1);
                }

                base.OnDrawItem(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void AddItems(MyDbObject[] objects)
        {
            this.Items.Clear();

            if (objects != null)
            {
                foreach (var o in objects)
                {
                    if ((o.Type == EDbObjectType.Table && ShowTables ||
                        o.Type == EDbObjectType.View && ShowViews ||
                        o.Type == EDbObjectType.Synonym && ShowSynonyms) &&
                        string.IsNullOrEmpty(FilterText) || (!string.IsNullOrEmpty(FilterText) && o.Name.ToLower().Contains(FilterText.ToLower())))
                    {
                        string value = o.Name;
                        Bitmap image = null;
                        switch (o.Type)
                        {
                            case EDbObjectType.Table: image = Properties.Resources.TableHS; break;
                            case EDbObjectType.View: image = Properties.Resources.view; break;
                            case EDbObjectType.Synonym: image = Properties.Resources.synonym; break;
                        }
                        this.Items.Add(new ImageComboBoxItem { Value = value, Image = image });
                    }
                }
            }
        }
    }
}
