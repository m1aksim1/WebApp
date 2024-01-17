using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.HttpOverrides;
using WebApp;
using WebApp.Models;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Enter()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Enter(string login, string password)
        {
            try
            {
                var account = new UserViewModel { Login = login, Password = password, Id = 0 };
                var claims = new List<Claim>
                {
                    new(ClaimsIdentity.DefaultNameClaimType, account.Id.ToString()),
                    new(ClaimsIdentity.DefaultRoleClaimType, "Admin" ),
                };
                ClaimsIdentity claimsIdentity = new(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claimsIdentity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var jwtToken = "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwt);
                Response.Cookies.Append("token", jwtToken);
                return Ok(jwtToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}