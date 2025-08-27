using HomeFinanceApp.Models;

using HomeFinanceApp.Views.Forms;
using HomeFinanceApp.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Controllers
{
    internal class FinanceController
    {
        private Family _model;
        private FinanceForm _view;

        public FinanceController(Family model, FinanceForm view)
        {
             _model = model;
            _view = view;
            
            _model.Subscribe(view);
            _view.financeController = this;
        }
        public void Start()
        {
            _model.StartSimulation();
        }
        public Stat GetStatByRoleId(int roleID)
        {
            var result = _model._familyMembers.First(x => (int)x.memberRole == roleID).stat;
            return result;
        }
        public List<Stat> GetStatOfAllMembers() 
        {
            var result = new List<Stat>();
            foreach (var member in _model._familyMembers)
                result.Add(member.stat);

            return result;
        }
    }
}
