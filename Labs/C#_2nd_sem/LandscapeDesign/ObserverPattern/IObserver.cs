using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapeDesign.ObserverPattern
{
    internal interface IObserver
    {
        void OnCityEvent(CityEventArgs e);
    }
}
