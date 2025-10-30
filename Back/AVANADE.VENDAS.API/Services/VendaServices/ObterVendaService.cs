using AVANADE.INFRASTRUCTURE.ServicesComum.EnumService;
using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Interfaces;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.DTOs.Response;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Repositories;
using AVANADE.VENDAS.API.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AVANADE.VENDAS.API.Services.VendaServices
{
    public class ObterVendaService : RetornoPadraoService, IServicoComBuscaPadrao
    {
        private readonly VendaRepository<VendaDbContext> _vendaRepository;
        public ObterVendaService(VendaRepository<VendaDbContext> vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        public bool Encontrado { get; set; }

        public async Task ObterVenda(Guid id)
        {
            var venda = await _vendaRepository.SelecionarObjetoAsync(v => v.Id == id);
            if (venda == null)
            {

                return;
            }

            Encontrado = true;
            Data = CriarVendaDto(venda);
            return;
        }

        public async Task ObterVendasPorCliente(ClaimsPrincipal user)
        {
            var userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue(JwtRegisteredClaimNames.Sub);
            Guid.TryParse(userIdValue, out Guid userId);


            var vendas = await _vendaRepository.ObterVendasPorClienteAsync(userId);
            var vendasDtos = new List<VendaResponseDto>();
            if (vendas == null || !vendas.Any())
            {
                return;
            }
            foreach(var venda in vendas)
            {
                vendasDtos.Add(CriarVendaDto(venda));
            }

            Encontrado = true;
            Data = vendasDtos;
            return;
        }

        private VendaResponseDto CriarVendaDto(Venda venda)
        {
            return new VendaResponseDto(
                venda.Id,
                venda.ClienteId,
                venda.ValorTotal,
                venda.StatusVenda.GetDescription(),
                venda.StatusPagamento.GetDescription(),
                venda.DataCriacao,
                venda.DataAtualizacao,
                venda.EstaAtivo,
                venda.ItensVenda
                );

        }
    }
}