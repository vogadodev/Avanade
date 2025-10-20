using AVANADE.ESTOQUE.API.Services.ProdutoServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        public async Task<IActionResult> CriarProduto(
            [FromForm] string produtoJson, 
            [FromForm] List<IFormFile>? imagens) 
        {           
                          
              var  dto = JsonSerializer.Deserialize<ProdutoRequestDto>(produtoJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });               
                     
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
            [FromQuery] string? nome,
            [FromQuery] string? nomeCategoria,
            [FromQuery] bool? emPromocao)
        {
            IEnumerable<ProdutoResponseDto> produtos;

            if (!string.IsNullOrWhiteSpace(nome))
            {
               await _obterProdutoService.ObterPorNome(nome);
            }
            else if (!string .IsNullOrEmpty(nomeCategoria))
            {
              await _obterProdutoService.ObterPorCategoria(nomeCategoria);
            }
            else if (emPromocao.HasValue && emPromocao.Value)
            {
               await _obterProdutoService.ObterEmPromocao();
            }
            else
            {
              await _obterProdutoService.ObterTodos();
            }

            return _obterProdutoService.ResponderRequest(this);
        }
    }
}

