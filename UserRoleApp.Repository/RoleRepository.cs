using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRoleApp.Repository
{
    public class RoleRepository : IRepository<Role, int>
    {
        UserRoleDbContext dbContext = new UserRoleDbContext();

        public Role FindById(int id)
        {
            return dbContext.Roles.FirstOrDefault(r=> r.RoleId == id  && r.IsActive == true )!;
        }

        public IEnumerable<Role> GetAll()
        {
            var roles = dbContext.Roles.ToList();
            return roles.Where(u => u.IsActive == true); ;
        }

        public IEnumerable<Role> GetByCriteria(string criteria)
        {
            return null;
        }

        public void Remove(int id)
        {
            var role = dbContext.Roles.Find(id);
            if (role != null) { role.IsActive = false; }
            else { throw new NullReferenceException(); }
            dbContext.SaveChanges();
        }

        public void Upsert(Role entity)
        {

            var role = dbContext.Roles.Find(entity.RoleId);
            dbContext.ChangeTracker.Clear();
            dbContext.ChangeTracker.DetectChanges();

            if (role == null)
            {
                dbContext.Roles.Add(entity);
                dbContext.SaveChanges();
            }
            else
            {
                dbContext.Roles
                    .Where(r => r.RoleId == entity.RoleId)
                    .ExecuteUpdate(setters =>
                        setters
                        .SetProperty(r1 => r1.RoleDescription, entity.RoleDescription)
                    );
            }
        }
    }
}
