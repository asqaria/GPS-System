using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPSsystem.Model
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string Token { get; set; }
    }
}
