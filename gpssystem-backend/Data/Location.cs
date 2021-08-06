using System;
using System.Collections.Generic;

#nullable disable

namespace GPSsystem.Data
{
    public partial class Location
    {
        public long LocationId { get; set; }
        public int DriverId { get; set; }
        public string Pos { get; set; }
        public DateTime Time { get; set; }
    }
}
