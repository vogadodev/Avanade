using AVANADE.MODULOS.Data;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Repositories
{
    public class UsuarioRepository<T> : BaseRepository<Usuario> where T : AvanadeDbContextComum<T>
    {
        public UsuarioRepository(T dbContext) : base(dbContext)
        {

        }

        public async Task<Usuario?> ObterPorEmailComSenhaAsync(string email)
        {
            return await DbSet.Include(u => u.UsuarioPassword)
                                  .AsNoTracking().
                                  FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}