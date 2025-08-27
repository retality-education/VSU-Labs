using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoBase.Core.Enums;
using AutoBase.Core.Events;
using AutoBase.Model.CarData;
using AutoBase.Model.Cars;
using AutoBase.Model.Factories;

namespace AutoBase.Model.Peoples
{
    internal class Worker
    {
        AutoBaseModel _autoBaseModel;
        Random _random = new Random();
        public Worker(AutoBaseModel autoBaseModel) { 
            _autoBaseModel = autoBaseModel;
        }

        public async void RepairCar(Car car)
        {
            await Task.Delay(2000);

            car.Condition = CarCondition.Working;

            if (car.Type == CarType.LightGuest)
                _autoBaseModel.Notify(new ModelEventArgs(EventType.GuestMoveAwayFromBase, car));
            else
                _autoBaseModel.Notify(new ModelEventArgs(EventType.WorkerCarMoveFromRepairToGarage, car));
        }
        public async void ExecuteOrder(CarRequest request)
        {
            CarType needCarType = request.NeedCarType;

            Car car;
            if (needCarType == CarType.LightWorker)
                car = AutoBaseFactory.GetLightWorkerCar();
            else
                car = AutoBaseFactory.GetCargoWorkerCar();
             
            _autoBaseModel.Notify(new ModelEventArgs(EventType.WorkerCarMoveToOrder, car));

            await Task.Run(async () => {
                await Task.Delay(6000);

                if (_random.Next(100) < 50)
                {
                    _autoBaseModel.Notify(new ModelEventArgs(EventType.WorkerCarCameFromOrderToGarage, car));
                    await Task.Delay(1500);
                }
                else
                {
                    _autoBaseModel.Notify(new ModelEventArgs(EventType.WorkerCarCameFromOrderToRepair, car));
                    await Task.Delay(1500);

                    _autoBaseModel.MoveCarToRepair(car, false);
                    _autoBaseModel.MoveWorkerToRepair(this, false);
                }
                

            });
        }
    }
}
