using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScdMergeWizard.Components;
using System.Drawing;

namespace ScdMergeWizard
{
    public class ColumnMapping
    {
        public string SourceColumn;
        public ETargetDatabaseType TargetDatabaseType;
        public string TargetColumn;
        public ETransformationCode TransformationCode;

        public string CustomInsertValue;
        public string CustomUpdateValue;
        public string CustomDeleteValue;

        public EColumnCompareMethod ColumnCompareMethod;

        public bool IsSourceColumnDefined
        {
            get
            {
                return (this.SourceColumn != " " && !string.IsNullOrEmpty(this.SourceColumn));
            }
        }

        public bool IsTargetColumnDefined
        {
            get
            {
                return (this.TargetColumn != " " && !string.IsNullOrEmpty(this.TargetColumn));
            }
        }

        public ColumnMapping(string sourceColumn, ETargetDatabaseType databaseType, string targetColumn, ETransformationCode transformationType, string insertValue, string updateValue, string deleteValue, EColumnCompareMethod columnCompareMethod)
        {
            this.SourceColumn = sourceColumn;
            this.TargetDatabaseType = databaseType;
            this.TargetColumn = targetColumn;
            this.TransformationCode = transformationType;
            this.CustomInsertValue = insertValue;
            this.CustomUpdateValue = updateValue;
            this.CustomDeleteValue = deleteValue;
            this.ColumnCompareMethod = columnCompareMethod;
        }

        public ColumnMapping(ColumnMapping cm)
        {
            this.SourceColumn = cm.SourceColumn;
            this.TargetDatabaseType = cm.TargetDatabaseType;
            this.TargetColumn = cm.TargetColumn;
            this.TransformationCode = cm.TransformationCode;
            this.CustomInsertValue = cm.CustomInsertValue;
            this.CustomUpdateValue = cm.CustomUpdateValue;
            this.CustomDeleteValue = cm.CustomDeleteValue;
            this.ColumnCompareMethod = cm.ColumnCompareMethod;
        }


