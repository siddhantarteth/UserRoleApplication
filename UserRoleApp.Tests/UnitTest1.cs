using Microsoft.EntityFrameworkCore;
using UserRoleApp.Repository;

namespace UserRoleApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
      static   IRepository<User, int> repository = new UserRepository();

        [TestMethod]
        public void TestFor_AddNewUser_ValidValues()
        {
            // Arrange
            // Assuming the user does not exist in the database
            int prevCount = repository.GetAll().Count();

            User user = new User
            {
                UserName = "Test4",
                Password = "password",
                FirstName = "Unit",
                LastName = "Test",
                IsActive = true
            };

            // Act
            repository.Upsert(user);

            // Assert
            int newCount = repository.GetAll().Count();
            Assert.AreEqual(prevCount + 1, newCount);

        }
        [TestMethod]
        [ExpectedException (typeof(DbUpdateException))]
        public void TestFor_AddNewUser_InvalidValues()
        {
            int prevCount = repository.GetAll().Count();

            User user = new User
            {
                UserName = "Test4",
               // Password = "password",
                FirstName = "Unit",
                LastName = "Test",
                IsActive = true
            };

            // Act
            repository.Upsert(user);

            // Assert
            int newCount = repository.GetAll().Count();
            Assert.AreEqual(prevCount + 1, newCount);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void TestFor_AddNewUser_DuplicateValues()
        {
            IRepository<User, int> repository = new UserRepository();

            User user = new User
            {
                UserName = "Test",
                Password = "password",
                FirstName = "Unit",
                LastName = "Test",
                IsActive = true
            };
            repository.Upsert(user);
        }
        [TestMethod]
        public void TestFor_RemoveUser()
        {
            User user = new User
            {
                UserName = "Test12", // Assuming this is the username of the user to be removed
                Password = "password",
                FirstName = "Unit",
                LastName = "Test",
                IsActive = true
            };

            // Add the user to the database
            repository.Upsert(user);
            repository.Remove(user.UserId);

           var check= repository.FindById(user.UserId);
            Assert.AreEqual(check, null);

            // Act

            // Assert
        }
        [TestMethod]
        public void Test_For_FindById()
        {
            IRepository<User, int> repository = new UserRepository();
            User user = new User()
            {
                UserName = "Test123",
                Password = "Pas",
                FirstName = "testing",
                LastName = "check id",
                IsActive = true
            };
            repository.Upsert(user);
            User check = repository.FindById(user.UserId);
            Assert.AreEqual(user.Password, check.Password);
            Assert.AreEqual(user.FirstName, check.FirstName);
        }
        [TestMethod]
        public void Test_For_FindById_Invalid()
        {
            IRepository<User, int> repository = new UserRepository();

            User check = repository.FindById(-22);
            Assert.IsNull(check);
        }
        [TestMethod]
        public void Test_For_GetAll()
        {
            IRepository<User, int> repository = new UserRepository();
            int prevcount = repository.GetAll().Count();
            User user = new User()
            {
                UserName = "TestAllNew",
                Password = "Password",
                FirstName = "TestAllNew",
                LastName = "check",
                IsActive = true
            };
            repository.Upsert(user);

            int currcount = repository.GetAll().Count();
            Assert.AreEqual(prevcount + 1, currcount);
        }

    }
}