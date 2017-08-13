using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using ScdMergeWizard.ExcHandling;

namespace ScdMergeWizard
{
    public static class MyQueryBuilder
    {
        private static void removeTablePrefixesIfNecessary()
        {
            if (GlobalVariables.Options.IgnoreDatabasePrefix)
            {
                if (GlobalVariables.SourceObjectName.Replace("[", "").Length == GlobalVariables.SourceObjectName.Length - 3
                    && GlobalVariables.SourceObjectName.Replace("]", "").Length == GlobalVariables.SourceObjectName.Length - 3
                    && GlobalVariables.SourceObjectName.Replace(".", "").Length == GlobalVariables.SourceObjectName.Length - 2)
                    GlobalVariables.SourceObjectName = GlobalVariables.SourceObjectName.Substring(GlobalVariables.SourceObjectName.IndexOf("[", 1), GlobalVariables.SourceObjectName.Length - GlobalVariables.SourceObjectName.IndexOf("[", 1));

                if (GlobalVariables.TargetObjectName.Replace("[", "").Length == GlobalVariables.TargetObjectName.Length - 3
                    && GlobalVariables.TargetObjectName.Replace("]", "").Length == GlobalVariables.TargetObjectName.Length - 3
                    && GlobalVariables.TargetObjectName.Replace(".", "").Length == GlobalVariables.TargetObjectName.Length - 2)
                    GlobalVariables.TargetObjectName = GlobalVariables.TargetObjectName.Substring(GlobalVariables.TargetObjectName.IndexOf("[", 1), GlobalVariables.TargetObjectName.Length - GlobalVariables.TargetObjectName.IndexOf("[", 1));
            }
        }


        private static string[] getHeader()
        {
            List<string> lines = new List<string>();

            // Add come comments at the beginning
            lines.Add(string.Format("Slowly Changing Dimension script by SCD Merge Wizard"));
            lines.Add(string.Format("Author: Miljan Radovic"));
            lines.Add(string.Format("Official web site: https://github.com/SQLPlayer/SCD-Merge-Wizard/"));
            lines.Add(string.Format("Version: {0}", Assembly.GetEntryAssembly().GetName().Version));
            lines.Add(string.Format("Publish date: {0}", GlobalVariables.LinkerTimestamp));
            lines.Add(string.Format("Script creation date: {0}", DateTime.Now));

            return lines.ToArray();
        }


