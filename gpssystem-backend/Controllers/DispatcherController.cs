using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPSsystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPSsystem.Controllers
{
    // scaffold-dbcontext "Server=DESKTOP-16OT93O;Database=gpssystem;Trusted_Connection=True;" microsoft.entityframeworkcore.sqlserver -outputdir Data -force


    //if (!optionsBuilder.IsConfigured)
    //{
    //    bool isProduction = false;
    //    string dbConn = string.Empty;
    //    if (isProduction)
    //    {
    //        dbConn = "Server=tcp:gps-prof-db.database.windows.net,1433;Initial Catalog=gpssystem;Persist Security Info=False;User ID=azamat;Password=Turar2380;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    //    }
    //    else {
    //        dbConn = "Server=DESKTOP-16OT93O;Database=gpssystem;Trusted_Connection=True;";
    //    }
    //    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //    optionsBuilder.UseSqlServer(dbConn);
    //}

    [Route("api/[controller]")]
    [ApiController]
    public class DispatcherController : ControllerBase
    {
        [HttpPost("addrequest")]
        public Response AddRequest([FromBody] RequestModel rm)
        {
            Response res = new Response();

            User user = new User(rm.Token);
            if (!user.CheckAdminToken())
            {
                res.IsSuccess = false;
                res.ErrorMessage = "Not authorized user";

                return res;
            }

            Request request = new Request();
            res = request.MakeRequest(rm);

            return res;
        }

        [HttpPost("getavaliabledrivers")]
        public Response GetAvaliableDrivers([FromBody] LoginRequest lr)
        {
            Response res = new Response();

            User user = new User(lr.Token);
            if (!user.CheckAdminToken())
            {
                res.IsSuccess = false;
                res.ErrorMessage = "Not authorized user";

                return res;
            }

            Request request = new Request();
            res = request.GetAvaliableDrivers();

            return res;
        }

        [HttpPost("getcurrentmap")]
        public Response GetCurrentMap([FromBody] DispatcherRequest dr)
        {
            Response res = new Response();

            User user = new User(dr.Token);
            if (!user.CheckAdminToken())
            {
                res.IsSuccess = false;
                res.ErrorMessage = "Not authorized user";

                return res;
            }

            Location location = new Location();
            res = location.GetCurrentMap();
            return res;
        }

        [HttpPost("gethospitallocation")]
        public Response GetHospitalLocations([FromBody] DispatcherRequest dr)
        {
            Response res = new Response();

            User user = new User(dr.Token);
            if (!user.CheckAdminToken())
            {
                res.IsSuccess = false;
                res.ErrorMessage = "Not authorized user";

                return res;
            }

            Location location = new Location();
            res = location.GetHospitalList();
            return res;
        }

        [HttpPost("getrequests")]
        public Response GetRequests([FromBody] DispatcherRequest dr)
        {
            Response res = new Response();

            User user = new User(dr.Token);
            if (!user.CheckAdminToken())
            {
                res.IsSuccess = false;
                res.ErrorMessage = "Not authorized user";

                return res;
            }

            Request request = new Request();
            res = request.GetAllRequests();
            return res;
        }

        [HttpPost("addlocation")]
        public Response AddLocation([FromBody] LocationRequest lr)
        {
            Response res = new Response();
            if (!lr.Token.Equals("f2220d10-9867-4030-970d-4f9456755a9c"))
            {
                res.IsSuccess = false;
                res.ErrorMessage = "You are not eligable to make request";
                return res;
            }

            Location location = new Location();
            res = location.AddLocation(lr.Imei, lr.Coordinates);
            return res;
        }

        [HttpPost("gettoken")]
        public Response GetToken([FromBody] LoginRequest lr)
        {
            Response res = new Response();
            try
            {
                User user = new User(lr.Email, lr.Password);
                res.Result = user.GetAdminToken();
            }
            catch (Exception ex) {
                res.IsSuccess = false;
                res.ErrorMessage = ex.Message.ToString();
            }

            return res;
        }

        [HttpPost("checktoken")]
        public bool CheckToken([FromBody] LoginRequest lr)
        {
            bool res = false;
            try
            {
                User user = new User(lr.Token);
                res = user.CheckAdminToken();
            }
            catch (Exception ex)
            {
                res = false;
            }

            return res;
        }

        //[HttpPost("responsetorequest")]
        //public Response ResponseToRequest([FromBody] DispatcherRequest dr)
        //{
        //    Response res = new Response();

        //    // token
        //    Request request = new Request();
        //    res.IsSuccess = request.ResponseToRequest(dr.request_id, dr.driver_id, dr.status);
        //    return res;
        //}

        //[HttpPost("updaterequeststatus")]
        //public Response UpdateRequestStatus([FromBody] DispatcherRequest dr)
        //{
        //    Response res = new Response();

        //    // token
        //    Request request = new Request();
        //    res.IsSuccess = request.UpdateRequestStatus(dr.request_id, dr.status);
        //    return res;
        //}

        //[HttpPost("getlocation")]
        //public Response GetLocation([FromBody] DispatcherRequest dr)
        //{
        //    Response res = new Response();

        //    // token
        //    Location location = new Location(dr.driver_id);
        //    string coordinates = location.GetLocation();

        //    if (string.IsNullOrEmpty(coordinates))
        //    {
        //        res.IsSuccess = false;
        //        res.ErrorMessage = "Not able to get coordinates of given driver";
        //    }
        //    else
        //    {
        //        res.Result = coordinates;
        //    }

        //    return res;
        //}
    }
}
