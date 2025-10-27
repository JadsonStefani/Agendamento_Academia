using Agendamento.Models;
using Agendamento.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Services
{
    public class ServAula : IServAula
    {
        private readonly AcademiaContext _context;

        public ServAula(AcademiaContext context)
        {
            _context = context;
        }
        public async Task<Aula> CriarAulaAsync(AulaDTO aulaDTO)
        {
            try
            {
                var aula = new Aula
                {
                    TipoAula = aulaDTO.TipoAula,
                    DataHora = aulaDTO.DataHora,
                    CapacidadeMaxParticipantes = aulaDTO.CapacidadeMaxParticipantes
                };

                _context.Aulas.Add(aula);
                await _context.SaveChangesAsync();
                 
                return aula;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
