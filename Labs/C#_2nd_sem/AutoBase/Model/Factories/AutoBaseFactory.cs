using AutoBase.Core.Enums;
using AutoBase.Model.CarData;
using AutoBase.Model.Cars;
using AutoBase.Model.Peoples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBase.Model.Factories
{
    internal static class AutoBaseFactory
    {
        private static Random random = new Random();
        public static Car GetLightGuestCar()
        {
            return new Car(CarType.LightGuest, CarCondition.Broken);
        }
        public static Car GetCargoWorkerCar()
        {
            return new Car(CarType.HeavyWorker, CarCondition.Working);
        }
        public static Car GetLightWorkerCar()
        {
            return new Car(CarType.LightWorker, CarCondition.Working);
        }

        public static Guest GetNewGuest()
        {
            return new Guest(GetLightGuestCar());
        }

        public static CarRequest GetRandomCarRequest()
        {
            if (random.Next(100) < 50)
                return new CarRequest(CarType.LightWorker);
            else
                return new CarRequest(CarType.HeavyWorker);
        }
        public static Worker GetWorker(AutoBaseModel autoBaseModel)
        {
            return new Worker(autoBaseModel);
        }
    }
}
