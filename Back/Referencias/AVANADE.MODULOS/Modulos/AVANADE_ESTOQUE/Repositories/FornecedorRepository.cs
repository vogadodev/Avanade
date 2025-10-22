using AVANADE.MODULOS.Data;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories
{
    public class FornecedorRepository<T> : BaseRepository<Fornecedor> where T : AvanadeDbContextComum<T>
    {
        public FornecedorRepository(T dbContext) : base(dbContext)
        {

        }

        public async Task<List<Fornecedor>> ObterTodosFornecedoresPaginado(int pagina, int qtdItemPagina)
        {
            return await DbSet
                .OrderBy(f => f.NomeFantasia)
                .Skip((pagina - 1) * qtdItemPagina)
                .Take(qtdItemPagina)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
