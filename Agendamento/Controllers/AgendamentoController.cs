using Agendamento.Services;
using Agendamento.Services.Agendamento.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : Controller
    {
        private readonly IServAgendamento _servAgendamento;

        public AgendamentoController(IServAgendamento servAgendamento)
        {
            _servAgendamento = servAgendamento;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<ListaAgendamentoDTO>>> Listar()
        {
            try
            {
                var agendamentos = await _servAgendamento.GetAgendamentosAsync();
                var agendamentosDTO = agendamentos.Select(a => new ListaAgendamentoDTO
                {
                    Id = a.Id,
                    AlunoId = a.AlunoId,
                    AlunoNome = a.Aluno.Nome,
                    AulaId = a.AulaId,
                    AulaTipo = a.Aula.TipoAula,
                    AulaDataHora = a.Aula.DataHora,
                    DataAgendamento = a.DataAgendamento
                }).ToList();
                return Ok(agendamentosDTO);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar agendamentos. " + ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Inserir(AgendamentoDTO agendamentoDTO)
        {
            try
            {
                var agendamento = await _servAgendamento.AgendarAulaAsync(agendamentoDTO);

                return Ok("Aula agendada com sucesso.");
            }  
            catch (Exception ex)
            {
                return BadRequest("Erro ao agendar aula. " + ex.Message);
            }
        } 

        [HttpDelete]
        [Route("{codigoAgendamento}")]
        public async Task<ActionResult> CancelarAgendamento(int codigoAgendamento)
        {
            try
            {
                var sucess = await _servAgendamento.CancelarAgendamentoAsync(codigoAgendamento);

                if (!sucess)
                    return NotFound(new { error = $"Agendamento com ID {codigoAgendamento} não encontrado" });

                return Ok("Agendamento cancelado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao cancelar agendamento. " + ex.Message);
            }
        }
    }
}
