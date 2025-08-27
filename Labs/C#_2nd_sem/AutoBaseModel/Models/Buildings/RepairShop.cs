using AutoBaseModel.Core.Enums;
using AutoBaseModel.Core.ObserverPattern;
using AutoBaseModel.Models.Persons;
using AutoBaseModel.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Models.Buildings
{
    internal class RepairShop : Building
    {
        private object _employeeLock = new object();
        private List<Employee> _employees = new();
        public RepairShop(Model model) : base(model){}

        protected override bool CanHandleRequest(Request? request)
        {
             if (request is null) 
                return false;

            return _employees.Any();
        }
        public void AddEmployee(Employee employee)
        {
            lock (_employeeLock)
            {
                _employees.Add(employee);
            }
        }
        private void RepairTowCase(int Time)
        {
            lock (_employees)
            {
                var emp = _employees.First();

                emp.RepairCarTowCase(Time);

                _employees.Remove(emp);
            }
        }
        private void RepairSimpleCase(int Time)
        {
            lock (_employees)
            {
                var emp = _employees.First();

                emp.RepairCarSimpleCase(Time);

                _employees.Remove(emp);
            }
        }
        protected override void HandleRequest(Request request)
        {
            var repairRequest = request as RepairRequest;

            int timeToRepair = 1500;

            switch (repairRequest!.Type)
            {
                case RepairRequestType.TowCase:
                    RepairTowCase(timeToRepair);
                    break;
                case RepairRequestType.SimpleRepair:
                    RepairSimpleCase(timeToRepair);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            _requests.Remove(request);
        }
    }
}
