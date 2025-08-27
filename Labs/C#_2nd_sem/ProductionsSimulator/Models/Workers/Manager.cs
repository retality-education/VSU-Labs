using Production.Core.Data;
using Production.Core.Enums;
using Production.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models.Workers
{
    internal class Manager : IStartable
    {
        private readonly Random _random = new Random();
        public event Action<ProductType>? NewOrderGenerated;

        public void StartThread ()
        {
            new Thread(GenerateOrders).Start();
        }

        private void GenerateOrders()
        {
            while (true)
            {
                var delay = _random.Next(6000, 12000); // 6-12 секунд
                Thread.Sleep(delay);

                var possibleOrders = new List<ProductType>(ProductData.RecipeBook.Keys);
                var randomOrder = possibleOrders[_random.Next(possibleOrders.Count)];

                NewOrderGenerated?.Invoke(randomOrder);
            }
        }
    }
}
