using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.ComponentModel;
using ScdMergeWizard.BckGrndWorker;
using ScdMergeWizard.ExcHandling;

namespace ScdMergeWizard.Database
{
    public abstract class MyBaseDbConnection
    {
        protected DbConnection _conn;
        protected string _connectionString;


        public MyBaseDbConnection(string connectionString)
        {
            CloseConnection();
            _connectionString = connectionString;
        }
        public MyBaseDbConnection(DbConnection cnn)
        {
            _conn = cnn;
            _connectionString = cnn.ConnectionString;
        }

        private void CloseConnection()
        {
            if (_conn != null && _conn.State == ConnectionState.Open)
                _conn.Close();
            if (_conn != null)
            {
                _conn.Dispose();
                _conn = null;
            }
        }

        public abstract DbConnection GetConn();
        public abstract DbCommand CreateCommand();
        public abstract DbCommandBuilder CreateCommandBuilder(DbDataAdapter adp);
        public abstract DbDataAdapter CreateAdapter(string sql);

        public bool IsConnectionOpened()
        {
            if (this.GetConn() != null && this.GetConn().State == ConnectionState.Open)
                return true;

            return false;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
