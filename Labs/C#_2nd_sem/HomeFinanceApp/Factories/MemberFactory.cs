using HomeFinanceApp.Core.Enums;
using HomeFinanceApp.Models;
using HomeFinanceApp.Models.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Factories
{
    internal static class MemberFactory
    {
        public static FamilyMember CreateFamilyMember(string name, MemberRole role, Family family,
                ManualResetEvent m1, ManualResetEvent m2)
        {
            var member = new FamilyMember(name, role, family, m1, m2);

            // Добавляем начальные потребности
            member.expenses = FinanceFactory.GetInitialExpenses(role);

            // Добавляем начальный доход для взрослых
            if (role == MemberRole.Father || role == MemberRole.Mother)
            {
                member.incomes.Add(new Income(IncomeTypes.Salary, role == MemberRole.Father ? 30000 : 20000));
            }

            return member;
        }

    }
}
