using PotatoServerTestsCore.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PotatoServerTestsCore.Helpers.Extensions
{
    public static class HttpClientExtensions
    {
        public async static Task<ApiResponse<T>> DoGetAsync<T>(this HttpClient client, string url, string token = null)
        {
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var x = await client.GetAsync(url);

            return new ApiResponse<T>(x);
        }

        public async static Task<ApiResponse<T>> DoPostAsync<T>(this HttpClient client, string url, object obj, string token = null)
        {
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(obj);
            HttpResponseMessage response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

            return new ApiResponse<T>(response);
        }

        public async static Task<ApiResponse<T>> DoDeleteAsync<T>(this HttpClient client, string url, string token = null)
        {
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return new ApiResponse<T>(await client.DeleteAsync(url));
        }
    }
}
