using OfficeOpenXml;

namespace Addressed
{
    public class ExcelParser
    {
        public static (List<Coordinates>, FileInfo) ReadExcelData()
        {
            // Получение пути к файлу
            string filePath = GetFilePath();

            // Создание объекта FileInfo для указанного пути
            FileInfo file = new FileInfo(filePath);

            Console.WriteLine(file);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            List<Coordinates> coordinatesList = new List<Coordinates>();

            // Использование блока using для автоматического закрытия и освобождения ресурсов ExcelPackage
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Первый лист в документе

                int rowCount = worksheet.Dimension.Rows;

                // Чтение данных из каждой строки и добавление координат в список
                for (int row = 2; row <= rowCount; row++)
                {
                    string latitude = worksheet.Cells[row, 1].Value?.ToString(); // Широта в первом столбце
                    string longitude = worksheet.Cells[row, 2].Value?.ToString(); // Долгота во втором столбце

                    if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(longitude))
                    {
                        coordinatesList.Add(new Coordinates
                        {
                            Latitude = latitude,
                            Longitude = longitude
                        });
                    }
                }
            }

            return (coordinatesList, file);
        }

        private static string GetFilePath()
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