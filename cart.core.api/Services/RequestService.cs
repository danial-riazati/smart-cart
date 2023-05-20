using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using cart.core.api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace cart.core.api.Services
{

    public class RequestService:IRequestService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        public RequestService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }


        public async Task<bool> PostDataAsync(string input)
        {
            var datamodel = new { id = input };
            string url = _configuration["BaseUrl"];
            var client = _clientFactory.CreateClient();
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            var random = new Random();
            string myDataModelJson = System.Text.Json.JsonSerializer.Serialize(datamodel);
            req.Content = new StringContent(myDataModelJson, Encoding.UTF8, "application/json");

            var res = client.SendAsync(req).Result;



            //var content = new StringContent(myDataModelJson, Encoding.UTF8, "application/json");
            //var response= await _httpClient.PostAsync("http://localhost:6060", content);
            if (res.StatusCode != System.Net.HttpStatusCode.OK)
                return true;
            else
                return false;
            
        }

        //public async Task<bool> DeleteJsonObject(string id)
        //{
        //    var data = new { id=id };
        //    var json = JsonConvert.SerializeObject(data);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Delete,
        //        RequestUri = new Uri("http://localhost:6060"),
        //        Content = content
        //    };
        //    var response = await _httpClient.SendAsync(request);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        Console.WriteLine("Object deleted successfully.");
        //        return true;
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Failed to delete object: {response.StatusCode}");
        //        return false;
        //    }
        //}
        //public async Task<bool> DeleteJsonObject()
        //{
          
        //    //var data = new { id = "?" };
        //    //var json = JsonConvert.SerializeObject(data);
        //    //var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    //var request = new HttpRequestMessage
        //    //{
        //    //    Method = HttpMethod.Delete,
        //    //    RequestUri = new Uri("http://localhost:6060"),
        //    //    Content = content
        //    //};
        //    //var response = await _httpClient.SendAsync(request);

        //    //if (response.IsSuccessStatusCode)
        //    //{
        //    //    Console.WriteLine("Object deleted successfully.");
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    Console.WriteLine($"Failed to delete object: {response.StatusCode}");
        //    //    return false;
        //    //}
        //}

        public Task<bool> DeleteJsonObject(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteJsonObject()
        {
            throw new NotImplementedException();
        }
    }
}
