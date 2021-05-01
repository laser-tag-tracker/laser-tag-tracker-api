using System;

namespace LaserTagTrackerApi.Model.DTOs
{
    public class CreateMatchDto
    {
        public string PlayerName{ get; set; }
        public int Rank{ get; set; }
        public int Score{ get; set; }
        public double Precision{ get; set; }
        public int TeamScore{ get; set; }
        public DateTime Date{ get; set; }
        
        // Given
        public int ChestGiven{ get; set; }
        public int BackGiven{ get; set; }
        public int ShouldersGiven{ get; set; }
        public int GunGiven{ get; set; }
        
        // Received
        public int ChestReceived{ get; set; }
        public int BackReceived{ get; set; }
        public int ShouldersReceived{ get; set; }
        public int GunReceived{ get; set; }
        
        // Location
        public string Address{ get; set; }
        public double Latitude{ get; set; }
        public double Longitude{ get; set; }
    }
}