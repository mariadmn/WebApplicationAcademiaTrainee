namespace WebApplication1.Models
{
    public class PessoaModel
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int Codigo { get; set; }
        public int QuantidadeFilhos { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal? Salario { get; set; }
    }
}
