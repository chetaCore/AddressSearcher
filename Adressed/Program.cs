using Addressed;

Console.WriteLine("Чтение excel файла");
var (coordinates, fileInfo) = ExcelParser.ReadExcelData();
Console.WriteLine("Чтение завершено");

Console.WriteLine("Ожидание поиска всех адресов");
List<Address> result = await SearchAddress.Search(coordinates);
Console.WriteLine("Поиск окончен");

Console.WriteLine("Начало создания excel документа");
CreateExcelDocs.Create(result, fileInfo);
Console.WriteLine("Программа завершила работу");

Console.ReadLine();