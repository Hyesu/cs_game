using System;

namespace HEngine.Event;

public class HEventBus
{
    public HEventHandle Subscribe<T>(Action<T> subscriber) where T : IHEvent
    {
        return null;
    }

    public void Publish<T>(T evt) where T : IHEvent
    {
    }
}