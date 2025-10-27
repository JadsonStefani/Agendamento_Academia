namespace Agendamento.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public EnumPlano Plano { get; set; }
        public ICollection<Agendamentos> Agendamentos { get; set; } = new List<Agendamentos>();
    }

    public enum EnumPlano
    {
        Mensal = 1,
        Trimestral = 2,
        Anual = 3
    }
}
