using LocaFilme.Business.Models;

namespace LocaFilme.Business.Interfaces
{
    public interface ILocacaoService : IDisposable
    {
        Task Adicionar(Locacao locacao);

        Task Atualizar(Locacao locacao);
        Task Remover(Guid id);
        Task AtualizarCliente(Cliente cliente);
    }
}
