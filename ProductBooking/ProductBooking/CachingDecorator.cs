using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;

namespace ProductBooking
{
    public class CachingDecorator : IDecorator
    {
        static List<HotelProduct> Allhotels = new List<HotelProduct>();
        static List<AirProduct> AllFlights = new List<AirProduct>();
        static List<ActivityProduct> AllActivities = new List<ActivityProduct>();
        static List<CarProduct> AllCars = new List<CarProduct>();
        public List<ActivityProduct> GetAllActivity(int choice)
        {
            if (choice == 0)
            {
                using (AllProductsDatabaseEntities entity = new AllProductsDatabaseEntities())
                {
                    if (AllActivities.Count == 0)
                    {
                        var cache = new System.Runtime.Caching.MemoryCache("MyTestCache");
                        cache["ActivityList"] = entity.ActivityProducts.ToList();                 // add
                        AllActivities = (List<ActivityProduct>)cache["ActivityList"]; // retrieve
                                                                                      // cache.Remove("ObjectList");                 // remove
                        return entity.ActivityProducts.ToList();
                    }
                    else
                    {
                        return AllActivities;
                    }
                }
            }
            else if (choice == 1)
            {
                AllActivities = new List<ActivityProduct>();
                return AllActivities;
            }
            return AllActivities;
        }

        public List<AirProduct> GetAllAir(int choice)
        {
            if (choice == 0)
            {
                using (AllProductsDatabaseEntities entity = new AllProductsDatabaseEntities())
                {
                    if (AllFlights.Count == 0)
                    {
                        var cache = new System.Runtime.Caching.MemoryCache("MyTestCache");
                        cache["FlightList"] = entity.AirProducts.ToList();                 // add
                        AllFlights = (List<AirProduct>)cache["FlightList"]; // retrieve
                                                                            // cache.Remove("ObjectList");                 // remove
                        return entity.AirProducts.ToList();
                    }
                    else
                    {
                        return AllFlights;
                    }
                }
            }
            else if (choice == 1)
            {
                AllFlights = new List<AirProduct>();
                return AllFlights;
            }
            return AllFlights;
        }

        public List<CarProduct> GetAllCar(int choice)
        {
            var cache = new System.Runtime.Caching.MemoryCache("MyTestCache");
            if (choice == 0)
            {
                using (AllProductsDatabaseEntities entity = new AllProductsDatabaseEntities())
                {
                    if (AllCars.Count == 0)
                    {
                        cache["CarList"] = entity.CarProducts.ToList();                 // add
                        AllCars = (List<CarProduct>)cache["CarList"]; // retrieve
                                                                      // cache.Remove("ObjectList");                 // remove
                        return entity.CarProducts.ToList();
                    }
                    else
                    {
                        return AllCars;
                    }
                }
            }
            else if(choice==1)
            {
                AllCars = new List<CarProduct>();
                return AllCars;
            }
            return AllCars;
        }

        public List<HotelProduct> GetAllHotel(int choice)
        {
            if (choice == 0)
            {
                using (AllProductsDatabaseEntities entity = new AllProductsDatabaseEntities())
                {
                    if (Allhotels.Count == 0)
                    {
                        var cache = new System.Runtime.Caching.MemoryCache("MyTestCache");
                        cache["ObjectList"] = entity.HotelProducts.ToList();                 // add
                        Allhotels = (List<HotelProduct>)cache["ObjectList"]; // retrieve
                                                                             // cache.Remove("ObjectList");                 // remove
                        return entity.HotelProducts.ToList();
                    }
                    else
                    {
                        return Allhotels;
                    }
                }
            }
            else if (choice == 1)
            {
                Allhotels = new List<HotelProduct>();
                return Allhotels;
            }
            return Allhotels;
        }
    }
}