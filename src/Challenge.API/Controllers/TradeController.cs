using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Challenge.Application.Portfolis.Query;
using Challenge.Application.Trades.Command.Creation;
using Challenge.Application.Trades.Command.Delete;
using Challenge.Application.Trades.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Challenge.API.Controllers
{
    public class TradeController : ApiControllerBase
    {
        public TradeController(IConfiguration configuration)
        {
        }

        //[Authorize]
        [HttpPost("{portfolioId:Guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromRoute, Required] Guid portfolioId,
            [FromBody] TradeCreationCommand model)
        {
            model.PortfolioId = portfolioId;
            var result = await Mediator.Send(model);

            if (!result.Result.Succeeded)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, result.Result.Errors.First());
            }
            if (result.TradeId != Guid.Empty)
            {
                return Ok(new { TradeId = result.TradeId.ToString() });
            }

            return BadRequest();
        }

        //[Authorize]
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TradeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            var tradeList = new ListTradeQuery();
            var result = await Mediator.Send(tradeList);

            return Ok(result);
        }

        //[Authorize]
        [HttpDelete("{Id:Guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute, Required] Guid Id)
        {
            var deleteCommand = new TradeDeletionCommand(Id);
            await Mediator.Send(deleteCommand).ConfigureAwait(false);

            return NoContent();
        }
    }
}
