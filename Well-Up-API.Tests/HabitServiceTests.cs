using Well_Up_API.Models;
using Well_Up_API.Services;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace Well_Up_API.Tests
{
    [TestClass]
    public class HabitServiceTests
    {
        private HabitService mockService;
        private Mock<PostgresDbContext> mockContext;
        private Mock<DbSet<Habit>> mockSet;

        [TestInitialize]
        public void Initialize()
        {
            var data = new List<Habit>() {
                new Habit() {HabitId = 0, HabitName = "Go for a run"}
                , new Habit(){HabitId = 1, HabitName = "Read a book"}
            }.AsQueryable();

            mockSet = new Mock<DbSet<Habit>>();
            mockSet.As<IQueryable<Habit>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Habit>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Habit>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Habit>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<PostgresDbContext>();
            mockContext.Setup(c => c.Habit).Returns(mockSet.Object);

            mockService = new HabitService(mockContext.Object);
        }
        [TestMethod]
        public void GetHabitsReturns2()
        {
            var habits = mockService.GetAllHabits();
            Assert.AreEqual(2, habits.Count);
            Assert.AreEqual("Go for a run", habits[0].HabitName);
        }
        [TestMethod]
        public void CreateHabitReturnsCorrectly()
        {
            var habit = new Habit() { HabitId = 2, HabitName = "Meditate" };
            var id = mockService.CreateHabit(habit);
            Assert.AreEqual(2, id);
        }   
    }
}