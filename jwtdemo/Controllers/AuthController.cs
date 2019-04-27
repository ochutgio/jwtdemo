using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace jwtdemo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost("Token")]
        public IActionResult GetToken()
        {

            // security key

            // symmetric security key

            // signing credentials

            return Ok("Hello from API") ;
        }
    }
}