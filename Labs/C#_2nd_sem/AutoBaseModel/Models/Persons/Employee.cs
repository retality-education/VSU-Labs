using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoBaseModel.Core.ObserverPattern;
using AutoBaseModel.Core.Enums;

namespace AutoBaseModel.Models.Persons
{
    internal class Employee
    {
        private Model _model;
        private Random _random = new Random();
        public Employee(Model model)
        {
            _model = model;
        }
        public async void ExecuteLightCase()
        {
            await Task.Delay(2000);

            await Task.Run(async () =>
            {
                _model.Notify(new EventData
                {
                    EventType = EventType.WorkerComeBackFromLightOrder
                });
                _model.AddMoney(_random.Next(10000, 20000));

                await Task.Delay(1100);

                _model.Notify(new EventData { EventType = EventType.WorkerGoBackToHouseFromGarage });
            });
        }
        public async void ExecuteTowCase()
        {
            await Task.Delay(4000);
            await Task.Run(async () =>
            {
                _model.Notify(new EventData
                {
                    EventType = EventType.WorkerComeBackFromTowOrder
                });

                await Task.Delay(500);

                _model.AddRequestToTowCaseRepair();
                _model.MoveWorkerToRepairShop(this, 0);
            });
        }
        public async void RepairCarTowCase(int Time)
        {
            await Task.Delay(Time); // ремонт

            await Task.Run(() =>
            {
                _model.AddMoney(_random.Next(50000, 60000));
                
                Task.Run(
                    () => _model.Notify(new EventData {
                        EventType = EventType.ClientLeaveWithRepairedCar})
                );

                Task.Run(
                    async () =>
                    {
                        _model.Notify(new EventData
                        {
                            EventType = EventType.TowCarComeBackToGarage
                        });

                        await Task.Delay(1100);

                        _model.Notify(new EventData { EventType = EventType.WorkerGoBackToHouseFromGarage });
                        
                    }

                 );
            });
        }
        public async void RepairCarSimpleCase(int Time)
        {
            await Task.Delay(Time);

            await Task.Run(() =>
            {
                _model.AddMoney(_random.Next(15000, 20000));

                _model.Notify(new EventData
                {
                    EventType = EventType.ClientLeaveFromAutoBaseSimple
                });
                _model.Notify(new EventData { EventType = EventType.WorkerGoBackToHouseFromRepair });
            });
        }
    }
}
