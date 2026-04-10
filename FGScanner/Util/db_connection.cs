using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FGScanner.Util
{
    public class db_connection
    {
       private readonly string _dbConnectionString;

        public db_connection()
        {
            var connStr = ConfigurationManager.ConnectionStrings["InventoryDb"];

            if (connStr == null || string.IsNullOrEmpty(connStr.ConnectionString))
            {
                throw new Exception("Connection string 'InventoryDb' not found in App.config.");
            }

            _dbConnectionString = connStr.ConnectionString;
        }

        public SqlConnection Getconnection()
        {
            var conn = new SqlConnection(_dbConnectionString);
            return conn;
        }
    }
}
