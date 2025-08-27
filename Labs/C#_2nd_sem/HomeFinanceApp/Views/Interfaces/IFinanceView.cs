using HomeFinanceApp.Core.Enums;
using HomeFinanceApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Views.Interfaces
{
    internal interface IFinanceView : IObserver
    {
        void CreateMember(int memberId, int roleId);
        void SavingsChanged(decimal money);
        void AmountChanged(decimal money);
        void StartNewMonth();
        void MemberDropMoney(int memberId, decimal money);
        void MemberGetMoney(int memberId, decimal money);
        void MemberInputMoneyToSavings(int memberId, decimal money);
        void MemberNeedExtraMoneyFromMoneyBox(int memberId, decimal money);
        void FamilyPostponedMoney(decimal money);
        void GatherEnded();
    }
}
