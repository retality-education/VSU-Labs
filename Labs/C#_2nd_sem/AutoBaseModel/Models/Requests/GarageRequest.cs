using AutoBaseModel.Core.Enums;
using AutoBaseModel.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Models.Requests
{
    internal class GarageRequest : Request  
    {
        public GarageRequestType Type { get; set; }
    }
}
