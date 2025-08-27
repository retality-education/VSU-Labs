using AutoBase.Core.Enums;
using AutoBase.Core.Events;
using AutoBase.Model.CarData;
using AutoBase.Model.Cars;
using AutoBase.Model.Peoples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBase.Model.Buildings
{
    internal class GarageBuilding
    {

        private AutoBaseModel _autoBaseModel;

        private object _workerLocker = new object();
        private object _requestLocker = new object();
        private Thread _thread;

        private List<Worker> _avaiableWorkers = new();
        private List<CarRequest> _availableRequests = new();

        public GarageBuilding(AutoBaseModel autoBaseModel)
        {
            _autoBaseModel = autoBaseModel;
            _thread = new Thread(Life);

            _thread.Start();
        }
        public void WorkerCameToWork(Worker worker)
        {
            lock (_workerLocker)
            {
                _avaiableWorkers.Add(worker);
            }
        }
        public void GetRequest(CarRequest car)
        {
            lock (_requestLocker)
            {
                _availableRequests.Add(car);
            }
        }


        private void Life()
        {
            while (true)
            {
                if (_avaiableWorkers.Any() && _availableRequests.Any())
                {
                    Worker worker;
                    CarRequest request;

                    lock (_workerLocker) {
                        worker = _avaiableWorkers.First();
                        _avaiableWorkers.Remove(worker);
                    }

                    lock (_requestLocker) {
                        request = _availableRequests.First();
                        _availableRequests.Remove(request);
                    }                    
                   
                    worker.ExecuteOrder(request);
                
                }
                Thread.Sleep(250);
            }
        }
    }
}
