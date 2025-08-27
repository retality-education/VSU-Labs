using LandscapeDesign.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapeDesign.Models
{
    internal class FlowerChange
    {
        public int FlowerId { get; }
        public FlowerType FlowerType { get; }

        public FlowerChange(int flowerId, FlowerType flowerType)
        {
            FlowerId = flowerId;
            FlowerType = flowerType;
        }
    }
}
