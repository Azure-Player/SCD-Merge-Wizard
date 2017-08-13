using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;
using ScdMergeWizard.BckGrndWorker;
using ScdMergeWizard.ExcHandling;

namespace ScdMergeWizard.Database
{
    public static class DbHelper
    {
        public static MyDbObject[] GetTablesViewsAndSynonyms(MyBaseDbConnection conn)
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
                    if (row[3].ToString().Contains("TABLE"))
                        _objects.Add(new MyDbObject { Name = string.Format("[{0}].[{1}].[{2}]", row[0].ToString(), row[1].ToString(), row[2].ToString()), Type = EDbObjectType.Table });
                    if (row[3].ToString().Contains("VIEW"))
                        _objects.Add(new MyDbObject { Name = string.Format("[{0}].[{1}].[{2}]", row[0].ToString(), row[1].ToString(), row[2].ToString()), Type = EDbObjectType.View });
                    if (row[3].ToString().Contains("SYNONYM"))
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

        public static MyDbColumn[] GetColumns(MyBaseDbConnection conn, string query)
        {
            List<MyDbColumn> columns = new List<MyDbColumn>();

            DbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SET FMTONLY ON; " + query + "; SET FMTONLY OFF";
            DbDataReader reader = cmd.ExecuteReader();

            DataTable sch = reader.GetSchemaTable();
            for (int i = 0; i < sch.Rows.Count; i++)
            {
                MyDbColumn c = new MyDbColumn();
                c.AllowDBNull = (bool)reader.GetSchemaTable().Rows[i]["AllowDBNull"];
                //BaseCatalogName = 
                //BaseColumnName = 
                //BaseSchemaName = 
                //BaseTableName = 
                c.ColumnName = "[" + (string)sch.Rows[i]["ColumnName"] + "]";
                c.ColumnOrdinal = (int)sch.Rows[i]["ColumnOrdinal"];
                c.ColumnSize = (int)sch.Rows[i]["ColumnSize"];
                c.DataType = (Type)sch.Rows[i]["DataType"];
                c.IsAutoIncrement = (bool)sch.Rows[i]["IsAutoIncrement"];
                c.IsKey = (!string.IsNullOrEmpty(sch.Rows[i]["IsKey"].ToString()) ? bool.Parse(sch.Rows[i]["IsKey"].ToString()) : false);
                c.IsLong = (bool)sch.Rows[i]["IsLong"];
                c.IsReadOnly = (bool)sch.Rows[i]["IsReadOnly"];
                c.IsRowVersion = (bool)sch.Rows[i]["IsRowVersion"];
                c.IsUnique = (bool)sch.Rows[i]["IsUnique"];
                c.NumericPrecision = (sch.Rows[i]["NumericPrecision"] != null && !string.IsNullOrEmpty(sch.Rows[i]["NumericPrecision"].ToString())) ? Convert.ToInt32(sch.Rows[i]["NumericPrecision"].ToString()) : -1;
                c.NumericScale = (sch.Rows[i]["NumericScale"] != null && !string.IsNullOrEmpty(sch.Rows[i]["NumericScale"].ToString())) ? Convert.ToInt32(sch.Rows[i]["NumericScale"].ToString()) : -1;
                c.ProviderType = (OleDbType)(int)sch.Rows[i]["ProviderType"];
                c.OleDbDataTypeString = reader.GetDataTypeName(i);
                columns.Add(c);
            }
            reader.Close();
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

        public static MyBaseDbConnection CreateConnection(string connectionString)
        {
            return new Database.MyAdoDbConnection(connectionString);
        }

    }
}
