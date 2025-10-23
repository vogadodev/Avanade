namespace AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Entidades
{
    public class UsuarioPassword
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public byte[] Hash { get; set; } = Array.Empty<byte>();
        public byte[] Salt { get; set; } = Array.Empty<byte>();
        public Usuario Usuario { get; set; } = null!;
    }
}
