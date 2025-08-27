using Production.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models.Moduls
{
    internal class FinalModule : Module
    {
        public event Action<Product>? ProductArrived;

        public FinalModule() : base(new List<Conveyor>(), ModuleType.Final)
        {
        }

        protected override void TryDoSomething()
        {
            var product = _products.First();
            _products.Remove(product);

            ProductArrived?.Invoke(product);
            Console.WriteLine($"Готовое блюдо прибыло в финальный модуль: {product.ProductType}");
        }
    }
}
