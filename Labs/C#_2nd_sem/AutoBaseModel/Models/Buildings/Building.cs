using AutoBaseModel.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBaseModel.Models.Buildings
{
    internal abstract class Building
    {
        protected Model _model;

        protected object _requestLock = new object();
        protected List<Request> _requests = new();

        protected virtual bool CanHandleRequest(Request? request) { 
            if (request is null)
                return false;

            return _requests.Any(); 
        }  

        private Thread _thread;

        public Building(Model model)
        {
            _model = model;

            _thread = new Thread(Life);
            _thread.Start();
        }
        protected abstract void HandleRequest(Request request); // _requestLock 
        public virtual void AddRequest(Request request)
        {
            lock (_requestLock)
            {
                _requests.Add(request);
            }
        }
        protected virtual void RemoveRequest(Request request)
        {
            lock (_requestLock)
            {
                _requests.Remove(request);
            }
        }
        private void Life()
        {
            while (true)
            {
                lock (_requestLock)
                {
                    var request = _requests.FirstOrDefault();
                    if (CanHandleRequest(request))
                        HandleRequest(request!);
                }

                Thread.Sleep(100);
            }
        }
    }
}
