using Moq;

using Well_Up_API.Services;
using Microsoft.EntityFrameworkCore;
using Well_Up_API.Models;
namespace Well_Up_API.Tests
{
    [TestClass]
    public class HabitLogServiceTests
    {
        private Mock<DbSet<HabitLog>> mockSet;
        private Mock<PostgresDbContext> mockContext;
        private HabitLogService mockService;
        [TestInitialize]
        public void Initialize()
        {
            var data = new List<HabitLog>(){
                new HabitLog(){HabitLogId = 1, HabitId = 1, UserId = 1, Date = new DateTime(2024,3,29)},
                new HabitLog(){HabitLogId = 2, HabitId = 2, UserId = 1, Date = new DateTime(2024,3,28)}
            }.AsQueryable();


            mockSet = new Mock<DbSet<HabitLog>>();
            mockSet.As<IQueryable<HabitLog>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<HabitLog>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<HabitLog>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<HabitLog>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<PostgresDbContext>();
            mockContext.Setup(c => c.HabitLog).Returns(mockSet.Object);

            mockService = new HabitLogService(mockContext.Object);
        }

        [TestMethod]
        public void GetLoggedHabitsReturns2()
        {
            var habits = mockService.GetLoggedHabits(1);
            Assert.AreEqual(2, habits.Count);

        }
        [TestMethod]
        public void LogHabitReturnsCorrectId()
        {
            var habit = new HabitLog() { HabitId = 1, UserId = 1, Date = DateTime.Now };
            var id = mockService.LogHabit(habit);
            Assert.IsTrue(id is int);
        }
    }
}