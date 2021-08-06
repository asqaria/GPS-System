using System;
using System.Collections.Generic;

#nullable disable

namespace GPSsystem.Data
{
    public partial class Request
    {
        public long RequestId { get; set; }
        public int DriverId { get; set; }
        public int? UserId { get; set; }
        public string ClientName { get; set; }
        public string AddressName { get; set; }
        public string AddressPos { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public DateTime? BackTime { get; set; }
        public string Status { get; set; }

        public virtual Driver Driver { get; set; }
    }
}
