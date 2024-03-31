using Moq;
using Well_Up_API.Services;
using Microsoft.EntityFrameworkCore;
using Well_Up_API.Models;

namespace Well_Up_API.Tests
{
    [TestClass]
    public class UserHabitServiceTests
    {
        private Mock<DbSet<UserHabit>> mockUserHabitSet;
        private Mock<DbSet<HabitLog>> mockHabitLogSet;
        private Mock<DbSet<Habit>> mockHabitSet;
        private Mock<PostgresDbContext> mockContext;
        private UserHabitService service;

        [TestInitialize]
        public void Initialize()
        {
            var userHabitData = new List<UserHabit>
        {
            new UserHabit { UserHabitId = 0, UserId = 1, HabitId = 1, Active = true },
            new UserHabit {UserHabitId=1, UserId = 1, HabitId = 2, Active = true },
            new UserHabit { UserHabitId=2, UserId = 2, HabitId = 1, Active = true }
        }.AsQueryable();

            mockUserHabitSet = new Mock<DbSet<UserHabit>>();
            mockUserHabitSet.As<IQueryable<UserHabit>>().Setup(m => m.Provider).Returns(userHabitData.Provider);
            mockUserHabitSet.As<IQueryable<UserHabit>>().Setup(m => m.Expression).Returns(userHabitData.Expression);
            mockUserHabitSet.As<IQueryable<UserHabit>>().Setup(m => m.ElementType).Returns(userHabitData.ElementType);
            mockUserHabitSet.As<IQueryable<UserHabit>>().Setup(m => m.GetEnumerator()).Returns(userHabitData.GetEnumerator());

            var habitLogList = new List<HabitLog>{
    new HabitLog { UserId = 1, HabitId = 1, Date = DateTime.Now },
    new HabitLog { UserId = 1, HabitId = 1, Date = DateTime.Now },
    new HabitLog { UserId = 1, HabitId = 2, Date = DateTime.Now }
};

            var habitLogData = habitLogList.AsQueryable();

            mockHabitLogSet = new Mock<DbSet<HabitLog>>();
            mockHabitLogSet.As<IQueryable<HabitLog>>().Setup(m => m.Provider).Returns(habitLogData.Provider);
            mockHabitLogSet.As<IQueryable<HabitLog>>().Setup(m => m.Expression).Returns(habitLogData.Expression);
            mockHabitLogSet.As<IQueryable<HabitLog>>().Setup(m => m.ElementType).Returns(habitLogData.ElementType);
            mockHabitLogSet.As<IQueryable<HabitLog>>().Setup(m => m.GetEnumerator()).Returns(habitLogData.GetEnumerator());
            mockHabitLogSet.Setup(m => m.Remove(It.IsAny<HabitLog>())).Callback<HabitLog>((entity) => habitLogList.Remove(entity));
            var habitData = new List<Habit>
            {
                new Habit { HabitId = 1, HabitName = "Habit1" },
                new Habit { HabitId = 2, HabitName = "Habit2" }
            }.AsQueryable();

            mockHabitSet = new Mock<DbSet<Habit>>();
            mockHabitSet.As<IQueryable<Habit>>().Setup(m => m.Provider).Returns(habitData.Provider);
            mockHabitSet.As<IQueryable<Habit>>().Setup(m => m.Expression).Returns(habitData.Expression);
            mockHabitSet.As<IQueryable<Habit>>().Setup(m => m.ElementType).Returns(habitData.ElementType);
            mockHabitSet.As<IQueryable<Habit>>().Setup(m => m.GetEnumerator()).Returns(habitData.GetEnumerator());


            mockContext = new Mock<PostgresDbContext>();
            mockContext.Setup(c => c.UserHabit).Returns(mockUserHabitSet.Object);
            mockContext.Setup(c => c.HabitLog).Returns(mockHabitLogSet.Object);
            mockContext.Setup(c => c.Habit).Returns(mockHabitSet.Object);

            service = new UserHabitService(mockContext.Object);
        }
        [TestMethod]
        public void GetUserHabitsByDateTest()
        {
            var result = service.GetUserHabitsByDate(1, DateTime.Now, true);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]

        public void StartTrackingHabitTest()
        {
            var result = service.StartTrackingHabit(new UserHabitRequest { UserId = 1, HabitId = -1, HabitName = "NewHabit" });
            Assert.IsTrue(result != -1);
        }
    }
}