using AVANADE.MODULOS.Data;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories
{
    public class MarcaRepository<T> : BaseRepository<Marca>  where T: AvanadeDbContextComum<T>
    {
        public MarcaRepository(T context) : base(context) { }
        public async Task<bool> NomeJaExiste(string nome, Guid? id = null) => 
            id.HasValue ? await DbSet.AnyAsync(m => m.Nome == nome && m.Id != id.Value)
                        : await DbSet.AnyAsync(m => m.Nome == nome);
        public async Task<List<Marca>> ObterTodasMarcasAsync(int pagina, int qtdItensPagina) => await DbSet.Skip((pagina - 1) * qtdItensPagina).Take(qtdItensPagina).ToListAsync();
    }
}
