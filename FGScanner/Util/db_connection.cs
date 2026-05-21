using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

            string connection = $"Data Source={server};Initial Catalog={db};User ID={user};Password={pass};Encrypt=False";

            if (!CanConnect(connection))
            {
                throw new Exception($"Unable to connect to the databaser server: {server}");
            }
            Console.WriteLine($"{connection}");
            _dbConnectionString = connection;
        }

        public SqlConnection Getconnection()
        {
            var conn = new SqlConnection(_dbConnectionString);
            return conn;
        }

        private bool CanConnect(string connectionString)
        {
            try
            {
                using(SqlConnection  conn = new SqlConnection(connectionString))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
