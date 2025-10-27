namespace Agendamento.Services.Agendamento.DTO
{
    public class AgendamentoDTO
    {
        public int AlunoId { get; set; }
        public int AulaId { get; set; }
    }
    public class ListaAgendamentoDTO
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public string AlunoNome { get; set; } = string.Empty;
        public int AulaId { get; set; }
        public string AulaTipo { get; set; } = string.Empty;
        public DateTime AulaDataHora { get; set; }
        public DateTime DataAgendamento { get; set; }
    }
}
