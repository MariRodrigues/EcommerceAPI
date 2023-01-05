using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Categorias.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace EcommerceAPI.Controller
{
    [ApiController]
    [ApiVersion("1", Deprecated = true)]
    [Route("v{version:apiVersion}/[controller]")]
    public class CategoriaController : ControllerBase
    {

        private readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra nova categoria",
                          OperationId = "Post")]
        [ProducesResponseType(201)]
        public IActionResult AdicionarCategoria([FromBody] CreateCategoriaDto categoriaDto)
        {
            var categoria = _categoriaService.CadastrarCategoria(categoriaDto);
            return CreatedAtAction(nameof(RecuperaCategoriaPorId), new { Id = categoria.Id }, categoria);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca categoria por ID",
                          OperationId = "Get")]
        [ProducesResponseType(typeof(ReadCategoriaDto), 200)]
        public IActionResult RecuperaCategoriaPorId(int id)
        {
            ReadCategoriaDto categoriaDto = _categoriaService.RecuperaCategoriaPorId(id);         
            return Ok(categoriaDto);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Pesquisa categorias utilizando filtros")]
        [ProducesResponseType(typeof(List<ReadCategoriaDto>), 200)]
        [ProducesResponseType(400)]
        public IActionResult PesquisarCategorias([FromQuery] FiltrosCategoria filtros)
        {
            var result = _categoriaService.PesquisarCategorias(filtros);
            
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }        

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Editar nome da categoria por Id",
                          OperationId = "Put")]
        [ProducesResponseType(typeof(ReadCategoriaDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult EditarNomeCategoria(int id, [FromBody] UpdateCategoriaDto categoriaDto)
        {
            var retorno = _categoriaService.EditarCategoria(id, categoriaDto);            
            if (retorno != null) return Ok(retorno);
            return NotFound();
        }
        
        [HttpPut("status/{id}")]
        [SwaggerOperation(Summary = "Editar o status da categoria por Id",
                          OperationId = "Put")]
        [ProducesResponseType(typeof(ReadCategoriaDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult EditarStatusCategoria(int id)
        {
            var result = _categoriaService.EditarStatusCategoria(id);
            if (result != null) return Ok(result);
            return NotFound();       
        }
    }
}
