using Agendamento.Models;
using Agendamento.Services.Agendamento.DTO;

namespace Agendamento.Services
{
    public interface IServAgendamento
    {
        Task<Agendamentos> AgendarAulaAsync(AgendamentoDTO agendamentoDTO);
        Task<bool> CancelarAgendamentoAsync(int agendamentoId);
        Task<List<Agendamentos>> GetAgendamentosAsync();
    }
}
