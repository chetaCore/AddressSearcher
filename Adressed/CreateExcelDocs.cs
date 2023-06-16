using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace Addressed
{
    public class CreateExcelDocs
    {
        public static void Create(List<Address> addressList, FileInfo fileInfo)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                // Добавление нового листа в документ
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Установка заголовков столбцов
                worksheet.Cells[1, 1].Value = "Latitude";
                worksheet.Cells[1, 2].Value = "Longitude";
                worksheet.Cells[1, 3].Value = "Address";

                // Запись каждой строки в отдельную ячейку столбца Excel
                for (int i = 0; i < addressList.Count; i++)
                {
                    // Запись координат и адреса в соответствующие ячейки
                    worksheet.Cells[i + 2, 1].Value = addressList[i].Coordinates.Latitude;
                    worksheet.Cells[i + 2, 2].Value = addressList[i].Coordinates.Longitude;
                    worksheet.Cells[i + 2, 3].Value = addressList[i].Name;
                }

                // Получение корректного пути для сохранения файла
                string filePath = GetValidFilePath();

                // Сохранение файла
                if (string.IsNullOrEmpty(filePath))
                {
                    package.SaveAs(fileInfo);
                }
                else
                {
                    package.SaveAs(new FileInfo(filePath));
                }
            }

            Console.WriteLine("Документ Excel создан успешно.");
        }

        private static string GetValidFilePath()
        {
            string filePath;

            do
            {
                Console.WriteLine("Введите путь для сохранения файла с новым названием или нажмите \"Enter\", если хотите изменить текущий документ:");
                filePath = Console.ReadLine();

                if (!string.IsNullOrEmpty(filePath))
                {
                    if (!IsValidFilePath(filePath))
                    {
                        Console.WriteLine("Некорректный формат пути. Пожалуйста, введите путь в формате диск + название файла и формат.");
                    }
                    else if (!DriveExists(filePath))
                    {
                        Console.WriteLine("Выбранный диск не существует. Пожалуйста, введите путь к существующему диску.");
                    }
                }
            } while (!string.IsNullOrEmpty(filePath) && (!IsValidFilePath(filePath) || !DriveExists(filePath)));

            return filePath;
        }

        private static bool IsValidFilePath(string filePath)
        {
            // Проверка формата пути с использованием регулярного выражения
            string pattern = @"^[a-zA-Z]:\\(?:[^\\/:*?""<>|\r\n]+\\)*[^\\/:*?""<>|\r\n]*\.[^\\/:*?""<>|\r\n]*$";
            return Regex.IsMatch(filePath, pattern);
        }

        private static bool DriveExists(string filePath)
        {
            // Извлечение диска из пути (например, "C:")
            string drive = Path.GetPathRoot(filePath);

            // Проверка существования диска
            return DriveInfo.GetDrives().Any(d => d.Name.Equals(drive, StringComparison.OrdinalIgnoreCase));
        }
    }
}