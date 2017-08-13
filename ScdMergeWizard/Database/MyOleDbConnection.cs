using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.ComponentModel;
using ScdMergeWizard.BckGrndWorker;
using ScdMergeWizard.ExcHandling;

namespace ScdMergeWizard.Database
{
    public class MyOleDbConnection
    {
        private OleDbConnection _conn;
        private string _connectionString;


        public MyOleDbConnection(string connectionString)
        {
            CloseConnection();
            _connectionString = connectionString;
        }

        public OleDbConnection GetConn()
        {
            try
            {
                if (_conn == null && !string.IsNullOrEmpty(_connectionString))
                    _conn = new OleDbConnection(_connectionString);

                if (_conn != null && _conn.State != ConnectionState.Open)
                    _conn.Open();

                return _conn;
            }
            catch (Exception ex)
            {
                MyExceptionHandler.NewEx(ex);
            }
            return null;
        }


        public bool IsConnectionOpened()
        {
            if (this.GetConn() != null && this.GetConn().State == ConnectionState.Open)
                return true;

            return false;
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

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
