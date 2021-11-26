using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class EmpresaModel
    {
        [Key]
        public int Codigo { get; set; }
        public string? Nome { get; set; }
        [Display(Name = "Nome Fantasia")]
        public string? NomeFantasia { get; set; }
        public string? CNPJ { get; set; }

        public bool Situação { get; set; }

        /* Não sei pq se tiver constrtor dá merda
         * public EmpresaModel(string Nome, string NomeFantasia, int Codigo, string CNPJ)
        {
            this.Nome = Nome;
            this.NomeFantasia = NomeFantasia;
            this.Codigo = Codigo;
            this.CNPJ = CNPJ;
        }*/

    }
}
