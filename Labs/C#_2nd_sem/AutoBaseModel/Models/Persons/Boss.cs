using AutoBaseModel.Core.Enums;
using AutoBaseModel.Core.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Models.Persons
{
    internal class Boss
    {
        private Thread _thread;
        private Model _model;
        public Boss(Model model)
        {
            _model = model;
            _thread = new Thread(Life);

            _thread.Start();
        }
        private void Life()
        {
            while (true)
            {
                _model.Notify(new EventData { EventType = EventType.BossScreaming });
                Thread.Sleep(5000);
            }
        }
    }
}
