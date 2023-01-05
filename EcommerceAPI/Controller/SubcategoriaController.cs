using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using EcommerceAPI.Domain.Subcategorias.DTO;
using EcommerceAPI.Application.Services;

namespace EcommerceAPI.Controller
{
    [ApiController]
    [ApiVersion("1", Deprecated = true)]
    [Route("v{version:apiVersion}/[controller]")]
    public class SubcategoriaController : ControllerBase
    {
        private readonly SubcategoriaService _subcategoriaService;
        public SubcategoriaController(SubcategoriaService subcategoriaService)
        {
            _subcategoriaService = subcategoriaService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra nova subcategoria",
                          OperationId = "Post")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult AdicionarSubcategoria([FromBody] CreateSubcategoriaDto subcategoriaDTO)
        {
            var result = _subcategoriaService.CadastrarSubcategoria(subcategoriaDTO);
            if (result != null)
            {
                return Ok();
            }           
            return BadRequest();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Editar o nome da subcategoria por Id",
                          OperationId = "Put")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult EditarNomeSubcategoria(int id, [FromBody] UpdateSubcategoriaDto subcategoriaDto)
        {
            var result = _subcategoriaService.EditarNomeSubcategoria(id, subcategoriaDto);
            if (result.IsFailed) return NotFound();
            return Ok();
        }

        [HttpPut("status/{id}")]
        [SwaggerOperation(Summary = "Editar o status da subcategoria por Id",
                          OperationId = "Put")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult EditarStatusSubcategoria(int id)
        {
            var result = _subcategoriaService.EditarStatusSubcategoria(id);
            if (result.IsFailed) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletar subcategoria por Id",
                          OperationId = "Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeletarSubcategoria(int id)
        {
            var result = _subcategoriaService.DeletarSubcategoria(id);
            if (result.IsFailed) return NotFound();
            return Ok();
        }
    }
}