        public string GetSourceColumn(EOperation operation, bool addPrefix, ECalledFrom calledFrom)
        {
            string s = "";

            // LOGIC - Transformation DML Schema
            if (operation == EOperation.SELECT)
            {
                s = SourceColumn;
            }
            if (operation == EOperation.INSERT)
            {
                if (this.TransformationCode == ETransformationCode.SCD2_DATE_FROM)
                    s = (calledFrom == ECalledFrom.DEFAULT) ? this.CustomInsertValue : this.CustomUpdateValue;

                else if (this.TransformationCode == ETransformationCode.SCD2_DATE_TO)
                    s = this.CustomInsertValue;

                else if (this.TransformationCode == ETransformationCode.SCD2_IS_ACTIVE)
                    s = this.CustomInsertValue;

                else if (this.TransformationCode == ETransformationCode.SCD3_PREVIOUS_VALUE)
                {
                    if (calledFrom == ECalledFrom.DEFAULT)
                        s = this.CustomInsertValue;

                    else if (calledFrom == ECalledFrom.SCD2_OUTPUT)
                    {
                        var cv = GlobalVariables.ColumnMappings.Find(cm => cm.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE && cm.SourceColumn == this.SourceColumn);
                        var pv = GlobalVariables.ColumnMappings.Find(cm => cm.TransformationCode == ETransformationCode.SCD3_PREVIOUS_VALUE && cm.SourceColumn == this.SourceColumn);

                        s = string.Format("CASE WHEN (INSERTED.{0} <> [source].{1} OR (INSERTED.{0} IS NULL AND [source].{1} IS NOT NULL) OR (INSERTED.{0} IS NOT NULL AND [source].{1} IS NULL)) THEN INSERTED.{0} ELSE INSERTED.{2} END", cv.TargetColumn, cv.SourceColumn, pv.TargetColumn);

                        //s = "INSERTED." + GlobalVariables.ColumnMappings.Find(cm => cm.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE && cm.SourceColumn == this.SourceColumn).TargetColumn;
                    }
                }
                else if (this.TransformationCode == ETransformationCode.SCD3_ORIGINAL_FIRST_VALUE && calledFrom == ECalledFrom.SCD2_OUTPUT)
                {
                    s = "INSERTED." + this.TargetColumn;
                }
                else if (this.TransformationCode == ETransformationCode.SCD3_DATE_FROM)
                {
                    if (calledFrom == ECalledFrom.DEFAULT)
                        s = this.CustomInsertValue;

                    else if (calledFrom == ECalledFrom.SCD2_OUTPUT)
                    {
                        ColumnMapping cv = GlobalVariables.ColumnMappings.Find(cm => cm.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE && cm.SourceColumn == this.SourceColumn);
                        ColumnMapping df = GlobalVariables.ColumnMappings.Find(cm => cm.TransformationCode == ETransformationCode.SCD3_DATE_FROM && cm.SourceColumn == this.SourceColumn);
                        s = string.Format("CASE WHEN (INSERTED.{0} <> [source].{1} OR (INSERTED.{0} IS NULL AND [source].{1} IS NOT NULL) OR (INSERTED.{0} IS NOT NULL AND [source].{1} IS NULL)) THEN {3} ELSE INSERTED.{2} END", cv.TargetColumn, cv.SourceColumn, df.TargetColumn, ((df.CustomUpdateValue.StartsWith("[") ? "[source]." + df.CustomUpdateValue : df.CustomUpdateValue)));
                    }
                }
                else if (this.TransformationCode == ETransformationCode.CREATED_DATE)
                    s = this.CustomInsertValue;

                else if (this.TransformationCode == ETransformationCode.MODIFIED_DATE)
                    s = this.CustomInsertValue;

                else if (this.TransformationCode == ETransformationCode.DELETED_DATE)
                    s = this.CustomInsertValue;

                else if (this.TransformationCode == ETransformationCode.IS_DELETED)
                    s = this.CustomInsertValue;

                else if (this.TransformationCode == ETransformationCode.VERSION_NUMBER)
                {
                    if (calledFrom == ECalledFrom.DEFAULT)
                        s = "1";
                    else if (calledFrom == ECalledFrom.SCD2_OUTPUT)
                    {
                        if (GlobalVariables.Options.SCD2VersionNumberMode == ESCD2VersionNumberMode.ResetTo1)
                            s = "1";
                        else
                            s = "INSERTED." + this.TargetColumn + " + 1";
                    }
                }
                else
                    s = SourceColumn;
            }
            if (operation == EOperation.UPDATE)
            {
                if (this.TransformationCode == ETransformationCode.SCD0)
                    s = null;

                else if (this.TransformationCode == ETransformationCode.SCD2_DATE_FROM)
                    s = this.CustomUpdateValue;

                else if (this.TransformationCode == ETransformationCode.SCD2_DATE_TO)
                    s = this.CustomUpdateValue;

                else if (this.TransformationCode == ETransformationCode.SCD2_IS_ACTIVE)
                    s = this.CustomUpdateValue;

                else if (this.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE)
                {
                    var cv = GlobalVariables.ColumnMappings.Find(cm => cm.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE && cm.SourceColumn == this.SourceColumn);
                    if (calledFrom == ECalledFrom.DEFAULT)
                        s = string.Format("CASE WHEN ([target].{0} <> [source].{1} OR ([target].{0} IS NULL AND [source].{1} IS NOT NULL) OR ([target].{0} IS NOT NULL AND [source].{1} IS NULL)) THEN [source].{1} ELSE [target].{0} END", cv.TargetColumn, cv.SourceColumn);
                    else if (calledFrom == ECalledFrom.SCD2_OUTPUT)
                        s = cv.SourceColumn;
                }
                else if (this.TransformationCode == ETransformationCode.SCD3_ORIGINAL_FIRST_VALUE)
                    s = null;
                else if (this.TransformationCode == ETransformationCode.SCD3_DATE_FROM)
                {
                    if (calledFrom == ECalledFrom.DEFAULT)
                    {
                        var cv = GlobalVariables.ColumnMappings.Find(cm => cm.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE && cm.SourceColumn == this.SourceColumn);
                        var df = GlobalVariables.ColumnMappings.Find(cm => cm.TransformationCode == ETransformationCode.SCD3_DATE_FROM && cm.SourceColumn == this.SourceColumn);
                        s = string.Format("CASE WHEN ([target].{0} <> [source].{1} OR ([target].{0} IS NULL AND [source].{1} IS NOT NULL) OR ([target].{0} IS NOT NULL AND [source].{1} IS NULL)) THEN {3} ELSE [target].{2} END", cv.TargetColumn, cv.SourceColumn, df.TargetColumn, ((df.CustomUpdateValue.StartsWith("[") ? "[source]." + df.CustomUpdateValue : df.CustomUpdateValue)));
                    }
                    else if (calledFrom == ECalledFrom.SCD2_OUTPUT)
                        s = null;
                }
                else if (this.TransformationCode == ETransformationCode.SCD3_PREVIOUS_VALUE)
                {
                    if (calledFrom == ECalledFrom.DEFAULT)
                    {
                        var cv = GlobalVariables.ColumnMappings.Find(cm => cm.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE && cm.SourceColumn == this.SourceColumn);
                        var pv = GlobalVariables.ColumnMappings.Find(cm => cm.TransformationCode == ETransformationCode.SCD3_PREVIOUS_VALUE && cm.SourceColumn == this.SourceColumn);
                        s = string.Format("CASE WHEN ([target].{0} <> [source].{1} OR ([target].{0} IS NULL AND [source].{1} IS NOT NULL) OR ([target].{0} IS NOT NULL AND [source].{1} IS NULL)) THEN [target].{0} ELSE [target].{2} END", cv.TargetColumn, cv.SourceColumn, pv.TargetColumn);
                    }
                    else
                        s = null;
                }
                else if (this.TransformationCode == ETransformationCode.SCD3_DATE_FROM)
                    s = this.CustomUpdateValue;

                else if (this.TransformationCode == ETransformationCode.CREATED_DATE)
                    s = null;

                else if (this.TransformationCode == ETransformationCode.MODIFIED_DATE)
                    s = this.CustomUpdateValue;

                else if (this.TransformationCode == ETransformationCode.DELETED_DATE)
                    s = null;
                else if (this.TransformationCode == ETransformationCode.IS_DELETED)
                    s = null;
                else if (this.TransformationCode == ETransformationCode.VERSION_NUMBER)
                {
                    if (calledFrom == ECalledFrom.DEFAULT)
                    {
                        s = this.TargetColumn + " + 1";
                    }
                    else if (calledFrom == ECalledFrom.SCD2_OUTPUT)
                    {
                        s = null;
                    }
                }
                else
                    s = SourceColumn;
            }
            if (operation == EOperation.DELETE)
            {
                if (this.TransformationCode == ETransformationCode.DELETED_DATE)
                    s = this.CustomDeleteValue;
                else if (this.TransformationCode == ETransformationCode.IS_DELETED)
                    s = this.CustomDeleteValue;
                else if (this.TransformationCode == ETransformationCode.SCD2_DATE_TO)
                    s = this.CustomDeleteValue;
                else if (this.TransformationCode == ETransformationCode.SCD2_IS_ACTIVE)
                    s = this.CustomDeleteValue;
                else if (this.TransformationCode == ETransformationCode.BUSINESS_KEY)
                    s = this.SourceColumn;
                else
                    s = null;
            }

            if (addPrefix && s.StartsWith("["))
            {
                if (TransformationCode == ETransformationCode.VERSION_NUMBER || TransformationCode == ETransformationCode.SCD3_PREVIOUS_VALUE)
                    s = s.Insert(0, "[target].");
                else
                    s = s.Insert(0, "[source].");
            }

            return s;

        }
    }

    public enum EOperation
    {
        INSERT,
        UPDATE,
        DELETE,
        SELECT
    }

    public enum ECalledFrom
    {
        DEFAULT,
        SCD2_OUTPUT
    }

    public enum ETargetDatabaseType
    {
        TARGET,
        HISTORY,
        UNDEF
    }


    public enum EColumnCompareMethod
    {
        Default,
        Geography
    }
}
