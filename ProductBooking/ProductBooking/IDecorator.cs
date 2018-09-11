using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace ProductBooking
{
    interface IDecorator
    {
        List<CarProduct> GetAllCar(int i);
        List<HotelProduct> GetAllHotel(int i);
        List<ActivityProduct> GetAllActivity(int i);
        List<AirProduct> GetAllAir(int i);
    }
}
