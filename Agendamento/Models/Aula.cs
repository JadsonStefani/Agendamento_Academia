namespace Agendamento.Models
{
    public class Aula
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public int CapacidadeMaxima { get; set; }
    }
}
