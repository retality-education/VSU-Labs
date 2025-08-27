using HomeFinanceApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Core.Interfaces
{
    internal interface IObserver
    {
        void OnFamilyEvent(FamilyEvents eventType, int memberId = -1, decimal money = 0);
    }
}
