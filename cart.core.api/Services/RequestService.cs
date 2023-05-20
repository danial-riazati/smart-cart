using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using cart.core.api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace cart.core.api.Services
{

    public class RequestService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public RequestService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }


        public async Task<bool> PostDataAsync(string input)
        {
            var datamodel = new { id=input };
            string myDataModelJson = System.Text.Json.JsonSerializer.Serialize(datamodel);
            string endPointConfig = _configuration["BaseUrl"];
            var content = new StringContent(myDataModelJson, Encoding.UTF8, "application/json");
            var response= await _httpClient.PostAsync("http://10.51.10.137:6060/", content);
            if (response.IsSuccessStatusCode)
                return true;
            else return false;   
        }

        public async Task<bool> DeleteJsonObject(string id)
        {
            var url = "http://10.51.10.137:6060/";
            var data = new { id=id };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url),
                Content = content
            };
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Object deleted successfully.");
                return true;
            }
            else
            {
                Console.WriteLine($"Failed to delete object: {response.StatusCode}");
                return false;
            }
        }
        public async Task<bool> DeleteJsonObject()
        {
            var url = "http://10.51.10.137:6060/";
            var data = new { id = "?" };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url),
                Content = content
            };
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Object deleted successfully.");
                return true;
            }
            else
            {
                Console.WriteLine($"Failed to delete object: {response.StatusCode}");
                return false;
            }
        }

    }
}
