using AutoBase.Core.Events;
using AutoBase.Core.Interfaces;
using AutoBase.Model.Buildings;
using AutoBase.Model.Peoples;
using AutoBase.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutoBase.Model.Cars;
using AutoBase.Model.Factories;
using AutoBase.Model.CarData;
using System.Diagnostics.Eventing.Reader;

namespace AutoBase.Model
{
    internal class AutoBaseModel : IObservable
    {
       
        private GarageBuilding _garageBuilding;
        private RepairBuilding _repairBuilding;
        private WorkerHouseBuilding _workerHouseBuilding;
        
        private Dispatcher _dispatcher;
        private Chief _chief;

        private Random _random;

        private object _observerLock = new object();
        private List<IObserver> observers = new();
        public AutoBaseModel()
        {
            _garageBuilding = new GarageBuilding(this);
            _repairBuilding = new RepairBuilding(this);
            _workerHouseBuilding = new WorkerHouseBuilding(this);

            _chief = new Chief(this);
            _dispatcher = new Dispatcher(this);
            _random = new Random();
        }
        public void Start()
        {
            Life();
        }
        private void Life()
        {
            // Запуск генерации оффлайн-заказов (гости к начальнику)
            Task.Run(async () =>
            {
                while (true)
                {
                    var guest = AutoBaseFactory.GetNewGuest(); // Создаём нового гостя
                    //activeCars.Add(guest.Car);

                    Notify(new ModelEventArgs(EventType.GuestMoveToChief, guest.Car));
                    
                    await Task.Delay(1000); // Едем к шефу

                    _chief.AddGuestToQueue(guest);  // Добавляем к начальнику
                    
                    await Task.Delay(_random.Next(6000, 8000)); // Ждём 6-12 сек
                }
            });

            // Запуск генерации онлайн-заказов (диспетчер)
            Task.Run(async () =>
            {
                while (true)
                {
                    var carRequest = AutoBaseFactory.GetRandomCarRequest();

                    _dispatcher.AddCarRequest(carRequest); // Добавляем к диспетчеру

                    await Task.Delay(_random.Next(4000, 6000)); // Ждём 5-8 сек
                }
            });
        }
        public void MoveWorkerToRepair(Worker worker, bool fg = true)
        {
            Task.Run(async () => {
                if (fg)
                    await Task.Delay(1000);
                _repairBuilding.WorkerCameToWork(worker);
            });
        }
        public void MoveWorkerToGarage(Worker worker)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                _garageBuilding.WorkerCameToWork(worker);
            });
        }
        public void MoveCarToRepair(Car car, bool fg = true)
        {
            Task.Run(async () => {
                if (fg)
                    await Task.Delay(2000);
                _repairBuilding.CarCameToRepair(car);
            });
        }
        public void AddRequestToGarage(CarRequest request)
        {
            Task.Run(() =>
            {
                _garageBuilding.GetRequest(request);
            });
        }

        #region IObservable Implementation
        public void Notify(ModelEventArgs args)
        {
            lock (_observerLock)
            {
                foreach (var observer in observers)
                    observer.OnAutobaseEvent(args);
            }
        }

        public void Subscribe(IObserver observer)
        {
            lock (_observerLock)
            {
                observers.Add(observer);
            }
        }

        public void Unsubscribe(IObserver observer)
        {
            lock(_observerLock)
            {
                observers.Remove(observer);
            }
        }



        #endregion
    }
}
