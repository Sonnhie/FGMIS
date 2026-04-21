using FGScanner.Util;
using OfficeOpenXml;
using OfficeOpenXml.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FGScanner
{
    internal static class Program
    {
        private static readonly db_connection _Connection = new db_connection();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            SplashScreen splash = new SplashScreen();
            splash.Show();
            splash.Refresh();
            LoadResources(splash);
            //Task.Run(() => LoadResources(splash))
            //    .ContinueWith(test =>
            //    {
            //        splash.Invoke(new Action(() =>
            //        {
            //            warehousemenu menu = new warehousemenu();
            //            menu.Show();
            //            splash.Close();
            //        }));
            //    });
            splash.Close();

            Application.Run(new login());
        }

        static void LoadResources(SplashScreen splash)
        {
            splash.UpdateProgress(10, "Loading configuration...");
            Thread.Sleep(500);

            splash.UpdateProgress(20, "Initializing database...");
            DatabaseTest();
            Thread.Sleep(500);

            splash.UpdateProgress(40,"Loading Excel library...");
            Thread.Sleep(500);

            splash.UpdateProgress(60, "Loading Rack location...");
            Thread.Sleep(500);

            splash.UpdateProgress(80, "Finalizing...");
            Thread.Sleep(300);

            splash.UpdateProgress(100, "Starting system...");
            Thread.Sleep(300);
        }

        static void DatabaseTest()
        {
            using (SqlConnection conn = _Connection.Getconnection())
            {
               if(conn == null)
                    throw new Exception("Failed to create database connection.");
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM actual_inventory", conn))
                    {
                        cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database connection failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
        }
    }
}