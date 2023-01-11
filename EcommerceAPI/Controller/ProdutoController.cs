using EcommerceAPI.Application.Commands.Produtos;
using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Produtos.DTO;
using EcommerceAPI.Infra.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace EcommerceAPI.Controller
{
    [ApiController]
    [ApiVersion("1")]
    //[Authorize(Roles = "admin")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoQueries _produtosQueries;
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoQueries produtosQueries, ProdutoService produtoService)
        {
            _produtosQueries = produtosQueries;
            _produtoService = produtoService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastrar novo produto")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post([FromServices] IMediator mediator, [FromBody] CreateProdutoCommand request)
        {
            var response = mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Atualizar produto")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Put([FromServices] IMediator mediator, [FromBody] UpdateProdutoCommand request)
        {
            var response = mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Listar todos os produtos",
                          OperationId = "GetAllProducts")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = _produtosQueries.GetAllProducts();
            return Ok(result);
        }

        [HttpGet("search")]
        [SwaggerOperation(Summary = "Buscar produtos com filtros",
                          OperationId = "GetAllFilter")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllProducts([FromQuery] FiltrosProduto filtros)
        {
            //var result = _produtosQueries.GetAllFilter(filtros);
            var result = _produtoService.PesquisarProdutos(filtros);
            return Ok(result);
        }
    }
}
