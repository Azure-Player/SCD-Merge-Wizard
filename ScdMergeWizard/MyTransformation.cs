using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ScdMergeWizard
{
    public class MyTransformation
    {
        public ETransformationCode Code;
        public string Name;
        public bool HasSourceColumn;
        //public bool HasOnInsertColumn { get { return !string.IsNullOrEmpty(OnInsertColumnDesc); } }
        //public bool HasOnUpdateColumn { get { return !string.IsNullOrEmpty(OnUpdateColumnDesc); } }
        //public bool HasOnDeleteColumn { get { return !string.IsNullOrEmpty(OnDeleteColumnDesc); } }
        public bool UseOnlyOnce;
        public bool HasOnInsertColumn = false;
        public bool HasOnUpdateColumn = false;
        public bool HasOnDeleteColumn = false;

        public Image Image;

        //public string Description;
        //public Image ImageDescription;

        public override string ToString()
        {
            return Name;
        }
    }


    public enum ETransformationCode
    {
        BUSINESS_KEY,

        SCD0,

        SCD1,

        SCD2,
        SCD2_DATE_FROM, // Starting date of newly added record
        SCD2_DATE_TO, // Ending date of an "old" record
        SCD2_IS_ACTIVE, // Status if SCD2 record is active

        //SCD3,
        SCD3_CURRENT_VALUE,
        SCD3_ORIGINAL_FIRST_VALUE,
        SCD3_PREVIOUS_VALUE,
        SCD3_DATE_FROM,

        CREATED_DATE, // Date when record is first time entered into target table
        MODIFIED_DATE, // Last date when record is modified

        DELETED_DATE, // Date when record is deleted
        IS_DELETED, // Status if record is deleted

        VERSION_NUMBER, // Defines version of the record

        SKIP
        //UNDEF
    }
}
