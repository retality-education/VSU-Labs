using Production.Core.Data;
using Production.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models.Moduls
{
    internal class AssemblyModule : Module
    {
        // Группировка продуктов по их целевому блюду
        private readonly Dictionary<ProductType, List<Product>> _productsByTarget = new();

        public AssemblyModule(List<Conveyor> conveyors) : base(conveyors, ModuleType.Assembly)
        {
        }

        protected override void TryDoSomething()
        {
            var product = _products.First();
            _products.Remove(product);

            // Если у продукта не указано целевое блюдо - игнорируем его
            if (product.TargetProduct == null)
                return;

            var targetDish = product.TargetProduct.Value;

            // Инициализируем список для этого целевого блюда при необходимости
            if (!_productsByTarget.ContainsKey(targetDish))
            {
                _productsByTarget[targetDish] = new List<Product>();
            }

            // Добавляем продукт в соответствующую группу
            _productsByTarget[targetDish].Add(product);

            // Проверяем возможность сборки блюда
            CheckForCompletion(targetDish);
        }

        private void CheckForCompletion(ProductType targetDish)
        {
            // Получаем рецепт для целевого блюда
            if (!ProductData.RecipeBook.TryGetValue(targetDish, out var requiredIngredients))
                return;

            // Получаем собранные ингредиенты для этого блюда
            if (!_productsByTarget.TryGetValue(targetDish, out var collectedProducts))
                return;

            // Группируем собранные продукты по типам
            var collectedGroups = collectedProducts
                .GroupBy(p => p.ProductType)
                .ToDictionary(g => g.Key, g => g.Count());

            // Проверяем, что собраны все необходимые ингредиенты в нужном количестве
            var requiredGroups = requiredIngredients
                .GroupBy(i => i)
                .ToDictionary(g => g.Key, g => g.Count());

            bool canProduce = requiredGroups.All(required =>
                collectedGroups.TryGetValue(required.Key, out var count) &&
                count >= required.Value);

            if (canProduce)
            {
                // Удаляем использованные ингредиенты
                foreach (var ingredient in requiredIngredients)
                {
                    var productToRemove = collectedProducts.First(p => p.ProductType == ingredient);
                    collectedProducts.Remove(productToRemove);
                }

                // Если список собранных продуктов для этого блюда пуст - удаляем запись
                if (collectedProducts.Count == 0)
                {
                    _productsByTarget.Remove(targetDish);
                }

                Task.Run(() =>
                {
                    Thread.Sleep(2000);

                    // Создаем готовое блюдо (без TargetProduct, так как это конечный продукт)
                    var finalProduct = new Product(targetDish, null);

                    // Отправляем на выход
                    _conveyors[0].AddProduct(finalProduct);
                });
            }
        }
    }
}
