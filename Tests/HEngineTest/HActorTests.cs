using HEngine.Actor;

namespace HEngineTest;

public class HActorTests
{
    private class TestParentComponent : HActorComponent
    {
        public int TickCount = 0;

        public override void Tick(float dt)
        {
            base.Tick(dt);
            TickCount++;
        }
    }

    private class TestChildComponentA : TestParentComponent
    {
    }

    private class TestChildComponentB : TestParentComponent
    {
    }

    private class TestTickableComponent : HActorComponent
    {
        public int TickCount = 0;
        
        public TestTickableComponent() : base()
        {
            Pref.Tickable = true;
        }

        public override void Tick(float dt)
        {
            base.Tick(dt);
            TickCount++;
        }
    }
    
    //////////////////////////////////////////////////////
    [Test]
    public void TestRegistration()
    {
        var actor = new HActor();

        // 같은 부모를 상속한 서로 다른 child 컴포넌트는 등록할 수 없음
        var compA = actor.RegisterComponent<TestChildComponentA>();
        var compB = actor.RegisterComponent<TestChildComponentB>();
        Assert.That(compA, Is.Not.Null);
        Assert.That(compB, Is.Null);

        // 등록한 컴포넌트가 조회되어야 함
        var found1 = actor.GetComponent<TestChildComponentA>();
        var found2 = actor.GetComponent<TestParentComponent>(); 
        Assert.That(found1, Is.EqualTo(compA));
        Assert.That(found2, Is.EqualTo(compA)); // 등록한 컴포넌트의 부모 타입으로도 조회되어야 함

        // 등록 해제한 컴포넌트는 조회되지 않아야 함
        var result = actor.UnregisterComponent(compA);
        var errResult = actor.UnregisterComponent(compB);
        Assert.That(result, Is.True);
        Assert.That(errResult, Is.False);
        
        var found3 = actor.GetComponent<TestChildComponentA>();
        var found4 = actor.GetComponent<TestParentComponent>();
        Assert.That(found3, Is.Null);
        Assert.That(found4, Is.Null);
    }

    [Test]
    public void TestTickable()
    {
        var actor = new HActor();
        var nonTickableComp = actor.RegisterComponent<TestParentComponent>();
        var tickableComp = actor.RegisterComponent<TestTickableComponent>();
        
        // tickable 속성이 켜진 컴포넌트만 tick되어야 함
        actor.Tick(0.1f);
        
        Assert.That(nonTickableComp.TickCount, Is.EqualTo(0));
        Assert.That(tickableComp.TickCount, Is.EqualTo(1));
    }
}