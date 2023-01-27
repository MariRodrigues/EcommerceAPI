using EcommerceAPI.Application.Commands.Categorias;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Categorias.DTO;
using EcommerceAPI.Infra.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace EcommerceAPI.Controller
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;
        private readonly CategoriaQueries _categoriaQueries;

        public CategoriaController(CategoriaService categoriaService, CategoriaQueries categoriaQueries)
        {
            _categoriaService = categoriaService;
            _categoriaQueries = categoriaQueries;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra nova categoria",
                          OperationId = "Post")]
        [ProducesResponseType(201)]
        public IActionResult CadastrarCategoria([FromServices] IMediator mediator, [FromBody] CreateCategoriaCommand request)
        {
            var response = mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Pesquisar categorias com filtros",
                          OperationId = "Get")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RecuperaCategoria([FromQuery] FiltrosCategoria filtros)
        {
            var response = await _categoriaQueries.GetAllFilter(filtros);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca categoria por ID",
                          OperationId = "Get")]
        [ProducesResponseType(200)]
        public IActionResult RecuperaCategoriaPorId(int id)
        {
            ReadCategoriaDto categoriaDto = _categoriaService.RecuperaCategoriaPorId(id);
            return Ok(categoriaDto);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Atualiza categoria",
                          OperationId = "Post")]
        [ProducesResponseType(200)]
        public IActionResult AtualizaCategoria([FromServices] IMediator mediator, [FromBody] UpdateCategoriaCommand request)
        {
            var response = mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("status/")]
        [SwaggerOperation(Summary = "Editar o status da categoria por Id",
                          OperationId = "Put")]
        [ProducesResponseType(200)]
        public IActionResult EditarStatusCategoria([FromServices] IMediator mediator, UpdateStatusCategoriaCommand request)
        {
            var response = mediator.Send(request);

            return Ok(response);
        }
    }
}
