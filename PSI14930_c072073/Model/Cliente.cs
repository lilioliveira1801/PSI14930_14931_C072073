namespace PSI14930_c072073.Model;
public class Cliente
{
    public int Id { get; set; }
    public string Perfil { get; set; }
    public int Pontuacao { get; set; }
    public string Descricao { get; set; }
    public ICollection<HistoricoSimulacaoInvestimento> Simulacoes { get; set; }
    public ICollection<HistoricoInvestimento> Investimentos { get; set; }

}
