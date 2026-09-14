using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;

namespace FGScanner.Util
{
    public class db_connection
    {
       private readonly string _dbConnectionString;

        public db_connection()
        {
            string folder = @"C:\FGIMS";
            Directory.CreateDirectory(folder);
            string envPath = Path.Combine(folder, "dbconfig.env");

            Env.Load(envPath);

            string server = Environment.GetEnvironmentVariable("DB_SERVER");
            string db = Environment.GetEnvironmentVariable("DB_NAME");
            string user = Environment.GetEnvironmentVariable("DB_USER");
            string pass = Environment.GetEnvironmentVariable("DB_PASSWORD");

            if (string.IsNullOrWhiteSpace(server))
                throw new Exception("Database server not found.");

            _dbConnectionString = $"Data Source={server};Initial Catalog={db};User ID={user};Password={pass};Encrypt=False";
        }

        public SqlConnection Getconnection()
        {
            var conn = new SqlConnection(_dbConnectionString);
            return conn;
        }
    }
}
