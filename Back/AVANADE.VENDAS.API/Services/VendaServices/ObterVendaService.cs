using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Interfaces;
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
            var venda = await _vendaRepository.SelecionarObjetoAsync(v=> v.Id == id);
            if (venda == null)
            {               
               
                return;
            }

            Encontrado = true;
            Data = venda;
            return;
        }

        public async Task ObterVendasPorCliente(ClaimsPrincipal user)
        {
            var userIdValue = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? user.FindFirstValue(JwtRegisteredClaimNames.Sub);
            Guid.TryParse(userIdValue, out Guid userId);


            var vendas = await _vendaRepository.ObterVendasPorClienteAsync(userId);
            if (vendas == null || !vendas.Any())
            {  
                return;
            }
            Encontrado = true;
            Data = vendas;
            return;
        }

    }
}