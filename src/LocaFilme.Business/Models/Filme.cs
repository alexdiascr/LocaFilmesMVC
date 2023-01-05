namespace LocaFilme.Business.Models
{
    public class Filme : Entity
    {
        public Guid LocacaoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations */
        public Locacao Locacao { get; set; }

        //public IEnumerable<Filmes> Filmes { get; set; }
    }
}
