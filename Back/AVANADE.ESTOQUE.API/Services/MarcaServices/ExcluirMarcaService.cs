using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Interfaces;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;

namespace AVANADE.ESTOQUE.API.Services.MarcaServices
{
    public class ExcluirMarcaService:RetornoPadraoService, IServicoDeExclusaoPadrao
    {
        private readonly MarcaRepository<EstoqueDbContext> _marcaRepository;

        public ExcluirMarcaService(MarcaRepository<EstoqueDbContext> marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task ExcluirMarca(Guid marcaId)
        {
            var marca = await _marcaRepository.SelecionarObjetoAsync(m=> m.Id == marcaId);
            if (marca == null)
                return;

            _marcaRepository.DbSet.Remove(marca);
            await _marcaRepository.DbContext.SaveChangesAsync();
        }
    }
}
