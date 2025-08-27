using Production.Core.Enums;
using Production.Core.Interfaces;
using Production.Models.Moduls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models
{
    internal class Conveyor : IStartable
    {
        public event Action<int, ModuleType, ModuleType> ConveyorCreated;

        // Conveyor_Id + Module_Id + type_of_product
        public event Action<int, int, ProductType>? ProductStartMoveOnConveyor;
        public int Id { get; } = AllId++;

        private static int AllId = 0;
        private List<Product> _products = new();
        private object _lockProducts = new object();
        private Thread _thread;
        public ModuleType _startModule { get; private set; }
        public Module? _targetModule { get; private set; }
        public Conveyor(ModuleType startModuleType, Module targetModule)
        {
            _startModule = startModuleType;

            _targetModule = targetModule;

            _thread = new Thread(Work);

        }
        public void StartThread()
        {
            if (!_thread.IsAlive)
            {
                ConveyorCreated?.Invoke(Id, _startModule, _targetModule!.moduleType);
                _thread.Start();
            }
        }
        public void AddProduct(Product product)
        {
            lock (_lockProducts)
            {
                _products.Add(product);
            }
        }
        public void SetStartModule(ModuleType moduleType)
        {
            _startModule = moduleType;
        }
        public void SetTargetModule(Module module)
        {
            _targetModule = module;
        }
        private void Work()
        {
            while (true)
            {
                lock (_lockProducts)
                {
                    if (_products.Any())
                    {
                        var temp = _products.First()!;
                        _products.Remove(temp);

                        Task.Run(() =>
                        {
                            if (_targetModule is not null)
                            {
                                ProductStartMoveOnConveyor?.Invoke(Id, _targetModule.Id, temp.ProductType);
                                Thread.Sleep(2000);
                                _targetModule.MoveProduct(temp);
                            }
                            else
                                throw new TargetModuleNotExistException("У конвейера нет конечного модуля!");
                        });
                    }
                }
                Thread.Sleep(100);
            }
        }

    }
    internal class TargetModuleNotExistException : Exception
    {
        public TargetModuleNotExistException(string? message) : base(message) { }

    }
}
