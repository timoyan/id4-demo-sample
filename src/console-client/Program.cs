using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace console_client {
    public class Program {
        private static async Task Main () {
            // discover endpoints from metadata
            var client = new HttpClient ();

            var disco = await client.GetDiscoveryDocumentAsync ("http://localhost:5000");
            if (disco.IsError) {
                Console.WriteLine (disco.Error);
                return;
            }
            Console.WriteLine (disco.Json);
            
            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });
            
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.AccessToken);
        }
    }
}