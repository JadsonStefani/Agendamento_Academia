using Agendamento.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulaController : ControllerBase
    {
        private readonly IServAula _servAula;

        public AulaController(IServAula servAula)
        {
            _servAula = servAula;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Inserir(AulaDTO aulaDTO)
        {
            try
            {
                var aula = await _servAula.CriarAulaAsync(aulaDTO);

                return Ok("Aula " + aula.TipoAula + " cadastrada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao cadastrar aula " + aulaDTO.TipoAula + ". " + ex.Message);
            }
        }
    }
}
