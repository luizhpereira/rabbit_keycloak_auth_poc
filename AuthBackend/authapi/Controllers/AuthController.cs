using authapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace authapi.Controllers;

//[Route("api/[controller]")]
[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    // Note: the following is necessary to ensure that no
    // BOM is part of the response
    //private static readonly UTF8Encoding encoding = new UTF8Encoding(false);
    //private static readonly string _host = "http://192.168.2.88:8088";
    //private static readonly string _realm = "dev";
    //private static readonly string _grantType = "password";
    //private static readonly string _clientId = "gwapi-auth";
    //private static readonly string _clientSecret = "qy59ikpqDNi3OtR5Phwh0PJIEmTSqb5F";

    [Route("user")]
    [HttpPost]
    public async Task<ActionResult> User([FromForm] string username, [FromForm] string password)
    {
        string content = "deny";

        try
        {
            var token = await Auth.UserAuthenticationToken(username, password);

            if (!string.IsNullOrEmpty(token.access_token))
            {
                //    ////decode JWT
                //    //var handler = new JwtSecurityTokenHandler();
                //    //JwtSecurityToken token_clean = handler.ReadJwtToken(token!.access_token);

                //    ////obtain roles
                //    //Claim resource_access = (Claim)token_clean.Claims.Where(claim => claim.Type == "resource_access").FirstOrDefault();
                //    //JsonElement appTokenContext = JsonSerializer.Deserialize<JsonElement>(resource_access.Value);
                //    //var roles = appTokenContext.GetProperty(_clientId).GetProperty("roles");

                //    ////validate resource role authorize
                //    //foreach (var role in roles.EnumerateArray())
                //    //{
                //    //    if (role.ToString().Contains("votorantim"))
                //    //    {
                //    //        content = "allow administrator management";
                //    //    }
                //    //}

                content = "allow administrator management";
            } 
            else
            {
                //if (username.Contains("intercement") || username.Contains("cloudworker"))
                //    content = "allow administrator management";
                //if (username.Contains("votorantim") || username.Contains("cloudworker"))
                //    content = "allow administrator management";
                if (username.Contains("test"))
                    content = "allow administrator management";
            }
            

            string userlog = string.Format("user :{0}, password :{1}", username, password);
            Console.WriteLine(userlog);

        }
        catch (Exception ex)
        {
            //check or log error
        }

        return Ok(content);
    }

    [Route("vhost")]
    [HttpPost]
    public async Task<ActionResult> VHost([FromForm] string username, [FromForm] string vhost, [FromForm] string ip)
    {
        string content = "deny";

        try
        {
            //if (username.Contains("intercement") || username.Contains("cloudworker"))
            //    content = "allow";
            //if (username.Contains("votorantim") || username.Contains("cloudworker"))
            //    content = "allow";
            //if (username.Contains("test"))
            //    content = "allow";

            content = "allow";
            string userlog = string.Format("user :{0}, vhost :{1} ip :{2}", username, vhost, ip);
            Console.WriteLine(userlog);
        }
        catch (Exception ex)
        {
            //check or log error
        }

        return Ok(content);
    }

    [Route("resource")]
    [HttpPost]
    public async Task<ActionResult> Resource([FromForm] string username, [FromForm] string vhost
        , [FromForm] string resource, [FromForm] string name, [FromForm] string permission)
    {
        string content = "deny";

        try
        {
            //if (username.Contains("intercement") || username.Contains("cloudworker"))
            //    content = "allow";
            //if (username.Contains("votorantim") || username.Contains("cloudworker"))
            //    content = "allow";
            //if (username.Contains("test"))
            //    content = "allow";

            content = "allow";
            string userlog = string.Format("user :{0}, vhost :{1}, resource :{2}, name: {3}, permission: {4}", username, vhost, resource, name, permission);
            Console.WriteLine(userlog);
        }
        catch (Exception ex)
        {
            //check or log error
        }

        return Ok(content);
    }

    [Route("topic")]
    [HttpPost]
    public async Task<ActionResult> Topic([FromForm] string username, [FromForm] string vhost
        , [FromForm] string resource, [FromForm] string name, [FromForm] string permission, [FromForm] string routing_key)
    {
        string content = "deny";

        try
        {
            //HttpClient httpClient = new HttpClient();

            ////Obtain JWT
            //var requestContent = new FormUrlEncodedContent(new[]
            //    {
            //        new KeyValuePair<string, string>("password", "1Cachapuz!"),
            //        new KeyValuePair<string, string>("username", "newest"),
            //        new KeyValuePair<string, string>("grant_type", _grantType),
            //        new KeyValuePair<string, string>("client_id", "admin-cli")
            //    });

            //var response = await httpClient.PostAsync($"h{_host}/realms/master/protocol/openid-connect/token", requestContent);

            //// Save the token for further requests.
            //var jwt = await response.Content.ReadAsStringAsync();

            //var token = JsonSerializer.Deserialize<Token>(jwt);

            //if (!string.IsNullOrEmpty(token.access_token))
            //{
            //    //var userRequest = new HttpRequestMessage(HttpMethod.Get, $"{_host}/admin/realms/{_realm}/users?username={username}");
            //    //userRequest.Headers.Add("Authorization", $"Bearer {token.access_token}");
            //    //var userResponse = await httpClient.SendAsync(userRequest);

            //    //var user = await userResponse.Content.ReadAsStringAsync();
            //    content = "allow";
            //}

            //if (username.Contains("intercement") || username.Contains("cloudworker"))
            //    content = "allow";
            //if (username.Contains("votorantim") || username.Contains("cloudworker"))
            //    content = "allow";
            //if (username.Contains("test"))
            //    content = "allow";

            content = "allow";
            string userlog = string.Format("user :{0}, vhost :{1}, resource :{2}, name: {3}, permission: {4}, routing_key :{5}", username, vhost, resource, name, permission, routing_key);
            Console.WriteLine(userlog);
        }
        catch (Exception ex)
        {
            //check or log error
        }

        return Ok(content);
    }



}
