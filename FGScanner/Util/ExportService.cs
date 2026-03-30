using OfficeOpenXml;
using OfficeOpenXml.LoadFunctions.Params;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner.Util
{
    public class ExportService
    {
        public static async Task ExportCSV(DataTable data, string filepath, IProgress<int> progress, string worksheetName)
        {
            ExcelPackage.License.SetNonCommercialPersonal("NIDEC");

            try
            {
               if(data == null || data.Rows.Count == 0)
                    throw new ArgumentNullException(nameof(data), "Data is empty or null.");
               if(string.IsNullOrWhiteSpace(filepath))
                    throw new ArgumentNullException(nameof(filepath), "File path is required");
               
                using(var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(worksheetName);
                    int ColIndex = 0;

                    for (int col = 0;col < data.Columns.Count; col++)
                    {
                        string Column_Name = data.Columns[col].ColumnName;

                        ColIndex++;
                        worksheet.Cells[1, ColIndex].Value = Column_Name;
                        worksheet.Cells[1, ColIndex].Style.Font.Size = 10;
                        
                        worksheet.Cells[1, ColIndex].Style.Font.Name = "Segoe UI";
                        worksheet.Cells[1, ColIndex].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[1, ColIndex].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[1, ColIndex].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    }

                    for (int row = 0;row < data.Rows.Count; row++)
                    {
                        ColIndex = 0;
                        for (int col = 0;col < data.Columns.Count;col++)
                        {
                            ColIndex++;
                            var cell = worksheet.Cells[row + 2, ColIndex];
                            var Value = data.Rows[row][col];

                            if (Value is DateTime dt)
                            {
                                cell.Value = dt;
                                cell.Style.Numberformat.Format = "yyyy-MM-dd"; // change to your desired format
                            }
                            else
                            {
                                cell.Value = Value;
                            }

                            cell.Style.Font.Size = 9;
                            cell.Style.Font.Name = "Segoe UI";
                        }

                        int percentage = (row + 1) * 100 / data.Rows.Count;
                        progress?.Report(percentage);
                        await Task.Delay(200);
                    }

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    FileInfo fileInfo = new FileInfo(filepath);
                    package.SaveAs(fileInfo);
                    progress?.Report(100);
                }

            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Export Error");
            }
        }
    }
}
