using AVANADE.MODULOS.Data;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories
{
    public class AvaliacaoRepository<T> : BaseRepository<Avaliacao> where T : AvanadeDbContextComum<T>
    {
        public AvaliacaoRepository(T context) : base(context) { }
        public async Task<bool> ProdutoExiste(Guid produtoId) => await DbSet.AnyAsync(p => p.Id == produtoId);
    }
}
