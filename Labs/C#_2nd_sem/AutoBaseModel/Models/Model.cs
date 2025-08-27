using AutoBaseModel.Core.ObserverPattern;
using AutoBaseModel.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoBaseModel.Core.Enums;
using AutoBaseModel.Models.Persons;
using AutoBaseModel.Models.Requests;
using AutoBaseModel.Core.Factories;

namespace AutoBaseModel.Models
{
    internal class Model : IObservable
    {
        private RepairShop _repairShop;
        private EmployeeHouse _employeeHouse;
        private Garage _garage;
        private Boss _boss;
        private Dispatcher _dispatcher;

        private List<IObserver> _observers = new();

        private Random rand = new Random();

        private decimal _money = 0;

        private Thread _thread;

        public Model() { 
            _repairShop = new RepairShop(this);
            _employeeHouse = new EmployeeHouse(this);
            _garage = new Garage(this);

            _boss = new Boss(this);
            _dispatcher = new Dispatcher(this);

            _thread = new Thread(Life);
        }
        public void Start()
        {
            _thread.Start();
        }

        private void Life()
        {
            while (true)
            {
                var request = Factory.CreateRandomRequest();
                if (request is GarageRequest)
                    Notify(new EventData { EventType = EventType.ClientComeToAutoBase });
                else if (request is RepairRequest)
                    Notify(new EventData { EventType = EventType.CarClientComeToAutoBase });
                
                Thread.Sleep(1000);
                
                _dispatcher.AddRequest(request);
                
                Thread.Sleep(5000);
            }
        }
        public void AddRequestToGarage(Request request)
        {
            _garage.AddRequest(request);
        }
        public void AddRequestToRepair(Request request)
        {
            _repairShop.AddRequest(request);
        }
        public void AddRequestToHouse(Request request)
        {
            _employeeHouse.AddRequest(request);
        }
        public void AddMoney(decimal money)
        {
            _money += money;
            Notify(new EventData { 
                EventType = EventType.MoneyChanged,
                Money = _money
            });
        }

        public async void MoveWorkerToRepairShop(Employee employee, int Time)
        {
            await Task.Delay(Time);

            _repairShop.AddEmployee(employee);
        }
        public async void MoveWorkerToGarage(Employee employee, int Time)
        {
            await Task.Delay(Time);

            _garage.AddEmployee(employee);
        }
        public void AddRequestToTowCaseRepair()
        {
            _repairShop.AddRequest(new RepairRequest
            {
                Type = RepairRequestType.TowCase
            });
        }
        public void Notify(EventData eventData)
        {
             _observers.ForEach(x => x.Update(eventData));
        }

        public void Subscribe(IObserver observer)
        {

             _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}
