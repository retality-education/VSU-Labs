using Production.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models
{
    internal class FinishedProductWarehouse
    {
        private readonly Dictionary<ProductType, int> _finishedProducts = new();
        private readonly object _lock = new();

        public void AddProduct(Product product)
        {
            lock (_lock)
            {
                if (_finishedProducts.ContainsKey(product.ProductType))
                {
                    _finishedProducts[product.ProductType]++;
                }
                else
                {
                    _finishedProducts[product.ProductType] = 1;
                }
                Console.WriteLine($"На склад добавлено: {product.ProductType}. Теперь: {_finishedProducts[product.ProductType]} шт.");
            }
        }
    }
}
