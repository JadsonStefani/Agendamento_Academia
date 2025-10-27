namespace Agendamento.Models
{
    public class Agendamentos
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; } = null!;
        public int AulaId { get; set; }
        public Aula Aula { get; set; } = null!;
        public DateTime DataAgendamento { get; set; } = DateTime.UtcNow;

    }
}
