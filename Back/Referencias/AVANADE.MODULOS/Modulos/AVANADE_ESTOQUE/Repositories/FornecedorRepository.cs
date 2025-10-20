using AVANADE.MODULOS.Data;
using AVANADE.MODULOS.Modulos.AVANADE_COMUM.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Entidades;

namespace AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories
{
    public class FornecedorRepository<T>: BaseRepository<Fornecedor> where T: AvanadeDbContextComum<T>
    {
        public FornecedorRepository(T dbContext):base(dbContext)
        {
            
        }
    }
}
