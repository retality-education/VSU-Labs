using AutoBaseModel.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Models.Requests
{
    internal class WorkerRequest : Request
    {
        public DestinationOfWorker destination { get; set; }
    }
}
