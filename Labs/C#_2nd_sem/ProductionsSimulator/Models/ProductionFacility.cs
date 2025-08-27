using Production.Core.Enums;
using Production.Core.Interfaces;
using Production.Models.Moduls;
using Production.Models.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models
{
    internal class ProductionFacility : IStartable
    {
        private readonly FinishedProductWarehouse _finishedWarehouse = new();

        // Модули
        private readonly StartModule _startModule;
        private readonly DistributionModule _distributionModule;
        private readonly FryingModule _fryingModule;
        private readonly BoilingModule _boilingModule;
        private readonly AssemblyModule _assemblyModule;
        private readonly FinalModule _finalModule;

        // Работники
        private readonly OrderWorker _orderWorker;
        private readonly DeliveryWorker _deliveryWorker;

        // Начальник
        private readonly Manager _manager;

        // Все стартуемые объекты
        private readonly List<IStartable> _startables = new();

        public ProductionFacility()
        {
            // 1. Создаем цепочку модулей
            _finalModule = new FinalModule();
            _assemblyModule = CreateAssemblyModule();
            _boilingModule = CreateBoilingModule();
            _fryingModule = CreateFryingModule();
            _distributionModule = CreateDistributionModule();
            _startModule = CreateStartModule();

            // 2. Создаем работников
            _orderWorker = new OrderWorker(_startModule);
            _deliveryWorker = new DeliveryWorker(_finishedWarehouse);

            // 3. Настройка начальника
            _manager = new Manager();
            _manager.NewOrderGenerated += _orderWorker.OnNewOrder;

            // 4. Подписка на события
            _finalModule.ProductArrived += _deliveryWorker.OnProductArrived;

            // 5. Сбор всех стартуемых объектов
            CollectStartables();
        }
        public IEnumerable<Module> GetAllModules()
        {
            return new Module[]
            {
            _startModule,
            _distributionModule,
            _fryingModule,
            _boilingModule,
            _assemblyModule,
            _finalModule
            };
        }

        public Manager GetManager() => _manager;
        public OrderWorker GetOrderWorker() => _orderWorker;
        public DeliveryWorker GetDeliveryWorker() => _deliveryWorker;   

        private StartModule CreateStartModule()
        {
            var conveyor = new Conveyor(ModuleType.Start, _distributionModule);
            return new StartModule(new List<Conveyor> { conveyor });
        }

        private DistributionModule CreateDistributionModule()
        {
            var fryingConveyor = new Conveyor(ModuleType.Distribution, _fryingModule);
            var boilingConveyor = new Conveyor(ModuleType.Distribution, _boilingModule);

            return new DistributionModule(new List<Conveyor>
            {
                fryingConveyor,
                boilingConveyor
            });
        }

        private FryingModule CreateFryingModule()
        {
            var conveyor = new Conveyor(ModuleType.Frying, _assemblyModule);
            return new FryingModule(new List<Conveyor> { conveyor });
        }

        private BoilingModule CreateBoilingModule()
        {
            var conveyor = new Conveyor(ModuleType.Boiling, _assemblyModule);
            return new BoilingModule(new List<Conveyor> { conveyor });
        }

        private AssemblyModule CreateAssemblyModule()
        {
            var conveyor = new Conveyor(ModuleType.Assembly, _finalModule);
            return new AssemblyModule(new List<Conveyor> { conveyor });
        }

        private void CollectStartables()
        {
            // Добавляем все модули
            _startables.Add(_startModule);
            _startables.Add(_distributionModule);
            _startables.Add(_fryingModule);
            _startables.Add(_boilingModule);
            _startables.Add(_assemblyModule);
            _startables.Add(_finalModule);

            // Добавляем все конвейеры
            foreach (var module in new Module[]
            {
                _startModule, _distributionModule,
                _fryingModule, _boilingModule, _assemblyModule
            })
            {
                _startables.AddRange(module._conveyors);
            }

            // Добавляем работников и начальника
            _startables.Add(_orderWorker);
            _startables.Add(_deliveryWorker);
            _startables.Add(_manager);
        }

        public void StartThread()
        {
            Console.WriteLine("Запуск производственного цеха...");
            foreach (var startable in _startables)
            {
                startable.StartThread();
            }
            Console.WriteLine("Все компоненты запущены");
        }
    }
}
