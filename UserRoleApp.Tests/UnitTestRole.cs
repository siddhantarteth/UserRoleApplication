using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoleApp.Repository;

namespace UserRoleApp.Tests
{
    [TestClass]

    public class UnitTestRole
    {
        static IRepository<Role, int> repository = new RoleRepository();

        [TestMethod]
        public void TestFor_AddNewRole_ValidValues()
        {
            // Arrange
            // Assuming the user does not exist in the database
            int prevCount = repository.GetAll().Count();

            Role role = new Role
            {
                RoleDescription = "this is a test Test",
                RoleName = "Test1234",
                
            };

            // Act
            repository.Upsert(role);

            // Assert
            int newCount = repository.GetAll().Count();
            Assert.AreEqual(prevCount, newCount);

        }
        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void TestFor_AddNewRole_InvalidValues()
        {
            int prevCount = repository.GetAll().Count();

            Role role = new Role
            {
                RoleDescription = "this is a test Test",
               // RoleName = "Test",
            };

            // Act
            repository.Upsert(role);

            // Assert
            int newCount = repository.GetAll().Count();
            Assert.AreEqual(prevCount , newCount);
        }
        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void TestFor_AddNewRole_DuplicateValues()
        {
            IRepository<Role, int> repository = new RoleRepository();

            Role role = new Role
            {
                RoleDescription = "this is a test Test",
                RoleName = "Test",
            };
            repository.Upsert(role);
        }
        [TestMethod]
        public void TestFor_RemoveUser()
        {
            Role role = new Role
            {
                RoleDescription = "this is a test Test",
                RoleName = "Test1000001",
            };
            // Add the user to the database
            repository.Upsert(role);
            repository.Remove(role.RoleId);

            var check = repository.FindById(role.RoleId);
            Assert.AreEqual(check, null);

            // Act

            // Assert
        }
        [TestMethod]
        public void Test_For_FindById()
        {
            IRepository<Role, int> repository = new RoleRepository();

            Role role = new Role
            {
                RoleDescription = "this is a test Test for it",
                RoleName = "Manager12",
                IsActive = true
            };
            repository.Upsert(role);
            Role check = repository.FindById(role.RoleId);
            Assert.AreEqual(role.RoleDescription, check.RoleDescription);
        }
        [TestMethod]
        public void Test_For_FindById_Invalid()
        {
            IRepository<Role, int> repository = new RoleRepository();

            Role check = repository.FindById(-22);
            Assert.IsNull(check);
        }
        [TestMethod]
        public void Test_For_GetAll()
        {
            IRepository<Role, int> repository = new RoleRepository();
            int prevcount = repository.GetAll().Count();
            Role role = new Role()
            {
                RoleDescription = "this is a test Test for it",
                RoleName = "Manager123",
                IsActive = true
            };
            repository.Upsert(role);

            int currcount = repository.GetAll().Count();
            Assert.AreEqual(prevcount + 1, currcount);
        }

    }
}
