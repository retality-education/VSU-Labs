using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBase.Core.Enums
{
    internal enum EventType
    {
        //online
        ReceiveOnlineOrder,
        WorkerMoveToGarage,
        WorkerCarMoveToOrder,
        WorkerCarCameFromOrderToGarage,
        WorkerCarCameFromOrderToRepair,
        WorkerCarMoveFromRepairToGarage,

        //offline
        GuestMoveToChief,
        ReceiveOfflineOrder,
        GuestMoveToRepair,
        WorkerMoveToRepair,
        GuestMoveAwayFromBase
    }
}
