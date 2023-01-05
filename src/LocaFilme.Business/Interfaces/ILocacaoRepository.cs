using LocaFilme.Business.Models;

namespace LocaFilme.Business.Interfaces
{
    public interface ILocacaoRepository : IRepository<Locacao>
    {
        Task<Locacao> ObterLocacaoCliente(Guid id);

        //Trabalhar com métodos especializados para a expressividade do código ficar melhor de 
        //entender
        Task<Locacao> ObterLocacaoFilmesCliente(Guid id);
    }
}
