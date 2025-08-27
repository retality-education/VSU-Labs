using Production.Models;
using Production.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Controller
{
    internal class ProductionController
    {
        private readonly ProductionFacility _facility;
        private readonly IProductionView _view;

        public ProductionController(ProductionFacility facility, IProductionView view)
        {
            _facility = facility;
            _view = view;

            SubscribeToEvents();

            _view.Controller = this;

        }

        private void SubscribeToEvents()
        {
            foreach (var module in _facility.GetAllModules())
            {
                module.ModuleCreated += _view.InitializeModule;
                foreach (var conveyor in module._conveyors)
                {
                    conveyor.ConveyorCreated += _view.InitializeConveyor;
                }
            }
            // Подписываемся на все конвейеры
            foreach (var module in _facility.GetAllModules())
            {
                foreach (var conveyor in module._conveyors)
                {
                    conveyor.ProductStartMoveOnConveyor += (conveyorId, moduleId, productType) =>
                        _view.ConveyorMovement(conveyorId, moduleId, productType);
                }
            }

            // Подписываемся на начальника
            _facility.GetManager().NewOrderGenerated += dishType =>
                _view.NewOrder(dishType);

            // Подписываемся на рабочих
            _facility.GetOrderWorker().WorkerAction += action =>
                _view.WorkerAction(action);
            _facility.GetDeliveryWorker().WorkerAction += action =>
                _view.DeliveryAction(action);
        }

        public void StartProduction()
        {
            _facility.StartThread();
        }
    }

}
