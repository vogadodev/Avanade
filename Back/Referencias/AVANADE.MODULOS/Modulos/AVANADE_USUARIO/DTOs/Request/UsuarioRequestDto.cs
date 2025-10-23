namespace AVANADE.MODULOS.Modulos.AVANADE_USUARIO.DTOs.Request
{
    public record UsuarioRequestDto(Guid? Id, string NomeCompleto, string Email, string Senha, string ConfirmacaoSenha);    
}
