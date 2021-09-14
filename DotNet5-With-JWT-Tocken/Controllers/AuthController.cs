using DotNet5_With_JWT_Tocken.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNet5_With_JWT_Tocken.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jWTAuthenticationManager;

        public AuthController(IJwtAuthenticationManager jWTAuthenticationManager)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
        }


        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<AuthController>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = jWTAuthenticationManager.Authenticate(userCred.UserName, userCred.Password);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

    }
}
