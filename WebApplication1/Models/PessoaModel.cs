using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PessoaModel 
    { 
 
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        [Display(Name = "Quantidade de Filhos")]
        public int QuantidadeFilhos { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
        public decimal Salario { get; set; }
        public bool Situação { get; set; }
    }
}
