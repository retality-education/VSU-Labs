using AutoBase.Model.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBase.Model.Peoples
{
    internal class Guest
    {
        public Car Car { get; private set; }
        public Guest(Car car) { 
            Car = car;
        }
    }
}
