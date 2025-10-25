﻿using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.INFRASTRUCTURE.ServicesComum.ServicoComMensagemService;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Interfaces;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Response;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.ContratosMensagem;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.Request;

namespace AVANADE.ESTOQUE.API.Services.ProdutoServices
{
    public class ObterProdutoService : RetornoPadraoService, IServicoComBuscaPadrao
    {

        private readonly ProdutoRepository<EstoqueDbContext> _produtoRepository;

        public ObterProdutoService(ProdutoRepository<EstoqueDbContext> produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public bool Encontrado { get; set; }

        public async Task ObterPorId(Guid id)
        {
            var produto = await _produtoRepository.ObterPorIdComRelacionamentosAsync(id);
            if (produto == null)
            {
                Mensagens.AdicionarErro("Id", "Produto não encontrado.");
                return;
            }
            Encontrado = true;
            Data = MapearProdutoParaResponseDto(produto);
        }

        public async Task ObterProdutosPaginadoComFiltro(int pagina, int qtdItensPagina, string? nome, string? nomeMarca, string? nomeCategoria, bool? estaEmPromocao)
        {
            var produtos = await _produtoRepository.ObterProdutosPaginadoComFiltrosAsync(pagina, qtdItensPagina, nome, nomeMarca, nomeCategoria, estaEmPromocao);
            Encontrado = produtos.Any();
            if (Encontrado)
                Data = produtos.Select(MapearProdutoParaResponseDto);
        }

        // Método auxiliar para mapear a entidade para o DTO de resposta
        private ProdutoResponseDto MapearProdutoParaResponseDto(Produto produto)
        {
            if (produto == null) return null;

            return new ProdutoResponseDto(
                produto.Id,
                produto.Nome,
                produto.Descricao,
                produto.CodigoUnico,
                produto.Preco,
                produto.PrecoPromocional,
                produto.EstaEmPromocao,
                produto.TemFreteGratis,
                produto.QuantidadeEstoque,
                produto.EstaAtivo,
                produto.MarcaId,
                produto.Marca!.Nome,
                produto.CategoriaId,
                produto.Categoria!.Nome,
                produto.Avaliacoes?.Select(a => new AvaliacaoResponseDto(a.Id, a.ProdutoId, a.NomeAutor, "", a.Comentario, a.Nota, a.DataEnvio)).ToList() ?? new List<AvaliacaoResponseDto>(),
                produto.Imagens?.Select(i => new ProdutoImagemDto(i.UrlImagem, i.TextoAlternativo, i.Ordem)).OrderBy(i => i.Ordem).ToList() ?? new List<ProdutoImagemDto>(),
                produto.Especificacoes?.Select(e => new ProdutoEspecificacaoDto(e.Chave, e.Valor)).ToList() ?? new List<ProdutoEspecificacaoDto>()
            );
        }

        public async Task ProdutosSemEstoque(PedidoRequestDto dto)
        {
            var listaDeIds = dto.listaDeProdutos.Select(p => p.IdProduto).ToList();

            var produtosDoBanco = await _produtoRepository.SelecionarListaObjetoAsync(p => listaDeIds.Contains(p.Id));
            var produtoSemEstoque = produtosDoBanco.Where(p => p.QuantidadeEstoque <= 0).ToList();

            var idsEncontrados = produtosDoBanco.Select(p => p.Id).ToHashSet();
            var produtosInexistentesDto = dto.listaDeProdutos
                                 .Where(p => !idsEncontrados.Contains(p.IdProduto))
                                 .ToList();

            if (!produtoSemEstoque.Any() && !produtosInexistentesDto.Any())
                return;

            Encontrado = true;
            var listaDeProdutosComErro = new List<ItemPedidoDto>();
            listaDeProdutosComErro.AddRange(CriarProdutosSemEstoqueDto(produtoSemEstoque));           
            listaDeProdutosComErro.AddRange(produtosInexistentesDto);           
            Data = listaDeProdutosComErro;
        }

        public List<ItemPedidoDto> CriarProdutosSemEstoqueDto(IList<Produto> listaProdutos)
        {
            return listaProdutos.Select(p => new ItemPedidoDto(p.Id, p.Nome, p.QuantidadeEstoque)).ToList();
        }
    }
}

