using AutoBase.Core.Enums;
using AutoBase.Model.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBase.Core.Events
{
    internal record ModelEventArgs
    {
        public EventType EventType { get; set; }
        public CarDto? CarInfo { get; set; }
        public ModelEventArgs(EventType eventType, Car? c = null)
        {
            EventType = eventType;
            if (c != null) 
                CarInfo = new CarDto(c);
        }
      
    }
    internal record CarDto
    {
        public int Id { get; private set; }
        public CarType Type { get; private set; }
        public CarCondition Condition { get; private set; }
        public CarDto(Car c)
        {
            Id = c.Id;
            Type = c.Type;
            Condition = c.Condition;
        }
    }
}
