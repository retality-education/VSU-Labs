using HomeFinanceApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Services
{
    internal static class ExpenseCalculator
    {
        private static readonly Random _random = new Random();

        public static Dictionary<ExpenseSubTypes, decimal> CalculateExpensePrices()
        {
            var prices = new Dictionary<ExpenseSubTypes, decimal>();

            // Генерация цен для всех подтипов
            foreach (ExpenseSubTypes subtype in Enum.GetValues(typeof(ExpenseSubTypes)))
            {
                int baseValue = (int)subtype / 10; // Получаем основной тип (первая цифра)
                int subtypeValue = (int)subtype % 10; // Получаем подтип (вторая цифра)

                // Генерируем случайный множитель 1.xxxx
                decimal multiplier = 1 + _random.Next(1000, 10000) / 10m;

                // Рассчитываем цену: базовый тип * подтип * множитель
                decimal price = baseValue * subtypeValue * multiplier;

                prices.Add(subtype, Math.Round(price, 2));
            }

            return prices;
        }
    }
}
