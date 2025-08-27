using HomeFinanceApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Models.Finance
{
    internal class Credit
    {
        public ExpenseTypes ExpenseType { get; }
        public ExpenseSubTypes ExpenseSubType { get; }
        public decimal OriginalAmount { get; }
        public decimal RemainingAmount { get; private set; }
        public bool IsPaid => RemainingAmount <= 0;

        public Credit(ExpenseTypes expenseType, ExpenseSubTypes expenseSubType, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive");

            ExpenseType = expenseType;
            ExpenseSubType = expenseSubType;
            OriginalAmount = amount;
            RemainingAmount = amount;
        }

        public decimal MakePayment(decimal money)
        {

            if (RemainingAmount <= money)
            {
                decimal remaining = money - RemainingAmount;
                RemainingAmount = 0;
                return remaining;
            }

            RemainingAmount -= money;
            return 0;
        }
    }
}
