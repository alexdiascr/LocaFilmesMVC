using LocaFilme.Business.Interfaces;
using LocaFilme.Business.Models;
using LocaFilme.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LocaFilme.Data.Repository
{
    public class LocacaoRepository : Repository<Locacao>, ILocacaoRepository 
    {
        public LocacaoRepository(MeuDbContext context) : base(context) { }

        public async Task<Locacao>? ObterLocacaoCliente(Guid Id)
        {
            return await Db?.Locacoes.AsNoTracking().Include(c => c.Cliente)
                .FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<Locacao>? ObterLocacaoFilmesCliente(Guid Id)
        {
            return await Db?.Locacoes.AsNoTracking()
                .Include(c => c.Produtos)
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(c => c.Id == Id);
        }
    }
}
