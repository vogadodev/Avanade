using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Interfaces;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;

namespace AVANADE.ESTOQUE.API.Services.FronecedorServices
{
    public class ExcluirFornecedorService : RetornoPadraoService, IServicoDeExclusaoPadrao
    {
        private readonly FornecedorRepository<EstoqueDbContext> _fornecedorRepository;

        public ExcluirFornecedorService(FornecedorRepository<EstoqueDbContext> fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }
        public async Task ExcluirFornecedor(Guid idFornecedor)
        {
            var fornecedor = await _fornecedorRepository.SelecionarObjetoAsync(f => f.Id == idFornecedor);
            if (fornecedor == null)
                return;

            _fornecedorRepository.DbSet.Remove(fornecedor);
            await _fornecedorRepository.DbContext.SaveChangesAsync();
        }
    }
}
