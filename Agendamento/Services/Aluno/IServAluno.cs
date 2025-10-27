using Agendamento.Models;

namespace Agendamento.Services
{
    public interface IServAluno
    {
        Task<Aluno> CadastrarAlunoAsync(AlunoDTO alunoDTO);
        Task<RelatorioAlunoDTO> GerarRelatorioAlunoAsync(int alunoId);
    }
}
