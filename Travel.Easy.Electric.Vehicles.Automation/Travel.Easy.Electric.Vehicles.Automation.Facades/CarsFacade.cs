using Microsoft.Extensions.Configuration;

namespace Travel.Easy.Electric.Vehicles.Automation.Facades
{
    public class CarsFacade
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationRoot _configurationRoot;
        public readonly string _baseUrl;

        public CarsFacade(HttpClient httpClient, IConfigurationRoot configurationRoot)
        {
            _httpClient = httpClient;
            _configurationRoot = configurationRoot;
            _baseUrl = _configurationRoot.GetValue<string>("baseUrl");
        }

        public HttpResponseMessage GetCars(int userId)
        {
            string url = $"{_baseUrl}/api/Cars";
            var queryParameters = new Dictionary<string, string>
            {
                { "userId", userId.ToString() }
            };
            var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
            var queryString = dictFormUrlEncoded.ReadAsStringAsync().Result;

            var response = _httpClient.GetAsync($"{url}?{queryString}").Result;

            return response;
        }

        public HttpResponseMessage GetCar(int carId, int userId)
        {
            string url = $"{_baseUrl}/api/Cars/{carId}";
            var queryParameters = new Dictionary<string, string>
            {
                { "userId", userId.ToString() }
            };
            var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
            var queryString = dictFormUrlEncoded.ReadAsStringAsync().Result;

            var response = _httpClient.GetAsync($"{url}?{queryString}").Result;

            return response;
        }
    }
}
