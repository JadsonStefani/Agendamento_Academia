using Agendamento.Models;
using Agendamento.Repositorio;
using Agendamento.Services.Agendamento.DTO;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Services
{
    public class ServAluno : IServAluno
    {
        private readonly AcademiaContext _context;

        public ServAluno(AcademiaContext context)
        {
            _context = context; 
        }
        #region Cadastrar Aluno
        public async Task<Aluno> CadastrarAlunoAsync(AlunoDTO alunoDTO)
        {
            try
            {
                await Validacao(alunoDTO);

                var aluno = new Aluno
                {
                    Nome = alunoDTO.Nome,
                    Plano = alunoDTO.Plano
                };

                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();
                 
                return aluno;
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }


        public async Task Validacao(AlunoDTO alunoDTO)
        { 
            if (!Enum.IsDefined(typeof(EnumPlano), alunoDTO.Plano))
            {
                throw new ArgumentException("Tipo de plano inválido.");
            }
        }
        #endregion


        #region Recuperar Aluno por Id
        public async Task<Aluno> RecuperarPorIdAsync(int id)
        {
            return await _context.Alunos
                .Include(a => a.Agendamentos)
                .ThenInclude(ag => ag.Aula)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        #endregion
        #region Relatório Aluno
        public async Task<RelatorioAlunoDTO> GerarRelatorioAlunoAsync(int alunoId)
        {
            var aluno = await RecuperarPorIdAsync(alunoId);

            if (aluno == null)
                throw new ArgumentException("Aluno não encontrado");

            var dataAtual = DateTime.Now;
            var inicioMes = new DateTime(dataAtual.Year, dataAtual.Month, 1);
            var fimMes = inicioMes.AddMonths(1);

            var agendamentosMes = aluno.Agendamentos
                .Where(ag => ag.DataAgendamento >= inicioMes && ag.DataAgendamento < fimMes)
                .ToList();

            var frequenciaPorTipo = agendamentosMes
                .GroupBy(ag => ag.Aula.TipoAula)
                .Select(g => new TipoAulaQtdeDTO
                {
                    TipoAula = g.Key,
                    QtdeMes = g.Count()
                })
                .OrderByDescending(t => t.QtdeMes)
                .ToList();

            return new RelatorioAlunoDTO
            {
                AlunoId = aluno.Id,
                AlunoNome = aluno.Nome, 
                TotalAulasMes = agendamentosMes.Count,
                TiposAulaFrequencia = frequenciaPorTipo
            };
        }
        #endregion
    }
}
