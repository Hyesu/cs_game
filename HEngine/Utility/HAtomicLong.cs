using System.Threading;

namespace HEngine.Utility
{
    public class HAtomicLong
    {
        private long _value;

        public HAtomicLong(long value)
        {
            _value = value;
        }
        
        public long Get()
        {
            return Interlocked.Read(ref _value);
        }

        public long Increment()
        {
            return Interlocked.Increment(ref _value);
        }

        public long Decrement()
        {
            return Interlocked.Decrement(ref _value);
        }

        public long Add(long value)
        {
            return Interlocked.Add(ref _value, value);
        }

        public long Exchange(long value)
        {
            return Interlocked.Exchange(ref _value, value);
        }
    }
}