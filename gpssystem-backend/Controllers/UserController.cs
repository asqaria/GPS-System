using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPSsystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPSsystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //[HttpPost("login")]
        //public bool login([FromBody] LoginRequest lr)
        //{
        //    User user = new User(lr.email, lr.password);
        //    return user.ToLogin() != null;
        //}

        //[HttpPost("registration")]
        //public bool registration([FromBody] LoginRequest lr)
        //{
        //    User user = new User(lr.username, lr.password, lr.email, lr.address);
        //    return user.ToRegister();
        //}

        //[HttpPost("makerequest")]
        //public Response makerequest([FromBody] LoginRequest lr)
        //{
        //    Response res = new Response();

        //    Data.User user = new User(lr.email, lr.password).ToLogin();
        //    if (user == null) {
        //        res.IsSuccess = false;
        //        res.ErrorMessage = "Not authorized user";

        //        return res;
        //    }

        //    // check request

        //    Request request = new Request();
        //    //if (!request.MakeRequest(user.UserId)) {
        //     //   res.IsSuccess = false;
        //     //   res.ErrorMessage = "Not able to make request";

        //      //  return res;
        //    //}
            
        //    return res;
        //}

        //[HttpPost("checkrequest")]
        //public Response checkrequest([FromBody] LoginRequest lr)
        //{
        //    Response res = new Response();

        //    Data.User user = new User(lr.email, lr.password).ToLogin();
        //    if (user == null)
        //    {
        //        res.IsSuccess = false;
        //        res.ErrorMessage = "Not authorized user";

        //        return res;
        //    }

        //    Request request = new Request();
        //    string status = request.CheckRequestStatus(user.UserId);

        //    if (string.IsNullOrEmpty(status))
        //    {
        //        res.IsSuccess = false;
        //        res.ErrorMessage = "Not able to check status";

        //        return res;
        //    }

        //    res.Result = status;

        //    return res;
        //}
    }
}
