using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace UserRoleApp.Repository
{
    public interface IUserRepository<TEntity, TIdentity> : IRepository<TEntity, TIdentity>
    {
        int GetRoleIdForUser(int userId);
        bool UpdateUserRole(UserRole userRole);


    }
}
