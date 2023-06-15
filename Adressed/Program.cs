using Adressed;

List<Coordinates> coordinates;
FileInfo fileInfo;

Console.WriteLine("Чтение excel файла");

(coordinates, fileInfo) = ExcelPars.ReadExcelData();

Console.WriteLine("Чтение завершено");

Console.WriteLine("Ожидание поиска всех адрессов");
Task<List<Address>> task = SearchAdress.Search(coordinates);
List<Address> result = await task;
Console.WriteLine("Поиск окончен");

Console.WriteLine("Начало создания excel документа");
CreateExelDocs.Create(result, fileInfo);
Console.WriteLine("Программа завершила работу");

Console.ReadLine();