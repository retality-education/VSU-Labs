using AutoBase.Core.Enums;
using AutoBase.Core.Events;
using AutoBase.Model.CarData;
using AutoBase.Model.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBase.Model.Peoples
{
    internal class Dispatcher
    {
        private Queue<CarRequest> _queueOfRequests = new();
        private AutoBaseModel _autoBaseModel;
        private object _queueLock = new object();
        private Thread _thread;
        public Dispatcher(AutoBaseModel autoBaseModel)
        {
            _autoBaseModel = autoBaseModel;
            _thread = new Thread(Life);

            _thread.Start();
        }
        public void AddCarRequest(CarRequest carRequest)
        {
            lock (_queueLock)
            {
                _queueOfRequests.Enqueue(carRequest);
            }
        }
        private void Life()
        {
            while (true)
            {
                lock (_queueLock)
                {
                    if (_queueOfRequests.Any())
                    {
                        var request = _queueOfRequests.Dequeue();

                        _autoBaseModel.Notify(
                            new ModelEventArgs(EventType.ReceiveOnlineOrder, new Car(request.NeedCarType, CarCondition.Working))
                         );
                        _autoBaseModel.AddRequestToGarage(request);
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}
