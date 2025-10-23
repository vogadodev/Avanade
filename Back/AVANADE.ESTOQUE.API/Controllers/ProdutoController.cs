using AVANADE.ESTOQUE.API.Services.ProdutoServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.AuthServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AVANADE.ESTOQUE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProdutoController : ControllerBase    {
        
        private readonly GravarProdutoService _gravarProdutoService;
        private readonly ObterProdutoService _obterProdutoService;
        private readonly ExcluirProdutoService _excluirProdutoService;

        public ProdutoController(
              GravarProdutoService gravarProdutoService
            , ObterProdutoService obterProdutoService
            , ExcluirProdutoService excluirProdutoService)
        {
            _gravarProdutoService = gravarProdutoService;
            _obterProdutoService = obterProdutoService;
            _excluirProdutoService = excluirProdutoService;
        }

        [Authorize(Policy = PoliciesTipoUsuario.ApenasAdm)]
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

        [Authorize(Policy = PoliciesTipoUsuario.Todos)]
        [HttpGet]
        public async Task<IActionResult> ObterProdutos(
            [FromQuery] int pagina,
            [FromQuery] int qtdItensPagina,
            [FromQuery] string? nome,
            [FromQuery] string? nomeMarca,
            [FromQuery] string? nomeCategoria,
            [FromQuery] bool? emPromocao)
        {
            await _obterProdutoService.ObterProdutosPaginadoComFiltro(pagina, qtdItensPagina , nome , nomeMarca, nomeCategoria, emPromocao);
            return _obterProdutoService.ResponderRequest(this);
        }

        [Authorize(Policy = PoliciesTipoUsuario.ApenasAdm)]
        [HttpDelete("{idProduto}")]
        public async Task<IActionResult>ExcluirProduto(Guid idProduto)
        {
            await _excluirProdutoService.ExcluirProduto(idProduto);
            return _excluirProdutoService.ResponderRequest(this);
        }
    }
}

