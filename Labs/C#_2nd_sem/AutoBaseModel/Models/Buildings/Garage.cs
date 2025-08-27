using AutoBaseModel.Core.Enums;
using AutoBaseModel.Core.ObserverPattern;
using AutoBaseModel.Models.Persons;
using AutoBaseModel.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace AutoBaseModel.Models.Buildings
{
    internal class Garage : Building
    {
        protected override bool CanHandleRequest(Request? request)
        {
            if (request == null)
                return false;
            return (request as GarageRequest)!.Type switch
            {
                GarageRequestType.EndRentCase => true,
                GarageRequestType.StartRentCase => true,
                GarageRequestType.LightCase => employees.Any(),
                GarageRequestType.TowTruckСase => employees.Any(),
                _ => throw new InvalidOperationException()
            };
        }

        private object _employeeLock = new object();
        private List<Employee> employees = new();
        public Garage(Model model) : base(model){}
        
        public void AddEmployee(Employee employee)
        {
            lock (_employeeLock)
            {
                employees.Add(employee);
            }
        }
        private async void LightCase(int Time)
        {
            await Task.Delay(Time);

            lock (_employeeLock)
            {
                var emp = employees.First();

                emp.ExecuteLightCase();

                employees.Remove(emp);
            }
        }
        private async void TowCase(int Time)
        {
            await Task.Delay(Time);
            lock (_employeeLock)
            {
                var emp = employees.First();

                emp.ExecuteTowCase();

                employees.Remove(emp);
            }
        }
        public async void StartRentCase(int Time)
        {
            await Task.Delay(Time); // ждём пока уедёт клиент

            await Task.Run(async () => {
                await Task.Delay(3500); // клиент катается и возвращается через 3500

                _model.Notify(new EventData
                {
                    EventType = EventType.ClientBackCar,
                    Time = Time
                });

                await Task.Delay(Time);

                _model.AddMoney(new Random().Next(5000, 10000));

                AddRequest(new GarageRequest { Type = GarageRequestType.EndRentCase });

            });

        }
        public async void EndRentCase(int Time)
        {
            await Task.Delay(Time);

            await Task.Run(() =>
            {
                _model.Notify(new EventData
                {
                    EventType = EventType.ClientLeaveFromAutoBase
                });
            });
        }
        protected override void HandleRequest(Request request)
        {
            _requests.Remove(request);

            var garageRequest = request as GarageRequest;

            int timeToGo = 500;

            switch (garageRequest!.Type)
            {
                case GarageRequestType.StartRentCase:
                    _model.Notify(new EventData { EventType = EventType.ClientRentCar, Time = timeToGo });
                    StartRentCase(timeToGo);
                    break;
                case GarageRequestType.EndRentCase:
                    EndRentCase(timeToGo);
                    break;
                case GarageRequestType.LightCase:
                    _model.Notify(new EventData { EventType = EventType.LightCarGoToOrder, Time = timeToGo });
                    LightCase(timeToGo);
                    break;
                case GarageRequestType.TowTruckСase:
                    _model.Notify(new EventData { EventType = EventType.TowCarGoToOrder, Time = timeToGo});
                    TowCase(timeToGo);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            
        }
    }
}
