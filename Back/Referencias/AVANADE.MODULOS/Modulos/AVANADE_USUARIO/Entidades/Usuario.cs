using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Enums;

namespace AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Entidades
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty!;     
        public string Email { get; set; } = string.Empty!;     
        public TipoUsuarioEnum Tipo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public UsuarioPassword UsuarioPassword { get; set; } = null!;
    }
}
