using Agendamento.Models;
using Agendamento.Repositorio;
using Agendamento.Services;
using Agendamento.Services.Agendamento;
using Agendamento.Services.Agendamento.DTO;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Services
{
    public class ServAgendamento : IServAgendamento
    {
        private readonly AcademiaContext _context;

        public ServAgendamento(AcademiaContext context)
        {
            _context = context;
        }
        #region Agendar Aula

        public async Task<Agendamentos> AgendarAulaAsync(AgendamentoDTO agendamentoDTO)
        {
            try
            {
                await Validacoes(agendamentoDTO);

                var agendamento = new Agendamentos
                {
                    AlunoId = agendamentoDTO.AlunoId,
                    AulaId = agendamentoDTO.AulaId,
                    DataAgendamento = DateTime.UtcNow
                };

                _context.Agendamentos.Add(agendamento);
                await _context.SaveChangesAsync();

                return agendamento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Validacoes(AgendamentoDTO agendamentoDTO)
        {
            var aluno = await _context.Alunos
                .Include(a => a.Agendamentos)
                .FirstOrDefaultAsync(a => a.Id == agendamentoDTO.AlunoId);

            if (aluno == null)
                throw new ArgumentException($"Aluno com ID {agendamentoDTO.AlunoId} não cadastrado.");

            var aula = await _context.Aulas
                .Include(a => a.Agendamentos)
                .FirstOrDefaultAsync(a => a.Id == agendamentoDTO.AulaId);

            if (aula == null)
                throw new ArgumentException("Aula com ID " + agendamentoDTO.AulaId + " não cadastrada.");

            if (aula.Agendamentos.Count >= aula.CapacidadeMaxParticipantes)
                throw new InvalidOperationException($"Aula {aula.TipoAula} já está com a capacidade máxima de participantes, não é possível inserir novos alunos.");

            var dataAtual = DateTime.Now;
            var inicioMes = new DateTime(dataAtual.Year, dataAtual.Month, 1);
            var fimMes = inicioMes.AddMonths(1);

            var agendamentosMes = aluno.Agendamentos
                .Count(ag => ag.DataAgendamento >= inicioMes && ag.DataAgendamento < fimMes);

            var limitePlano = 0;

            if (aluno.Plano == EnumPlano.Mensal)
                limitePlano = 12;
            else if (aluno.Plano == EnumPlano.Trimestral)
                limitePlano = 20;
            else if (aluno.Plano == EnumPlano.Anual)
                limitePlano = 30;

            if (agendamentosMes >= limitePlano)
                throw new InvalidOperationException($"Limite de aulas mensais ({limitePlano}) atingido para o plano '{aluno.Plano}'.");

        }
        #endregion

        #region Cancelar Agendamento
        public async Task<bool> CancelarAgendamentoAsync(int agendamentoId)
        {
            try
            {
                var agendamento = await _context.Agendamentos
                    .FirstOrDefaultAsync(a => a.Id == agendamentoId);

                if (agendamento == null)
                    return false;

                _context.Agendamentos.Remove(agendamento);
                await _context.SaveChangesAsync();
                 
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Listar
        public async Task<List<Agendamentos>> GetAgendamentosAsync()
        {
            return await _context.Agendamentos
                .Include(a => a.Aluno)
                .Include(a => a.Aula)
                .ToListAsync();
        }
        #endregion
    }
}
