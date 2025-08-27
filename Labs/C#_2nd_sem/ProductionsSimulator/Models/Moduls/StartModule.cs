using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models.Moduls
{
    internal class StartModule : Module
    {
        public StartModule(List<Conveyor> conveyors) : base(conveyors, Core.Enums.ModuleType.Start) {
            if (conveyors.Count != 1)
                throw new StartModuleException("Start module should have 1 conveyor!");
        }

        protected override void TryDoSomething()
        {
            var temp = _products.First()!;
            _products.Remove(temp);

            _conveyors[0].AddProduct(temp);
        }
    }

    internal class StartModuleException : Exception
    {
        public StartModuleException(string? message) : base(message){}
    }
}
