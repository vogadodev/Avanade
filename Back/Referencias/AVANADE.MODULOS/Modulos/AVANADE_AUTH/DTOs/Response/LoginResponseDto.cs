using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Enums;

namespace AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Response
{
    public record LoginResponseDto(Guid? IDUsuario, string NomeUsuario , string? email = null, TipoUsuarioEnum? tipo = null);    
}
