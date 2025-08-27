using AutoBase.Model;
using AutoBase.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBase.Controller
{
    internal class AutoBaseController
    {
        AutoBaseModel _autoBase;
        AutoBaseForm _autoBaseForm;

        public AutoBaseController(AutoBaseModel autoBase, AutoBaseForm autoBaseForm)
        {
            _autoBase = autoBase;
            _autoBaseForm = autoBaseForm;

            Configure();
        }

        private void Configure()
        {
            _autoBaseForm.autoBaseController = this;
            _autoBase.Subscribe(_autoBaseForm);
        }
        public void Start()
        {
            _autoBase.Start();
        }
    }
}
