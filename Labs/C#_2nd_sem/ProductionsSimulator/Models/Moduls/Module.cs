    using Production.Core.Enums;
using Production.Core.Interfaces;
using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Production.Models.Moduls
    {
        internal abstract class Module : IStartable
        {
            public event Action<int, ModuleType> ModuleCreated;
            public int Id { get; } = AllId++;
            private static int AllId = 0;

            public ModuleType moduleType { get; private set; }
            private readonly object _lockProducts = new object();
            protected List<Product> _products { get; private set; } = new();

            // public чтобы можно было по цепочке запустить все конвейеры 
            public readonly List<Conveyor> _conveyors;

            private Thread _thread;
            public Module(List<Conveyor> conveyors, ModuleType moduleType)
            {
                _conveyors = conveyors;
                _thread = new Thread(Work);
                this.moduleType = moduleType;
            }
            public void StartThread()
            {
                if (!_thread.IsAlive)
                {
                    _thread.Start();
                    ModuleCreated?.Invoke(Id, moduleType);
                }
           }
            public void MoveProduct(Product product) {
                lock (_lockProducts)
                {
                    _products.Add(product);
                }
            }

            protected abstract void TryDoSomething();
            private void Work()
            {
                while (true)
                {
                    lock (_lockProducts)
                    {
                        if (_products.Any())
                        {
                            TryDoSomething();
                        }
                        Thread.Sleep(150);
                    }
                }
            }
        }
    }
