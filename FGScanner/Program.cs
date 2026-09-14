using FGScanner.Util;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace FGScanner
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (SplashScreen splash = new())
            {
                splash.Show();
                splash.Refresh();

                try
                {
                    LoadResources(splash);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"FGIMS could not start.\n\n{ex.Message}",
                        "Startup error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                splash.Close();
            }

            Application.Run(new login());
        }

        static void LoadResources(SplashScreen splash)
        {
            Stopwatch minimumDisplayTime = Stopwatch.StartNew();

            splash.UpdateProgress(10, "Reading secure database configuration...");
            db_connection connection = new();

            splash.UpdateProgress(35, "Connecting to the inventory database...");
            DatabaseTest(connection);

            splash.UpdateProgress(65, "Initializing spreadsheet and reporting services...");
            ExcelPackage.License.SetNonCommercialPersonal("NIDEC");

            splash.UpdateProgress(85, "Checking application templates and resources...");
            string templatePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Templates");
            if (!System.IO.Directory.Exists(templatePath))
                throw new System.IO.DirectoryNotFoundException("The application Templates folder is missing.");

            splash.UpdateProgress(100, "Ready. Opening secure sign-in...");

            int remainingMs = 900 - (int)minimumDisplayTime.ElapsedMilliseconds;
            if (remainingMs > 0)
                Thread.Sleep(remainingMs);
        }

        static void DatabaseTest(db_connection connection)
        {
            using SqlConnection conn = connection.Getconnection();
            conn.Open();
            using SqlCommand cmd = new(
                "SELECT CASE WHEN OBJECT_ID('dbo.actual_inventory','U') IS NULL THEN 0 ELSE 1 END",
                conn);
            int exists = Convert.ToInt32(cmd.ExecuteScalar());
            if (exists != 1)
                throw new Exception("The configured database does not contain the inventory tables required by FGIMS.");
        }
    }
}
