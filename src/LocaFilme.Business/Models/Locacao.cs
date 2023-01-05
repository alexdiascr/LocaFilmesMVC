namespace LocaFilme.Business.Models
{
    public class Locacao : Entity
    {
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataDevolucao { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations */
        public IEnumerable<Filme> Produtos { get; set; }
        public Cliente Cliente { get; set; }
    }
}
