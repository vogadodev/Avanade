using System.ComponentModel;

namespace AVANADE.MODULOS.Modulos.AVANADE_COMUM.Enums
{
    public enum EnumEndpointPrincipalGateway
    {
        [Description("http://avanade-gateway-api:8080")]
        EndPointGatewayDockerCompose,
        [Description("https://localhost:7046/")]
        EndpointLocalHost
    }
}
