using AVANADE.INFRASTRUCTURE.ServicesComum.RetornoPadraoAPIs;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Enums;
using Microsoft.AspNetCore.Http;

namespace AVANADE.INFRASTRUCTURE.MiddlewaresGlobais
{
    public class GlobalExceptionMiddleware : RetornoPadraoService
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //TODO- EM PRODUÇÃO RETORNA MENSAGEM GENÉRICA SEM EXPOR DETALHES DA EXCEÇÃO
                Mensagens.Add(new Mensagem(ex.Message, EnumTipoMensagem.Erro));

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(this);
                Mensagens.Clear();
            }
        }
    }
}