        private static string getExtendedComments()
        {
            StringBuilder q = new StringBuilder();
            int sourceColumnWidth = 0, transformationColumnWidth = 0, targetColumnWidth = 0, customInsertColumnWidth = 0, customUpdateColumnWidth = 0, customDeleteColumnWidth = 0;
            string rowTemplate;
            string srcHeaderText = "Source Column", trfHeaderText = "Transformation", tgtHeaderText = "Target Column", insertHeaderText = "On Insert", updateHeaderText = "On Update", deleteHeaderText = "On Delete";

            if (GlobalVariables.Options.ShowExtendedComments)
            {
                q.AppendLine(string.Format("Source : {0}", GlobalVariables.SourceObjectName));
                q.AppendLine(string.Format("Target : {0}", GlobalVariables.TargetObjectName));
                q.AppendLine();

                // Get max column lenghts
                sourceColumnWidth = GlobalVariables.ColumnMappings.Where(m => m.TransformationCode != ETransformationCode.SKIP).OrderByDescending(c => c.SourceColumn.Length).First().SourceColumn.Length;
                targetColumnWidth = GlobalVariables.ColumnMappings.Where(m => m.TransformationCode != ETransformationCode.SKIP).OrderByDescending(c => c.TargetColumn.Length).First().TargetColumn.Length;
                transformationColumnWidth = 
                    GlobalVariables.Transformations.Find(tr=>tr.Code ==
                        GlobalVariables.ColumnMappings.Where(m => m.TransformationCode != ETransformationCode.SKIP).OrderByDescending(c => GlobalVariables.Transformations.Find(t=>t.Code == c.TransformationCode).Name.Length).First().TransformationCode).Name.Length;
                customInsertColumnWidth = GlobalVariables.ColumnMappings.Where(m => m.TransformationCode != ETransformationCode.SKIP).OrderByDescending(c => c.CustomInsertValue.Length).First().CustomInsertValue.Length;
                customUpdateColumnWidth = GlobalVariables.ColumnMappings.Where(m => m.TransformationCode != ETransformationCode.SKIP).OrderByDescending(c => c.CustomUpdateValue.Length).First().CustomUpdateValue.Length;
                customDeleteColumnWidth = GlobalVariables.ColumnMappings.Where(m => m.TransformationCode != ETransformationCode.SKIP).OrderByDescending(c => c.CustomDeleteValue.Length).First().CustomDeleteValue.Length;

                if (srcHeaderText.Length > sourceColumnWidth)
                    sourceColumnWidth = srcHeaderText.Length;
                if (trfHeaderText.Length > transformationColumnWidth)
                    transformationColumnWidth = trfHeaderText.Length;
                if (tgtHeaderText.Length > targetColumnWidth)
                    targetColumnWidth = tgtHeaderText.Length;

                if (insertHeaderText.Length > customInsertColumnWidth)
                    customInsertColumnWidth = insertHeaderText.Length;
                if (updateHeaderText.Length > customUpdateColumnWidth)
                    customUpdateColumnWidth = updateHeaderText.Length;
                if (deleteHeaderText.Length > customDeleteColumnWidth)
                    customDeleteColumnWidth = deleteHeaderText.Length;

                rowTemplate = string.Format("| {{0, -{0}}} | {{1, -{1}}} | {{2, -{2}}} | {{3, -{3}}} | {{4, -{4}}} | {{5, -{5}}} |", sourceColumnWidth, transformationColumnWidth, targetColumnWidth, customInsertColumnWidth, customUpdateColumnWidth, customDeleteColumnWidth);

                // Add header
                string hdr = string.Format(rowTemplate, srcHeaderText, trfHeaderText, tgtHeaderText, insertHeaderText, updateHeaderText, deleteHeaderText);
                q.AppendLine(hdr);
                string sss3 = "";
                for (int i = 0; i < hdr.Length; i++)
                    sss3 += "-";
                q.AppendLine(sss3);
                foreach (ColumnMapping cm in GlobalVariables.ColumnMappings.Where(m => m.TransformationCode != ETransformationCode.SKIP))
                {
                    q.AppendLine(string.Format(rowTemplate, cm.SourceColumn, GlobalVariables.Transformations.Find(t => t.Code == cm.TransformationCode).Name, cm.TargetColumn, cm.CustomInsertValue, cm.CustomUpdateValue, cm.CustomDeleteValue));
                    // q.Append(string.Format(exCommentsFormatRow, Environment.NewLine, cm.SourceColumn, cm.TargetColumn, GlobalVariables.Transformations.Find(t => t.Code == cm.TransformationCode).Name));
                }

                q.AppendLine(sss3);

                // Query options
                //q.Append(string.Format("{0}{0}Query Options:", Environment.NewLine));
                //    q.Append(string.Format("{0}{0}When first record for given BusinessKey is added, DateFrom value will be set to {1}", Environment.NewLine, qo.DateFromModeForFirstRecord.ToString()));
                //    if (qo.DateFromModeForFirstRecord == EDateFromMode.Custom)
                //        query.Append(" '" + qo.DateFromCustomValue + "'");
                //    query.Append(string.Format(".{0}", Environment.NewLine));

                //    query.Append(string.Format("When record is not active anymore, DateTo value will be set to {1}", Environment.NewLine, qo.DateToMode.ToString()));
                //    if (qo.DateToMode == EDateToMode.Custom)
                //        query.Append(" '" + qo.DateToCustomValue + "'");
                //    query.Append(string.Format(".{0}", Environment.NewLine));

                //    //qo.DateToDateFromOverlap
                //    string df, dt;
                //    switch (qo.DateToDateFromOverlap)
                //    {
                //        case EDateToDateFromOverlap.DateToEqualsDateFrom: df = "Current DateTime"; dt = "Current DateTime"; break;
                //        case EDateToDateFromOverlap.DateToMinus1DayDateFrom: df = "Current DateTime"; dt = "Current DateTime minus 1 day"; break;
                //        case EDateToDateFromOverlap.DateToDateFromPlus1Day: df = "Current DateTime plus 1 day"; dt = "Current DateTime"; break;
                //        default: df = "ERR"; dt = "ERR"; break;
                //    }
                //    query.Append(string.Format("When newer record for given BusinessKey is added, DateTo for old record will be set to {1} and DateFrom for new record will be set to {2}.{0}", Environment.NewLine, dt, df));

                //    // qo.IgnoreDatabasePrefix
                //    query.Append(string.Format("When source and target database are on the same server, database prefix {1} be ignored.{0}", Environment.NewLine, (qo.IgnoreDatabasePrefix) ? "will" : "won't"));

                //    //qo.RecordsOnTargetNotFoundOnSource
                //    string action = "";
                //    switch (qo.RecordsOnTargetNotFoundOnSource)
                //    {
                //        case ERecordsOnTargetNotFoundOnSourceMode.UpdateDateTo: action = "will be updated - DateTo will be set depending on DateToMode"; break;
                //        case ERecordsOnTargetNotFoundOnSourceMode.PhysicallyDelete: action = "will be physically deleted (possible referential integrity problems)"; break;
                //        case ERecordsOnTargetNotFoundOnSourceMode.DoNothing: action = "won't be changed"; break;
                //    };
                //    query.Append(string.Format("When records on the source are deleted, records on the target {1}.{0}", Environment.NewLine, action));

                //    // qo.UseDatetime2
                //    query.Append(string.Format("My tables {1} datetime2 for storing DateFrom and DateTo values.{0}", Environment.NewLine, (qo.UseDatetime2) ? "use" : "don't use"));
            }

            StringBuilder sb2 = new StringBuilder();
            foreach (string line in Regex.Split(q.ToString(), Environment.NewLine))
            {
                sb2.Append(Environment.NewLine + "-- " + line);
            }
            return sb2.ToString();
        }

