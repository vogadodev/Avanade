using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.MenssagemService;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Interfaces;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Response;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;

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

        public async Task ObterTodos(int pagina, int qtdItensPagina)
        {
            var produtos = await _produtoRepository.ObterTodosComRelacionamentosAsync(pagina, qtdItensPagina);
            Data = produtos.Select(MapearProdutoParaResponseDto);
        }

        public async Task ObterPorNome(string nome, int pagina, int qtdItensPagina)
        {
            var produtos = await _produtoRepository.ObterCompletoPeloNomeAsync(nome, pagina, qtdItensPagina);
            Encontrado = produtos.Any();
            if (Encontrado)
                Data = produtos!.Select(MapearProdutoParaResponseDto);
        }        

        public async Task ObterPorCategoria(string nomeCategoria, int pagina, int qtdItensPagina)
        {
            var produtos = await _produtoRepository.ObterCompletoPeloCategoriaAsync(nomeCategoria, pagina, qtdItensPagina);
            Encontrado = produtos.Any();
            if (Encontrado)
                Data = produtos!.Select(MapearProdutoParaResponseDto);
        }

        public async Task ObterEmPromocao(int pagina, int qtdItensPagina)
        {
            var produtos = await _produtoRepository.ObterCompletoEmPromocaoAsync(pagina, qtdItensPagina);
            Encontrado = produtos.Any();
            if (Encontrado)
                Data = produtos!.Select(MapearProdutoParaResponseDto);
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
    }
}

