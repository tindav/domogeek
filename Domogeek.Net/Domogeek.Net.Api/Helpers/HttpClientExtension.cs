using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Domogeek.Net.Api.Helpers
{
    public static class HttpClientExtension
    {
        public static async Task<string> GetStringWithAcceptAndKeepAliveAsync(this HttpClient client, string uri)
        {
            var request = new HttpRequestMessage { RequestUri = new Uri(uri), Method = HttpMethod.Get };
            request.Headers.Accept.ParseAdd("application/json");
            request.Headers.Connection.Add("keep-alive");
            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
