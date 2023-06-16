using Newtonsoft.Json;
using System.Diagnostics;
using static Addressed.GeocodingResponse;

namespace Addressed
{
    public class SearchAddress
    {
        private const string ApiKey = "e16bc495-37fa-407e-9db3-c2c8604daa0b";

        public static async Task<List<Address>> Search(List<Coordinates> coordinates)
        {
            var addressList = new List<Address>();

            using (var httpClient = new HttpClient())
            {
                foreach (var coordinate in coordinates)
                {
                    var requestUrl = $"https://geocode-maps.yandex.ru/1.x/?apikey={ApiKey}&format=json&geocode={coordinate.Longitude},{coordinate.Latitude}&result=1";

                    try
                    {
                        // Отправка запроса на Yandex Geocoding API
                        var response = await httpClient.GetAsync(requestUrl);

                        // Проверка статуса ответа
                        if (response.IsSuccessStatusCode)
                        {
                            // Чтение содержимого ответа
                            var content = await response.Content.ReadAsStringAsync();

                            // Десериализация JSON-данных в объект
                            var responseData = JsonConvert.DeserializeObject<ResponseData>(content);

                            // Получение найденного адреса из ответа
                            string foundAddress = responseData.Response.GeoObjectCollection.FeatureMember[0].GeoObject.Description + " " + responseData.Response.GeoObjectCollection.FeatureMember[0].GeoObject.Name;

                            // Создание объекта адреса
                            var address = new Address
                            {
                                Name = foundAddress,
                                Coordinates = new Coordinates
                                {
                                    Latitude = coordinate.Latitude,
                                    Longitude = coordinate.Longitude
                                }
                            };

                            // Добавление адреса в список
                            addressList.Add(address);
                        }
                        else
                        {
                            Debug.Print($"Error: {response.StatusCode}");
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Print($"Error: {ex.Message}");
                        return null;
                    }
                }
            }

            return addressList;
        }
    }
}