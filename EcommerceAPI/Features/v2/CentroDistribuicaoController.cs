using EcommerceAPI.Application.Commands.Centros;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace EcommerceAPI.Features.v2
{
    [ApiController]
    [ApiVersion("2")]
    [Route("v{version:apiVersion}/[controller]")]
    public class CentroDistribuicaoController : ControllerBase
    {
        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra novo Centro de Distribuição",
                          OperationId = "Post")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateCD([FromServices] IMediator mediator, [FromBody] CreateCentroCommand request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Atualiza Centro de Distribuição",
                          OperationId = "Post")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> EditCD([FromServices] IMediator mediator, [FromBody] UpdateCentroCommand request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("status")]
        [SwaggerOperation(Summary = "Atualiza o status do Centro de Distribuição",
                          OperationId = "Post")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> EditStatusCD([FromServices] IMediator mediator, [FromBody] UpdateStatusCentroCommand request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
