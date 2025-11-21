namespace PSI14930_c072073.Model;
public class HistoricoSimulacaoPerfil
{
    public int Id { get; set; }
    public double Valor { get; set; }
    public int Prazo { get; set; }
    public bool Liquidez { get; set; }
    public bool Rentabilidade { get; set; }
    public string Perfil { get; set; }
    public int Pontuacao { get; set; }
    public DateOnly Data { get; set; }

}
