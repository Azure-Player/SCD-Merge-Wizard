using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScdMergeWizard
{
    public class Options
    {
        private ERecordsOnTargetNotFoundOnSourceMode _recordsOnTargetNotFoundOnSourceMode;
        private ESCD2VersionNumberMode _scd2VersionNumberMode;
        private bool _ignoreDatabasePrefix = true;
        private bool _showExtendedComments = false;
        private ESCD13UpdateMode _scd13UpdateMode;

        [CategoryAttribute("Global Settings")
        , DisplayName("Records on Target not found on Source")
        , DefaultValue(ERecordsOnTargetNotFoundOnSourceMode.UpdateTargetField)
        , Description("Defines what will happen with records on target when record with same business key is deleted from source. Note that if you decide to physically delete records, referential integrity of Data Warehouse may fail.")
        ]
        public ERecordsOnTargetNotFoundOnSourceMode RecordsOnTargetNotFoundOnSourceMode
        {
            get { return _recordsOnTargetNotFoundOnSourceMode; }
            set { _recordsOnTargetNotFoundOnSourceMode = value; }
        }

        [CategoryAttribute("SCD Settings")
        , DisplayName("SCD2 Version Number Mode")
        , DefaultValue(ESCD2VersionNumberMode.ResetTo1)
        , Description("Defines how Version Number will be counting when newer version of SCD2 record is added")
        ]
        public ESCD2VersionNumberMode SCD2VersionNumberMode
        {
            get { return _scd2VersionNumberMode; }
            set { _scd2VersionNumberMode = value; }
        }

        [CategoryAttribute("Global Settings")
        , DisplayName("Ignore Database Prefix")
        , DefaultValue(true)
        , Description("Ignores database prefix when server name and database name are the same for source and target")
        ]
        public bool IgnoreDatabasePrefix
        {
            get { return _ignoreDatabasePrefix; }
            set { _ignoreDatabasePrefix = value; }
        }


        [CategoryAttribute("Global Settings")
        , DisplayName("Show Extended Comments")
        , DefaultValue(false)
        , Description("Shows extended comments at the beginning of the Merge Query, below header")
        ]
        public bool ShowExtendedComments
        {
            get { return _showExtendedComments; }
            set { _showExtendedComments = value; }
        }


        [CategoryAttribute("SCD Settings")
        , DisplayName("SCD1, SCD3 Update Mode")
        , DefaultValue(ESCD13UpdateMode.UpdateCurrentRecordsOnly)
        , Description("Defines if only current or current and outdated SCD1 and SCD3 records will be updated. Outdated records exists only if at least one SCD2 transformation is defined.")
        ]
        public ESCD13UpdateMode SCD13UpdateMode
        {
            get { return _scd13UpdateMode; }
            set { _scd13UpdateMode = value; }
        }

        

    }

    public enum ERecordsOnTargetNotFoundOnSourceMode
    {
        UpdateTargetField,
        PhysicallyDelete,
        DoNothing
    }

    public enum ESCD2VersionNumberMode
    {
        ResetTo1,
        ContinueCounting
    }

    public enum ESCD13UpdateMode
    {
        UpdateCurrentRecordsOnly,
        UpdateCurrentAndOutdatedRecords
    }
}
