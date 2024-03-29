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
        private Mock<DbSet<MoodLog>> mockSet;
        private Mock<PostgresDbContext> mockContext;
        private MoodLogService mockService;

        [TestInitialize]
        public void Initialize()
        {
            //Populate moods 
            var moods = new List<Mood>(){
                new Mood(){MoodId = 1, MoodName = "Happy"},
                new Mood(){MoodId = 2, MoodName = "Sad"}
            }.AsQueryable();
            var data = new List<MoodLog>(){
                new MoodLog(){MoodLogId = 1, MoodId = 1, UserId = 1, Date = new DateTime(2024,3,29)},
                new MoodLog(){MoodLogId = 2, MoodId = 2, UserId = 1, Date = new DateTime(2024,3,28)},
                new MoodLog(){MoodLogId = 3, MoodId = 1, UserId = 2, Date = new DateTime(2024,3,29)}
            }.AsQueryable();

            mockSet = new Mock<DbSet<MoodLog>>();
            mockSet.As<IQueryable<MoodLog>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<MoodLog>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<MoodLog>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<MoodLog>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<PostgresDbContext>();
            mockContext.Setup(c => c.MoodLog).Returns(mockSet.Object);

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

            // For Mood
            var moodData = new List<Mood>().AsQueryable();
            var mockMoodSet = new Mock<DbSet<Mood>>();

            mockMoodSet.As<IQueryable<Mood>>().Setup(m => m.Provider).Returns(moodData.Provider);
            mockMoodSet.As<IQueryable<Mood>>().Setup(m => m.Expression).Returns(moodData.Expression);
            mockMoodSet.As<IQueryable<Mood>>().Setup(m => m.ElementType).Returns(moodData.ElementType);
            mockMoodSet.As<IQueryable<Mood>>().Setup(m => m.GetEnumerator()).Returns(moodData.GetEnumerator());

            mockContext.Setup(c => c.Mood).Returns(mockMoodSet.Object);

            var userLog = mockService.GetByUser(1);
            Assert.AreEqual(2, userLog.Count);
            Assert.AreEqual(new DateTime(2024, 3, 29), userLog[0].Date);
        }
    }

}