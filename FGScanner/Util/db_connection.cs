using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.Util
{
    public class db_connection
    {
       private readonly string _dbConnectionString;

        public db_connection()
        {
            _dbConnectionString = "Data Source=192.168.101.41;Initial Catalog=InventoryDB;User ID=Administrator;Encrypt=False";
        }

        public SqlConnection Getconnection()
        {
            var conn = new SqlConnection(_dbConnectionString);
            return conn;
        }
    }
}
