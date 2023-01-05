using LocaFilme.Business.Interfaces;
using LocaFilme.Business.Models;
using LocaFilme.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LocaFilme.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(MeuDbContext context) : base(context) { }

        public async Task<Cliente> ObterClientePorLocacao(Guid LocacaoId)
        {
            return await Db.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(f => f.LocacaoId == LocacaoId);
        }
    }
}
