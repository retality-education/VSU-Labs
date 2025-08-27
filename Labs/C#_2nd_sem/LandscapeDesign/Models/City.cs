// City.cs
using LandscapeDesign.ObserverPattern;
using System;
using System.Collections.Generic;
using LandscapeDesign.Enums;
using System.Formats.Asn1;

namespace LandscapeDesign.Models
{
    internal class City : IObservable
    {
        private Mayor _mayor;
        private readonly List<Area> _areas = new();
        private readonly Designer _florist;
        private int _nextAreaId = 0;
        public const int COUNT_OF_AREAS = 3;

        private List<IObserver> _observers = new();
    
        public City()
        {
            _florist = new Designer(this);
            _mayor = new Mayor(this);
        }
        public List<Area> GetAreas() => _areas;
        public void SendRequestToFlorist(DesignRequest designRequest)
        {
            _florist.AddRequest(designRequest);
        }
        public void ChangeArea(AreaChange areaChange)
        {
            var area = _areas[areaChange.AreaId];


            if (areaChange.NewObjectType is not null)
                area.SetMainObject((ObjectType)areaChange.NewObjectType);

            Thread.Sleep(500);

            if (areaChange.FlowerChanges is not null) {
                foreach (var flowerChange in areaChange.FlowerChanges)
                {
                    // с помощью свойства оповещение происходит внутри цветка
                    area._flowers[flowerChange.FlowerId].Type = flowerChange.FlowerType;
                    Thread.Sleep(1000); // задержка между установками новых цветов
                }
            }

        }
        public void Start()
        {
            Task.Run(() => {
                GenerateAreas(COUNT_OF_AREAS);
                
                Thread.Sleep(500);

                _mayor.Start();
            });
        }

        public void Notify(CityEventArgs e)
        {
            foreach (var observer in _observers)
            {
                observer.OnCityEvent(e);
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        private void GenerateAreas(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var area = new Area(this, _nextAreaId++);
                _areas.Add(area);
            }
        }
    }
}