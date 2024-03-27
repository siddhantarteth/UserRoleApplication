using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using UserRoleApp.Repository;

namespace UserRoleAppp.Process
{
    public class UserProcess
    {
        static IUserRepository<User, int> userRepository = new UserRepository();


        public IEnumerable<User> GetAllUsers()
        {
            return userRepository.GetAll();
        }
        public  User GetUserById(int userId)
        {
            return userRepository.FindById(userId);
        }
        public  void UpdateUser(int UserId,string FirstName,string LastName)
        {
            User existingUser = userRepository.FindById(UserId);
            if(FirstName.Length==0)
                FirstName=existingUser.FirstName;
            if(LastName.Length==0)
                LastName=existingUser.LastName;

            if (existingUser != null)
            {
                existingUser.FirstName = FirstName;
                existingUser.LastName = LastName;
                userRepository.Upsert(existingUser);
            }
            else
            {
                throw new ArgumentException($"User with ID {UserId} does not exist.");
            }
        }
        public  void AddUser(string UserName,string LastName,string FirstName,string Password)
        {
            if ((Password.Length >= 8 && Password.Any(char.IsDigit) && UserName.Length>=5))
            {
               User user=new User();
                user.UserName = UserName;
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.Password = Password;
                user.IsActive = true;


                userRepository.Upsert(user);
            }
            else 
            {
                throw new ArgumentException("UserName / Password is not valid");

            }
        }
        public  void RemoveUser(int userId)
        {
            User userToRemove = userRepository.FindById(userId);

            if (userToRemove != null)
            {
                userToRemove.IsActive = false;

                userRepository.Remove(userId);
            }
            else
            {
                throw new ArgumentException($"User with ID {userId} does not exist.");
            }
        }
        public Role GetRoleForUser(int userid)
        {
           int roleid= userRepository.GetRoleIdForUser(userid);
             IRepository<Role, int> roleRepository = new RoleRepository();
            Role role= roleRepository.FindById(roleid);
            return role;
        }
        public void UpdateRole(int userid,string roleName)
        {
            IRepository<Role, int> roleRepository = new RoleRepository();
            UserRole userRole = new UserRole();
          
            userRole.UserId = userid;
            Role Roles =roleRepository.GetAll().Where(e=>e.RoleName==roleName).FirstOrDefault();
            userRole.RoleId = Roles.RoleId;
            
          
            userRepository.UpdateUserRole(userRole);
        }





    }
}
