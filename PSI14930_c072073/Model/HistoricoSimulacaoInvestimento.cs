namespace PSI14930_c072073.Model;
public class HistoricoSimulacaoInvestimento
{
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public int IdProduto { get; set; }
    public double ValorInvestido { get; set; }
    public double ValorFinal { get; set; }
    public int Prazo{ get; set; }
    public DateOnly Data { get; set; }
    public Cliente Cliente { get; set; }
    public Produto Produto { get; set; }

}
