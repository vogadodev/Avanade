using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.GeradorDeIDsService;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Request;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;

namespace AVANADE.ESTOQUE.API.Services.FronecedorServices
{

    public class GravarFornecedorService : RetornoPadraoService
    {
        private readonly FornecedorRepository<EstoqueDbContext> _fornecedorRepository;
        private readonly ValidarFornecedorService _validarFornecedorService;

        public GravarFornecedorService(
              FornecedorRepository<EstoqueDbContext> fornecedorRepository
            , ValidarFornecedorService validarFornecedorService)
        {
            _fornecedorRepository = fornecedorRepository;
            _validarFornecedorService = validarFornecedorService;
        }
        public async Task GravarFornecedor(FornecedorRequestDto dto)
        {
            var fornecedorExistente = await _fornecedorRepository.SelecionarObjetoAsync(f => f.Id == dto.Id);
            var ehAtualizacao = fornecedorExistente != null;

            var dtoTemErro = await _validarFornecedorService.Validar(dto, ehAtualizacao);
            if (dtoTemErro)
            {
                Mensagens.AddRange(_validarFornecedorService.Mensagens);
                return;
            }
           
            if (ehAtualizacao)
            {
                PreencherFornecedor(fornecedorExistente!, dto);
                _fornecedorRepository.DbSet.Update(fornecedorExistente!);
            }
            else
            {
                var novoFornecedor = new Fornecedor() { Id = CriarIDService.CriarNovoID() };
                PreencherFornecedor(novoFornecedor, dto);
            }

            await _fornecedorRepository.DbContext.SaveChangesAsync();
        }
        private void PreencherFornecedor(Fornecedor fornecedor , FornecedorRequestDto dto)
        {
            fornecedor.RazaoSocial = dto.RazaoSocial;
            fornecedor.NomeContato = dto.NomeContato;
            fornecedor.Telefone = dto.Telefone;
            fornecedor.NomeFantasia = dto.NomeFantasia;
            fornecedor.CNPJ = dto.CNPJ;
        }
    }
}
