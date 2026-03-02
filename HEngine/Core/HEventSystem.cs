using System;
using HEngine.Event;

namespace HEngine.Core
{
    public class HEventSystem : HSystem
    {
        private readonly HEventBus _eventBus = new();

        public HEventHandle Subscribe<T>(Action<T> listener) where T : class, IHEvent
        {
            return _eventBus.Subscribe(listener);
        }

        public void Unsubscribe(HEventHandle handle)
        {
            _eventBus.Unsubscribe(handle);
        }

        public void Publish<T>(T evt) where T : IHEvent
        {
            _eventBus.Publish(evt);
        }
    }
}