using HomeFinanceApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Models.Finance
{
    internal class Income
    {
        public IncomeTypes IncomeType { get; set; }
        public decimal IncomeAmount { get; set; }
        public Income(IncomeTypes incomeType, decimal incomeAmount)
        {
            IncomeType = incomeType;
            IncomeAmount = incomeAmount;
        }
    }
}
