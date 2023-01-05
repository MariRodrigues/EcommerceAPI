using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Produtos.DTO;
using EcommerceAPI.Infra.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EcommerceAPI.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;
        private readonly ProdutoRepository _repository;

        public ProdutoController(ProdutoService service, ProdutoRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra produtos",
                          OperationId = "Post")]
        public IActionResult AdicionarProduto([FromBody] CreateProdutoDto produtoDto)
        {
            var produto = _service.CadastrarProduto(produtoDto);
            if(produto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(RecuperaProdutoPorId), new { Id = produto.Id }, produto);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca produtos por ID",
                          OperationId = "Get")]
        [ProducesResponseType(typeof(ReadProdutoDto), 200)]
        public IActionResult RecuperaProdutoPorId(int id)
        {
            var produtoDto = _service.RecuperarProdutoPorId(id);
            return Ok(produtoDto);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Pesquisa por produtos utilizando filtros")]
        [ProducesResponseType(typeof(ReadProdutoDto), 200)]
        public IActionResult PesquisarProduto([FromQuery] FiltrosProduto filtros)
        {
            var produtoDto = _repository.GetAll();

            return Ok(produtoDto);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Editar produtos por Id",
                          OperationId = "Put")]
        [ProducesResponseType(typeof(ReadProdutoDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult EditarProduto(int id, [FromBody] UpdateProdutoDto produtoDto)
        {
            var produto = _service.EditarProduto(id, produtoDto);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

    }
}