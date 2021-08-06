using System;
using System.Collections.Generic;

#nullable disable

namespace GPSsystem.Data
{
    public partial class CurrentPo
    {
        public long RequestId { get; set; }
        public int DriverId { get; set; }
        public string AddressPos { get; set; }
        public string DriverPos { get; set; }
        public DateTime Time { get; set; }
        public string Status { get; set; }
    }
}
