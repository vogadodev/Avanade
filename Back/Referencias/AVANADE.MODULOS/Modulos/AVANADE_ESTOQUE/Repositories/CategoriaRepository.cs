using AVANADE.MODULOS.Data;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories
{
    public class CategoriaRepository <T>: BaseRepository<Categoria> where T : AvanadeDbContextComum<T>
    {
        public CategoriaRepository(T context) : base(context) { }
        public async Task<bool> NomeJaExiste(string nome, Guid? id = null) => id.HasValue ? await DbSet.AnyAsync(c => c.Nome == nome && c.Id != id.Value) : await DbSet.AnyAsync(c => c.Nome == nome);
        
        public async Task<List<Categoria>> ObterCategoriasESubCategorias()
        {
            return await DbSet.Include(c => c.Subcategorias).Where(c=> c.CategoriaPaiId == null).ToListAsync();
        }
    
    }
}
