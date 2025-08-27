using LandscapeDesign.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapeDesign.Models
{
    internal class AreaChange
    {
        public int AreaId { get; }
        public ObjectType? NewObjectType { get; } // null значит ничего не делать
        public List<FlowerChange>? FlowerChanges { get; } // null значит ничего не делать

        public AreaChange(int areaId, ObjectType? newObjectType, List<FlowerChange>? flowerChanges)
        {
            AreaId = areaId;
            NewObjectType = newObjectType;
            FlowerChanges = flowerChanges;
        }
    }
}
