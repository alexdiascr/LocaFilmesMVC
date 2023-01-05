using LocaFilme.Business.Interfaces;
using LocaFilme.Business.Models;
using LocaFilme.Business.Models.Validations;

namespace LocaFilme.Business.Services
{
    public class FilmeService : BaseService, IFilmeService
    {
        private readonly IFilmeRepository _produtoRepository;

        public FilmeService(IFilmeRepository produtoRepository,
                              INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Filme filme)
        {
            if (!ExecutarValidacao(new FilmeValidation(), filme)) return;

            await _produtoRepository.Adicionar(filme);
        }

        public async Task Atualizar(Filme filme)
        {
            if (!ExecutarValidacao(new FilmeValidation(), filme)) return;

            await _produtoRepository.Atualizar(filme);
        }

        public async Task Remover(Guid id)
        {
            await _produtoRepository.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    } 
}
