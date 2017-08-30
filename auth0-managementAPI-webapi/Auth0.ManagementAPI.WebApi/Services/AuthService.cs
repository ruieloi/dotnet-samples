
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementAPI.WebApi.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Auth0.ManagementAPI.WebApi.Services
{
    public class AuthService : IAuthService
    {
        public string ClientID { get; private set; }
        public string ClientSecret { get; private set; }
        public string Domain { get; private set; }
        public string CurrentAccessToken { get; private set; }
        public DateTime AccessTokenExpirationDate { get; set; }

        public AuthService(string domain, string clientId, string clientSecret)
        {
            this.Domain = domain;
            this.ClientID = clientId;
            this.ClientSecret = clientSecret;
        }

        public async Task<string> GetTokenAsync()
        {
            try
            {
                if (this.CurrentAccessToken == null || this.AccessTokenExpirationDate < DateTime.Now.AddMinutes(1))
                {
                    var auth0AuthenticationClient = new AuthenticationApiClient(
                        new Uri(String.Format("https://{0}", Domain)));

                    var tokenRequest = new ClientCredentialsTokenRequest()
                    {
                        ClientId = ClientID,
                        ClientSecret = ClientSecret,
                        Audience = String.Format("https://{0}/api/v2/", Domain)
                    };
                    var tokenResponse =
                        await auth0AuthenticationClient.GetTokenAsync(tokenRequest);

                    this.CurrentAccessToken = tokenResponse.AccessToken;
                    this.AccessTokenExpirationDate = DateTime.Now.AddMinutes(-1).AddSeconds(tokenResponse.ExpiresIn);
                }
            }
            catch (Exception ex)
            {
                //TODO LOG
            }

            return this.CurrentAccessToken;
        }



    }
}
