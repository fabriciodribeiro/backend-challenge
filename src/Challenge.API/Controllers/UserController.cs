using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Challenge.Application.Accounts.Commands.Login;
using Challenge.Application.Accounts.Commands.Signup;
using Challenge.Application.Accounts.Query.ListUsers;
using Challenge.Application.Accounts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Challenge.API.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;

        public UserController(IConfiguration configuration,
            ILogger<UserController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Login([FromBody] AccountLoginCommand model)
        {
            bool result = await Mediator.Send(model);

            if (result)
            {

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                int expireHours;
                int.TryParse(_configuration["ExpireHours"], out expireHours);

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(expireHours),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("signup")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Signup([FromBody] AccountSignupCommand model)
        {
            var result = await Mediator.Send(model);

            if(!result.Result.Succeeded)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, result.Result.Errors.First());
            }
            if (result.UserId != Guid.Empty)
            {
                return Ok(new{Userid = result.UserId.ToString()});
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("list")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AccountDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            var accountList = new ListUsersQuery();
            var result = await Mediator.Send(accountList);

            return Ok(result);
        }
    }
}
