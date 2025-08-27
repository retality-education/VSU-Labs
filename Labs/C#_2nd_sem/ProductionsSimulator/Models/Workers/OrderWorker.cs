using Production.Core.Data;
using Production.Core.Enums;
using Production.Core.Interfaces;
using Production.Models.Moduls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models.Workers
{
    internal class OrderWorker : IStartable
    {
        private readonly StartModule _startModule;
        public event Action<string> WorkerAction;   
        public OrderWorker(StartModule startModule)
        {
            _startModule = startModule;
        }

        public void OnNewOrder(ProductType dishType)
        {
            WorkerAction?.Invoke($"Принял заказ на {dishType}");

            Task.Run(() =>
            {
                Thread.Sleep(2000);
                if (ProductData.RecipeBook.TryGetValue(dishType, out var requiredProcessedIngredients))
                {
                    // Для каждого обработанного ингредиента находим исходное сырье
                    foreach (var processedIngredient in requiredProcessedIngredients)
                    {
                        // Ищем исходный продукт для жареного ингредиента
                        var rawProductForFried = ProductData.ResultOfFrying
                            .FirstOrDefault(x => x.Value == processedIngredient).Key;

                        // Ищем исходный продукт для вареного ингредиента
                        var rawProductForBoiled = ProductData.ResultOfBoiling
                            .FirstOrDefault(x => x.Value == processedIngredient).Key;

                        // Определяем какое сырье нужно (жареное или вареное)
                        var rawProductType = rawProductForFried != default
                            ? rawProductForFried
                            : rawProductForBoiled;

                        if (rawProductType != default)
                        {
                            // Создаем продукт с указанием целевого блюда
                            var rawProduct = new Product(rawProductType, dishType);
                            _startModule.MoveProduct(rawProduct);
                            Console.WriteLine($"Отправлено сырье: {rawProductType} для блюда: {dishType}");
                        }
                        else
                        {
                            Console.WriteLine($"Не найдено сырье для ингредиента: {processedIngredient}");
                        }
                    }
                }
            });
        }

        public void StartThread()
        {
            Console.WriteLine("Работник по заказам готов к работе");
        }
    }
}
