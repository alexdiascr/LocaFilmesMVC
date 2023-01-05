using LocaFilme.Business.Interfaces;
using LocaFilme.Business.Models;
using LocaFilme.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LocaFilme.Data.Repository
{
    public class FilmeRepository : Repository<Filme>, IFilmeRepository
    {
        public FilmeRepository(MeuDbContext context) : base(context) { }

        public async Task<Filme>? ObterFilmeLocacao(Guid Id)
        {
            return await Db.Filmes?.AsNoTracking().Include(f => f.Locacao)
                .FirstOrDefaultAsync(p => p.Id == Id);                
        }

        public async Task<IEnumerable<Filme>> ObterFilmesPorLocacoes()
        {
            return await Db.Filmes.AsNoTracking().Include(f => f.Locacao)
                .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Filme>> ObterFilmesPorLocacao(Guid locacaoId)
        {
            return await Buscar(p => p.LocacaoId == locacaoId);
        }

    }
}
