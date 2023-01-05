using LocaFilme.App.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LocaFilme.App.ViewModels
{
    public class LocacaoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Data de devolução")]
        public DateTime DataDevolucao { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataAtualizacao { get; set; }
        
        public ClienteViewModel Cliente { get; set; }

        public IEnumerable<FilmeViewModel> Filmes { get; set; }


        //[DisplayName("Tipo")]
        //public int TipoFornecedor { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        //public string Nome { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        //public string Documento { get; set; }
    }
}