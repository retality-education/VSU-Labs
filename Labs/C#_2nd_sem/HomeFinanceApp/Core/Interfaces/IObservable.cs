using HomeFinanceApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Core.Interfaces
{
    internal interface IObservable
    {
        void Notify(FamilyEvents familyEvents, 
            int memberId = 0, 
            decimal summa = 0);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
    }
}
