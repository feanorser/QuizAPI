using QuizAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;

namespace QuizAPITest
{
    [TestFixture]
    public class TeamsControllerTests
    {
        private const string id1 = "5979B253-34CF-44F0-82DC-2D2C426B4B81";
        private Team teamTest1 = new Team
        {
            Name = "Team test #1",
            Id = new Guid(id1)
        };

        private const string id2 = "5979B253-34CF-44F0-82DC-1D2C426B4B81";
        private Team teamTest2 = new Team
        {
            Name = "Team test #2",
            Id = new Guid(id1)
        };

        private const string BaseUrl = "https://localhost:7038/api/teams";
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
        public async Task PostNewTeam()
        {
            var response = await _httpClient.PostAsJsonAsync("", teamTest1);
            response.EnsureSuccessStatusCode();
            var createdTeam = await response.Content.ReadFromJsonAsync<Team>();
            Assert.That(createdTeam, Is.EqualTo(teamTest1));
        }

        [Test, Order(2)]
        public async Task PostSameTeam()
        {
            var response = await _httpClient.PostAsJsonAsync("", teamTest1);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test, Order(3)]
        public async Task GetTeamById()
        {
            var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{id1}");
            response.EnsureSuccessStatusCode();

            var loadedTeam = await response.Content.ReadFromJsonAsync<Team>();
            Assert.That(loadedTeam, Is.EqualTo(teamTest1));
        }

        [Test, Order(4)]
        public async Task UpdateTeamById()
        {
            teamTest1.Name = "Team test 1";
            var response = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}/{id1}", teamTest1);
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{id1}");
            response.EnsureSuccessStatusCode();
            var loadedTeam = await response.Content.ReadFromJsonAsync<Team>();
            Assert.That(loadedTeam, Is.EqualTo(teamTest1));
        }

        [Test, Order(10)]
        public async Task DeleteTeamById()
        {
            var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/{id1}");
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{id1}");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [TearDown]
        public void Teardown()
        {
            _httpClient?.Dispose();
        }
    }
}
