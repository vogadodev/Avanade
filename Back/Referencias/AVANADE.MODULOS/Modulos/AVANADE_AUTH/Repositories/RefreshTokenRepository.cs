using AVANADE.MODULOS.Data;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Repositories;

namespace AVANADE.MODULOS.Modulos.AVANADE_AUTH.Repositories
{
    public class RefreshTokenRepository<T>: BaseRepository<RefreshToken> where T : AvanadeDbContextComum<T>
    {
        public RefreshTokenRepository(T dbContext):base(dbContext)
        {
            
        }
    }
}
