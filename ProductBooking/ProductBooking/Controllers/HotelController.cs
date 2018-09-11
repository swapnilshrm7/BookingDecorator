using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;

namespace ProductBooking.Controllers
{
    public class HotelController : ApiController
    {
        CachingDecorator cache = new CachingDecorator();
        static List<HotelProduct> Allhotels = new List<HotelProduct>();
        public IEnumerable<HotelProduct> GetAllValues()
        {
            Allhotels = cache.GetAllHotel(0);
            return Allhotels;
        }
        [HttpPut]
        public string PutValue([FromBody]ItemDetail detail)
        {
            using (AllProductsDatabaseEntities entity = new AllProductsDatabaseEntities())
            {
                cache.GetAllHotel(1);
                if (detail.BookOrSave.ToLower() == "book")
                {
                    if (entity.HotelProducts.Find(detail.Id).IsBooked != true)
                    {
                        entity.HotelProducts.Find(detail.Id).IsBooked = true;
                        entity.SaveChanges();
                        return "Hotel Booked successfully";
                    }
                    else
                        return "Hotel already booked";
                }
                else if (detail.BookOrSave.ToLower() == "save")
                {
                    if (entity.HotelProducts.Find(detail.Id).IsSaved != true)
                    {
                        entity.HotelProducts.Find(detail.Id).IsSaved = true;
                        entity.SaveChanges();
                        return "Hotel saved successfully";
                    }
                    else
                        return "Hotel already saved";
                }
                else
                {
                    return "unexpected input";
                }
            }
        }
        [HttpPost]
        public void InsertInto([FromBody]HotelProduct hotel)
        {
            using (AllProductsDatabaseEntities entity = new AllProductsDatabaseEntities())
            {
                cache.GetAllHotel(1);
                entity.HotelProducts.Add(hotel);
                entity.SaveChanges();
            }
        }
    }
}
