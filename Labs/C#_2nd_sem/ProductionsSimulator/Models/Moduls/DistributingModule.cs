using Production.Core.Data;
using Production.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models.Moduls
{
    internal class DistributionModule : Module
    {
        public DistributionModule(List<Conveyor> conveyors) : base(conveyors, ModuleType.Distribution)
        {
        }

        protected override void TryDoSomething()
        {
            var product = _products.First();
            _products.Remove(product);

            // Определяем куда отправить продукт
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                Conveyor targetConveyor = DetermineTargetConveyor(product);
                targetConveyor.AddProduct(product);
            });
        }

        private Conveyor DetermineTargetConveyor(Product product)
        {
            // 1. Проверяем, нужно ли этот продукт жарить для получения целевого продукта
            if (NeedToFry(product))
            {
                var fryingConveyor = _conveyors.FirstOrDefault(c =>
                    c._targetModule is FryingModule);
                return fryingConveyor ?? throw new InvalidOperationException("No frying conveyor available!");
            }

            // 2. Проверяем, нужно ли этот продукт варить для получения целевого продукта
            if (NeedToBoil(product))
            {
                var boilingConveyor = _conveyors.FirstOrDefault(c =>
                    c._targetModule is BoilingModule);
                return boilingConveyor ?? throw new InvalidOperationException("No boiling conveyor available!");
            }

            throw new InvalidOperationException("No available end module!");
        }
        private bool NeedToFry(Product product)
        {
            // Проверяем, есть ли в рецепте целевого продукта жареный вариант этого продукта
            if (ProductData.RecipeBook.TryGetValue((ProductType)product.TargetProduct!, out var recipe))
            {
                foreach (var ingredient in recipe)
                {
                    if (ProductData.ResultOfFrying.TryGetValue(product.ProductType, out var friedProduct)
                        && ingredient == friedProduct)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool NeedToBoil(Product product)
        {
            // Проверяем, есть ли в рецепте целевого продукта вареный вариант этого продукта
            if (ProductData.RecipeBook.TryGetValue((ProductType)product.TargetProduct!, out var recipe))
            {
                foreach (var ingredient in recipe)
                {
                    if (ProductData.ResultOfBoiling.TryGetValue(product.ProductType, out var boiledProduct)
                        && ingredient == boiledProduct)
                    {
                        return true;
                    }
                }
            }
            return false;
        }   
    }
}
