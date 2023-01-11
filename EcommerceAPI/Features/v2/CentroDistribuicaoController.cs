using EcommerceAPI.Application.Commands.Centros;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Centros.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.Features.v2
{
    [ApiController]
    [ApiVersion("2")]
    [Route("v{version:apiVersion}/[controller]")]
    public class CentroDistribuicaoController : ControllerBase
    {
        private readonly CentroDistribuicaoService _centroService;

        public CentroDistribuicaoController(CentroDistribuicaoService centroService)
        {
            _centroService = centroService;
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
            var response = _centroService.PesquisarCentros(filtrosCD).FirstOrDefault();
            return Ok(response);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Pesquisa Centros com filtros",
                          OperationId = "Get")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll([FromQuery] FiltrosCD filtros)
        {
            var response = _centroService.PesquisarCentros(filtros);
            return Ok(response);
        }
    }
}
