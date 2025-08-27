using HomeFinanceApp.Core.Enums;
using HomeFinanceApp.Models.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Factories
{
    internal static class FinanceFactory
    {
        private static readonly Random _random = new Random();
        public static Credit CreateCredit(Expense expense, decimal amount)
        {
            return new Credit(expense.ExpenseTypes, expense.ExpenseSubTypes, amount);
        }

        public static Expense CreateExpense(ExpenseTypes type, ExpenseSubTypes subType)
        {
            return new Expense
            {
                ExpenseTypes = type,
                ExpenseSubTypes = subType
            };
        }

        public static Income CreateIncome(IncomeTypes type, decimal amount)
        {
            return new Income(type, amount);
        }

        public static Expense CreateNewRandomExpense(List<Expense> existingExpenses)
        {
            // Получаем все существующие подтипы, сгруппированные по типам
            var existingSubTypesByType = existingExpenses
               
                .GroupBy(e => e.ExpenseTypes)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ExpenseSubTypes).ToList()
                );

            // Проверяем типы по порядку от Utilities (1) до Hobby (6)
            for (int typeValue = 1; typeValue <= 7; typeValue++)
            {
                var currentType = (ExpenseTypes)typeValue;

                // Получаем существующие подтипы для этого типа или пустой список
                existingSubTypesByType.TryGetValue(currentType, out var existingSubTypes);
                existingSubTypes ??= new List<ExpenseSubTypes>();

                // Все возможные подтипы для текущего типа
                var allSubTypesForType = Enum.GetValues(typeof(ExpenseSubTypes))
                    .Cast<ExpenseSubTypes>()
                    .Where(st => (int)st / 10 == typeValue)
                    .ToList();

                // Находим недостающие подтипы
                var missingSubTypes = allSubTypesForType
                    .Except(existingSubTypes)
                    .ToList();

                // Если есть недостающие подтипы - выбираем случайный
                if (missingSubTypes.Any())
                {
                    var subTypeToAdd = missingSubTypes[_random.Next(missingSubTypes.Count)];
                    return CreateExpense(currentType, subTypeToAdd);
                }

                // Если в этом типе уже есть 2+ подтипа - переходим к следующему
            }

            // Если во всех типах уже по 2+ подтипа - выбираем полностью случайный
            var randomType = (ExpenseTypes)_random.Next(2, 8); // 2-7 (без Food)
            var allSubTypes = Enum.GetValues(typeof(ExpenseSubTypes))
                .Cast<ExpenseSubTypes>()
                .Where(st => (int)st / 10 == (int)randomType)
                .ToList();

            return CreateExpense(randomType, allSubTypes[_random.Next(allSubTypes.Count)]);
        }

        public static Income CreateRandomIncome()
        {
            int chance = _random.Next(100);

            if (chance < 5) // 5% на лотерею
            {
                return new Income(IncomeTypes.Lottery, _random.Next(5000, 50001));
            }
            else if (chance < 15) // 10% на подарок (5-15)
            {
                return new Income(IncomeTypes.Gift, _random.Next(1000, 10001));
            }
            else if (chance < 85) // 70% на доп. работу (только для взрослых)
            {
                return new Income(IncomeTypes.Salary, _random.Next(5000, 20001));
            }
            else // 15% ничего не получаем
            {
                return new Income(IncomeTypes.SocialPayment, _random.Next(1000, 3001));
            }
        }
        public static List<Expense> GetInitialExpenses(MemberRole role)
        {
            var expenses = new List<Expense>
        {
            // У всех должна быть еда
            CreateExpense(ExpenseTypes.Food, ExpenseSubTypes.Food)
        };

            // Добавляем 1-2 случайные потребности в зависимости от роли
            int count = _random.Next(1, 5);

            for (int i = 0; i < count; i++)
            {
                // Исключаем еду, так как она уже есть
                var possibleTypes = Enum.GetValues(typeof(ExpenseTypes))
                    .Cast<ExpenseTypes>()
                    .Where(t => t != ExpenseTypes.Food)
                    .ToList();

                var randomType = possibleTypes[_random.Next(possibleTypes.Count)];
                var subTypes = Enum.GetValues(typeof(ExpenseSubTypes))
                    .Cast<ExpenseSubTypes>()
                    .Where(st => (int)st / 10 == (int)randomType)
                    .ToList();

                var randomSubType = subTypes[_random.Next(subTypes.Count)];

                expenses.Add(CreateExpense(randomType, randomSubType));
            }

            return expenses;
        }
    }
}
