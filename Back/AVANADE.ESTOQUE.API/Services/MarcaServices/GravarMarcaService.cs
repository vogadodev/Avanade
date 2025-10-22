using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.GeradorDeIDsService;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;

namespace AVANADE.ESTOQUE.API.Services.MarcaServices
{
    public class GravarMarcaService : RetornoPadraoService
    {
        private readonly MarcaRepository<EstoqueDbContext> _marcaRepository;
        private readonly ValidarMarcaService _validarMarcaService;
        public GravarMarcaService(MarcaRepository<EstoqueDbContext> marcaRepository, ValidarMarcaService validarMarcaService)
        {
            _marcaRepository = marcaRepository;
            _validarMarcaService = validarMarcaService;
        }

        public async Task GravarMarca(MarcaRequestDto dto)
        {
            var marcarExistente = await _marcaRepository.SelecionarObjetoAsync(m => m.Id == dto.Id);
            var ehAtualizacao = marcarExistente != null;

            var dtoTemErros = await _validarMarcaService.Validar(dto, ehAtualizacao);
            if (dtoTemErros)
            {
                Mensagens.AddRange(_validarMarcaService.Mensagens);
                return;
            }

           
            if (ehAtualizacao)
            {
                PreencherMarca(marcarExistente!, dto);
                _marcaRepository.DbSet.Update(marcarExistente!);
            }
            else
            {
                var novaMarca = new Marca() { Id = CriarIDService.CriarNovoID() };
                PreencherMarca(novaMarca , dto);
            }
        }
        private void PreencherMarca(Marca marca, MarcaRequestDto dto)
        {
            marca.Nome = dto.Nome;
        }
    }
}
