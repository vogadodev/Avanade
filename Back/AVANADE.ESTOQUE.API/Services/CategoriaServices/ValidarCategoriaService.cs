using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.ServicoComMensagemService;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Resourcers;

namespace AVANADE.ESTOQUE.API.Services.CategoriaServices
{
    public class ValidarCategoriaService : MensagemService
    {
        private readonly CategoriaRepository<EstoqueDbContext> _categoriaRepository;

        public ValidarCategoriaService(CategoriaRepository<EstoqueDbContext> categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<bool> Validar(CategoriaRequestDto dto, bool ehAtualizacao)
        {
            ValidarCampoNomeObrigatorio(dto);
            ValidarCampoDescricao(dto);

            if (!Mensagens.TemErros() && !ehAtualizacao)
            {
                await ValidarSeNomeJaExiste(dto);
            }

            return Mensagens.TemErros();
        }

        private void ValidarCampoNomeObrigatorio(CategoriaRequestDto dto)
        {
            Mensagens.AdicionarErroSe(string.IsNullOrWhiteSpace(dto.Nome), CategoriaResourcer.NomeObrigatorio);
        }
      
        private void ValidarCampoDescricao(CategoriaRequestDto dto)
        {
            Mensagens.AdicionarErroSe(string.IsNullOrWhiteSpace(dto.Descricao), CategoriaResourcer.DescricaoObrigatorio);
        }

        private async Task ValidarSeNomeJaExiste(CategoriaRequestDto dto)
        {
            if (await _categoriaRepository.ValidarExistenciaAsync(c => c.Nome == dto.Nome && c.Id != dto.id))
            {
                Mensagens.AdicionarErro(string.Format(CategoriaResourcer.NomeJaCadastrado, dto.Nome));
            }
        }
    }
}
