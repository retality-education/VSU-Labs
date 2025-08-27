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
    internal class DeliveryWorker : IStartable
    {
        private readonly FinishedProductWarehouse _warehouse;
        public event Action<ProductType> WorkerAction;

        public DeliveryWorker(FinishedProductWarehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public void OnProductArrived(Product product)
        {
            WorkerAction?.Invoke(product.ProductType);
            Thread.Sleep(2000);
            _warehouse.AddProduct(product);
        }

        public void StartThread()
        {
            Console.WriteLine("Работник по доставке готов к работе");
        }
    }
}
