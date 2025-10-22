using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Interfaces;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;

namespace AVANADE.ESTOQUE.API.Services.ProdutoServices
{
    public class ExcluirProdutoService:RetornoPadraoService , IServicoDeExclusaoPadrao
    {
        private readonly ProdutoRepository<EstoqueDbContext> _produtoRepository;
        public ExcluirProdutoService(ProdutoRepository<EstoqueDbContext> produtoReposiotory)
        {
            _produtoRepository = produtoReposiotory;
            
        }

        public async Task ExcluirProduto(Guid produtoId)
        {
            var produto = await _produtoRepository.SelecionarObjetoAsync(p=> p.Id == produtoId);
            if (produto == null)
                return;
            _produtoRepository.DbSet.Remove(produto);
            await _produtoRepository.DbContext.SaveChangesAsync();
        }
    }
}
