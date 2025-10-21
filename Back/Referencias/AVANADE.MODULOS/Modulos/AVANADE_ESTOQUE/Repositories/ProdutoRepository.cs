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
        public async Task<IEnumerable<Produto>> ObterTodosComRelacionamentosAsync(int pagina, int qtdItensPagina) =>
            await DbSet.Include(p => p.Marca)
            .Include(p => p.Categoria)
            .Include(p => p.Avaliacoes)
            .Include(p => p.Imagens)
            .Include(p => p.Especificacoes)
            .OrderBy(p => p.EstaEmPromocao == true)
            .AsNoTracking()
            .Skip(qtdItensPagina * (pagina - 1))
            .Take(qtdItensPagina)
            .ToListAsync();

        public async Task<IEnumerable<Produto>> ObterCompletoPeloNomeAsync(string nome, int pagina, int qtdItensPagina) =>
            await DbSet.Include(p => p.Marca)
                .Include(p => p.Categoria)
                .Include(p => p.Avaliacoes)
                .Include(p => p.Imagens)
                .Include(p => p.Especificacoes)
                .Where(p => p.Nome.
                 Contains(nome))
                .OrderBy(p => p.EstaEmPromocao == true)
                .Skip(qtdItensPagina * (pagina - 1))
                .Take(qtdItensPagina)
                .AsNoTracking()
                .ToListAsync();

        public async Task<IEnumerable<Produto>> ObterCompletoPeloCategoriaAsync(string categoria, int pagina, int qtdItensPagina) =>
            await DbSet.Include(p => p.Marca)
               .Include(p => p.Categoria)
               .Include(p => p.Avaliacoes)
               .Include(p => p.Imagens)
               .Include(p => p.Especificacoes)
               .Where(p => p.Categoria!.Nome.Contains(categoria))
               .OrderBy(p => p.EstaEmPromocao == true)
               .Skip(qtdItensPagina * (pagina - 1))
               .Take(qtdItensPagina)
               .AsNoTracking()
               .ToListAsync();

        public async Task<IEnumerable<Produto>> ObterCompletoEmPromocaoAsync(int pagina, int qtdItensPagina) =>
            await DbSet.Include(p => p.Marca)
              .Include(p => p.Categoria)
              .Include(p => p.Avaliacoes)
              .Include(p => p.Imagens)
              .Include(p => p.Especificacoes)
              .Where(p => p.EstaEmPromocao == true)
              .OrderBy(p => p.EstaEmPromocao == true)
              .Skip(qtdItensPagina * (pagina - 1))
              .Take(qtdItensPagina)
              .AsNoTracking()
              .ToListAsync();
    }
}

