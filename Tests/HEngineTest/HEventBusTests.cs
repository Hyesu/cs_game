using HEngine.Event;

namespace HEngineTest;

public class HEventBusTests
{
    class TestEventA : IHEvent
    {
    }

    class TestEventB : IHEvent
    {
    }
    
    class TestEventListener
    {
        public int Triggered;
    }
    
    //////////////////////////////////////////
    [Test]
    public void TestPubSub()
    {
        var eventBus = new HEventBus();
        var listener1 = new TestEventListener();
        var listener2 = new TestEventListener();

        // 여러 개의 리스너가 구독
        var handle1 = eventBus.Subscribe<TestEventA>(evt => listener1.Triggered++);
        var handle2 = eventBus.Subscribe<TestEventA>(evt => listener2.Triggered++);
        Assert.That(handle1, Is.Not.Null);
        Assert.That(handle2, Is.Not.Null);

        // 구독하지 않은 이벤트 발생시 아무 통지도 오지 않는다
        eventBus.Publish(new TestEventB());
        Assert.That(listener1.Triggered, Is.Zero);
        Assert.That(listener2.Triggered, Is.Zero);
        
        // 구독 중인 이벤트 발생시 리스너에게 통지가 온다
        eventBus.Publish(new TestEventA());
        Assert.That(listener1.Triggered, Is.EqualTo(1));
        Assert.That(listener2.Triggered, Is.EqualTo(1));
        
        // 한 개의 리스너가 구독 해제하고 이벤트 발생시
        handle2.Dispose();
        eventBus.Publish(new TestEventA());
        Assert.That(listener1.Triggered, Is.EqualTo(2));
        Assert.That(listener2.Triggered, Is.EqualTo(1)); // 해제한 리스너는 이벤트를 받지 못함
    }
}