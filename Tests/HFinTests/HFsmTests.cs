using Feature.Fsm;

namespace FeatureTest;

public class HFsmTests : FeatureTestBase
{
    private static int RandomInt(int min = 0, int max = 0)
    {
        // TODO: 랜덤 라이브러리 구현 필요함
        var rnd = new Random();

        if (0 == min && 0 == max)
        {
            return rnd.Next();
        }

        return rnd.Next(min, max);
    }
    
    class TestState : HFsmState
    {
        public int Triggered { get; set; }
        public int SuccessCount { get; set; }
        public int FailCount { get; set; }

        protected override void OnEnter()
        {
            Triggered++;
        }

        protected override void OnUpdate(float dt)
        {
            // TODO: 랜덤 라이브러리 구현 필요함
            var isSuccess = RandomInt() % 2 == 0;
            if (isSuccess)
            {
                SuccessCount++;
                SetTransition(HFsmTransition.Success);
            }
            else
            {
                FailCount++;
                SetTransition(HFsmTransition.Fail);
            }
        }
    }

    class FailState : HFsmState
    {
        public int EnteredCount { get; set; }
        public int ExitedCount { get; set; }

        protected override void OnEnter()
        {
            EnteredCount++;
        }

        protected override void OnExit()
        {
            ExitedCount++;
        }
    }

    class SuccessState : HFsmState
    {
        public int EnteredCount { get; set; }
        public int ExitedCount { get; set; }

        protected override void OnEnter()
        {
            EnteredCount++;
        }

        protected override void OnExit()
        {
            ExitedCount++;
        }
    }

    //////////////////////////////////////////////
    [Test]
    public void TestFsmMachine()
    {
        // arrange - 간단한 성공/실패에 대한 fsm 구성
        // Test --> Success --> Test
        //      --> Fail    -->
        var testState = new TestState();
        var failState = new FailState();
        var successState = new SuccessState();
        
        var machine = new HFsmMachine();
        machine.AddState(testState, true);
        machine.AddTransition(testState, HFsmTransition.Success, successState);
        machine.AddTransition(testState, HFsmTransition.Fail, successState);
        
        machine.AddState(failState);
        machine.AddTransition(failState, HFsmTransition.None, testState);
        
        machine.AddState(successState);
        machine.AddTransition(successState, HFsmTransition.None, testState);

        machine.Start();
        
        // act - 랜덤하게 업데이트하고나서 기댓값 확인
        var tryCnt = RandomInt(10, 30);
        for (int i = 0; i < tryCnt; i++)
        {
            machine.Update(0.1f);
        }
        
        // assert - 트랜지션에 따라 상태가 전환되었는지 확인
        Assert.That(testState.Triggered, Is.EqualTo(tryCnt + 1)); // +1은 initial state로서의 트리거 횟수
        Assert.That(testState.SuccessCount, Is.EqualTo(successState.EnteredCount));
        Assert.That(testState.FailCount, Is.EqualTo(failState.EnteredCount));
        
        // assesrt - enter와 exit가 짝이 맞게 호출되었는지 확인
        Assert.That(successState.EnteredCount, Is.EqualTo(successState.ExitedCount));
        Assert.That(failState.EnteredCount, Is.EqualTo(failState.ExitedCount));
    }
}