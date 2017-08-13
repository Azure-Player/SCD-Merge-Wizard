using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.ComponentModel;
using ScdMergeWizard.ExcHandling;

namespace ScdMergeWizard.Database
{
    public class MyAdoDbConnection : MyBaseDbConnection
    {
        public MyAdoDbConnection(string connectionString) : base(connectionString) { }
        public MyAdoDbConnection(SqlConnection cnn) : base(cnn) { }

        public override DbConnection GetConn()
        {
            try
            {
                if (_conn == null && !string.IsNullOrEmpty(_connectionString))
                    _conn = new SqlConnection(_connectionString);

                if (_conn != null && _conn.State != ConnectionState.Open)
                    _conn.Open();

                return _conn as SqlConnection;
            }
            catch (Exception ex)
            {
                MyExceptionHandler.NewEx(ex);
            }
            return null;
        }

        public override DbCommand CreateCommand()
        {
            return GetConn().CreateCommand();
        }

        public override DbDataAdapter CreateAdapter(string sql)
        {
            return new SqlDataAdapter(sql, GetConn() as SqlConnection);
        }

        public override DbCommandBuilder CreateCommandBuilder(DbDataAdapter adp)
        {
            return new SqlCommandBuilder(adp as SqlDataAdapter);
        }

    }
}
