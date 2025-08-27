using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Core.ObserverPattern
{
    internal interface IObserver
    {
        void Update(EventData eventData);
    }
}
