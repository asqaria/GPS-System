using System;
using System.Collections.Generic;

#nullable disable

namespace GPSsystem.Data
{
    public partial class Driver
    {
        public Driver()
        {
            Requests = new HashSet<Request>();
        }

        public int DriverId { get; set; }
        public string Imei { get; set; }
        public int HospitalId { get; set; }

        public virtual Hospital Hospital { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
