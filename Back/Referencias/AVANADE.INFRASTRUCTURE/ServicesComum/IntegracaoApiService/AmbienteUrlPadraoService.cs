using AVANADE.INFRASTRUCTURE.ServicesComum.EnumService;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Enums;

namespace AVANADE.INFRASTRUCTURE.ServicesComum.IntegracaoApiService
{
    public static class AmbienteUrlPadraoService
    {
        public static string UrlPadraoService()
        {
            //Para testes com dockercompose utilize essa linha
            //return EnumEndpointPrincipalGateway.EndPointGatewayDockerCompose.GetDescription();
            //Para testes locais sem dockercompose utilize essa linha
            return EnumEndpointPrincipalGateway.EndpointLocalHost.GetDescription();
        }
    }
}
