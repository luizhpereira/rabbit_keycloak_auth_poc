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
                content = "allow administrator management";
            } 
            else
            {
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
