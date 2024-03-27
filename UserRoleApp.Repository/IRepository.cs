using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRoleApp.Repository
{
    public interface IRepository<TEntity, TIdentity>
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByCriteria(string criteria);
        TEntity FindById(TIdentity id);
        void Upsert(TEntity entity);
        void Remove(TIdentity id);
    }
}
