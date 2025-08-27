using AutoBase.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBase.Model.CarData
{
    internal class CarRequest
    {
        public CarType NeedCarType { get; set; }
        public CarRequest(CarType needCarType)
        {
            NeedCarType = needCarType;
        }
    }
}
