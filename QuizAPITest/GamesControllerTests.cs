using Moq;
using QuizAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuizAPITest
{
    [TestFixture]
    public class GameControllerTests
    {
        [SetUp]
        public void Setup()
        {
            var games = new List<Game> { 
                new Game{Name = "Game #1", GameType = "Normal", Description = "Game #1",
                DateTime = new DateTime(2023,12,12,18,30,0), RoundsCount = 7, Id = new Guid("{5979B253-34CF-44F0-82DC-2D2C426B4B84}"),
                Results = null}
            };
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}