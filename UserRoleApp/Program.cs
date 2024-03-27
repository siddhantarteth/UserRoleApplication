using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using UserRoleAppp.Process;

namespace UserRoleApp
{
    
    public class Program
    {

        static void Main(string[] args)
        { int choice=0;
            do
            {
                Console.Clear();
                choice=DisplayMainOptions();
                if (choice == 1)
                {
                    int uchoice = 0;
                    do
                    {
                        Console.Clear();
                        uchoice = DisplayUserOptions(); ;
                        if (uchoice == 1) { ListAllUsers(); }
                        //else if (uchoice == 2) { FindUserById(); }
                        //else if (uchoice == 3) { AddNewUser(); }
                        //else if (uchoice == 4) { UpdateUser(); }
                        //else if (uchoice == 5) { RemoveUser(); }
                    }
                    while (uchoice != 0);
                }
                if (choice == 2)
                {
                    int rchoice = 0;
                    do
                    {
                        Console.Clear();
                        rchoice = DisplayRoleOptions(); ;
                        //if (rchoice == 1) { ListAllRoles(); }
                        //else if (rchoice == 2) { FindRoleById(); }
                        //else if (rchoice == 3) { AddNewRole(); }
                        //else if (rchoice == 4) { UpdateRole(); }
                        //else if (rchoice == 5) { RemoveRole(); }
                    }
                    while (rchoice != 0);
                }

            }

            while (choice != 0);
        }  
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