using AutoBaseModel.Models;
using AutoBaseModel.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Controllers
{
    internal class Controller
    {
        private Model _model;
        private MainForm _view;
        public Controller(Model model, MainForm view)
        {
            _model = model;
            _view = view;

            _view.controller = this;
            _model.Subscribe(_view);
        }
        public void Start()
        {
            _model.Start();
        }
    }
}
