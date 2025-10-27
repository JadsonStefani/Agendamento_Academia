namespace Agendamento.Models
{
    public class Aula
    {
        public int Id { get; set; }
        public string TipoAula { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public int CapacidadeMaxParticipantes { get; set; }
        public ICollection<Agendamentos> Agendamentos { get; set; } = new List<Agendamentos>();
    }
}
