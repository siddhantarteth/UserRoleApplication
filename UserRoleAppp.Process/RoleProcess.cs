using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoleApp.Repository;

namespace UserRoleAppp.Process
{
    public class RoleProcess
    {
        static IRepository<Role, int> roleRepository = new RoleRepository();

        public  IEnumerable<Role> GetAllRoles()
        {
            return roleRepository.GetAll();
        }
        public  Role GetRoleById(int roleId)
        {
            return roleRepository.FindById(roleId);
        }
        public  void RemoveRole(int roleId)
        {
            Role roleToRemove = roleRepository.FindById(roleId);

            if (roleToRemove != null)
            {
                roleToRemove.IsActive = false;

                roleRepository.Remove(roleId);
            }
            else
            {
                throw new ArgumentException($"User with ID {roleId} does not exist.");
            }
        }

        public  void AddRole(string RoleName,string RoleDescription)
        {
            if (RoleName.Length >=5)
            {
                Role role = new Role();
                role.RoleDescription = RoleDescription;
                role.RoleName = RoleName;
                role.IsActive = true;

                roleRepository.Upsert(role);
            }
            else
            {
                throw new ArgumentException("Role name is less than 5 characters");
            }
        }
        public  void UpdateRole(string RoleDescription, int RoleId)
        {
            Role existingRole = roleRepository.FindById(RoleId);
          
            if(RoleDescription.Length==0) { RoleDescription = existingRole.RoleDescription; }

            if (existingRole != null)
            {
                existingRole.RoleDescription = RoleDescription;
                roleRepository.Upsert(existingRole);
            }
            else
            {
                throw new ArgumentException($"User with ID {RoleId} does not exist.");
            }
        }
    }
}
