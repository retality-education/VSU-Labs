// Florist.cs
using System;
using System.Collections.Concurrent;
using System.Threading;
using LandscapeDesign.ObserverPattern;
using LandscapeDesign.Enums;

namespace LandscapeDesign.Models
{
    internal class Designer
    {
        private Thread _thread;
        private ConcurrentQueue<DesignRequest> _requests = new();
        private readonly City _city;
        public Designer(City city)
        {
            _city = city;
            _thread = new Thread(Life);
            
            _thread.Start();
        }

        private void Life()
        {
            while (true)
            {
                if (_requests.TryDequeue(out var request))
                {
                    _city.Notify(new CityEventArgs
                    {
                        EventType = EventType.DesignerRidingToCity
                    }); // дизайнер выезжает в город
                    Thread.Sleep(2500);

                    foreach (var change in request)
                    {
                        _city.Notify(new CityEventArgs
                        {
                            EventType = EventType.DesignerComeToArea,
                            AreaId = change.AreaId
                        }); // дизайнер идёт к конкретной области
                        Thread.Sleep(2000);
                        _city.ChangeArea(change);
                    }

                    _city.Notify(new CityEventArgs { 
                        EventType = EventType.DesignerComeBackToShop
                    }); // дизайнер закончил и уезжает обратно
                    Thread.Sleep(2000);
                }
                Thread.Sleep(200);
            }
        }
        public void AddRequest(DesignRequest request) => _requests.Enqueue(request);
    }
}