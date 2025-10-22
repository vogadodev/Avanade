using AVANADE.ESTOQUE.API.Data;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Interfaces;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.DTOs.Response;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;

namespace AVANADE.ESTOQUE.API.Services.FronecedorServices
{
    public class ObterFornecedorService : RetornoPadraoService, IServicoComBuscaPadrao
    {
        private readonly FornecedorRepository<EstoqueDbContext> _fornecedorRepository;

        public ObterFornecedorService(FornecedorRepository<EstoqueDbContext> fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;           
        }

        public bool Encontrado { get ; set ; }

        public async Task ObterFornecedorPorNomeFantasia(string nomeFantasia)
        {
            var fornecedor = await _fornecedorRepository.SelecionarObjetoAsync(f=> f.NomeFantasia == nomeFantasia);
            
            if(fornecedor == null) 
               return;
            
            Encontrado = true;
            Data = CriarFornecedorDto(fornecedor);
        }

        public async Task ObterTodosFornecedorPaginado(int pagina, int qtdItemPagina)
        {
            var listaFornecedores = await _fornecedorRepository.ObterTodosFornecedoresPaginado(pagina, qtdItemPagina);
            if(!listaFornecedores.Any())
                return;

            Encontrado = true;
            Data = listaFornecedores.Select(f=> 
                    CriarFornecedorDto(f)
                    ).ToList();
        }

        private FornecedorResponseDto CriarFornecedorDto(Fornecedor fornecedor)
        {
            return new FornecedorResponseDto(
                fornecedor.Id,
                fornecedor.RazaoSocial,
                fornecedor.NomeContato,
                fornecedor.Telefone,
                fornecedor.NomeFantasia,
                fornecedor.CNPJ,
                fornecedor.Email
                );
        }
    }
}
