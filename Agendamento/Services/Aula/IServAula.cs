using Agendamento.Models;

namespace Agendamento.Services
{
    public interface IServAula
    {
        Task<Aula> CriarAulaAsync(AulaDTO aulaDTO);
    }
}
