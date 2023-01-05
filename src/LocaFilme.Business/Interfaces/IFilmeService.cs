using LocaFilme.Business.Models;

namespace LocaFilme.Business.Interfaces
{
    public interface IFilmeService : IDisposable
    {
        Task Adicionar(Filme filme);

        Task Atualizar(Filme filme);
        Task Remover(Guid id);
    }
}
