using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScdMergeWizard.Database;

namespace ScdMergeWizard
{
    public static class GlobalVariables
    {
        public static MyBaseDbConnection SourceConnection = null;
        public static MyBaseDbConnection TargetConnection = null;

        public static List<MyDbColumn> SourceColumns = new List<MyDbColumn>();
        public static List<MyDbColumn> TargetColumns = new List<MyDbColumn>();
        public static List<MyDbColumn> UserColumns = new List<MyDbColumn>();

        public static List<MyTransformation> Transformations = new List<MyTransformation>();

        public static List<MyUserVariable> UserColumnsDefinitions = new List<MyUserVariable>();
        public static List<MyUserVariable> LoadedUserColumnsDefinitions = new List<MyUserVariable>();
        public static string UserColumnsDefsQuery;

        public static List<ColumnMapping> ColumnMappings = null;
        public static List<ColumnMapping> LoadedColumnMappings = null;


        public static string SourceObjectName;
        public static bool SourceIsTableOrViewMode;
        public static string SourceCommandText;
        public static string TargetObjectName;


        // A delegate type for hooking up change notifications.
        public delegate void ProjectModifiedEventHandler();
        public static event ProjectModifiedEventHandler ProjectModified;

        private static bool _isProjectModified;
        public static bool IsProjectModified
        {
            get { return _isProjectModified; }
            set
            {
                if (_isProjectModified != value)
                {
                    _isProjectModified = value;

                    if (ProjectModified != null)
                        ProjectModified();
                }
            }
        }

