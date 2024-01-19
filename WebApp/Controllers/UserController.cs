using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private APIClient _client;
        public UserController(APIClient client) 
        {
            _client = client;
        }
        [HttpPost]
        [Authorize]
        public void Logout()
        {
            Response.Cookies.Delete("token");
            Response.Redirect("Enter");
        }
        public IActionResult Enter()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("~/Views/User/Logout.cshtml");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public void Enter(string login, string password)
        {
            var account = _client.Users.FirstOrDefault(x => x.Login.Equals(login) && x.Password.Equals(password));
            if(account == null)
            {
                Response.Redirect("Enter");
            }
            var claims = new List<Claim>
            {
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
            Response.Redirect("Enter");
        }
    }
}