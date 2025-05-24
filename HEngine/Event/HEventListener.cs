using System;

namespace HEngine.Event
{
    public class HEventListener<T> : IHEventListener
        where T : class, IHEvent
    {
        private readonly Action<T> _impl;

        public HEventListener(Action<T> impl)
        {
            _impl = impl;
        }

        public void Listen(IHEvent evt)
        {
            var converted = evt as T;
            _impl.Invoke(converted!);
        }
    }   
}