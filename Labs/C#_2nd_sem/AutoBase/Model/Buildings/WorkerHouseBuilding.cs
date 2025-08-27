using AutoBase.Model.Peoples;
using AutoBase.Core.Events;
using AutoBase.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoBase.Core.Interfaces;
using AutoBase.Model.Factories;

namespace AutoBase.Model.Buildings
{
    internal class WorkerHouseBuilding : IObserver
    {
        private AutoBaseModel _autoBaseModel;
        public WorkerHouseBuilding(AutoBaseModel autoBaseModel)
        {
            _autoBaseModel = autoBaseModel;

            _autoBaseModel.Subscribe(this);
        }

        public Worker GetWorker()
        {
            return AutoBaseFactory.GetWorker(_autoBaseModel);
        }

        public void OnAutobaseEvent(ModelEventArgs args)
        {
            switch (args.EventType)
            {
                case EventType.ReceiveOnlineOrder:
                    _autoBaseModel.Notify(new ModelEventArgs(EventType.WorkerMoveToGarage));
                    _autoBaseModel.MoveWorkerToGarage(GetWorker());
                    break;
                case EventType.ReceiveOfflineOrder:
                    _autoBaseModel.Notify(new ModelEventArgs(EventType.WorkerMoveToRepair));
                    _autoBaseModel.MoveWorkerToRepair(GetWorker());
                    break;
                default:
                    break;
            }
        }
    }
}
