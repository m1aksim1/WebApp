using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebApp.Models;

namespace WebApp
{
    public class APIClient
    {
        public readonly List<UserViewModel> Users = new List<UserViewModel>();
        private readonly HttpClient _client = new();


        public APIClient(IConfiguration configuration)
        {
            Users.Add(new UserViewModel { Login = configuration["Login"], Password = configuration["Password"] });
            _client.BaseAddress = new Uri(configuration["IPAddress"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public T? GetRequest<T>(string requestUrl)
        {
            var response = _client.GetAsync(requestUrl);    
            var result = response.Result.Content.ReadAsStringAsync().Result;
            if (response.Result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
				throw new Exception(result);
			}
		}
        public void PostRequest<T>(string requestUrl, T model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _client.PostAsync(requestUrl, data);

            var result = response.Result.Content.ReadAsStringAsync().Result;
            if (!response.Result.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }
        }
        public void PutRequest<T>(string requestUrl, T model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _client.PutAsync(requestUrl, data);

            var result = response.Result.Content.ReadAsStringAsync().Result;
            if (!response.Result.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }
        }
    }
}