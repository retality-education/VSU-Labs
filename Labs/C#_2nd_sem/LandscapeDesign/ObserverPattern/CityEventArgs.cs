using LandscapeDesign.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapeDesign.ObserverPattern
{
    internal class CityEventArgs
    {
        public EventType EventType { get; set; }
        public int AreaId { get; set; } = -1;
        public int FlowerId { get; set; } = -1;
        public FlowerType FlowerType { get; set; } = FlowerType.NonFlower;
        public ObjectType ObjectType { get; set; } = ObjectType.NonObject;

    }
}
