// Area.cs
using LandscapeDesign.Enums;
using LandscapeDesign.ObserverPattern;
using System;
using System.Collections.Generic;

namespace LandscapeDesign.Models
{
    internal class Area
    {
        
        private City _city;
        public int AreaId { get; private set; }
        public LandscapeObject _mainObject { get; set; }

        private const int COUNT_FLOWERS = 3;
        public Flower[] _flowers = new Flower[COUNT_FLOWERS];

        public Area(City city, int areaId)
        {
            AreaId = areaId;

            _city = city;

            _mainObject = new LandscapeObject();
            _city.Notify(new CityEventArgs { 
                EventType = EventType.CreatePlaceForObject, 
                AreaId = AreaId
            }); // разместить пустой объект

            for (int i = 0; i < 3; i++)
            {
                _flowers[i] = new Flower(i, AreaId, _city);
                _city.Notify(new CityEventArgs { 
                    EventType = EventType.CreatePlaceForFlower, 
                    FlowerId = i, 
                    AreaId = AreaId
                }); // разместить 3 горшка пустых
            }
        }

        public void SetMainObject(ObjectType obj)
        {
            _city.Notify(new CityEventArgs { 
                EventType = EventType.MainObjectStartBuilding, 
                AreaId = AreaId 
            }); // начало строительства главного объекта
            
            Thread.Sleep(2000);
            
            _mainObject.ChangeObject(obj);

            Thread.Sleep(200);

            _city.Notify(new CityEventArgs { 
                EventType = EventType.MainObjectEndBuilding, 
                AreaId = AreaId, 
                ObjectType = _mainObject.Type 
            }); // закончено строительство
        }
    }
}