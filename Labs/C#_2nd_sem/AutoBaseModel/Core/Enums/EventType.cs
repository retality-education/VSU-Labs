using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Core.Enums
{
    internal enum EventType
    {
        MoneyChanged,
        BossScreaming,

        LightCarGoToOrder,
        TowCarGoToOrder,
        WorkerComeBackFromLightOrder,
        WorkerComeBackFromTowOrder,
        TowCarComeBackToGarage,
        
        WorkerGoToGarage,
        WorkerGoToRepairShop,
        WorkerGoBackToHouseFromGarage,
        WorkerGoBackToHouseFromRepair,

        ClientComeToAutoBase,
        CarClientComeToAutoBase,
        CarClientGoToRepair,
        ClientGoToGarage,
        
        ClientRentCar,
        ClientBackCar,
        ClientLeaveWithRepairedCar,
        ClientLeaveFromAutoBase,
        ClientLeaveFromAutoBaseSimple
    }
}
