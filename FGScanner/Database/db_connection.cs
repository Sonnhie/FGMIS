using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;

namespace FGScanner.Database
{
    public class db_connection
    {
        public static string GetConnectionString()
        {
            string folder = @"C:\FGIMS";
            string envPath = Path.Combine(folder, "dbconfig.env");

            // Only load if the file exists
            if (File.Exists(envPath))
            {
                Env.Load(envPath);
            }

            string server = Environment.GetEnvironmentVariable("DB_SERVER");
            string db = Environment.GetEnvironmentVariable("DB_NAME");
            string user = Environment.GetEnvironmentVariable("DB_USER");
            string pass = Environment.GetEnvironmentVariable("DB_PASSWORD");

            if (string.IsNullOrWhiteSpace(server))
            {
                throw new Exception("Database server not found in environment variables.");
            }

            return $"Data Source={server};Initial Catalog={db};User ID={user};Password={pass};Encrypt=False";
        }
    }
}
