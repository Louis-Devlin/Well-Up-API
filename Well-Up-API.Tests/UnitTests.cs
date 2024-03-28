using Well_Up_API.Models;
namespace Well_Up_API.Tests
{
    [TestClass]
    public class MoodTests
    {
        [TestMethod]
        public void MoodGettersWorkAsExpected()
        {
            Mood m = new Mood() { MoodName = "Happy" };
            Assert.AreEqual(m.MoodName, "Happy");
        }
    }
}