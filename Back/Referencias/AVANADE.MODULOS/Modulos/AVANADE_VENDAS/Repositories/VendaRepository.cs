using AVANADE.MODULOS.Data;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Repositories
{
    public class VendaRepository<T> : BaseRepository<Venda> where T : AvanadeDbContextComum<T>
    {
        public VendaRepository(T context) : base(context) { }
        
        public async Task<Venda?> ObterVendaCompletaAsync(Guid id)
        {
            return await DbSet
                .Include(v => v.ItensVenda)
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }
     
        public async Task<IEnumerable<Venda>> ObterVendasPorClienteAsync(Guid clienteId)
        {
            return await DbSet
                .Include(v => v.ItensVenda)
                .Where(v => v.ClienteId == clienteId)
                .OrderByDescending(v => v.DataVenda)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
