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
    public partial class Page01_Welcome : TheMasterPage, IMasterPage
    {
        public Page01_Welcome()
        {
            InitializeComponent();
        }

        public bool IsShowable()
        {
            return true;
        }

        public void OnPageEntering()
        {
            
        }

        public PageLeavingEventArgs OnPageLeaving(PageLeavingEventArgs e)
        {
            /*
            GlobalVariables.SourceColumns.Add(new Database.MyDbColumn { ColumnName = "ID" });
            GlobalVariables.SourceColumns.Add(new Database.MyDbColumn { ColumnName = "S_NAME" });
            GlobalVariables.SourceColumns.Add(new Database.MyDbColumn { ColumnName = "I_AGE" });
            GlobalVariables.SourceColumns.Add(new Database.MyDbColumn { ColumnName = "S_ADDRESS" });
            GlobalVariables.SourceColumns.Add(new Database.MyDbColumn { ColumnName = "D_CREATED" });
            GlobalVariables.SourceColumns.Add(new Database.MyDbColumn { ColumnName = "D_MODIFIED" });

            GlobalVariables.TargetColumns.Add(new Database.MyDbColumn { ColumnName = "EmployeeID" });
            GlobalVariables.TargetColumns.Add(new Database.MyDbColumn { ColumnName = "Name" });
            GlobalVariables.TargetColumns.Add(new Database.MyDbColumn { ColumnName = "Age_Current" });
            GlobalVariables.TargetColumns.Add(new Database.MyDbColumn { ColumnName = "Age_Original" });
            GlobalVariables.TargetColumns.Add(new Database.MyDbColumn { ColumnName = "Age_Previous" });
            GlobalVariables.TargetColumns.Add(new Database.MyDbColumn { ColumnName = "Age_Effective_From" });
            GlobalVariables.TargetColumns.Add(new Database.MyDbColumn { ColumnName = "Address" });
            GlobalVariables.TargetColumns.Add(new Database.MyDbColumn { ColumnName = "StartDate" });
            GlobalVariables.TargetColumns.Add(new Database.MyDbColumn { ColumnName = "EndDate" });
            GlobalVariables.TargetColumns.Add(new Database.MyDbColumn { ColumnName = "IsActiveRecord" });

            GlobalVariables.UserColumns.Add(new Database.MyDbColumn { ColumnName = "@CurrentDateTime" });
            */
            return e;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
