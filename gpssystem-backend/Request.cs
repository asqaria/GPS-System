using GPSsystem.Data;
using GPSsystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPSsystem
{
    public class Request
    {
        public Response GetAllRequests()
        {
            Response res = new Response();

            try {
                using (var context = new gpssystemContext())
                {
                    res.Result = context.Requests.Where(m => m.Status != "Completed").ToList();
                }
            }
            catch (Exception ex) {
                res.IsSuccess = false;
                res.ErrorMessage = ex.Message;
            }

            return res;
        }

        public Response MakeRequest(RequestModel rm)
        {
            Response res = new Response();

            try
            {
                Data.Request request = new Data.Request();
            
                request.ClientName = rm.Name;
                request.DriverId = rm.DriverId;
                request.AddressName = rm.Address;
                request.AddressPos = rm.AddressPos;
                request.StartTime = DateTime.UtcNow;
                request.Status = "In progress";

                bool isDriverAvaliable = false;
                using (var context = new gpssystemContext())
                {
                    isDriverAvaliable = context.AvaliableDrivers.Any(m => m.DriverId.Equals(request.DriverId));
                }
                
                if (!isDriverAvaliable) {
                    res.IsSuccess = false;
                    res.ErrorMessage = "Driver '#"+request.DriverId+"' is not avaliable";
                    return res;
                }
            
                using (var context = new gpssystemContext())
                {
                    context.Requests.Add(request);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) {
                res.IsSuccess = false;
                res.ErrorMessage = ex.Message;
            }

            return res;
        }

        public Response GetAvaliableDrivers()
        {
            Response res = new Response();

            try
            {
                using (var context = new gpssystemContext())
                {
                    res.Result = context.AvaliableDrivers.ToList();
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.ErrorMessage = ex.Message;
            }

            return res;
        }

        //public string CheckRequestStatus(int user_id)
        //{
        //    string status = null;
        //    using (var context = new gpssystemContext())
        //    {
        //        status = context.Requests.OrderBy(u => u.RequestId).LastOrDefault(u => u.UserId == user_id)?.Status;
        //    }

        //    return status;
        //}

        //public bool ResponseToRequest(long request_id, int driver_id, string status)
        //{
        //    using (var context = new gpssystemContext())
        //    {
        //        Data.Request request = context.Requests.FirstOrDefault(m => m.RequestId == request_id);
        //        request.DriverId = driver_id;
        //        request.Status = status;
        //        context.SaveChanges();
        //    }

        //    return true;
        //}

        // update status to completed manualy for now
        // TODO: change to auto update base on radis of destination
        //public bool UpdateRequestStatus(long request_id, string status)
        //{
        //    using (var context = new gpssystemContext())
        //    {
        //        Data.Request request = context.Requests.FirstOrDefault(m => m.RequestId == request_id);
        //        request.Status = status;
        //        context.SaveChanges();
        //    }

        //    return true;
        //}
    }
}
