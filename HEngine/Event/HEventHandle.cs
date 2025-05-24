using System;

namespace HEngine.Event
{
    public class HEventHandle : IDisposable
    {
        public readonly long Id;
        public readonly int EventHash;
        private readonly HEventBus _bus;
        
        private bool _isDisposed;
    
        public HEventHandle(long id, int eventHash, HEventBus bus)
        {
            Id = id;
            EventHash = eventHash;
            _bus = bus;
        }
    
        public void Dispose()
        {
            if (_isDisposed)
                return;
        
            _bus.Unsubscribe(this);
        }
    }   
}