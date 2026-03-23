using OfficeOpenXml;
using System;
using System.IO;
using OpenQA.Selenium; // Bắt buộc thêm thư viện này để chụp ảnh

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

                // Mở rộng điều kiện để nhận diện cả "PASS", "PASSED" và "FAIL", "FAILED"
                if (status.ToUpper() == "PASS" || status.ToUpper() == "PASSED")
                {
                    worksheet.Cells[row, statusCol].Style.Font.Color.SetColor(System.Drawing.Color.Green);
                }
                else if (status.ToUpper() == "FAIL" || status.ToUpper() == "FAILED")
                {
                    worksheet.Cells[row, statusCol].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                }

                package.Save();
            }
        }

        // Bổ sung hàm chụp ảnh màn hình
        public static void TakeScreenshot(IWebDriver driver, string testCaseName)
        {
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string imagesDirectory = Path.Combine(baseDirectory, "Images");

                // Tạo thư mục Images nếu chưa tồn tại
                if (!Directory.Exists(imagesDirectory))
                {
                    Directory.CreateDirectory(imagesDirectory);
                }

                // Định dạng tên file: TênTestCase_NămThángNgày_GiờPhútGiây.png
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"{testCaseName}_{timestamp}.png";
                string filePath = Path.Combine(imagesDirectory, fileName);

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi hệ thống khi chụp ảnh: {ex.Message}");
            }
        }
    }
}