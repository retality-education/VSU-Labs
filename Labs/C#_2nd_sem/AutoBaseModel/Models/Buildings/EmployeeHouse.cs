using AutoBaseModel.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoBaseModel.Core.Enums;
using AutoBaseModel.Core.ObserverPattern;
using AutoBaseModel.Core.Factories;

namespace AutoBaseModel.Models.Buildings
{
    internal class EmployeeHouse : Building
    {
        public EmployeeHouse(Model model) : base(model) { }
       
        protected override void HandleRequest(Request request)
        {
            RemoveRequest(request!);

            var workerRequest = request as WorkerRequest;

            int timeToGo = 500;

            switch (workerRequest!.destination) 
            {
                case DestinationOfWorker.GoToRepairShop:
                    _model.Notify(new EventData { EventType = EventType.WorkerGoToRepairShop, Time = timeToGo });
                    _model.MoveWorkerToRepairShop(Factory.GetEmployee(_model), timeToGo);
                    break;
                case DestinationOfWorker.GoToGarage:
                    _model.Notify(new EventData { EventType = EventType.WorkerGoToGarage, Time = timeToGo });
                    _model.MoveWorkerToGarage(Factory.GetEmployee(_model), timeToGo);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            
        }
    }
}
