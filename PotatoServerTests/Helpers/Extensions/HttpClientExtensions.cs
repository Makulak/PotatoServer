﻿using PotatoServerTestsCore.Models;
using Newtonsoft.Json;
using PotatoServer.ViewModels;
using PotatoServer.ViewModels.Core.User;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PotatoServerTestsCore.Helpers.Extensions
{
    public static class HttpClientExtensions
    {
        public async static Task<string> GetUserTokenAsync(this HttpClient client, string email, string password)
        {
            var loginVm = new UserSignInVm
            {
                Email = email,
                Password = password
            };

            var response = await client.DoPostAsync<TokenVmResult>("api/auth/sign-in", loginVm);

            if (response.IsSuccessStatusCode)
                return response.Value.Token;
            else
                throw new TestExecutionException($"{response.StatusCode} - {response.ValueString}");
        }

        public async static Task<ApiResponse<T>> DoGetAsync<T>(this HttpClient client, string url, string token = null)
        {
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return new ApiResponse<T>(await client.GetAsync(url));
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
