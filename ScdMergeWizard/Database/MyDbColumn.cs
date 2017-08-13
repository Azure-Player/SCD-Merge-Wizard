using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace ScdMergeWizard.Database
{
    public class MyDbColumn
    {
        public string ColumnName;
        public int ColumnOrdinal;
        public int ColumnSize;
        public int NumericPrecision;
        public int NumericScale;
        public Type DataType;
        public OleDbType ProviderType;
        public EOleDbDataType OleDbDataType;
        public string OleDbDataTypeString;
        public bool IsLong;
        public bool? AllowDBNull;
        public bool IsReadOnly;
        public bool IsRowVersion;
        public bool IsUnique;
        public bool IsKey;
        public bool IsAutoIncrement;
        public string BaseSchemaName;
        public string BaseCatalogName;
        public string BaseTableName;
        public string BaseColumnName;
        public string SqlDbTypeString
        {
            get 
            {
                switch (ProviderType)
                {
                    case OleDbType.BigInt: return "bigint";
                    case OleDbType.Binary: return string.Format("binary({0})", ColumnSize);
                    case OleDbType.Boolean: return "bit";
                    case OleDbType.BSTR: return string.Format("varchar({0})", ColumnSize);
                    case OleDbType.Char: return string.Format("char({0})", ColumnSize);
                    case OleDbType.Currency: return "money";
                    case OleDbType.Date: return "date";
                    case OleDbType.DBDate: return "date";
                    case OleDbType.DBTime: return "time";
                    case OleDbType.DBTimeStamp: return "smalldatetime";
                    case OleDbType.Decimal: return string.Format("decimal({0,1})", NumericPrecision, NumericScale);
                    case OleDbType.Double: return string.Format("float({0,1})", NumericPrecision, NumericScale);
                    case OleDbType.Guid: return "uniqueidentifier";
                    case OleDbType.Integer: return "int";
                    case OleDbType.Numeric: return string.Format("numeric({0,1})", NumericPrecision, NumericScale);
                    case OleDbType.SmallInt: return "smallint";
                    case OleDbType.TinyInt: return "tinyint";
                    case OleDbType.VarBinary: return string.Format("varbinary({0})", ColumnSize);
                    case OleDbType.VarChar: return string.Format("varchar({0})", ColumnSize);
                    case OleDbType.Variant: return "sql_variant";
                    default: return string.Empty;
                };
            }
        }
        public bool IsNumeric
        {
            get
            {
                string dataType = DataType.ToString();

                return dataType.Equals("System.Byte") ||
                    dataType.Equals("System.SByte") ||
                    dataType.Equals("System.Int16") ||
                    dataType.Equals("System.UInt16") ||
                    dataType.Equals("System.Int32") ||
                    dataType.Equals("System.UInt32") ||
                    dataType.Equals("System.Int64") ||
                    dataType.Equals("System.UInt64") ||
                    dataType.Equals("System.Single") ||
                    dataType.Equals("System.Double") ||
                    dataType.Equals("System.Decimal");
            }
        }

        public ETransformationCode TransformationType;

        public override string ToString()
        {
            return ColumnName;
        }

        public enum EOleDbDataType
        {
            DBTYPE_BYTES,
            DBTYPE_BOOL,
            DBTYPE_STR,
            DBTYPE_DBTIMESTAMP,
            DBTYPE_DBTIME2,
            DBTYPE_NUMERIC,
            DBTYPE_R8,
            DBTYPE_I4,
            DBTYPE_CY,
            DBTYPE_WSTR,
            DBTYPE_R4,
            DBTYPE_I2,
            DBTYPE_VARIANT,
            DBTYPE_SQLVARIANT,
            DBTYPE_UI1,
            DBTYPE_UDT,
            DBTYPE_GUID,
            DBTYPE_XML
        }
    }
}
