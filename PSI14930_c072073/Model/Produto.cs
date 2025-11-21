
namespace PSI14930_c072073.Model;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Tipo  { get; set; }
    public decimal Rentabilidade { get; set; }
    public string Risco { get; set; }
    public ICollection<HistoricoSimulacaoInvestimento> Simulacoes { get; set; }
    public ICollection<HistoricoInvestimento> Investimentos { get; set; }
}
