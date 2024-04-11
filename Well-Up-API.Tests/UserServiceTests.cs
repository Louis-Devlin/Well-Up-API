using Moq;
using Well_Up_API.Services;
using Microsoft.EntityFrameworkCore;
using Well_Up_API.Models;

namespace Well_Up_API.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<DbSet<User>> mockUserSet;
        private Mock<PostgresDbContext> mockContext;
        private UserService service;

        [TestInitialize]
        public void Initialize()
        {
            var userData = new List<User>{
                new User { UserId = 1, Email = "User1@test.com", Password = "1wvPyLZgis8vPXRv63Ayow==" },
                new User { UserId = 2, Email = "User2@test.com", Password = "su0nP2m2ftyFtLBvfjn80g==" }
            }.AsQueryable();
            mockUserSet = new Mock<DbSet<User>>();
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator());

            mockContext = new Mock<PostgresDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockUserSet.Object);

            service = new UserService(mockContext.Object);
        }
        [TestMethod]
        public void Register_ExistingUser_ReturnsMinusOne()
        {
            User user = new User() { Email = "User1@test.com", Password = "blahblahblah" };
            Assert.AreEqual(-1, service.Register(user));
        }
        [TestMethod]
        public void Register_NewUser_ReturnsUserId()
        {
            User user = new User() { Email = "User3@test.com", Password = "testesttest" };
            Assert.IsTrue(service.Register(user) != -1);
        }

        [TestMethod]
        public void Login_ExistingUser_ReturnsUser()
        {
            User user = new User() { Email = "User1@test.com", Password = "Password1" };
            Assert.IsNotNull(service.Login(user));
        }
        [TestMethod]
        public void Login_NonExistingUser_ReturnsNull()
        {
            User user = new User() { Email = "none@blah.com", Password = "Invalid" };
            Assert.IsNull(service.Login(user));
        }

        [TestMethod]
        public void Update_ExistingUser_ReturnsTrue()
        {
            UserRequest user = new UserRequest() { Name = "NewName", Password = "Password1", NewPassword = "NewPassword" };
            Assert.IsTrue(service.Update(1, user));
        }
        [TestMethod]
        public void Update_NonExistingUser_ReturnsFalse()
        {
            UserRequest user = new UserRequest() { Name = "NewName", Password = "Password1", NewPassword = "NewPassword" };
            Assert.IsFalse(service.Update(3, user));
        }
        [TestMethod]
        public void Delete_ExistingUser_ReturnsTrue()
        {
            Assert.IsTrue(service.Delete(1));
        }
        [TestMethod]
        public void Delete_NonExistingUser_ReturnsFalse()
        {
            Assert.IsFalse(service.Delete(3));
        }
    }
}