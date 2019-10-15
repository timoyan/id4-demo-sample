using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace console_resource_owner
{
    class Program
    {
        static HttpClient _tokenClient = new HttpClient();
        static DiscoveryCache _cache = new DiscoveryCache("http://127.0.0.1:5000");

        private static async Task Main()
        {
            var response = await RequestTokenWithPasswordAsync();
            var renewResponse = await RefreshTokenAsync(response.RefreshToken); 
            Console.WriteLine(renewResponse.Json);

            // await GetClaimsAsync(response.AccessToken);
            // 

            // var response2  = await RequestTokenWithClientCredentialAsync();
            // Console.WriteLine(response2.Json);
        }

        static async Task<TokenResponse> RequestTokenWithPasswordAsync()
        {
            var disco = await _cache.GetAsync();
            if (disco.IsError) throw new Exception(disco.Error);

            var response = await _tokenClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",
                UserName = "alice",
                Password = "password",
                Scope = "api1 offline_access openid profile",
            });

            if (response.IsError) throw new Exception(response.Error);
            return response;
        }

        static async Task<TokenResponse> RequestTokenWithClientCredentialAsync()
        {
            var disco = await _cache.GetAsync();
            if (disco.IsError) throw new Exception(disco.Error);

            var response = await _tokenClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",
                Scope = "api1",
            });

            if (response.IsError) throw new Exception(response.Error);
            return response;
        }

        static async Task GetClaimsAsync(string token)
        {
            var disco = await _cache.GetAsync();
            if (disco.IsError) throw new Exception(disco.Error);

            var response = await _tokenClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = disco.UserInfoEndpoint,
                Token = token
            });

            if (response.IsError) throw new Exception(response.Error);

            Console.WriteLine(response.Json);
        }

         static async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            Console.WriteLine("Using refresh token: {0}", refreshToken);

            var disco = await _cache.GetAsync();
            var response = await _tokenClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "ro.client",
                ClientSecret = "secret",
                RefreshToken = refreshToken
            });

            if (response.IsError) throw new Exception(response.Error);
            return response;
        }
    }
}
