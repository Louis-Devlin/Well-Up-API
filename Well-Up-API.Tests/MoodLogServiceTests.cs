using Moq;
using Well_Up_API.Services;
using Microsoft.EntityFrameworkCore;
using Well_Up_API.Models;
using System.Text.Json;
namespace Well_Up_API.Tests
{
    [TestClass]
    public class MoodLogServiceTests
    {
        private Mock<DbSet<Mood>> mockMoodSet;
        private Mock<DbSet<MoodLog>> mockMoodLogSet;
        private Mock<PostgresDbContext> mockContext;
        private MoodLogService mockService;

        [TestInitialize]
        public void Initialize()
        {
            var data = new List<MoodLog>(){
                new MoodLog(){MoodLogId = 1, MoodId = 1, UserId = 1, Date = new DateTime(2024,3,29)},
                new MoodLog(){MoodLogId = 2, MoodId = 2, UserId = 1, Date = new DateTime(2024,3,28)},
                new MoodLog(){MoodLogId = 3, MoodId = 1, UserId = 2, Date = new DateTime(2024,3,29)}
            }.AsQueryable();

            mockMoodLogSet = new Mock<DbSet<MoodLog>>();
            mockMoodLogSet.As<IQueryable<MoodLog>>().Setup(m => m.Provider).Returns(data.Provider);
            mockMoodLogSet.As<IQueryable<MoodLog>>().Setup(m => m.Expression).Returns(data.Expression);
            mockMoodLogSet.As<IQueryable<MoodLog>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockMoodLogSet.As<IQueryable<MoodLog>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var moodData = new List<Mood>(){
                new Mood(){MoodId = 1, MoodName = "Happy", PositionX = 1, PositionY = 1},
                new Mood(){MoodId = 2, MoodName = "Sad", PositionX = 2, PositionY = 2}
            }.AsQueryable();

            mockMoodSet = new Mock<DbSet<Mood>>();
            mockMoodSet.As<IQueryable<Mood>>().Setup(m => m.Provider).Returns(moodData.Provider);
            mockMoodSet.As<IQueryable<Mood>>().Setup(m => m.Expression).Returns(moodData.Expression);
            mockMoodSet.As<IQueryable<Mood>>().Setup(m => m.ElementType).Returns(moodData.ElementType);
            mockMoodSet.As<IQueryable<Mood>>().Setup(m => m.GetEnumerator()).Returns(() => moodData.GetEnumerator());

            mockContext = new Mock<PostgresDbContext>();
            mockContext.Setup(c => c.Mood).Returns(mockMoodSet.Object);
            mockContext.Setup(c => c.MoodLog).Returns(mockMoodLogSet.Object);


            mockService = new MoodLogService(mockContext.Object);
        }

        [TestMethod]
        public void LogMoodReturnsCorrectId()
        {
            var mood = new MoodLog() { MoodId = 1, UserId = 1, Date = DateTime.Now };
            var id = mockService.LogMood(mood);
            Assert.AreEqual(1, id);
        }
        [TestMethod]
        public void GetMoodLogReturnsCorrectValue()
        {
            var moodLog = mockService.Get(1);
            Assert.AreEqual(1, moodLog.MoodLogId);
            Assert.AreEqual(1, moodLog.MoodId);
            Assert.AreEqual(1, moodLog.UserId);
            Assert.AreEqual(new DateTime(2024, 3, 29), moodLog.Date);
        }
        [TestMethod]
        public void GetByUserReturnsCorrectValues()
        {
            var habitData = new List<HabitLog>().AsQueryable();
            var mockHabitSet = new Mock<DbSet<HabitLog>>();

            mockHabitSet.As<IQueryable<HabitLog>>().Setup(m => m.Provider).Returns(habitData.Provider);
            mockHabitSet.As<IQueryable<HabitLog>>().Setup(m => m.Expression).Returns(habitData.Expression);
            mockHabitSet.As<IQueryable<HabitLog>>().Setup(m => m.ElementType).Returns(habitData.ElementType);
            mockHabitSet.As<IQueryable<HabitLog>>().Setup(m => m.GetEnumerator()).Returns(habitData.GetEnumerator());

            mockContext.Setup(c => c.HabitLog).Returns(mockHabitSet.Object);




            var userLog = mockService.GetByUser(1);
            Assert.AreEqual(2, userLog.Count);
            Assert.AreEqual(new DateTime(2024, 3, 29), userLog[0].Date);
        }
        [TestMethod]
        public void GetWeeklyTotals_ShouldReturnDictionary()
        {

            var result = mockService.GetWeeklyTotals(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Dictionary<string, int>));
            Assert.AreEqual(4, result.Count); // Assuming there are 4 colors: red, yellow, blue, green
        }

        [TestMethod]
        public void GetTotalsByDay_ShouldReturnListOfMoodLogCountResponse()
        {

            var userId = 1;
            var date = new DateTime(2024, 3, 28);


            var result = mockService.GetTotalsByDay(userId, date);


            Assert.IsInstanceOfType(result, typeof(List<MoodLogCountResponse>));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Sad", result[0].MoodName);
            Assert.AreEqual("red", result[0].Colour);
            
        }
    }

}