        private static string getUserVariablesDefs()
        {
            bool isUsedVar;
            var linesList = GlobalVariables.UserColumnsDefsQuery.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();
            var linesArray = linesList.ToArray();


            string[] usedUserVariables = GlobalVariables.ColumnMappings.Select(c=>c.CustomInsertValue).Union(GlobalVariables.ColumnMappings.Select(c=>c.CustomUpdateValue)).Union(GlobalVariables.ColumnMappings.Select(c=>c.CustomDeleteValue)).Distinct().Where(s=>s.StartsWith("@")).ToArray();

            foreach (string line in linesArray)
            {
                if(line.Contains("@"))
                {
                    isUsedVar = false;
                    foreach(string userVar in usedUserVariables)
                    {
                        if (line.Contains(userVar + " "))
                        {
                            isUsedVar = true;
                            break;
                        }
                    }

                    if (!isUsedVar)
                        linesList.Remove(line);
                }
            }

            if(linesList.LastOrDefault() != null)
            {
                if(linesList.LastOrDefault().LastIndexOf(",") == linesList.LastOrDefault().Length - 1)
                    linesList[linesList.Count - 1] = linesList.LastOrDefault().Substring(0, linesList.LastOrDefault().LastIndexOf(","));
            }

            if (linesList.Count == 2)
                return "-- No custom variables used in this query";
            else
                return string.Join(Environment.NewLine, linesList.ToArray());
        }


        private static string addTabsAtTheBeginningOfEveryLine(string input, int tabsToAdd)
        {
            string[] lines = Regex.Split(input, "\n");
            string tabs = string.Empty;
            string final = string.Empty;

            for (int i = 0; i < tabsToAdd; i++)
            {
                tabs += "\t";
            }

            for (int i = 0; i < lines.Length; i++)
            {
                final += tabs + lines[i] + ((i < lines.Length - 1) ? "\n" : string.Empty);
            }

            return final;
        }

        private static string makeCsvList(string[] values, int tabsCount)
        {
            string s = "";
            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0)
                    s += ",";
                if (i > 0 && i < values.Length)
                    s += Environment.NewLine;
                for (int k = 0; k < tabsCount; k++)
                    s += "\t";

