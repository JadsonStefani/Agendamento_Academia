namespace Agendamento.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public EnumPlano Plano { get; set; }
    }

    public enum EnumPlano
    {
        Mensal,
        Trimestral,
        Anual
    }
}
