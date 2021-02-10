using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;

namespace PotatoServerTestsCore.Models
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; private set; }
        public bool IsSuccessStatusCode { get; private set; }
        public bool IsValueDeserializedProperly { get; private set; }
        public T Value { get; private set; }
        public string ValueString { get; private set; }

        public ApiResponse(HttpResponseMessage response)
        {
            IsSuccessStatusCode = response.IsSuccessStatusCode;

            var bodyString = response.Content.ReadAsStringAsync();
            bodyString.Wait();
            ValueString = bodyString.Result;

            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                Value = JsonConvert.DeserializeObject<T>(ValueString, settings);
                IsValueDeserializedProperly = true;
            }
            catch(Exception)
            {
                IsValueDeserializedProperly = false;
            }
        }
    }
}
