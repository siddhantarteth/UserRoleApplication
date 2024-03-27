using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using UserRoleAppp.Process;
using UserRoleApp.Repository;

namespace UserRoleApp
{

    public class Program
    {

        static void Main(string[] args)
        {
            int choice = 0;
            do
            {
                Console.Clear();
                choice = DisplayMainOptions();
                if (choice == 1)
                {
                    int uchoice = 0;
                    do
                    {
                       Console.Clear();
                        uchoice = DisplayUserOptions(); ;
                        if (uchoice == 1) { ListAllUsers(); }
                        else if (uchoice == 2) { FindUserbyId(); }
                        else if (uchoice == 3) { AddNewUser(); }
                        else if (uchoice == 4) { UpdateUser(); }
                        else if (uchoice == 5) { RemoveUser(); }
                        Console.WriteLine("Press any key to show options");
                        Console.ReadKey();
                    }
                    while (uchoice != 0);
                }
               else if (choice == 2)
                {
                    int rchoice = 0;
                    do
                    {
                        Console.Clear();
                        rchoice = DisplayRoleOptions(); ;
                        if (rchoice == 1) { ListAllRoles(); }
                        else if (rchoice == 2) { FindRolebyId(); }
                        else if (rchoice == 3) { AddNewRole(); }
                        else if (rchoice == 4) { UpdateRole(); }
                        else if (rchoice == 5) { RemoveRole(); }
                        Console.WriteLine("Press any key to show options");
                        Console.ReadKey();
                    }
                    while (rchoice != 0);
                }
                else if(choice ==3)
                {
                    DisplayUserRoleOptions();
                    Console.ReadKey();
                }

            }

            while (choice != 0);
        }
        /// User method definitions      
        static void ListAllUsers()
        {
            Console.WriteLine("*****List of Users * *****");
            UserProcess up = new UserProcess();

            var users = up.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"User ID: {user.UserId}, Username: {user.UserName}, First Name: {user.FirstName}, Last Name: {user.LastName}");
            }

        }
        static void FindUserbyId()
        {
            Console.WriteLine("Enter Id to search the user : ");
            int Userid=int.Parse(Console.ReadLine());
            UserProcess up=new UserProcess();
            try
            {
                var user = up.GetUserById(Userid);
                if (user != null)
                {
                    Console.WriteLine("Username : {0}, FirstName: {1},Last Name:{2}", user.UserName, user.FirstName, user.LastName);
                }
                else { Console.WriteLine("Usernotexist"); }
             }
            catch (Exception e) { Console.WriteLine(e.Message); }
            
         }
        static void AddNewUser()
        {
            Console.WriteLine("Enter Username: ");
            string Username=Console.ReadLine();
            Console.WriteLine("Enter FirstName");
            string FirstName=Console.ReadLine();
            Console.WriteLine("Enter Lastname");
            string LastName=Console.ReadLine();
            Console.WriteLine("Enter Password");
            string Password=Console.ReadLine();
            UserProcess up=    new UserProcess();
            up.AddUser(Username, LastName, FirstName,Password);
            Console.WriteLine("User Added");

        }
        static void UpdateUser()
        {
            Console.WriteLine("Enter User Id to be updated:");
            int userid=int.Parse(Console.ReadLine());
            UserProcess up= new UserProcess();
            var check=up.GetUserById(userid);
            if (check != null)
            {
                Console.WriteLine("Enter Firstname ({0})",check.FirstName);
                string firstname = Console.ReadLine();
                
                Console.WriteLine("Enter LastName {0}",check.LastName);
                string lastname = Console.ReadLine();

                up.UpdateUser(userid,firstname,lastname);
            }
            else
            {
                Console.WriteLine("User Not Found");
            }
        }
        static void RemoveUser()
        {
            Console.WriteLine("Enter User Id to be Removed");
            int Userid=int.Parse(Console.ReadLine());
            UserProcess up=new UserProcess();
            up.RemoveUser(Userid);
            Console.WriteLine("User Removed");
        }
        /// Role Method Definition

        static void ListAllRoles()
        {
            Console.WriteLine("*****List of Roles * *****");
            RoleProcess rp = new RoleProcess();

            var roles = rp.GetAllRoles();
            foreach (var role in roles)
            {
                Console.WriteLine($"Role ID: {role.RoleId}, Rolename: {role.RoleName}, RollDescription: {role.RoleDescription}");
            }

        }
        static void FindRolebyId()
        {
            Console.WriteLine("Enter Id to search the role : ");
            int Roleid = int.Parse(Console.ReadLine());
            RoleProcess rp = new RoleProcess();
            try
            {
                var role = rp.GetRoleById(Roleid);
                if (role != null)
                {
                    Console.WriteLine("RoleName: {0}, RoleDescription: {1}", role.RoleName,role.RoleDescription);
                }
                else { Console.WriteLine("Role "); }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

        }
        static void AddNewRole()
        {
            Console.WriteLine("Enter RoleName: ");
            string RoleName = Console.ReadLine();
            Console.WriteLine("Enter Role Description");
            string RoleDescription = Console.ReadLine();
            
            RoleProcess rp = new RoleProcess();
            rp.AddRole(RoleName,RoleDescription);
            Console.WriteLine("Role Added");

        }
        static void UpdateRole()
        {
            Console.WriteLine("Enter Role Id to be updated:");
            int roleid = int.Parse(Console.ReadLine());
            RoleProcess rp = new RoleProcess();
            var check = rp.GetRoleById(roleid);
            if (check != null)
            {
               

                Console.WriteLine("Enter RoleDescription {0}", check.RoleDescription);
                string RoleDescription = Console.ReadLine();

                rp.UpdateRole(RoleDescription,roleid);
            }
            else
            {
                Console.WriteLine("User Not Found");
            }
        }
        static void RemoveRole()
        {
            Console.WriteLine("Enter Role Id to be Removed");
            int Roleid = int.Parse(Console.ReadLine());
            RoleProcess rp = new RoleProcess();
            rp.RemoveRole(Roleid);
            Console.WriteLine("Role Removed");
        }
        //Menu
        static void DisplayUserRoleOptions()
        {
            Console.WriteLine("*****Login Management System ******");
            Console.WriteLine("***** Manage User Roles ******");
            Console.WriteLine("Enter User Id");
            int userid=int.Parse(Console.ReadLine());
            RoleProcess rp = new RoleProcess();

            var roles = rp.GetAllRoles();
            foreach(var role in roles)
            {
                Console.Write("| {0} |",role.RoleName);
            }
            Console.WriteLine();
            Console.WriteLine("Enter Role name");
            string rolename=Console.ReadLine(); 
            UserProcess up=new UserProcess();
            up.UpdateRole(userid, rolename);
            Console.WriteLine("Role Updated");

        }
        static int DisplayUserOptions()
        {
            Console.WriteLine("***** Login Management System ******");
            Console.WriteLine("***** Manage Users ******");
            Console.WriteLine("*** 1. List all Users");
            Console.WriteLine("*** 2. Find User By Id");
            Console.WriteLine("*** 3. Add new User");
            Console.WriteLine("*** 4. Update User Details");
            Console.WriteLine("*** 5. Remove User");
            Console.WriteLine("*** 0. Back to Main Menu");
            Console.WriteLine("*******************");
            Console.Write("\nYourChoice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice >= 0 && choice < 6) return choice;
            }
            else
            {
                Console.WriteLine("Invalid Choice.");
            }
            return choice;

        }
        static int DisplayRoleOptions()
        {
            Console.WriteLine("***** Login Management System ******");
            Console.WriteLine("***** Manage Roles ******");
            Console.WriteLine("*** 1. List all Roles");
            Console.WriteLine("*** 2. Find Roles By Id");
            Console.WriteLine("*** 3. Add new Roles");
            Console.WriteLine("*** 4. Update Roles Details");
            Console.WriteLine("*** 5. Remove Roles");
            Console.WriteLine("*** 0. Back to Main Menu");
            Console.WriteLine("*******************");
            Console.Write("\nYourChoice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice >= 0 && choice < 6) return choice;
            }
            else
            {
                Console.WriteLine("Invalid Choice.");
            }
            return choice;

        }
        static int DisplayMainOptions()
        {
            Console.WriteLine("Screen 1");

            Console.WriteLine("*****Login Management System * *****");
            Console.WriteLine("*** 1. Manage Users");
            Console.WriteLine("*** 2. Manage Roles");
            Console.WriteLine("*** 3. Manage User Roles");
            Console.WriteLine("******* 0. Quit ");
            Console.WriteLine("***********************************");
            Console.Write("\nYourChoice: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice >= 0 && choice < 4) return choice;
            }
            else
            {
                Console.WriteLine("Invalid Choice.");
            }
            return choice;
        }
    }
}

/*
The UI Screens 
The startup screens will be as follows: 
Screen 1: 
*****Login Management System ****** 
*** 1. Manage Users 
*** 2. Manage Roles 
*** 3. Manage User Roles 
***
***
*** 0. Quit
*******************
Your Choice: 

Screen 2: When client selects choice 1: 
*****Login Management System ****** 
***** Manage Users ****** 
*** 1. List all Users
*** 2. Find User By Id
*** 3. Add new User
*** 4.Update User Details
*** 5. Remove User  			///NOTE THAT WHEN REMOVING USERS, The userRoles should also be removed.
***
***
*** 0. Back to Main Menu
*******************
Enter Choice: 

Screen 3: When client selects choice 2: 
//SIMILAR Screen list Screen 2 


Screen 4: When client select choice 3: 
*****Login Management System ****** 
***** Manage User Roles ****** 

Enter User Id: __
Roles available: [ "Role1", "Role2"...]
Enter Role Name: __

Save this mapping? Y/n : 
//Find the role Id based on the selected rolename and 
//save the mapping if the client selects Y. 
else discard and go back to main menu.
*/