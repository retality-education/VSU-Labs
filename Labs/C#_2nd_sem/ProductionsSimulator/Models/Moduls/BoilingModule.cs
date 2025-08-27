using Production.Core.Data;
using Production.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models.Moduls
{
    internal class BoilingModule : Module
    {
        public BoilingModule(List<Conveyor> conveyors) : base(conveyors, ModuleType.Boiling)
        {
        }

        protected override void TryDoSomething()
        {
            var product = _products.First();

            if (ProductData.ResultOfBoiling.TryGetValue(product.ProductType, out var boiledProductType))
            {
                _products.Remove(product);

                Task.Run(() =>
                {
                    Thread.Sleep(2000);

                    
                    product.ProductType = boiledProductType; // Преобразуем продукт

                    // Отправляем дальше по конвейеру
                    _conveyors[0].AddProduct(product);
                });
            }
        }
    }
}
