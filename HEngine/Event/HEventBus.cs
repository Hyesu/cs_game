using System;
using System.Collections.Concurrent;
using HEngine.Utility;

namespace HEngine.Event
{
    public class HEventBus
    {
        private HAtomicLong _handleIdGenerator = new(0);
        private ConcurrentDictionary<int, HEventDispatcher> _dispatchers = new();
    
        public HEventHandle Subscribe<T>(Action<T> listener) where T : class, IHEvent
        {
            var id = _handleIdGenerator.Increment();
            var eventHash = typeof(T).GetHashCode();
            var handle = new HEventHandle(id, eventHash, this);
            var dispatcher = _dispatchers.GetOrAdd(eventHash, _ => new HEventDispatcher());
        
            dispatcher.Subscribe(handle.Id, new HEventListener<T>(listener));
            return handle;
        }

        public void Unsubscribe(HEventHandle handle)
        {
            if (!_dispatchers.TryGetValue(handle.EventHash, out var dispatcher))
            {
                return;
            }
        
            dispatcher.Unsubscribe(handle.Id);
        }

        public void Publish<T>(T evt) where T : IHEvent
        {
            var eventHash = typeof(T).GetHashCode();
            if (!_dispatchers.TryGetValue(eventHash, out var dispatcher))
            {
                return;
            }
        
            dispatcher.Publish(evt);
        }
    }   
}