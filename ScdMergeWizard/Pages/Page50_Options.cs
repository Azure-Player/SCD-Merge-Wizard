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
    public partial class Page50_Options : TheMasterPage, IMasterPage
    {
        public Page50_Options()
        {
            InitializeComponent();
        }

        public bool IsShowable()
        {
            return true;
        }

        public void OnPageEntering()
        {
            propertyGrid1.SelectedObject = GlobalVariables.Options;
        }

        public PageLeavingEventArgs OnPageLeaving(PageLeavingEventArgs e)
        {
            if (!GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode == ETransformationCode.DELETED_DATE || cm.TransformationCode == ETransformationCode.IS_DELETED) && GlobalVariables.Options.RecordsOnTargetNotFoundOnSourceMode == ERecordsOnTargetNotFoundOnSourceMode.UpdateTargetField)
            {
                e.Cancel = true;
                e.PageErrors.Add(new PageError { Control = propertyGrid1, ErrorMessage = "Cannot select UpdateTargetField option when there are no 'Deleted Date' and/or 'Is Deleted' transformations defined" });
            }

            return e;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            GlobalVariables.IsProjectModified = true;
        }
    }
}
