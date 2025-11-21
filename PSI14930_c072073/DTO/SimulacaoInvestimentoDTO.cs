namespace PSI14930_c072073.DTO;

//public class SimulacaoInvestimentoDTO

    public class SimulacaoRequest
    {
        public int ClienteId { get; set; }
        public double Valor { get; set; }
        public int PrazoMeses { get; set; }
        public string TipoProduto { get; set; }
    }

    public class ProdutoValidadoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public double Rentabilidade { get; set; }
        public string Risco { get; set; }
    }

    public class ResultadoSimulacaoResponse
    {
        public double ValorFinal { get; set; }
        public double RentabilidadeEfetiva { get; set; }
        public int PrazoMeses { get; set; }
    }

    public class SimulacaoResponse
    {
        public ProdutoValidadoResponse ProdutoValidado { get; set; }
        public ResultadoSimulacaoResponse ResultadoSimulacao { get; set; }
        public DateTime DataSimulacao { get; set; }
    }

