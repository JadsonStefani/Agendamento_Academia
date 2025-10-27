using Agendamento.Services; 
using Microsoft.AspNetCore.Mvc;

namespace Agendamento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IServAluno _servAluno;

        public AlunoController(IServAluno servAluno)
        {
            _servAluno = servAluno;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Inserir(AlunoDTO alunoDTO)
        {
            try
            {
                var aluno = await _servAluno.CadastrarAlunoAsync(alunoDTO);
                 
                return Ok("Aluno(a) "+ aluno.Nome +" cadastrado(a) com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao cadastrar aluno(a) " + alunoDTO.Nome + ". " + ex.Message);
            }
        }

        [HttpGet]
        [Route("relatorio/{codigoAluno}")]
        public async Task<ActionResult<RelatorioAlunoDTO>> GetRelatorioAluno(int codigoAluno)
        {
            try
            {
                var relatorio = await _servAluno.GerarRelatorioAlunoAsync(codigoAluno);
                return Ok(relatorio);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
