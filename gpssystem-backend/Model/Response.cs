using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPSsystem.Model
{
    public class Response
    {
        public Response() {
            this.IsSuccess = true;
            this.ErrorMessage = null;
            this.Result = null;
        }

        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public object Result { get; set; }
    }
}
