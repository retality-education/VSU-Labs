using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Core.ObserverPattern
{
    internal interface IObservable
    {
        void Notify(EventData eventData);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
    }
}
