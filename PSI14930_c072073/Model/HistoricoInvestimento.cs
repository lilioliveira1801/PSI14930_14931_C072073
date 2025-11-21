
namespace PSI14930_c072073.Model;
public class HistoricoInvestimento
{
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public int IdProduto { get; set; }
    public double Valor { get; set; }
    public DateOnly Data { get; set; }
    public Cliente Cliente { get; set; }

    public Produto Produto { get; set; }    

}
