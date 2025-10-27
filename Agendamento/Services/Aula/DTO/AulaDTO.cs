namespace Agendamento.Services
{
    public class AulaDTO
    {
        public string TipoAula { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public int CapacidadeMaxParticipantes { get; set; }
    }
}
