using System;
using System.Collections.Generic;

namespace LaserTagTrackerApi.Model
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public List<Match> Matches { get; set; }
    }
}