using AutoBaseModel.Core.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Views
{
    internal interface IAutoBase : IObserver
    {
        public void MoneyChanged(decimal money);
        public void BossScreaming();

        public void WorkerGoToGarage();

        public void LightCarGoToOrder();
        public void TowCarGoToOrder();
        public void WorkerComeBackFromLightOrder();
        public void WorkerComeBackFromTowOrder();
        public void TowCarComeBackToGarage();
        public void WorkerGoToRepairShop();
        public void WorkerGoBackToHouseFromGarage();
        public void WorkerGoBackToHouseFromRepair();
        public void ClientComeToAutoBase();
        public void CarClientComeToAutoBase();
        public void CarClientGoToRepair();
        public void ClientGoToGarage();

        public void ClientRentCar();
        public void ClientBackCar();
        public void ClientLeaveWithRepairedCar();
        public void ClientLeaveFromAutoBase();
        public void ClientLeaveFromAutoBaseSimple();
    }
}
