using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LaserTagTrackerApi.Database;
using LaserTagTrackerApi.Model;
using LaserTagTrackerApi.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LaserTagTrackerApi.Tests
{
    public class IntegrationTests : IClassFixture<LaserTagTrackerApiWebApplicationFactory>
    {
        private readonly HttpClient client;
        private readonly ApplicationDbContext db;

        public IntegrationTests(LaserTagTrackerApiWebApplicationFactory factory)
        {
            this.client = factory.CreateClient();
            this.db = factory.Context;
            db.Database.EnsureDeleted();
        }

        [Fact]
        public async Task<Guid> Register()
        {
            var registerDto = new CredentialsDto()
            {
                Username = "John Shepard",
                Password = "Thali<3"
            };
            var registerResponse = await client.PostAsJsonAsync("api/auth/register", registerDto);
            Assert.True(registerResponse.IsSuccessStatusCode);
            var result = await registerResponse.Content.ReadAsAsync<RegisteredUserDto>();

            Assert.Equal("John Shepard", result.Username);
            Assert.NotNull(result.Id.ToString());

            return result.Id;
        }

        [Fact]
        public async Task<string> Auth()
        {
            // Given
            await Register();
            var loginDto = new CredentialsDto()
            {
                Username = "John Shepard",
                Password = "Thali<3"
            };

            // When
            var loginResponse = await client.PostAsJsonAsync("api/auth/login", loginDto);

            // Then
            Assert.True(loginResponse.IsSuccessStatusCode);
            var result = await loginResponse.Content.ReadAsAsync<AuthSuccessDto>();
            Assert.NotNull(result.Token);
            Assert.Equal("John Shepard", result.Username);
            return result.Token;
        }

        [Fact]
        public async void CreateMatch()
        {
            // Given
            var token = await Auth();
            var today = DateTime.Now;
            var matchDto = new CreateMatchDto()
            {
                PlayerName = "John Shepard",
                Rank = 12,
                Score = 22,
                Precision = 32,
                TeamScore = 42,
                Date = today,
                ChestGiven = 53,
                BackGiven = 64,
                ShouldersGiven = 47,
                GunGiven = 48,
                ChestReceived = 49,
                BackReceived = 10,
                ShouldersReceived = 11,
                GunReceived = 12,
                Address = "Shepard Address",
                Latitude = 14,
                Longitude = 14
            };
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // When
            var registerResponse = await client.PostAsJsonAsync("api/matches", matchDto);
            Assert.True(registerResponse.IsSuccessStatusCode);
            var result = await registerResponse.Content.ReadAsAsync<Match>();

            // Then
            Assert.Equal("John Shepard", result.PlayerName);
            Assert.NotNull(result.Id.ToString());

            Assert.Equal(12, result.Rank);
            Assert.Equal(22, result.Score);
            Assert.Equal(32, result.Precision);
            Assert.Equal(42, result.TeamScore);
            Assert.Equal(today, result.Date);
            Assert.Equal(53, result.ChestGiven);
            Assert.Equal(64, result.BackGiven);
            Assert.Equal(47, result.ShouldersGiven);
            Assert.Equal(48, result.GunGiven);
            Assert.Equal(49, result.ChestReceived);
            Assert.Equal(10, result.BackReceived);
        }
        
        [Fact]
        public async void GetMatches()
        {
            // Given
            var token = await Auth();
            var today = DateTime.Now;
            var match = new Match()
            {
                PlayerName = "John Shepard",
                Rank = 12,
                Score = 22,
                Precision = 32,
                TeamScore = 42,
                Date = today,
                ChestGiven = 53,
                BackGiven = 64,
                ShouldersGiven = 47,
                GunGiven = 48,
                ChestReceived = 49,
                BackReceived = 10,
                ShouldersReceived = 11,
                GunReceived = 12,
                Address = "Shepard Address",
                Latitude = 14,
                Longitude = 14
            };
            var user = await db.Users.FirstAsync();
            user.Matches.Add(match);
            db.Users.Update(user);
            await db.SaveChangesAsync();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // When
            var registerResponse = await client.GetAsync("api/matches");
            Assert.True(registerResponse.IsSuccessStatusCode);
            var result = await registerResponse.Content.ReadAsAsync<List<Match>>();

            // Then
            Assert.Single(result);
            var matchResult = result[0];
            
            Assert.Equal("John Shepard", matchResult.PlayerName);
            Assert.NotNull(matchResult.Id.ToString());

            Assert.Equal(12, matchResult.Rank);
            Assert.Equal(22, matchResult.Score);
            Assert.Equal(32, matchResult.Precision);
            Assert.Equal(42, matchResult.TeamScore);
            Assert.Equal(today, matchResult.Date);
            Assert.Equal(53, matchResult.ChestGiven);
            Assert.Equal(64, matchResult.BackGiven);
            Assert.Equal(47, matchResult.ShouldersGiven);
            Assert.Equal(48, matchResult.GunGiven);
            Assert.Equal(49, matchResult.ChestReceived);
            Assert.Equal(10, matchResult.BackReceived);
        }
    }
}