using PSI14930_c072073.Model;

namespace PSI14930_c072073.Logica;

public class HistoricoSimulacaoPerfilCalculo
{
          
        public void CalcularPerfil(HistoricoSimulacaoPerfil simulacao)
        {
            int pontosValor = simulacao.Valor <= 10000 ? 50 : simulacao.Valor <= 30000 ? 70 : 90;
            int pontosPrazo = simulacao.Prazo <= 12 ? 50 : simulacao.Prazo <= 24 ? 70 : 90;
            int pontosLiquidez = simulacao.Liquidez ? 50 : 70;
            int pontosRentabilidade = simulacao.Rentabilidade ? 90 : 50;

            int soma = pontosValor + pontosPrazo + pontosLiquidez + pontosRentabilidade;
            simulacao.Pontuacao = soma / 4;

            simulacao.Perfil = simulacao.Pontuacao <= 50 ? "Conservador"
                               : simulacao.Pontuacao <= 70 ? "Moderado"
                               : "Arrojado";
        }    

}
