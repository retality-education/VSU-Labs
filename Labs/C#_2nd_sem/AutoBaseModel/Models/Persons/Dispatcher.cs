using AutoBaseModel.Core.Enums;
using AutoBaseModel.Core.ObserverPattern;
using AutoBaseModel.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Models.Persons
{
    internal class Dispatcher
    {
        private object _requestLock = new object();
        private List<Request> _requests = new();
        private Thread _thread;
        private Model _model;
        public Dispatcher(Model model)
        {
            _model = model;
            _thread = new Thread(Life);

            _thread.Start();
        }
        public void AddRequest(Request request)
        {
            lock (_requestLock)
            {
                _requests.Add(request);
            }
        }
        private async void HandleRequest(Request request)
        {
            _requests.Remove(request);

            if (request is GarageRequest garageRequest)
            {
                _model.Notify(new EventData { EventType = EventType.ClientGoToGarage });
                await Task.Delay(500);
                
                if(garageRequest.Type == GarageRequestType.LightCase 
                    || garageRequest.Type == GarageRequestType.TowTruckСase)
                    Task.Run(() => _model.AddRequestToHouse(new WorkerRequest { destination = DestinationOfWorker.GoToGarage}));
                await Task.Delay(1000);
                Task.Run(() => _model.AddRequestToGarage(garageRequest));
            }
            else if (request is RepairRequest repairRequest)
            {
                _model.Notify(new EventData { EventType= EventType.CarClientGoToRepair });
                await Task.Delay(500);
                Task.Run(() => _model.AddRequestToHouse(new WorkerRequest { destination = DestinationOfWorker.GoToRepairShop }));
                Task.Run(() => _model.AddRequestToRepair(repairRequest));
            }
            
            
        }
        private void Life()
        {
            while (true)
            {
                lock (_requestLock)
                {
                    var request = _requests.FirstOrDefault();
                    
                    if (request is not null)
                        HandleRequest(request!);
                }

                Thread.Sleep(100);
            }
        }
    }
}
