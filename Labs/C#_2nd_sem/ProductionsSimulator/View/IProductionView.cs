using Production.Controller;
using Production.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.View
{
    internal interface IProductionView
    {
        void ConveyorMovement(int conveyorId, int moduleId, ProductType productType);
        void NewOrder(ProductType dishType);
        void WorkerAction(string action);
        void InitializeConveyor(int conveyorId, ModuleType moduleType1, ModuleType moduleType2);
        void InitializeModule(int moduleId,  ModuleType moduleType);
        void DeliveryAction(ProductType productType);

        ProductionController Controller { get; set; }
    }
}
