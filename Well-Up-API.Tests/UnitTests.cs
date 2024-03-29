using Well_Up_API.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using Well_Up_API.Services;
using System.Text.Json;
namespace Well_Up_API.Tests
{
    [TestClass]
    public class MoodTests
    {
        private Mock<DbSet<Mood>> mockSet;
        private Mock<PostgresDbContext> mockContext;
        private MoodService mockService;

        [TestInitialize]
        public void Initialize()
        {
            string text = File.ReadAllText(@"./moods.json");
            var data = JsonSerializer.Deserialize<List<Mood>>(text).AsQueryable();
            foreach (var mood in data)
            {
                mood.Colour = (mood.PositionX >= 5) ? ((mood.PositionY >= 5) ? "green" : "blue") : ((mood.PositionY >= 5) ? "yellow" : "red");
            }


            mockSet = new Mock<DbSet<Mood>>();
            mockSet.As<IQueryable<Mood>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Mood>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Mood>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Mood>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<PostgresDbContext>();
            mockContext.Setup(c => c.Mood).Returns(mockSet.Object);

            mockService = new MoodService(mockContext.Object);
        }

        [TestMethod]
        public void GetAllMoodsReturns2()
        {
            var moods = mockService.GetMoods();
            Assert.AreEqual(100, moods.Count);
        }

        [TestMethod]
        public void GetMoodByGroupInvalidValueReturnsAll()
        {
            var moods = mockService.GetMoodGroup("blah");
            Assert.AreEqual(100, moods.Count);
        }
        [TestMethod]
        public void GetMoodByGroupPositiveReturnsYellowAndGreen(){
            var moods = mockService.GetMoodGroup("positive");
            foreach(var mood in moods){
                Assert.IsTrue(mood.PositionY >= 7 && mood.PositionY <= 9);
                Assert.IsTrue(mood.Colour == "green" || mood.Colour == "yellow");
            }
        }
        [TestMethod] 
        public void GetMoodByGroupNegativeReturnsBlueAndRed(){
            var moods = mockService.GetMoodGroup("negative");
            foreach(var mood in moods){
                Assert.IsTrue(mood.PositionY >= 0 && mood.PositionY <= 2);
                Assert.IsTrue(mood.Colour == "blue" || mood.Colour == "red");
            }
        }
        [TestMethod]
        public void GetMoodByGroupNeutralReturnsCorrectPositions(){
            var moods = mockService.GetMoodGroup("neutral");
            foreach(var mood in moods){
                Assert.IsTrue(mood.PositionY >= 3 && mood.PositionY <= 6);
            }
        }
    }
}