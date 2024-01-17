using QuizAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;

namespace QuizAPITest
{
    [TestFixture]
    public class GameControllerTests
    {
        private const string id = "5979B253-34CF-44F0-82DC-2D2C426B4B84";
        private Game gameTest = new Game { Name = "Game #1", GameType = "Normal", Description = "Game #1",
        DateTime = new DateTime(2023, 12, 12, 18, 30, 0).ToUniversalTime(), RoundsCount = 7, Id = new Guid(id),
        Results = null };
        private const string BaseUrl = "https://localhost:7038/api/games"; 
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        [Test, Order(1)]
        public async Task PostNewGame()
        {
            var response = await _httpClient.PostAsJsonAsync("", gameTest);
            response.EnsureSuccessStatusCode();

            // Assert
            var createdGame = await response.Content.ReadFromJsonAsync<Game>();
            Assert.That(createdGame, Is.EqualTo(gameTest));

        }

        [Test, Order(2)]
        public async Task GetGameById()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{id.ToLower()}");
            response.EnsureSuccessStatusCode();

            // Assert
            var loadedGame = await response.Content.ReadFromJsonAsync<Game>();
            Assert.That(loadedGame, Is.EqualTo(gameTest));
        }

        [Test, Order(3)]
        public async Task DeleteGameById()
        {
            var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/{id.ToLower()}");
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [TearDown]
        public void Teardown()
        {
            _httpClient?.Dispose();
        }
    }
}