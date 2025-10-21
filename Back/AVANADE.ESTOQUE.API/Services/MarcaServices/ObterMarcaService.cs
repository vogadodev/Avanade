using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Interfaces;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;

namespace AVANADE.ESTOQUE.API.Services.MarcaServices
{
    public class ObterMarcaService : RetornoPadraoService, IServicoComBuscaPadrao
    {
        private readonly MarcaRepository<EstoqueDbContext> _MarcaRepository;

        public bool Encontrado { get; set; }
        public ObterMarcaService(MarcaRepository<EstoqueDbContext> marcaRepository)
        {
            _MarcaRepository = marcaRepository;
        }

        public async Task ObterPorNome(string nome)
        {
            var marca = await _MarcaRepository.SelecionarListaObjetoAsync(m=> m.Nome.Contains(nome));
            if (!marca.Any())
            {
                return;
            }
            Encontrado = true;
            Data = marca;
        }

        public async Task ObterTodas(int pagina, int qtdItensPagina)
        {
            var marca = await _MarcaRepository.ObterTodasMarcasAsync(pagina, qtdItensPagina);
            if (!marca.Any())
            {
                return;
            }
            Encontrado = true;
            Data = marca;
        }
    }
}
