using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoBase.Core.Events;
using AutoBase.Core.Enums;
using AutoBase.Model.Peoples;
using AutoBase.Model.Cars;

namespace AutoBase.Model.Buildings
{
    internal class RepairBuilding
    {
        private AutoBaseModel _autoBaseModel;

        private object _workerLocker = new object();
        private object _carsLocker = new object();
        private Thread _thread;

        private List<Worker> _avaiableWorkers = new();
        private List<Car> _carsToRepair = new();

        public RepairBuilding(AutoBaseModel autoBaseModel)
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
        public void CarCameToRepair(Car car)
        {
            lock (_carsLocker)
            {
                _carsToRepair.Add(car);
            }
        }


        private void Life()
        {
            while (true)
            {
                if (_avaiableWorkers.Any() && _carsToRepair.Any())
                {
                    Worker worker;
                    Car car;

                    lock (_workerLocker)
                    {
                        worker = _avaiableWorkers.First();
                        _avaiableWorkers.Remove(worker);
                    }
                    lock (_carsLocker)
                    {
                        car = _carsToRepair.First();
                        _carsToRepair.Remove(car);
                    }

                    worker.RepairCar(car);
                }
                Thread.Sleep(250);
            }
        }

    }
}