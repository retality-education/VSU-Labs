using AutoBaseModel.Models;
using AutoBaseModel.Models.Persons;
using AutoBaseModel.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Core.Factories
{
    internal static class Factory
    {
        private static Random rand = new Random();
        public static Employee GetEmployee(Model model)
        {
            return new Employee(model);
        }
        public static Request CreateRandomRequest()
        {
            if (rand.Next(10) < 5)
            {
                var random = rand.Next(0, 10);
                if (random < 3)
                    return new GarageRequest { Type = Enums.GarageRequestType.StartRentCase };
                else if (random < 7)
                    return new GarageRequest { Type = Enums.GarageRequestType.TowTruckСase };
                else
                    return new GarageRequest { Type = Enums.GarageRequestType.LightCase };
            }else
            {
                return new RepairRequest { Type = Enums.RepairRequestType.SimpleRepair };
            }
        }
    }
}
