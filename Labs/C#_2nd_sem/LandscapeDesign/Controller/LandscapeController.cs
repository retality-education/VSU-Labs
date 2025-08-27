// LandscapeController.cs
using LandscapeDesign.Models;
using LandscapeDesign.View;
using System;

namespace LandscapeDesign.Controller
{
    internal class LandscapeController
    {
        private City _city;
        private FormView _view;

        public LandscapeController(City city, FormView formView)
        {
            _view = formView;
            _city = city; // Передаем обработчик событий в модель

            _city.Subscribe(_view);
            _view.controller = this;
        }

        public void Start()
        {
            _city.Start();
        }
    }
}