using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;

namespace ProductBooking.Controllers
{
    public class CarController : ApiController
    {
        CachingDecorator cache = new CachingDecorator();
        static List<CarProduct> AllCars = new List<CarProduct>();
        public IEnumerable<CarProduct> GetAllValues()
        {
            AllCars = cache.GetAllCar(0);
            return AllCars;
        }
        [HttpPut]
        public string PutValue([FromBody]ItemDetail detail)
        {
            cache.GetAllCar(1);
            using (AllProductsDatabaseEntities entity = new AllProductsDatabaseEntities())
            {
                if (detail.BookOrSave.ToLower() == "book")
                {
                    if (entity.CarProducts.Find(detail.Id).IsBooked != true)
                    {
                        entity.CarProducts.Find(detail.Id).IsBooked = true;
                        entity.SaveChanges();
                        return "Car Booked successfully";
                    }
                    else
                        return "Car already booked";
                }
                else if (detail.BookOrSave.ToLower() == "save")
                {
                    if (entity.CarProducts.Find(detail.Id).IsSaved != true)
                    {
                        entity.CarProducts.Find(detail.Id).IsSaved = true;
                        entity.SaveChanges();
                        return "Car saved successfully";
                    }
                    else
                        return "Car already saved";
                }
                else
                {
                    return "unexpected input";
                }
            }
        }
        [HttpPost]
        public void InsertInto([FromBody]CarProduct car)
        {
            using (AllProductsDatabaseEntities entity = new AllProductsDatabaseEntities())
            {
                cache.GetAllCar(1);
                entity.CarProducts.Add(car);
                entity.SaveChanges();
            }
        }
    }
}
