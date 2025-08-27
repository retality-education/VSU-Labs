using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapeDesign.ObserverPattern
{
    internal interface IObservable
    {
        void Notify(CityEventArgs e);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
    }
}
