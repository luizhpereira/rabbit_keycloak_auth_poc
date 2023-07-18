using authapi.Models;
using System.Text.Json;

namespace authapi
{
    public class Auth
    {
        //private static readonly string _host = "http://192.168.2.88:8088";
        private static readonly string _host = "https://cachapuz.westeurope.cloudapp.azure.com/Keycloak";
        private static readonly string _realm = "dev";
        private static readonly string _grantType = "password";
        //private static readonly string _clientId = "gwapi-auth";
        private static readonly string _clientId = "Mobile";
        //private static readonly string _clientSecret = "qy59ikpqDNi3OtR5Phwh0PJIEmTSqb5F";
        private static readonly string _clientSecret = "2RM5xuf6xa2NSEhwwmD1Q0UdRPIBJ2TA";

        public Auth() 
        {
        
        }

        public static async Task<Token> UserAuthenticationToken(string username, string password)
        {
            HttpClient httpClient = new HttpClient();

            //Obtain JWT
            var requestContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("grant_type", _grantType),
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                });

            var response = await httpClient.PostAsync($"{_host}/realms/{_realm}/protocol/openid-connect/token", requestContent);

            // Save the token for further requests.
            var jwt = await response.Content.ReadAsStringAsync();

            var token = JsonSerializer.Deserialize<Token>(jwt);

            return await Task.FromResult(token);
        }

        public static async Task<Token> MasterAuthenticationToken() 
        {
            HttpClient httpClient = new HttpClient();

            //Obtain JWT
            var requestContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("password", "1Cachapuz!"),
                    new KeyValuePair<string, string>("username", "newest"),
                    new KeyValuePair<string, string>("grant_type", _grantType),
                    new KeyValuePair<string, string>("client_id", "admin-cli")
                });

            var response = await httpClient.PostAsync($"h{_host}/realms/master/protocol/openid-connect/token", requestContent);
            
            // Save the token for further requests.
            var jwt = await response.Content.ReadAsStringAsync();

            var token = JsonSerializer.Deserialize<Token>(jwt);

            return await Task.FromResult<Token>(token);
        }
    }
}
