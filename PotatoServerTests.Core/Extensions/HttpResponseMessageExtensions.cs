using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace PotatoServerTestsCore.Helpers.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> GetResponseBodyAsync<T>(this HttpResponseMessage response, T objectType)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeAnonymousType(responseString, objectType);
            }
            catch (JsonReaderException ex)
            {
                throw new TestExecutionException($"{responseString}' cannot be deserialized to type {typeof(T)}.", ex);
            }
        }
    }
}
