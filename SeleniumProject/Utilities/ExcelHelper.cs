using OfficeOpenXml;
using System;
using System.IO;

namespace SeleniumProject.Utilities
{
    public class ExcelHelper
    {
        public static string GetTestDataPath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(baseDirectory, "TestData", "TestData.xlsx");
        }

        public static string ReadData(int row, int col)
        {
            Environment.SetEnvironmentVariable("EPPlusLicenseContext", "NonCommercial");
            FileInfo fileInfo = new FileInfo(GetTestDataPath());

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                return worksheet.Cells[row, col].Value?.ToString() ?? "";
            }
        }

        public static void WriteResult(int row, int statusCol, string status, int actualResultCol, string actualResult)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo fileInfo = new FileInfo(GetTestDataPath());

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                worksheet.Cells[row, statusCol].Value = status;
                worksheet.Cells[row, actualResultCol].Value = actualResult;

                if (status.ToUpper() == "PASS")
                {
                    worksheet.Cells[row, statusCol].Style.Font.Color.SetColor(System.Drawing.Color.Green);
                }
                else if (status.ToUpper() == "FAIL")
                {
                    worksheet.Cells[row, statusCol].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                }

                package.Save();
            }
        }
    }
}