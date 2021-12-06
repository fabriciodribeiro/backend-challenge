using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Challenge.Application.Accounts.ViewModels;
using Challenge.Application.Portfolis.Command.Creation;
using Challenge.Application.Portfolis.Command.Delete;
using Challenge.Application.Portfolis.Query;
using Challenge.Application.Portfolis.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Challenge.API.Controllers
{
    public class PorfolioController : ApiControllerBase
    {
        
        public PorfolioController(IConfiguration configuration)
        {
        }

        [Authorize]
        [HttpPost("{accountId:Guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromRoute, Required] Guid accountId,
            [FromBody] PortfolioCreationCommand model)
        {
            model.Account = accountId;
            var result = await Mediator.Send(model);

            if(!result.Result.Succeeded)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, result.Result.Errors.First());
            }
            if (result.PortfolioId != Guid.Empty)
            {
                return Ok(new{PortfolioId = result.PortfolioId.ToString()});
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PortfolioDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            var portfolioList = new ListPortfoliosQuery();
            var result = await Mediator.Send(portfolioList);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{Id:Guid}")]        
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute, Required] Guid Id)
        {
            var deleteCommand = new PortfolioDeletionCommand(Id);
            await Mediator.Send(deleteCommand).ConfigureAwait(false);

            return NoContent();
        }

        [Authorize]
        [HttpGet("{Id:Guid}/balance")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AccountDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Balance([FromRoute, Required] Guid Id)
        {
            var portfolioList = new ListPortfolioBalanceQuery(Id);
            var result = await Mediator.Send(portfolioList);

            if (!result.Result.Succeeded)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, result.Result.Errors.First());
            }
            if (result.Balance != null)
            {
                return Ok(new { Balance = result.Balance });
            }

            return BadRequest();            
        }
    }
}
