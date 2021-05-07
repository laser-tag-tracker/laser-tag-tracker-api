using System;

namespace LaserTagTrackerApi.Model.DTOs
{
    public class AuthSuccessDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
    }
}