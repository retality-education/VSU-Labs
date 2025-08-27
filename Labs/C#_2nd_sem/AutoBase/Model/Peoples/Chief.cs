using AutoBase.Core.Enums;
using AutoBase.Core.Events;
using AutoBase.Model.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBase.Model.Peoples
{
    internal class Chief
    {
        private Queue<Guest> queueOfVisitors = new();
        private AutoBaseModel _autoBaseModel;
        private object _queueLock = new object();
        private Thread _thread;
        public Chief(AutoBaseModel autoBaseModel) 
        {
            _autoBaseModel = autoBaseModel;
            _thread = new Thread(Life);

            _thread.Start();
        }
        public void AddGuestToQueue(Guest guest)
        {
            lock (_queueLock)
            {
                queueOfVisitors.Enqueue(guest);
            }
        }
        private void Life()
        {
            Guest guest;
            while (true)
            {
                lock (_queueLock)
                {
                    if (queueOfVisitors.Any())
                    {
                        guest = queueOfVisitors.Dequeue();

                        _autoBaseModel.Notify(
                            new ModelEventArgs(EventType.ReceiveOfflineOrder) 
                         );


                        Thread.Sleep(2000);

                        _autoBaseModel.Notify(
                            new ModelEventArgs(EventType.GuestMoveToRepair, guest.Car)
                        );

                        _autoBaseModel.MoveCarToRepair(guest.Car);
                    }
                }
                Thread.Sleep(100); 
            }
        }
    }
}
