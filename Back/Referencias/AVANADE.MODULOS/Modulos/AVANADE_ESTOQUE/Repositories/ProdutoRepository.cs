using AVANADE.MODULOS.Data;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories
{
    public class ProdutoRepository<T> : BaseRepository<Produto> where T : AvanadeDbContextComum<T>
    {
        public ProdutoRepository(T context) : base(context) { }
        public async Task<Produto?> ObterPorIdComRelacionamentosAsync(Guid? id) =>
            await DbSet.Include(p => p.Marca)
            .Include(p => p.Categoria)
            .Include(p => p.Avaliacoes)
            .Include(p => p.Imagens)
            .Include(p => p.Especificacoes)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        public async Task<IEnumerable<Produto>> ObterTodosComRelacionamentosAsync() =>
            await DbSet.Include(p => p.Marca)
            .Include(p => p.Categoria)
            .Include(p => p.Avaliacoes)
            .Include(p => p.Imagens)
            .Include(p => p.Especificacoes)
            .AsNoTracking()
            .ToListAsync();

        public async Task<IEnumerable<Produto>> ObterCompletoPeloNomeAsync(string nome) =>
            await DbSet.Include(p => p.Marca)
                .Include(p => p.Categoria)
                .Include(p => p.Avaliacoes)
                .Include(p => p.Imagens)
                .Include(p => p.Especificacoes)
                .Where(p => p.Nome.
                 Contains(nome))
                .AsNoTracking()
                .ToListAsync();

        public async Task<IEnumerable<Produto>> ObterCompletoPeloCategoriaAsync(string categoria) =>
            await DbSet.Include(p => p.Marca)
               .Include(p => p.Categoria)
               .Include(p => p.Avaliacoes)
               .Include(p => p.Imagens)
               .Include(p => p.Especificacoes)
               .Where(p => p.Categoria!.Nome.Contains(categoria))
               .AsNoTracking()
               .ToListAsync();

        public async Task<IEnumerable<Produto>> ObterCompletoEmPromocaoAsync() => 
            await DbSet.Include(p => p.Marca)
              .Include(p => p.Categoria)
              .Include(p => p.Avaliacoes)
              .Include(p => p.Imagens)
              .Include(p => p.Especificacoes)
              .Where(p => p.EstaEmPromocao)
              .AsNoTracking()
              .ToListAsync();
    }
}

