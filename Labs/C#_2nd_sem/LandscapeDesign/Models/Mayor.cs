// Mayor.cs
using LandscapeDesign.Enums;
using LandscapeDesign.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LandscapeDesign.Models
{
    internal class Mayor
    {
        private City _city;
        private Random _random = new();
        private Array _objectTypes = Enum.GetValues(typeof(ObjectType));
        private Array _flowerTypes = Enum.GetValues(typeof(FlowerType));
        private Thread _thread;

        public Mayor(City city)
        {
            _city = city;
            _thread = new Thread(Life);
        }
        public void Start()
        {
            _thread.Start();
        }
        private void Life()
        {
            while (true)
            {
                _city.Notify(new CityEventArgs {
                    EventType = EventType.MayorComingToCity
                }); //идёт в город, доходит до места между областями
                Thread.Sleep(3000);

                var designRequest = GenerateDesignRequest();
                _city.Notify(new CityEventArgs
                {
                    EventType = EventType.MayorGeneratedIdea
                }); // показываем лампочку над головой(придумал идею)
                Thread.Sleep(500); // стоит с лампочкой

                _city.Notify(new CityEventArgs { 
                    EventType = EventType.MayorGoingToDesigner
                }); // идёт к дизайнерам
                Thread.Sleep(2500); 
                _city.SendRequestToFlorist(designRequest); // передаёт запрос дизайнерам

                _city.Notify(new CityEventArgs { 
                    EventType = EventType.MayorGoingAwayFromCity
                }); // уходит из города
                Thread.Sleep(30000);
            }
        }


        private DesignRequest GenerateDesignRequest()
        {
            var changes = new List<AreaChange>();
            foreach (var area in _city.GetAreas())
            {
                var flowerChanges = new List<FlowerChange>();
                for (int i = 0; i < area._flowers.Length; i++)
                {
                    if (_random.Next(2) == 0) continue;

                    flowerChanges.Add(new FlowerChange(
                        flowerId: i,
                        flowerType: (FlowerType)_flowerTypes.GetValue(_random.Next(_flowerTypes.Length))
                    ));
                }

                if (!flowerChanges.Any())
                    flowerChanges = null;

                changes.Add(new AreaChange(
                    areaId: area.AreaId,
                    newObjectType: _random.Next(2) == 0 ? null : (ObjectType)_objectTypes.GetValue(_random.Next(_objectTypes.Length)),
                    flowerChanges
                ));
            }
            return new DesignRequest(changes);
        }
    }
}