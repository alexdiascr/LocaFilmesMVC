using LocaFilme.Business.Models;

namespace LocaFilme.Business.Interfaces
{
    public interface IFilmeRepository : IRepository<Filme>
    {
        //Retorna uma lista de filmes por locacao
        Task<IEnumerable<Filme>> ObterFilmesPorLocacao(Guid fornecedorId);

        //Obter uma lista de filmes e locacoes daquele produto
        // - Para ter filmes com informação do locacao
        Task<IEnumerable<Filme>> ObterFilmesPorLocacoes();

        //Retorna um filme e a locacao dele - Id do filme
        Task<Filme> ObterFilmeLocacao(Guid Id);
    }
}
