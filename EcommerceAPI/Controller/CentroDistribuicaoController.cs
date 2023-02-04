using EcommerceAPI.Application.Commands.Centros;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Centros.DTO;
using EcommerceAPI.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.Controller
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    public class CentroDistribuicaoController : ControllerBase
    {
        private readonly ICentroQueries _centroQueries;

        public CentroDistribuicaoController(ICentroQueries centroQueries)
        {
            _centroQueries = centroQueries;
        }

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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca CD por Id",
                          OperationId = "Get")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetById(int id)
        {
            FiltrosCD filtrosCD = new() { Id = id };
            var response = _centroQueries.GetAllFilter(filtrosCD).FirstOrDefault();
            return Ok(response);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Pesquisa Centros com filtros",
                          OperationId = "Get")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] FiltrosCD filtros)
        {
            var response = _centroQueries.GetAllFilter(filtros);
            return Ok(response);
        }
    }
}
