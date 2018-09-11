using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;

namespace ProductBooking.Controllers
{
    public class AirController : ApiController
    {
        CachingDecorator cache = new CachingDecorator();
        static List<AirProduct> AllFlights = new List<AirProduct>();
        public IEnumerable<AirProduct> GetAllValues()
        {
            AllFlights = cache.GetAllAir(0);
            return AllFlights;
        }
        [HttpPut]
        public string PutValue([FromBody]ItemDetail detail)
        {
            using (AllProductsDatabaseEntities entity = new AllProductsDatabaseEntities())
            {
                cache.GetAllAir(1);
                if (detail.BookOrSave.ToLower() == "book")
                {
                    if (entity.AirProducts.Find(detail.Id).IsBooked != true)
                    {
                        entity.AirProducts.Find(detail.Id).IsBooked = true;
                        entity.SaveChanges();
                        return "Flight Booked successfully";
                    }
                    else
                        return "Flight already booked";
                }
                else if (detail.BookOrSave.ToLower() == "save")
                {
                    if (entity.AirProducts.Find(detail.Id).IsSaved != true)
                    {
                        entity.AirProducts.Find(detail.Id).IsSaved = true;
                        entity.SaveChanges();
                        return "Flight saved successfully";
                    }
                    else
                        return "Flight already saved";
                }
                else
                {
                    return "unexpected input";
                }
            }
        }
        [HttpPost]
        public void InsertInto([FromBody]AirProduct flight)
        {
            using (AllProductsDatabaseEntities entity = new AllProductsDatabaseEntities())
            {
                cache.GetAllAir(1);
                entity.AirProducts.Add(flight);
                entity.SaveChanges();
            }
        }


    }
}
