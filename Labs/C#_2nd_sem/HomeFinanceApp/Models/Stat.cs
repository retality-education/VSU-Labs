using HomeFinanceApp.Core.Enums;
using HomeFinanceApp.Models.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Models
{
    internal class Stat
    {
        public List<(Credit, decimal)> wasteMoneyOnCredits = new();
        public List<(Expense, decimal)> wasteMoneyOnExpenses = new();
        public List<Income> incomeMoneyToFamily = new();
        public void Clear()
        {
            wasteMoneyOnCredits.Clear();
            incomeMoneyToFamily.Clear();
            wasteMoneyOnExpenses.Clear();
        }
    }
}
