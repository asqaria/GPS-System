using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPSsystem.Model
{
    public class DispatcherRequest
    {
        public long RequestId { get; set; }
        public int DriverId { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
    }
}
