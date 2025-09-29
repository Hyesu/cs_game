using HEngine.Utility;

namespace Feature.IdGenerator;

public class HIncrementalGenerator : IHIdGenerator
{
    private readonly HAtomicLong _generator = new(0);

    public void Initilaize(long lastUid)
    {
        _generator.Exchange(lastUid);
    }
    
    public long Next()
    {
        return _generator.Increment();
    }
}