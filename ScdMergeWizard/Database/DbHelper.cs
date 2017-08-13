using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using ScdMergeWizard.BckGrndWorker;
using ScdMergeWizard.ExcHandling;

namespace ScdMergeWizard.Database
{
    public static class DbHelper
    {
        public static MyDbObject[] GetTablesViewsAndSynonyms(MyOleDbConnection conn)
        {
            List<MyDbObject> _objects = new List<MyDbObject>();

            try
            {
                DataTable t = conn.GetConn().GetSchema("Tables");

                List<string> lst = new List<string>();
                foreach (DataRow row in t.Rows)
                {
                    lst.Add(row[3].ToString());
                }

                foreach (DataRow row in t.Rows)
                {
                    if (row[3].ToString() == "TABLE")
                        _objects.Add(new MyDbObject { Name = string.Format("[{0}].[{1}].[{2}]", row[0].ToString(), row[1].ToString(), row[2].ToString()), Type = EDbObjectType.Table });
                    if (row[3].ToString() == "VIEW")
                        _objects.Add(new MyDbObject { Name = string.Format("[{0}].[{1}].[{2}]", row[0].ToString(), row[1].ToString(), row[2].ToString()), Type = EDbObjectType.View });
                    if (row[3].ToString() == "SYNONYM")
                        _objects.Add(new MyDbObject { Name = string.Format("[{0}].[{1}].[{2}]", row[0].ToString(), row[1].ToString(), row[2].ToString()), Type = EDbObjectType.Synonym });
                }


                //_objects.Add(new MyDbObject { Name = "Test", Type = EDbObjectType.View });

                return _objects.ToArray();
            }
            catch
            {

            }
            return null;
        }

        public static MyDbColumn[] GetColumns(MyOleDbConnection conn, string query)
        {
            List<MyDbColumn> columns = new List<MyDbColumn>();

            OleDbCommand cmd = conn.GetConn().CreateCommand();
            cmd.CommandText = "SET FMTONLY ON; " + query + "; SET FMTONLY OFF";
            OleDbDataReader reader = cmd.ExecuteReader();


            for (int i = 0; i < reader.GetSchemaTable().Rows.Count; i++)
            {
                columns.Add(new MyDbColumn
                {
                    AllowDBNull = (bool)reader.GetSchemaTable().Rows[i]["AllowDBNull"],
                    //BaseCatalogName = 
                    //BaseColumnName = 
                    //BaseSchemaName = 
                    //BaseTableName = 
                    ColumnName = "[" + (string)reader.GetSchemaTable().Rows[i]["ColumnName"] + "]",
                    ColumnOrdinal = (int)reader.GetSchemaTable().Rows[i]["ColumnOrdinal"],
                    ColumnSize = (int)reader.GetSchemaTable().Rows[i]["ColumnSize"],
                    DataType = (Type)reader.GetSchemaTable().Rows[i]["DataType"],
                    IsAutoIncrement = (bool)reader.GetSchemaTable().Rows[i]["IsAutoIncrement"],
                    IsKey = (bool)reader.GetSchemaTable().Rows[i]["IsKey"],
                    IsLong = (bool)reader.GetSchemaTable().Rows[i]["IsLong"],
                    IsReadOnly = (bool)reader.GetSchemaTable().Rows[i]["IsReadOnly"],
                    IsRowVersion = (bool)reader.GetSchemaTable().Rows[i]["IsRowVersion"],
                    IsUnique = (bool)reader.GetSchemaTable().Rows[i]["IsUnique"],
                    NumericPrecision = (reader.GetSchemaTable().Rows[i]["NumericPrecision"] != null && !string.IsNullOrEmpty(reader.GetSchemaTable().Rows[i]["NumericPrecision"].ToString())) ? Convert.ToInt32(reader.GetSchemaTable().Rows[i]["NumericPrecision"].ToString()) : -1,
                    NumericScale = (reader.GetSchemaTable().Rows[i]["NumericScale"] != null && !string.IsNullOrEmpty(reader.GetSchemaTable().Rows[i]["NumericScale"].ToString())) ? Convert.ToInt32(reader.GetSchemaTable().Rows[i]["NumericScale"].ToString()) : -1,
                    ProviderType = (OleDbType)(int)reader.GetSchemaTable().Rows[i]["ProviderType"],

                    OleDbDataTypeString = reader.GetDataTypeName(i)
                });
            }


            return columns.ToArray();
        }

        public static string GetBuiltConnectionString(string inputConnectionString)
        {
            MSDASC.DataLinks oDL = null;
            string res = string.Empty;
            
            try
            {
                oDL = new MSDASC.DataLinksClass();
                ADODB.Connection conn = new ADODB.ConnectionClass();

                conn.ConnectionString = inputConnectionString;
                object oConn = (object)conn;
                if (oDL.PromptEdit(ref oConn))
                    res = conn.ConnectionString;
            }
            catch
            {
                try
                {
                    ADODB._Connection oConn = (ADODB._Connection)oDL.PromptNew();
                    if (oConn != null)
                        res = oConn.ConnectionString.ToString();
                }
                catch (Exception ex)
                {
                    MyExceptionHandler.NewEx(ex);
                }
            }

            return string.IsNullOrEmpty(res) ? null : res;
        }
    }
}
