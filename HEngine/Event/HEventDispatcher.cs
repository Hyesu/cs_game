using System.Collections.Generic;

namespace HEngine.Event
{
    public class HEventDispatcher
    {
        private readonly Dictionary<long, IHEventListener> _listeners = new();
    
        public void Subscribe(long id, IHEventListener listener)
        {
            _listeners.TryAdd(id, listener);
        }

        public void Unsubscribe(long id)
        {
            _listeners.Remove(id);
        }

        public void Publish<T>(T evt) where T : IHEvent
        {
            foreach (var listener in _listeners.Values)
            {
                listener.Listen(evt);
            }
        }
    }   
}