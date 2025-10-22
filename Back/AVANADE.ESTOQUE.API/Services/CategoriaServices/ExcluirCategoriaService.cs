using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Interfaces;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;

namespace AVANADE.ESTOQUE.API.Services.CategoriaServices
{
    public class ExcluirCategoriaService : RetornoPadraoService, IServicoDeExclusaoPadrao
    {
        private readonly CategoriaRepository<EstoqueDbContext> _categoriaRepository;
        public ExcluirCategoriaService(CategoriaRepository<EstoqueDbContext> categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task ExcluirCategoria(Guid categoriaID)
        {
            var categoria = await _categoriaRepository.SelecionarObjetoAsync(c => c.Id == categoriaID);
            if (categoria == null)
                return;

                 _categoriaRepository.DbSet.Remove(categoria);
           await _categoriaRepository.DbContext.SaveChangesAsync();
        }

    }
}
