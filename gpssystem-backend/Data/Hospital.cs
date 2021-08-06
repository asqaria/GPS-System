using System;
using System.Collections.Generic;

#nullable disable

namespace GPSsystem.Data
{
    public partial class Hospital
    {
        public Hospital()
        {
            Drivers = new HashSet<Driver>();
        }

        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string HospitalPos { get; set; }

        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
