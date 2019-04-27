using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JsonWebTokenDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        public AuthController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("token")]
        public IActionResult GetToken()
        {

            // security key
            // string securityKey = "this_is_very_long_security_key_for_token_validation_project";
            // symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JsonWebToken:SigningKey"]));
            // var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JsonWebToken:SigningKey"]));
            // signing credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);


            // claim
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            claims.Add(new Claim("id", "110"));
            claims.Add(new Claim("our_custom_claim", "our_custom_values"));
            // create token
            //var token = new JwtSecurityToken(
            //    issuer: "localhost",
            //    audience: "readers",
            //    expires: DateTime.Now.AddHours(1),
            //    claims: claims,
            //    signingCredentials: signingCredentials
            //    );
            var token = new JwtSecurityToken(
               issuer: Configuration["JsonWebToken:Issuer"],
               audience: Configuration["JsonWebToken:Audience"],
               expires: DateTime.Now.AddHours(1),
               claims: claims,
               signingCredentials: signingCredentials
               );
            // return token

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}