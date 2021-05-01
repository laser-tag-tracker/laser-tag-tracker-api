using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LaserTagTrackerApi.Database;
using LaserTagTrackerApi.Model.DTOs;
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
            factory.Context.Database.EnsureDeleted();
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
        
        // [Fact]
        // public async Task<string> Auth()
        // {
        //     // Given
        //     var loginDto = new CredentialsDto()
        //     {
        //         Username = "John Shepard",
        //         Password = "Thali<3"
        //     };
        //
        //     // When
        //     var loginResponse = await client.PostAsJsonAsync("api/auth/login", loginDto);
        //
        //     // Then
        //     Assert.True(loginResponse.IsSuccessStatusCode);
        //     var result = await loginResponse.Content.ReadAsAsync<AuthSuccessDto>();
        //     Assert.NotNull(result.Token);
        //     return result.Token;
        // }
        //
        // [Fact]
        // public async void CreateMatch()
        // {
        //     // Given
        //     var token = await Auth();
        //     
        //     var matchDto = new CreateMatchDto()
        //     {
        //         // Set values
        //     };
        //     client.DefaultRequestHeaders.Authorization =
        //         new AuthenticationHeaderValue("Bearer", token);
        //     
        //     // When
        //     var registerResponse = await client.PostAsJsonAsync("api/matches", matchDto);
        //     Assert.True(registerResponse.IsSuccessStatusCode);
        //     var result = await registerResponse.Content.ReadAsAsync<MatchDto>();
        //     
        //     // Then
        //     Assert.Equal("John Shepard", result.Username);
        //     Assert.NotNull(result.Id.ToString());
        //
        //     return result.Id;
        // }
    }
    
}