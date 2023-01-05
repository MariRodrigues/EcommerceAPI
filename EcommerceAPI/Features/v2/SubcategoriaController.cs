using EcommerceAPI.Application.Commands.Subcategorias;
using EcommerceAPI.Domain.Subcategorias.DTO;
using EcommerceAPI.Infra.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace EcommerceAPI.Features.v2
{
    [ApiController]
    [ApiVersion("2")]
    //[Authorize(Roles = "admin")]
    [Route("v{version:apiVersion}/[controller]")]
    public class SubcategoriaController : ControllerBase
    {

        private readonly SubcategoriaQueries _subcategoriaQueries;

        public SubcategoriaController(SubcategoriaQueries subcategoriaQueries)
        {
            _subcategoriaQueries = subcategoriaQueries;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra nova subcategoria",
                          OperationId = "Post")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult AdicionarSubcategoria([FromServices] IMediator mediator, [FromBody] CreateSubcategoriaCommand request)
        {
            var result = mediator.Send(request);
            if (result.IsCompleted)
            {
                return CreatedAtAction(nameof(AdicionarSubcategoria), new { result.Id }, result);
            }
            return BadRequest();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Pesquisar categorias com filtros",
                          OperationId = "Get")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RecuperaCategoria([FromQuery] SubcategoriaFiltros filtros)
        {
            return Ok(await _subcategoriaQueries.RecuperarSubcategoriaFilter(filtros));
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Atualiza subcategoria",
                          OperationId = "Post")]
        [ProducesResponseType(200)]
        public IActionResult AtualizaSubcategoria([FromServices] IMediator mediator, [FromBody] UpdateSubcategoriaCommand request)
        {
            var response = mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("status/")]
        [SwaggerOperation(Summary = "Editar o status da subcategoria por Id",
                          OperationId = "Put")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult EditarStatusCategoria([FromServices] IMediator mediator, UpdateStatusSubcategoriaCommand request)
        {
            var response = mediator.Send(request);

            return Ok(response);
        }
    }
}
