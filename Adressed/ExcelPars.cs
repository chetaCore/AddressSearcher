using OfficeOpenXml;

namespace Adressed
{
    public class ExcelPars
    {
        public static (List<Coordinates>, FileInfo) ReadExcelData()
        {

            string filePath = InputPath();

            List<Coordinates> coords = new();
            FileInfo file = new FileInfo(filePath);

            Console.WriteLine(file);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Первый лист в документе

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    string latitude = worksheet.Cells[row, 1].Value?.ToString(); // Широта в первом столбце
                    string longitude = worksheet.Cells[row, 2].Value?.ToString(); // Долгота во втором столбце

                    Coordinates coord = new();

                    if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(longitude))
                    {
                        coord.Latitude = latitude;
                        coord.Longitude = longitude;
                        coords.Add(coord);
                    }
                }
            }

            return (coords, file);
        }

        private static string InputPath()
        {
            string filePath = "";

            do
            {
                Console.WriteLine("Введите путь к файлу; пример: D:\\input.xlsx");
                filePath = Console.ReadLine();

                if (string.IsNullOrEmpty(filePath))
                {
                    Console.Clear();
                    Console.WriteLine("Вы не ввели путь к файлу. Повторите попытку.");
                }
                else if (!File.Exists(filePath))
                {
                    Console.Clear();
                    Console.WriteLine("Файл не найден. Повторите попытку.");
                }
            } while (string.IsNullOrEmpty(filePath) || !File.Exists(filePath));

            return filePath;
        }
    }
}