        static GlobalVariables()
        {
            // {0} SourceColumn
            // {1} TargetColumn
            // {2} OnInsert
            // {3} OnUpdate
            // {4} OnDelete

            Transformations.Add(new MyTransformation
            {
                Code = ETransformationCode.BUSINESS_KEY,
                Name = "Business Key",
                HasSourceColumn = true,
                UseOnlyOnce = false,
                HasOnInsertColumn = false,
                HasOnUpdateColumn = false,
                HasOnDeleteColumn = false,
                Image = Properties.Resources.key
            });

            Transformations.Add(new MyTransformation
            {
                Code = ETransformationCode.SCD0,
                Name = "SCD0",
                HasSourceColumn = true,
                UseOnlyOnce = false,
                HasOnInsertColumn = false,
                HasOnUpdateColumn = false,
                HasOnDeleteColumn = false,
                Image = Properties.Resources.Numbers_0_icon
            });

            Transformations.Add(new MyTransformation
            {
                Code = ETransformationCode.SCD1,
                Name = "SCD1",
                HasSourceColumn = true,
                UseOnlyOnce = false,
                HasOnInsertColumn = false,
                HasOnUpdateColumn = false,
                HasOnDeleteColumn = false,
                Image = Properties.Resources.Numbers_1_icon
            });

            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.SCD2,
                Name = "SCD2",
                HasSourceColumn = true,
                UseOnlyOnce = false,
                HasOnInsertColumn = false,
                HasOnUpdateColumn = false,
                HasOnDeleteColumn = false,
                Image = Properties.Resources.Numbers_2_icon
            });

            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.SCD2_DATE_FROM,
                Name = "SCD2 Date From",
                HasSourceColumn = false,
                UseOnlyOnce = true,
                HasOnInsertColumn = true, //"Record is inserted for the first time or as a newer version",
                HasOnUpdateColumn = true, //"Record already exists and needs to be updated",
                HasOnDeleteColumn = false,
                Image = Properties.Resources.date_next
            });

            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.SCD2_DATE_TO,
                Name = "SCD2 Date To",
                HasSourceColumn = false,
                UseOnlyOnce = true,
                HasOnInsertColumn = true, //"Record is inserted for the first time or as a newer version",
                HasOnUpdateColumn = true, //"Record already exists and needs to be updated",
                HasOnDeleteColumn = true, //"Record is logically deleted on the source",
                Image = Properties.Resources.date_previous
            });

            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.SCD2_IS_ACTIVE,
                Name = "SCD2 Is Active",
                HasSourceColumn = false,
                UseOnlyOnce = true,
                HasOnInsertColumn = true, //"Record is inserted for the first time or as a newer version",
                HasOnUpdateColumn = true, //"Record already exists and needs to be updated",
                HasOnDeleteColumn = true, //"Record is logically deleted on the source",
                Image = Properties.Resources.check
            });

            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.SCD3_CURRENT_VALUE,
                Name = "SCD3 Current",
                HasSourceColumn = true,
                UseOnlyOnce = false,
                Image = Properties.Resources.Numbers_3_icon
            });

            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.SCD3_DATE_FROM,
                Name = "SCD3 Date From",
                HasSourceColumn = true,
                UseOnlyOnce = false,
                HasOnInsertColumn = true, //"Record is inserted for the first time",
                HasOnUpdateColumn = true, //"Record already exists and needs to be updated",
                Image = Properties.Resources.date_next
            });
            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.SCD3_ORIGINAL_FIRST_VALUE,
                Name = "SCD3 Original",
                HasSourceColumn = true,
                UseOnlyOnce = false,
                Image = Properties.Resources.Numbers_3_icon
            });

            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.SCD3_PREVIOUS_VALUE,
                Name = "SCD3 Previous",
                HasSourceColumn = true,
                UseOnlyOnce = false,
                HasOnInsertColumn = true, //"Initial Value",
                Image = Properties.Resources.Numbers_3_icon
            });

            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.CREATED_DATE,
                Name = "Created Date",
                HasSourceColumn = false,
                UseOnlyOnce = true,
                HasOnInsertColumn = true, //"Value when Created",
                Image = Properties.Resources.date_add
            });

            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.MODIFIED_DATE,
                Name = "Modified Date",
                HasSourceColumn = false,
                UseOnlyOnce = true,
                HasOnInsertColumn = true, //"Initial Value",
                HasOnUpdateColumn = true, //"Value when Modified",
                Image = Properties.Resources.date_edit
            });

            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.DELETED_DATE,
                Name = "Deleted Date",
                HasSourceColumn = false,
                UseOnlyOnce = true,
                HasOnInsertColumn = true, //"Initial Value",
                HasOnDeleteColumn = true, //"Value when Deleted",
                Image = Properties.Resources.date_delete
            });
            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.IS_DELETED,
                Name = "Is Deleted",
                HasSourceColumn = false,
                UseOnlyOnce = true,
                HasOnDeleteColumn = true, //"Value when Deleted",
                HasOnInsertColumn = true, //"Value when inserted (Not Deleted)",
                Image = Properties.Resources.the_delete_icon
            });
            
            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.VERSION_NUMBER,
                Name = "Version Number",
                HasSourceColumn = false,
                UseOnlyOnce = true,
                Image = Properties.Resources.old_versions
            });

            Transformations.Add(new MyTransformation {
                Code = ETransformationCode.SKIP,
                Name = "Skip",
                HasSourceColumn = false,
                UseOnlyOnce = false,
                Image = Properties.Resources.arrow_skip
            });

            ColumnMappings = new List<ColumnMapping>();
            LoadedColumnMappings = new List<ColumnMapping>();

        }

        /*
        public static ERecordsOnTargetNotFoundOnSourceMode RecordsOnTargetNotFoundOnSourceMode;

        public static bool ShowExtendedComments;

        public static bool IgnoreDatabasePrefix;

        public static bool ResetCountingVersionNumberOnSCD2;
        */

        public static DateTime LinkerTimestamp
        {
            get
            {
                string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
                const int c_PeHeaderOffset = 60;
                const int c_LinkerTimestampOffset = 8;
                byte[] b = new byte[2048];
                System.IO.Stream s = null;

                try
                {
                    s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    s.Read(b, 0, 2048);
                }
                finally
                {
                    if (s != null)
                    {
                        s.Close();
                    }
                }

                int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
                int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
                dt = dt.AddSeconds(secondsSince1970);
                dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
                return dt;
            }
        }

        private static Options _options;
        public static Options Options
        {
            get
            {
                if (_options == null)
                {
                    _options = new Options();
                }
                return _options;
            }
        }
    }



}
