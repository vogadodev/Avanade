using AVANADE.ESTOQUE.API.Services.ProdutoServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace AVANADE.ESTOQUE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProdutoController : ControllerBase
    {
        // Injetar todos os serviços necessários
        private readonly GravarProdutoService _gravarProdutoService;
        private readonly ObterProdutoService _obterProdutoService;

        public ProdutoController(
            GravarProdutoService gravarProdutoService,
            ObterProdutoService obterProdutoService)
        {
            _gravarProdutoService = gravarProdutoService;
            _obterProdutoService = obterProdutoService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CriarProduto([FromForm] ProdutoRequestDto dto, [FromForm] List<IFormFile>? imagens)
        {
            await _gravarProdutoService.GravarProduto(dto, imagens);
            return _gravarProdutoService.ResponderRequest(this);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterProdutoPorId(Guid id)
        {
            await _obterProdutoService.ObterPorId(id);
            return _obterProdutoService.ResponderRequest(this);
        }

        [HttpGet]
        public async Task<IActionResult> ObterProdutos(
            [FromQuery] int pagina,
            [FromQuery] int qtdItensPagina,
            [FromQuery] string? nome,
            [FromQuery] string? nomeCategoria,
            [FromQuery] bool? emPromocao)
        {

            if (!string.IsNullOrWhiteSpace(nome))
            {
                await _obterProdutoService.ObterPorNome(nome, pagina, qtdItensPagina);
            }
            else if (!string.IsNullOrEmpty(nomeCategoria))
            {
                await _obterProdutoService.ObterPorCategoria(nomeCategoria, pagina, qtdItensPagina);
            }
            else if (emPromocao.HasValue && emPromocao.Value)
            {
                await _obterProdutoService.ObterEmPromocao(pagina, qtdItensPagina);
            }
            else
            {
                await _obterProdutoService.ObterTodos(pagina, qtdItensPagina);
            }

            return _obterProdutoService.ResponderRequest(this);
        }
    }
}

