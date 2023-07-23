using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TravelEasy.EV.API.Models.UserModels;

namespace Travel.Easy.Electric.Vehicles.Automation.Facades
{
    public class UsersFacade
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationRoot _configurationRoot;
        public readonly string _baseUrl;

        public UsersFacade(HttpClient httpClient, IConfigurationRoot configurationRoot)
        {
            _httpClient = httpClient;
            _configurationRoot = configurationRoot;
            _baseUrl = _configurationRoot.GetValue<string>("baseUrl");
        }

        public HttpResponseMessage GetUser(int id)
        {
            string url = $"{_baseUrl}/api/Users/{id}";

            var response = _httpClient.GetAsync(url).Result;

            return response;
        }

        public HttpResponseMessage PostRegisterUser(UserRegisterRequestModel userModel)
        {
            string url = $"{_baseUrl}/api/Users/Register";
            var body = AddBody(userModel);

            var response = _httpClient.PostAsync(url, body).Result;

            return response;
        }

        public HttpResponseMessage PostLoginUser(UserLoginRequestModel userModel)
        {
            string url = $"{_baseUrl}/api/Users/Login";
            var body = AddBody(userModel);

            var response = _httpClient.PostAsync(url, body).Result;

            return response;
        }

        private StringContent AddBody(object body)
        {
            if (body == null)
            {
                throw new Exception($"Response is Empty.");
            }

            return new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
        }
    }
}
