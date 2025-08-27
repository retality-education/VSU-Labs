using AutoBase.Core.Events;
using AutoBase.Core.Interfaces;
using AutoBase.Model.Peoples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBase.View
{
    internal interface IAutoBaseView : IObserver
    {
        //online
        void OnReceiveOnlineOrder(CarDto car);
        void OnWorkerMoveToGarage();
        void OnWorkerCarMoveToOrder(int CarId, CarDto car);
        void OnWorkerCarCameFromOrderToGarage(int CarId);
        void OnWorkerCarCameFromOrderToRepair(CarDto car);
        void OnWorkerCarMoveFromRepairToGarage(CarDto car);

        //offline
        void OnGuestMoveToChief(int CarId);
        void OnReceiveOfflineOrder();
        void OnGuestMoveToRepair(int CarId);
        void OnWorkerMoveToRepair();
        void OnGuestMoveAwayFromBase(int CarId);
    }
}
