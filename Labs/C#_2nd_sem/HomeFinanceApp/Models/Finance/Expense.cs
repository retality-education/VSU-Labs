using HomeFinanceApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Models.Finance
{
    internal struct Expense
    {
        public ExpenseTypes ExpenseTypes { get; set; }
        public ExpenseSubTypes ExpenseSubTypes { get; set; }
        public Expense(ExpenseTypes expenseTypes, ExpenseSubTypes expenseSubTypes)
        {
            ExpenseTypes = expenseTypes;
            ExpenseSubTypes = expenseSubTypes;
        }
    }
}