                s += values[i];
            }
            return s;
        }


        private static string makeColumnEqualator(string[] leftColumns, string[] rightColumns, string separator, bool isNullCompare, int tabsCount = 0)
        {
            string s = "";

            if (leftColumns.Length != rightColumns.Length)
                return null;

            for (int i = 0; i < leftColumns.Length; i++)
            {
                if (i > 0)
                    s += separator;
                if (i > 0 && i < leftColumns.Length)
                    s += Environment.NewLine;
                for (int k = 0; k < tabsCount; k++)
                    s += "\t";

                if (isNullCompare)
                    s += string.Format("({0} = {1} OR ({0} IS NULL AND {1} IS NULL))", leftColumns[i], rightColumns[i]);
                else
                    s += string.Format("{0} = {1}", leftColumns[i], rightColumns[i]);
            }

            return s;
        }


        private static string makeColumnComparer(string[] leftColumns, string[] rightColumns, string separator, bool isNullCompare, int tabsCount = 0)
        {
            string s = "";

            if (leftColumns.Length != rightColumns.Length)
                return null;

            if (GlobalVariables.Options.ComparisonMethod == EComparisonMethod.StandardSQL)
            {
                for (int i = 0; i < leftColumns.Length; i++)
                {
                    if (i > 0)
                        s += (" " + separator);
                    if (i > 0 && i < leftColumns.Length)
                        s += Environment.NewLine;
                    for (int k = 0; k < tabsCount; k++)
                        s += "\t";

                    if (isNullCompare)
                        s += string.Format("({0} <> {1} OR ({0} IS NULL AND {1} IS NOT NULL) OR ({0} IS NOT NULL AND {1} IS NULL))", leftColumns[i], rightColumns[i]);
                    else
                        s += string.Format("{0} <> {1}", leftColumns[i], rightColumns[i]);
                }
            }
            else
            {
                string LeftFields = "";
                string RightFields = "";
                for (int i = 0; i < leftColumns.Length; i++)
                {
                    LeftFields += string.Format("{0} as Col{1:X}, ", leftColumns[i], i);
                    RightFields += string.Format("{0} as Col{1:X}, ", rightColumns[i], i);
                }
                LeftFields = LeftFields.Substring(0, LeftFields.Length - 2);
                RightFields = RightFields.Substring(0, RightFields.Length - 2);
                s += string.Format("\tHASHBYTES('MD5', (SELECT {0} FOR xml raw)){2}\t<> HASHBYTES('MD5', (SELECT {1} FOR xml raw))", LeftFields, RightFields, Environment.NewLine);                
            }
            return s;
        }

        private static string makeDecorated(string[] lines)
        {
            string s = "";
            int width = 50;

            s += "-- ";
            for (int i = 0; i < width; i++)
                s += "=";
            foreach (string line in lines)
            {
                s += Environment.NewLine;
                s += "-- " + line;
            }
            s += Environment.NewLine;
            s += "-- ";
            for (int i = 0; i < width; i++)
                s += "=";


            return s;
        }


        public static string GetQuery()
        {
            StringBuilder query = new StringBuilder();
            string[] leftColumns, rightColumns;
            ColumnMapping[] tmpMappings;
            List<string> stringList = new List<string>();
            Guid GUID = Guid.NewGuid();

            removeTablePrefixesIfNecessary();

            //

            query.AppendLine(makeDecorated(getHeader()));

            if (GlobalVariables.Options.ShowExtendedComments)
            {
                query.AppendLine();
                query.AppendLine();
                query.AppendLine(makeDecorated(new[] { "TRANSFORMATIONS" }));
                query.AppendLine(getExtendedComments());
            }

            query.AppendLine();
            query.AppendLine(makeDecorated(new[] { "USER VARIABLES" }));
            query.AppendLine(getUserVariablesDefs());

            query.Append(string.Format("{0}{0}", Environment.NewLine));



            // SCD 1
            if (GlobalVariables.ColumnMappings.Any(scd1 => scd1.TransformationCode == ETransformationCode.SCD1
                    || scd1.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE
                    || scd1.TransformationCode == ETransformationCode.SCD3_DATE_FROM
                    || scd1.TransformationCode == ETransformationCode.SCD3_ORIGINAL_FIRST_VALUE
                    || scd1.TransformationCode == ETransformationCode.SCD3_PREVIOUS_VALUE))
            {
                query.AppendLine(makeDecorated(new[] { "SCD1" + ((GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode.ToString().StartsWith("SCD3_"))) ? " + SCD3" : "") }));

                // MERGE...
                query.Append(string.Format("MERGE {0} as [target]{1}USING{1}({1}", GlobalVariables.TargetObjectName, Environment.NewLine));

                // USING TABLE OR VIEW
                if (GlobalVariables.SourceIsTableOrViewMode)
                {
                    query.AppendLine(string.Format("\tSELECT"));
                    leftColumns = GlobalVariables.ColumnMappings.Select(sc => sc.SourceColumn).Union(GlobalVariables.ColumnMappings.Select(v1 => v1.CustomInsertValue)).Union(GlobalVariables.ColumnMappings.Select(v2 => v2.CustomUpdateValue)).Union(GlobalVariables.ColumnMappings.Select(v3 => v3.CustomDeleteValue)).Where(q => !string.IsNullOrEmpty(q) && !q.StartsWith("@")).ToArray();
                    query.AppendLine(makeCsvList(leftColumns, 2));
                    query.AppendLine(string.Format("\tFROM {0}", GlobalVariables.SourceObjectName));
                }
                // OR USING QUERY
                else
                {
                    query.AppendLine(addTabsAtTheBeginningOfEveryLine(GlobalVariables.SourceCommandText, 1));
                }
                query.Append(string.Format(") as [source]{0}", Environment.NewLine));


                // ON BUSINESS KEYS            
                query.Append(string.Format("ON{0}({0}", Environment.NewLine));

                leftColumns = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode == ETransformationCode.BUSINESS_KEY).Select(sc => "[source]." + sc.SourceColumn).ToArray();
                rightColumns = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode == ETransformationCode.BUSINESS_KEY).Select(sc => "[target]." + sc.TargetColumn).ToArray();
                query.AppendLine(makeColumnEqualator(leftColumns, rightColumns, "AND", false, 1));

                query.AppendLine(string.Format(")"));

                // WHEN MATCHED
                query.Append(string.Format("{0}WHEN MATCHED ", Environment.NewLine));

                // AND THIS IS LAST (ACTIVE) SCD2 RECORD
                if (GlobalVariables.Options.SCD13UpdateMode == ESCD13UpdateMode.UpdateCurrentRecordsOnly)
                {
                    tmpMappings = GlobalVariables.ColumnMappings.Where(m => m.TransformationCode == ETransformationCode.SCD2_DATE_TO || m.TransformationCode == ETransformationCode.SCD2_IS_ACTIVE).ToArray();
                    leftColumns = tmpMappings.Select(c => "[target]." + c.TargetColumn).ToArray();
                    rightColumns = tmpMappings.Select(c => c.GetSourceColumn(EOperation.INSERT, true, ECalledFrom.DEFAULT)).ToArray();
                    if (leftColumns.Length > 0)
                    {
                        query.Append(string.Format("AND{0}({0}", Environment.NewLine));
                        query.Append(makeColumnEqualator(leftColumns, rightColumns, "AND", true, 1));
                        query.Append(string.Format("{0}){0}", Environment.NewLine));
                    }
                }

                // AND WHEN THERE ARE DIFFERENCES IN SCD1 (SCD3) COLUMNS
                leftColumns = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode == ETransformationCode.SCD1 || cm.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE).Select(sc => "[source]." + sc.SourceColumn).ToArray();
                rightColumns = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode == ETransformationCode.SCD1 || cm.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE).Select(sc => "[target]." + sc.TargetColumn).ToArray();
                if (leftColumns.Length > 0)
                {
                    query.Append(string.Format("AND{0}({0}", Environment.NewLine));
                    query.Append(makeColumnComparer(leftColumns, rightColumns, "OR", true, 1));
                    query.Append(string.Format("{0}){0}", Environment.NewLine));
                }

                // AND THERE ARE NO DIFFERENCES IN ALL SCD2 COLUMNS
                if (GlobalVariables.Options.SCD13UpdateMode == ESCD13UpdateMode.UpdateCurrentRecordsOnly)
                {
                    leftColumns = GlobalVariables.ColumnMappings.Where(scd2 => scd2.TransformationCode == ETransformationCode.SCD2).Select(sc => "[source]." + sc.SourceColumn).ToArray();
                    rightColumns = GlobalVariables.ColumnMappings.Where(scd2 => scd2.TransformationCode == ETransformationCode.SCD2).Select(sc => "[target]." + sc.TargetColumn).ToArray();
                    if (leftColumns.Length > 0)
                    {
                        query.Append(string.Format("AND{0}({0}", Environment.NewLine));
                        query.Append(makeColumnEqualator(leftColumns, rightColumns, "AND", true, 1));
                        query.Append(string.Format("{0}){0}", Environment.NewLine));
                    }
                }

                // THEN UPDATE
                query.Append(string.Format("THEN UPDATE{0}SET{0}", Environment.NewLine));

                tmpMappings = GlobalVariables.ColumnMappings.Where(m => m.TransformationCode == ETransformationCode.SCD1 ||
                    m.TransformationCode == ETransformationCode.MODIFIED_DATE ||
                    m.TransformationCode == ETransformationCode.VERSION_NUMBER ||
                    m.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE ||
                    m.TransformationCode == ETransformationCode.SCD3_DATE_FROM ||
                    m.TransformationCode == ETransformationCode.SCD3_PREVIOUS_VALUE).ToArray();
                leftColumns = tmpMappings.Select(tc => "[target]." + tc.TargetColumn).ToArray();
                rightColumns = tmpMappings.Select(c => c.GetSourceColumn(EOperation.UPDATE, true, ECalledFrom.DEFAULT)).ToArray();

                query.AppendLine(makeColumnEqualator(leftColumns, rightColumns, ",", false, 1));


                // WHEN NOT MATCHED (ONLY IF THERE ARE NO SCD2 COLUMNS)
                if (!GlobalVariables.ColumnMappings.Any(scd2 => scd2.TransformationCode == ETransformationCode.SCD2))
                {
                    // WHEN NOT MATCHED THEN INSERT
                    query.AppendLine(string.Format("{0}WHEN NOT MATCHED BY TARGET{0}THEN INSERT{0}({0}", Environment.NewLine));

                    tmpMappings = GlobalVariables.ColumnMappings.Where(sc => sc.TransformationCode != ETransformationCode.SKIP).ToArray();
                    leftColumns = tmpMappings.Select(c => c.TargetColumn).ToArray();

                    query.Append(makeCsvList(leftColumns, 1));

                    // VALUES
                    query.Append(string.Format("{0}){0}VALUES{0}({0}", Environment.NewLine));

                    leftColumns = tmpMappings.Select(c => c.GetSourceColumn(EOperation.INSERT, true, ECalledFrom.DEFAULT)).ToArray();
                    query.AppendLine(makeCsvList(leftColumns, 1));

                    query.Append(string.Format(")"));

                    // WHEN NOT MATCHED BY SOURCE
                    if (GlobalVariables.Options.RecordsOnTargetNotFoundOnSourceMode == ERecordsOnTargetNotFoundOnSourceMode.UpdateTargetField)
                    {
                        query.AppendLine(string.Format("{0}{0}WHEN NOT MATCHED BY SOURCE AND", Environment.NewLine));

                        tmpMappings = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode == ETransformationCode.DELETED_DATE || cm.TransformationCode == ETransformationCode.IS_DELETED).ToArray();

                        leftColumns = tmpMappings.Select(c => c.TargetColumn).ToArray();
                        rightColumns = tmpMappings.Select(c => c.GetSourceColumn(EOperation.INSERT, true, ECalledFrom.DEFAULT)).ToArray();
                        if (leftColumns.Length > 0)
                            query.AppendLine(makeColumnEqualator(leftColumns, rightColumns, "AND", true, 1));

                        query.AppendLine(string.Format("{0}THEN UPDATE{0}SET{0}", Environment.NewLine));

                        leftColumns = tmpMappings.Select(c => c.TargetColumn).ToArray();
                        rightColumns = tmpMappings.Select(c => c.GetSourceColumn(EOperation.DELETE, true, ECalledFrom.DEFAULT)).ToArray();

                        query.AppendLine(makeColumnEqualator(leftColumns, rightColumns, ",", false, 1));
                    }

                    else if (GlobalVariables.Options.RecordsOnTargetNotFoundOnSourceMode == ERecordsOnTargetNotFoundOnSourceMode.PhysicallyDelete)
                    {
                        query.Append(string.Format("{0}{0}WHEN NOT MATCHED BY SOURCE{0}THEN DELETE", Environment.NewLine));
                    }

                }
                query.AppendLine(";");
            }



            // SCD2 + SCD3
            if (GlobalVariables.ColumnMappings.Any(scd2 => scd2.TransformationCode == ETransformationCode.SCD2))
            {
                query.AppendLine();
                query.AppendLine();
                query.AppendLine(makeDecorated(new[] { "SCD2" + ((GlobalVariables.ColumnMappings.Any(cm => cm.TransformationCode.ToString().StartsWith("SCD3_"))) ? " + SCD3" : "") }));

                query.Append(string.Format("INSERT INTO {1}{0}({0}", Environment.NewLine, GlobalVariables.TargetObjectName));

                // ENUM COLUMNS
                tmpMappings = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode != ETransformationCode.SKIP).ToArray();
                leftColumns = tmpMappings.Select(c => c.TargetColumn).ToArray();
                query.AppendLine(makeCsvList(leftColumns, 1));

                // SELECT COLUMNS
                query.Append(string.Format("){0}SELECT{0}", Environment.NewLine));
                //leftColumns = tmpMappings.Select(cm => cm.GetSourceColumn(EOperation.INSERT, false)).ToArray();
                leftColumns = tmpMappings.Select(cm => cm.TargetColumn).ToArray();
                query.AppendLine(makeCsvList(leftColumns, 1));


                // FROM
                query.Append(string.Format("FROM{0}(", Environment.NewLine));

                query.Append(string.Format("{0}\tMERGE {1} as [target]{0}\tUSING{0}\t({0}", Environment.NewLine, GlobalVariables.TargetObjectName));


                // USING TABLE OR VIEW
                if (GlobalVariables.SourceIsTableOrViewMode)
                {
                    query.AppendLine(string.Format("\t\tSELECT"));
                    leftColumns = GlobalVariables.ColumnMappings.Select(sc => sc.SourceColumn).Union(GlobalVariables.ColumnMappings.Select(v1 => v1.CustomInsertValue)).Union(GlobalVariables.ColumnMappings.Select(v2 => v2.CustomUpdateValue)).Union(GlobalVariables.ColumnMappings.Select(v3 => v3.CustomDeleteValue)).Where(q => !string.IsNullOrEmpty(q) && !q.StartsWith("@")).ToArray();
                    query.AppendLine(makeCsvList(leftColumns, 3));
                    query.AppendLine(string.Format("\t\tFROM {0}", GlobalVariables.SourceObjectName));
                }
                // OR USING QUERY
                else
                {
                    query.Append(addTabsAtTheBeginningOfEveryLine(GlobalVariables.SourceCommandText, 1));
                }

                query.Append(string.Format("{0}\t) as [source]{0}", Environment.NewLine));

                // ON BUSINESS KEYS            
                query.Append(string.Format("\tON{0}\t({0}", Environment.NewLine));
                leftColumns = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode == ETransformationCode.BUSINESS_KEY).Select(sc => "[source]." + sc.SourceColumn).ToArray();
                rightColumns = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode == ETransformationCode.BUSINESS_KEY).Select(sc => "[target]." + sc.TargetColumn).ToArray();
                query.AppendLine(makeColumnEqualator(leftColumns, rightColumns, "AND", false, 2));
                query.Append(string.Format("\t){0}{0}", Environment.NewLine));

                //WHEN NOT MATCHED
                query.Append(string.Format("\tWHEN NOT MATCHED BY TARGET{0}\tTHEN INSERT{0}\t({0}", Environment.NewLine));

                tmpMappings = GlobalVariables.ColumnMappings.Where(sc => sc.TransformationCode != ETransformationCode.SKIP).ToArray();
                leftColumns = tmpMappings.Select(c => c.TargetColumn).ToArray();
                query.AppendLine(makeCsvList(leftColumns, 2));

                query.Append(string.Format("\t){0}\tVALUES{0}\t({0}", Environment.NewLine));
                leftColumns = tmpMappings.Select(c => c.GetSourceColumn(EOperation.INSERT, false, ECalledFrom.DEFAULT)).ToArray();
                query.AppendLine(makeCsvList(leftColumns, 2));


                query.Append(string.Format("\t){0}", Environment.NewLine));

                if (GlobalVariables.Options.RecordsOnTargetNotFoundOnSourceMode != ERecordsOnTargetNotFoundOnSourceMode.DoNothing)
                    query.Append(string.Format("{0}\t", Environment.NewLine));


                if (GlobalVariables.Options.RecordsOnTargetNotFoundOnSourceMode == ERecordsOnTargetNotFoundOnSourceMode.UpdateTargetField)
                {
                    query.AppendLine(string.Format("{0}{0}WHEN NOT MATCHED BY SOURCE AND", Environment.NewLine));

                    tmpMappings = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode == ETransformationCode.SCD2_DATE_TO ||
                        cm.TransformationCode == ETransformationCode.SCD2_IS_ACTIVE ||
                        cm.TransformationCode == ETransformationCode.DELETED_DATE ||
                        cm.TransformationCode == ETransformationCode.IS_DELETED).ToArray();

                    leftColumns = tmpMappings.Select(c => c.TargetColumn).ToArray();
                    rightColumns = tmpMappings.Select(c => c.GetSourceColumn(EOperation.INSERT, true, ECalledFrom.DEFAULT)).ToArray();
                    if (leftColumns.Length > 0)
                        query.AppendLine(makeColumnEqualator(leftColumns, rightColumns, "AND", true, 1));

                    query.AppendLine(string.Format("{0}THEN UPDATE{0}SET{0}", Environment.NewLine));

                    leftColumns = tmpMappings.Select(c => c.TargetColumn).ToArray();
                    rightColumns = tmpMappings.Select(c => c.GetSourceColumn(EOperation.DELETE, true, ECalledFrom.DEFAULT)).ToArray();

                    query.AppendLine(makeColumnEqualator(leftColumns, rightColumns, ",", false, 1));
                }

                else if (GlobalVariables.Options.RecordsOnTargetNotFoundOnSourceMode == ERecordsOnTargetNotFoundOnSourceMode.PhysicallyDelete)
                {
                    query.Append(string.Format("WHEN NOT MATCHED BY SOURCE{0}\tTHEN DELETE{0}", Environment.NewLine));
                }

                query.AppendLine();
                query.AppendLine();

                // WHEN MATCHED
                query.Append(string.Format("WHEN MATCHED "));

                tmpMappings = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode == ETransformationCode.SCD2_DATE_TO || cm.TransformationCode == ETransformationCode.SCD2_IS_ACTIVE).ToArray();
                leftColumns = tmpMappings.Select(c => c.TargetColumn).ToArray();
                rightColumns = tmpMappings.Select(c => c.GetSourceColumn(EOperation.INSERT, true, ECalledFrom.DEFAULT)).ToArray();
                if (leftColumns.Length > 0)
                {
                    query.Append(string.Format("AND{0}({0}", Environment.NewLine));
                    query.Append(makeColumnEqualator(leftColumns, rightColumns, "AND", true, 1));
                    query.Append(string.Format("{0}){0}", Environment.NewLine));
                }

                // AND THERE ARE DIFFERENCES IN SCD2's
                tmpMappings = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode == ETransformationCode.SCD2 || cm.TransformationCode == ETransformationCode.SCD3_CURRENT_VALUE).ToArray();
                leftColumns = tmpMappings.Select(c => "[target]." + c.TargetColumn).ToArray();
                rightColumns = tmpMappings.Select(c => "[source]." + c.SourceColumn).ToArray();
                if (leftColumns.Length > 0)
                {
                    query.Append(string.Format("AND{0}({0}", Environment.NewLine));
                    query.AppendLine(makeColumnComparer(leftColumns, rightColumns, "OR", true, 1));
                    query.Append(string.Format("{0}){0}", Environment.NewLine));
                }

                query.Append(string.Format("\tTHEN UPDATE{0}\tSET{0}", Environment.NewLine));

                tmpMappings = GlobalVariables.ColumnMappings.Where(cm =>
                    //cm.TransformationCode == ETransformationCode.SCD2 ||
                    cm.TransformationCode == ETransformationCode.SCD2_DATE_TO ||
                    cm.TransformationCode == ETransformationCode.SCD2_IS_ACTIVE ||
                    cm.TransformationCode == ETransformationCode.MODIFIED_DATE /*||
                    cm.TransformationCode == ETransformationCode.VERSION_NUMBER*/).ToArray();

                leftColumns = tmpMappings.Select(cm => cm.TargetColumn).ToArray();
                rightColumns = tmpMappings.Select(cm => cm.GetSourceColumn(EOperation.UPDATE, true, ECalledFrom.DEFAULT)).ToArray();
                query.AppendLine(makeColumnEqualator(leftColumns, rightColumns, ",", false, 2));


                // OUTPUT
                query.Append(string.Format("{0}{0}\tOUTPUT{0}\t\t$Action as [MERGE_ACTION_{1}],{0}", Environment.NewLine, GUID));

                tmpMappings = GlobalVariables.ColumnMappings.Where(cm => cm.TransformationCode != ETransformationCode.SKIP).ToArray();


                leftColumns = tmpMappings.Select(c => c.GetSourceColumn(EOperation.INSERT, true, ECalledFrom.SCD2_OUTPUT) + " AS " + c.TargetColumn).ToArray();
                Array.Sort(leftColumns, delegate(string s1, string s2)
                {
                    var tmp1 = s1.Substring(s1.LastIndexOf("["));
                    var tmp2 = s2.Substring(s2.LastIndexOf("["));
                    return tmp1.CompareTo(tmp2);
                });


                query.AppendLine(makeCsvList(leftColumns, 2));

                query.Append(string.Format("{0})MERGE_OUTPUT{0}WHERE MERGE_OUTPUT.[MERGE_ACTION_{1}] = 'UPDATE' {0}", Environment.NewLine, GUID));

                foreach (ColumnMapping cm in GlobalVariables.ColumnMappings.Where(m => m.TransformationCode == ETransformationCode.BUSINESS_KEY))
                {
                    query.AppendLine(string.Format("\tAND MERGE_OUTPUT.{0} IS NOT NULL", cm.TargetColumn));
                }

                query.Append(";");
            }

            return query.ToString();

        }
    }


}
