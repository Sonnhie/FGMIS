using FGScanner.Util;
using FGScanner.Services;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

            splash.UpdateProgress(8, "Reading secure database configuration...");
            db_connection connection = new();

            splash.UpdateProgress(25, "Connecting to the inventory database...");
            DatabaseTest(connection);

            splash.UpdateProgress(48, "Initializing spreadsheet and reporting services...");
            ExcelPackage.License.SetNonCommercialPersonal("NIDEC");

            splash.UpdateProgress(65, "Checking required application files...");
            CheckApplicationFiles();

            splash.UpdateProgress(82, "Checking real-time inventory hub...");
            bool hubConnected = InventoryRealtimeClient
                .CheckConnectionAsync(TimeSpan.FromSeconds(3))
                .GetAwaiter()
                .GetResult();

            string realtimeStatus = hubConnected
                ? "Real-time updates connected."
                : InventoryRealtimeClient.IsConfigured
                    ? "Hub offline; periodic refresh is enabled."
                    : "Hub not configured; periodic refresh is enabled.";

            splash.UpdateProgress(100, $"Ready. {realtimeStatus} Opening secure sign-in...");

            int remainingMs = 900 - (int)minimumDisplayTime.ElapsedMilliseconds;
            if (remainingMs > 0)
                Thread.Sleep(remainingMs);
        }

        static void CheckApplicationFiles()
        {
            string templatePath = Path.Combine(AppContext.BaseDirectory, "Templates");
            string[] requiredFiles =
            {
                "packinglist.xlsx",
                "TransferSlip.xlsx",
                "SF-78-FG003_Rev.00_Stock Card.xlsx"
            };

            if (!Directory.Exists(templatePath))
            {
                throw new DirectoryNotFoundException(
                    $"The application Templates folder is missing: {templatePath}");
            }

            string[] missingFiles = requiredFiles
                .Where(fileName =>
                {
                    string filePath = Path.Combine(templatePath, fileName);
                    return !File.Exists(filePath) || new FileInfo(filePath).Length == 0;
                })
                .ToArray();

            if (missingFiles.Length > 0)
            {
                throw new FileNotFoundException(
                    "Required application files are missing or empty: " +
                    string.Join(", ", missingFiles));
            }
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
