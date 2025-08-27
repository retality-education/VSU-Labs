using AutoBase.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBase.Model.Cars
{
    internal class Car
    {
        private static int Global_Id = 0;
        public int Id { get; private set; }
        public CarType Type { get; protected set; }
        public CarCondition Condition { get; set; }
        public Car(CarType type,CarCondition condition) {
            Id = Global_Id;
            
            Type = type;
            Condition = condition;
            
            Global_Id++;
        }
        public Car() { }

        public void BecameRepaired()
        {
            Condition = CarCondition.Working;
        }
        public void BecameBroken()
        {
            Condition = CarCondition.Broken;
        }
    }
}
