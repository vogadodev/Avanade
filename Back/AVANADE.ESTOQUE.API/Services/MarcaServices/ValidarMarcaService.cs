using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.ServicoComMensagemService;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Resourcers;

namespace AVANADE.ESTOQUE.API.Services.MarcaServices
{
    public class ValidarMarcaService : MensagemService
    {
        private readonly MarcaRepository<EstoqueDbContext> _marcaRepository;

        public ValidarMarcaService(MarcaRepository<EstoqueDbContext> marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task<bool> Validar(MarcaRequestDto dto , bool ehAtualizacao)
        {
            ValidarCampoNomeObrigatorio(dto);
            
            if (!Mensagens.TemErros() && !ehAtualizacao)
            {
                await ValidarSeNomeJaExiste(dto);
            }

            return Mensagens.TemErros();
        }

        private void ValidarCampoNomeObrigatorio(MarcaRequestDto dto)
        {
            Mensagens.AdicionarErroSe(string.IsNullOrWhiteSpace(dto.Nome), MarcaResourcer.NomeObrigatorio);
        }

        private async Task ValidarSeNomeJaExiste(MarcaRequestDto dto)
        {
            if (await _marcaRepository.ValidarExistenciaAsync(m => m.Nome == dto.Nome && m.Id != dto.Id))
            {
                Mensagens.AdicionarErro(string.Format(MarcaResourcer.NomeJaCadastrado, dto.Nome));
            }
        }
    }
}
