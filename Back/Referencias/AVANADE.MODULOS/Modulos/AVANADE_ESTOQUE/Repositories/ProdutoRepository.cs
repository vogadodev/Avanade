﻿using AVANADE.MODULOS.Data;
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
        public async Task<IEnumerable<Produto>> ObterProdutosPaginadoComFiltrosAsync(
          int pagina,
          int qtdItensPagina,
          string? nome,
          string? nomeMarca,
          string? nomeCategoria,
          bool? emPromocao)
        {
            return await DbSet
                .Include(p => p.Marca)
                .Include(p => p.Categoria)
                .Include(p => p.Avaliacoes)
                .Include(p => p.Imagens)
                .Include(p => p.Especificacoes)
                .AsNoTracking()
                .Where(p =>

                    (string.IsNullOrWhiteSpace(nome) || p.Nome.Contains(nome)) &&
                    (string.IsNullOrWhiteSpace(nomeMarca) || (p.Marca != null && p.Marca.Nome.Contains(nomeMarca))) &&
                    (string.IsNullOrWhiteSpace(nomeCategoria) || (p.Categoria != null && p.Categoria.Nome.Contains(nomeCategoria))) &&
                    (!emPromocao.HasValue || p.EstaEmPromocao == emPromocao.Value)
                )
                .OrderByDescending(p => p.EstaEmPromocao)
                .Skip(qtdItensPagina * (pagina - 1))
                .Take(qtdItensPagina)
                .ToListAsync();
        }
    }
}

