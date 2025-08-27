using AutoBase.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBase.Core.Interfaces
{
    internal interface IObservable
    {
        void Notify(ModelEventArgs args);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
    }
}
