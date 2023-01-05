namespace LocaFilme.Business.Models
{
    public class Cliente : Entity
    {
        public Guid LocacaoId { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations */
        public Locacao Locacao { get; set; }
    }
}
