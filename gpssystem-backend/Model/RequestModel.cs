using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPSsystem.Model
{
    public class RequestModel
    {
        public int DriverId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AddressPos { get; set; }
        public string Token { get; set; }
    }
}
