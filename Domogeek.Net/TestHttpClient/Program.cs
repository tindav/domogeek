using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestHttpClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var handler = new HttpClientHandler
            {
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                ServerCertificateCustomValidationCallback = (a, b, c, d) => true,
            };

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Connection.Add("keep-alive");
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                //client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
                var response = await client.GetStringAsync("https://particulier.edf.fr/bin/edf_rc/servlets/ejptemponew?Date_a_remonter=2019-02-25&TypeAlerte=EJP");
                Console.WriteLine(response);
            }
        }
    }
}
