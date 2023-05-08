using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using cart.core.api.Models;
using Newtonsoft.Json;

namespace cart.core.api.Repos
{

    public class RequestRepo
    {
        private readonly HttpClient _httpClient;
        public RequestRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
       

        public async Task<HttpResponseMessage> PostDataAsync(string id)
        {
            var datamodel = new { id = id };
            string myDataModelJson = System.Text.Json.JsonSerializer.Serialize(datamodel);
            var content = new StringContent(myDataModelJson, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("http://10.51.10.137:6060/", content);
        }

        public async Task DeleteJsonObject(int id)
        {
            var url = "http://10.51.10.137:6060/";
            var data = new { id = id };
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
            }
            else
            {
                Console.WriteLine($"Failed to delete object: {response.StatusCode}");
            }
        }

    }
}
