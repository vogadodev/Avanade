using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Enums;

namespace AVANADE.MODULOS.Modulos.AVANADE_AUTH.DTOs.Request
{
    public class UsuarioAuthDto
    {
        public Guid IdUsuario { get; set; }
        public string Email { get; set; }
        public string NomeUsuario { get; set; }
        public TipoUsuarioEnum Tipo { get; set; }
    }
}
