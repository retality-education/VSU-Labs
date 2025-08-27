using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Core.Enums
{
    internal enum FamilyEvents
    {
        CreateMember, //(MemberId)

        StartNewMonth,

        MemberDropMoney, //(MemberId, count) 
        MemberGetMoney, //(MemberId, count)
        MemberInputMoneyToSavings, //(MemberId, count)
        MemberNeedExtraMoneyFromMoneyBox, //(MemberId, count)

        FamilyPostponedMoney, //(count)

        SavingsValueChanged,
        AmountValueChanged,

        GatherEnded
    }
}
