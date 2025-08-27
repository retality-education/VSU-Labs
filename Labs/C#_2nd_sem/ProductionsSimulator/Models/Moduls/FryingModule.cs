using Production.Core.Data;
using Production.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuleType = Production.Core.Enums.ModuleType;

namespace Production.Models.Moduls
{
    internal class FryingModule : Module
    {
        public FryingModule(List<Conveyor> conveyors) : base(conveyors, ModuleType.Frying)
        {
        }

        protected override void TryDoSomething()
        {
            var product = _products.First();

            if (ProductData.ResultOfFrying.TryGetValue(product.ProductType, out var friedProductType))
            {
                _products.Remove(product);

                Task.Run(() =>
                {
                    Thread.Sleep(3000);

                    
                    product.ProductType = friedProductType; // Преобразуем продукт

                    // Отправляем дальше по конвейеру
                    _conveyors[0].AddProduct(product);
                });
            }
            else
                throw new Exception("На платформу по обжарке попал необжариваемый продукт!");
        }
    }
}
