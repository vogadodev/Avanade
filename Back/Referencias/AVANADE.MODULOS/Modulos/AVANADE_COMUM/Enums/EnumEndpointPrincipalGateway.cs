using System.ComponentModel;

namespace AVANADE.MODULOS.Modulos.AVANADE_COMUM.Enums
{
    public enum EnumEndpointPrincipalGateway
    {
        [Description("https://avanade.apigateway.com.br/")]
        EndPointGatewayDockerCompose,
        [Description("https://localhost:7046/")]
        EndpointLocalHost
    }
}
