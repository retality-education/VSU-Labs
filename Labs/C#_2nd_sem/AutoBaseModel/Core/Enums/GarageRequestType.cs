using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Core.Enums
{
    internal enum GarageRequestType
    {
        StartRentCase, // аренда машины
        EndRentCase, // конец аренды машины
        TowTruckСase, // буксирный случай
        LightCase // небольшой ремонт на месте поломки 
    }
}
