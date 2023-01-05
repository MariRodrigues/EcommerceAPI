using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Centros.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace EcommerceAPI.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CentroDistribuicaoController : ControllerBase
    {
        private readonly CentroDistribuicaoService _centroService;
        public CentroDistribuicaoController(CentroDistribuicaoService centroService)
        {
            _centroService = centroService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra novo centro de distribuição",
                          OperationId = "Post")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CadastrarCentro([FromBody] CreateCentroDto createCentroDto)
        {
            var centro = await _centroService.CadastrarCentro(createCentroDto);
            return CreatedAtAction(nameof(BuscarCentroPorId), new { Id = centro.Id }, centro);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca CD por ID",
                          OperationId = "Get")]
        [ProducesResponseType(typeof(ReadCentroDistribuicao), 200)]
        public IActionResult BuscarCentroPorId(int id)
        {
            var centro = _centroService.RecuperarCentroPorId(id);
            return Ok(centro);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Busca CD utilizando filtros",
                          OperationId = "Get")]
        [ProducesResponseType(typeof(ReadCentroDistribuicao), 200)]
        public IActionResult PesquisarCentros([FromQuery] FiltrosCD filtros)
        {
            var result = _centroService.PesquisarCentros(filtros);
            return Ok(result);
        }

        [HttpPut("status/{id}")]
        [SwaggerOperation(Summary = "Editar o status do CD por Id",
                          OperationId = "Put")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult EditarStatus(int id)
        {
            var result = _centroService.EditarStatusCentro(id);
            if (result.IsFailed) return BadRequest(result);
            return NoContent();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Editar o CD por Id",
                          OperationId = "Put")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EditarCentro(int id, [FromBody] UpdateCentroDto centroDto)
        {
            var result = await _centroService.EditarCentro(id, centroDto);
            if (result.IsFailed) return BadRequest(result);
            return NoContent();
        }

    }
}
