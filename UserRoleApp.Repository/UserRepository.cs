using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRoleApp.Repository
{
    public class UserRepository : IUserRepository<User, int>

    {
        UserRoleDbContext dbContext = new UserRoleDbContext(); 


        public User FindById(int id)
        {
                        return dbContext.Users.FirstOrDefault(u => u.UserId == id && u.IsActive==true)!;
        }

        public IEnumerable<User> GetAll()
        {
            var users = dbContext.Users.ToList();
            return users.Where( u => u.IsActive == true);
        }

        public IEnumerable<User> GetByCriteria(string criteria)
        {
            return null;
        }

        public int GetRoleIdForUser(int userId)
        {
            UserRole userRole = dbContext.UserRoles.FirstOrDefault( ur => ur.UserId == userId);

            if (userRole != null)
            {
                return userRole.RoleId;
            }
            else
            {
             
                throw new ArgumentException("User not found", nameof(userId));
            }
        }

        public void Remove(int id)
        {
           var user= dbContext.Users.Find(id);
           if(user != null) { user.IsActive = false; }
            else { throw new NullReferenceException(); }
            dbContext.SaveChanges();
        }

        public bool UpdateUserRole(UserRole userRole)
        {
            var newUserRole = dbContext.UserRoles.FirstOrDefault(c=>c.UserId==userRole.UserId);
            if(newUserRole == null) 
            { 
                dbContext.UserRoles.Add(userRole);
                dbContext.SaveChanges();
            }
            else
            {
                dbContext.UserRoles
                    .Where(u => u.UserId == userRole.UserId)
                    .ExecuteUpdate(setters =>

                    setters.SetProperty(u=>u.RoleId, userRole.RoleId)
                    );
            }
            return true;
        }

        public void Upsert(User entity)
        {
            var user = dbContext.Users.Find(entity.UserId);
            dbContext.ChangeTracker.Clear();
            dbContext.ChangeTracker.DetectChanges();

            if(user == null)
            {
                dbContext.Users.Add(entity);
                dbContext.SaveChanges();
            }
            else
            {
                dbContext.Users
                    .Where(u => u.UserId == entity.UserId)
                    .ExecuteUpdate(setters => 
                        setters.SetProperty( u1 => u1.FirstName, entity.FirstName )
                        .SetProperty(u1 => u1.LastName, entity.LastName)
                    );
            }
        }

       
    }
}
