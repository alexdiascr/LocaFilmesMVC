using LocaFilme.Business.Models;

namespace LocaFilme.Business.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        //Obter Locacao por Cliente
        Task<Cliente> ObterClientePorLocacao(Guid LocacaoId);
    }
}
