using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;
using System.Net;

namespace Adressed
{
    public class AddressData
    {
        public string FormattedAddress { get; set; }
    }

    public class ResponseData
    {
        public Response Response { get; set; }
    }

    public class Response
    {
        public GeoObjectCollection GeoObjectCollection { get; set; }
    }

    public class GeoObjectCollection
    {
        public FeatureMember[] FeatureMember { get; set; }
    }

    public class FeatureMember
    {
        public GeoObject GeoObject { get; set; }
    }

    public class GeoObject
    {
        public string name { get; set; }
        public string description { get; set; }
    }

    public class SearchAdress
    {
        private const string ApiKey = "e16bc495-37fa-407e-9db3-c2c8604daa0b";

        public static async Task<List<Address>> Search(List<Coordinates> coordinates)
        {
            var httpClient = new HttpClient();

            var addressList = new List<Address>();

            foreach (var coord in coordinates)
            {
                var requestUrl = $"https://geocode-maps.yandex.ru/1.x/?apikey={ApiKey}&format=json&geocode={coord.Longitude},{coord.Latitude}&result=1";
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

                        string foundAddress = responseData.Response.GeoObjectCollection.FeatureMember[0].GeoObject.description + " " + responseData.Response.GeoObjectCollection.FeatureMember[0].GeoObject.name;

                        Address address = new();
                        address.Name = foundAddress;

                        address.Coordinates.Latitude = coord.Latitude;
                        address.Coordinates.Longitude = coord.Longitude;

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

            return addressList;
        }
    }
}

  