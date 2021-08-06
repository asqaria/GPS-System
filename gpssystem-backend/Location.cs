using GPSsystem.Data;
using GPSsystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPSsystem
{
    public class Location
    {
        public Location() { }

        public Response GetCurrentMap()
        {
            Response res = new Response();

            try {
                using (var context = new gpssystemContext())
                {
                    res.Result = context.CurrentPos.ToList();
                }
            }
            catch (Exception ex) {
                res.IsSuccess = false;
                res.ErrorMessage = ex.Message;
            }

            return res;
        }

        public Response GetHospitalList()
        {
            Response res = new Response();

            try
            {
                using (var context = new gpssystemContext())
                {
                    res.Result = context.Hospitals.Select(m => new { m.HospitalId, m.HospitalName, m.HospitalPos }).ToList();
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.ErrorMessage = ex.Message;
            }

            return res;
        }

        public Response AddLocation(string imei, string coordinates)
        {
            Response res = new Response();

            try {
                Data.Location location = new Data.Location();
                location.Pos = coordinates;
                location.Time = DateTime.UtcNow;

                using (var context = new gpssystemContext())
                {
                    Driver driver = context.Drivers.FirstOrDefault(m => m.Imei.Equals(imei));
                    if (driver == null)
                    {
                        res.IsSuccess = false;
                        res.ErrorMessage = "Not able to find driver with IMEI " + imei;
                        return res;
                    }
                    else {
                        location.DriverId = driver.DriverId;
                    }

                    Data.Request request = context.Requests.OrderBy(m => m.RequestId).LastOrDefault(m => m.DriverId.Equals(driver.DriverId));
                    Data.Hospital hospital = context.Hospitals.FirstOrDefault(m => m.HospitalId.Equals(driver.HospitalId));
                    if (request == null || hospital == null)
                    {
                        res.IsSuccess = false;
                        res.ErrorMessage = "Not able to find request or hospital of driver " + imei;
                        return res;
                    }
                    else {
                        string[] driverCoords = location.Pos.Split(", ");
                        string[] addressCoords = request.AddressPos.Split(", ");

                        if (request.Status.Equals("In progress"))
                        {
                            double distance = GetDistance(driverCoords, addressCoords);
                            if (distance <= 0.1)
                            {
                                request.ArrivalTime = DateTime.UtcNow;
                                request.Status = "Arrived";
                            }
                        }
                        else if (request.Status.Equals("Arrived"))
                        {
                            double distance = GetDistance(driverCoords, addressCoords);
                            if (distance > 0.1)
                            {
                                request.ArrivalTime = DateTime.UtcNow;
                                request.Status = "Coming back";
                            }
                        }
                        else if (request.Status.Equals("Coming back"))
                        {
                            string[] HospitalCoords = hospital.HospitalPos.Split(", ");
                            double distance = GetDistance(driverCoords, HospitalCoords);

                            if (distance <= 0.1)
                            {
                                request.BackTime = DateTime.UtcNow;
                                request.Status = "Completed";
                            }
                        } 
                        else if (request.Status.Equals("Completed")){
                            
                        }
                        else {
                            res.IsSuccess = false;
                            res.ErrorMessage = "Unknown status: " + request.Status;
                            return res;
                        }                     
                    }


                    context.Locations.Add(location);
                    context.SaveChanges();
                }
            }
            catch (Exception ex) {
                res.IsSuccess = false;
                res.ErrorMessage = ex.Message;
            }

            return res;
        }

        private double GetDistance(string[] coor1, string[] coor2)
        {
            double lat1 = Convert.ToDouble(coor1[0]);
            double lon1 = Convert.ToDouble(coor1[1]);
            double lat2 = Convert.ToDouble(coor2[0]);
            double lon2 = Convert.ToDouble(coor2[1]);
            var R = 6371; // Radius of the earth in km
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        private double ToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }

        //public string GetLocation() 
        //{
        //    string coordinates = null;

        //    using (var context = new gpssystemContext())
        //    {
        //        coordinates = context.Locations.LastOrDefault(m => m.DriverId == this.driver_id)?.Pos;
        //    }

        //    return coordinates;
        //}
    }
}
