# AddressSearcher

**Инструкция по использованию программы:**

1. Убедитесь, что у вас установлен .NET Framework или .NET Core на вашем компьютере.

2. Откройте редактор кода или интегрированную среду разработки (IDE) вашего выбора.

3. Создайте новый проект и добавьте файлы с кодом, которые мы рассмотрели вместе.

4. Подключите необходимые зависимости. Убедитесь, что у вас установлены пакеты Newtonsoft.Json и OfficeOpenXml. Если они не установлены, вы можете добавить их с помощью менеджера пакетов NuGet.

5. Разберем каждый класс и его функциональность:
   - **Coordinates:** Этот класс представляет координаты местоположения и содержит свойства для широты и долготы.
   - **Address:** Этот класс представляет адрес и содержит свойства для имени адреса и координат.
   - **CreateExelDocs:** Этот класс отвечает за создание документа Excel на основе списка адресов.
   - **ExcelParser:** Этот класс отвечает за чтение данных из файла Excel и возвращает список координат и информацию о файле.
   - **SearchAddress:** Этот класс выполняет поиск адресов на основе списка координат с использованием сервиса геокодирования Yandex Geocoding API.

6. В программе мы использовали некоторые внешние зависимости, поэтому убедитесь, что у вас есть подключение к интернету, чтобы программа могла обращаться к сервису геокодирования Yandex Geocoding API.

7. Вызовите метод `ExcelParser.ReadExcelData` для чтения данных из файла Excel. Убедитесь, что файл существует и содержит данные в формате широты и долготы.

8. После чтения данных из файла Excel, вызовите метод `SearchAddress.Search` и передайте список координат для выполнения поиска адресов. Метод вернет список адресов, соответствующих переданным координатам.

9. Вызовите метод `CreateExelDocs.Create` и передайте список адресов и объект `FileInfo` для создания документа Excel с указанными адресами.

10. Запустите программу и проверьте результаты. Убедитесь, что адреса были успешно найдены и создан документ Excel с соответствующими данными.

11. Если возникнут ошибки, убедитесь, что все зависимости правильно установлены, файл Excel содержит корректные данные и у вас есть подключение к интернету для использования сервиса геокодирования.
