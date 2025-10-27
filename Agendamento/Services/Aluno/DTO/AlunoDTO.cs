using Agendamento.Models;

namespace Agendamento.Services
{
    public class AlunoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public EnumPlano Plano { get; set; }
    }
    public class TipoAulaQtdeDTO
    {
        public string TipoAula { get; set; } = string.Empty;
        public int QtdeMes { get; set; }
    }
    public class RelatorioAlunoDTO
    {
        public int AlunoId { get; set; }
        public string AlunoNome { get; set; } = string.Empty;
        public int TotalAulasMes { get; set; } 
        public List<TipoAulaQtdeDTO> TiposAulaFrequencia { get; set; } = new();
    }
}
