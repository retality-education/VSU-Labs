// Flower.cs
using LandscapeDesign.Enums;
using LandscapeDesign.ObserverPattern;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LandscapeDesign.Models
{
    internal class Flower
    {
        private readonly City _city;
        public FlowerState State { get; private set; }
        public readonly int FlowerId;
        public readonly int AreaId;

        private CancellationTokenSource? _cts;
        private FlowerType _type;

        public FlowerType Type
        {
            get => _type;
            set
            {
                // Всегда отменяем предыдущий цикл, если он был
                CancelCurrentLifeCycle();

                _type = value;

                _city.Notify(new CityEventArgs { 
                    EventType = EventType.FlowerChanged,
                    AreaId = AreaId,
                    FlowerId = FlowerId,
                    FlowerType = _type
                });

                // Запускаем новый цикл только если это реальный цветок
                if (value != FlowerType.NonFlower)
                {
                    State = FlowerState.Fresh;
                    StartLifeCycle();
                }
            }
        }

        public Flower(int flowerId, int areaId, City city, FlowerType flowerType = FlowerType.NonFlower)
        {
            AreaId = areaId;
            FlowerId = flowerId;
            _city = city;
         
            _type = flowerType; // Используем свойство для установки начального значения
        }

        private void StartLifeCycle()
        {
            _cts = new CancellationTokenSource();

            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(20), _cts.Token);
                    State = FlowerState.Wilted;
                    _city.Notify(new CityEventArgs
                    {
                        EventType = EventType.FlowerChangeStateToWilted,
                        AreaId = AreaId,
                        FlowerId = FlowerId,
                        FlowerType = Type,
                    }); 
                }
                catch (OperationCanceledException)
                {
                    // Нормальное поведение при отмене
                }
            }, _cts.Token);
        }

        private void CancelCurrentLifeCycle()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
    }
}