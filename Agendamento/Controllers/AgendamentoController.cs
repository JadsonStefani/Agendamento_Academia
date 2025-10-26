using Agendamento.Services.Agendamento;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : Controller
    {
        private readonly IServAluno _servAgendamento;

        public AgendamentoController(IServAluno servAgendamento)
        {
            _servAgendamento = servAgendamento;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Listar()
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Inserir([FromBody] int codigo)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{codigo}")]
        public IActionResult Atualizar([FromRoute] int codigo, [FromBody] int reg)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{codigo}")]
        public IActionResult Remover([FromRoute] int codigo)
        {
            try
            { 
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
