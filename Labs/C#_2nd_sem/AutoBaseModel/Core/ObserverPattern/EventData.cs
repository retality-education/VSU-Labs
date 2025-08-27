using AutoBaseModel.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Core.ObserverPattern
{
    internal record EventData
    {
        public EventType EventType { get; set; }
        public decimal Money { get; set; }
        public int Time { get; set; }
    }
}
