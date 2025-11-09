using System.Collections.Immutable;
using HEngine.Core;
using HFin.Cheat;

namespace FeatureTest;

public class CheatSpotlightTests
{
    [Test(Description = "커맨드 추가 - 실패")]
    public void TestAdd_Fail()
    {
        var spotlight = new CheatSpotlight();
        var errAction1 = new CheatAction(null!);
        var errAction2 = new CheatAction(string.Empty);
        var executor = (ImmutableArray<string> args) => HResultCode.Success;

        // 인수 테스트
        {
            var errResult1 = spotlight.Add(errAction1, executor);
            var errResult2 = spotlight.Add(errAction2, executor);
            
            Assert.That(errResult1, Is.EqualTo(HResultCode.InvalidArgument));
            Assert.That(errResult2, Is.EqualTo(HResultCode.InvalidArgument));
        }
    }

    [Test(Description = "커맨드 실행 - 실패")]
    public void TestExecute_Fail()
    {
        var spotlight = new CheatSpotlight();
        var action = new CheatAction("ExpectedCommand");
        var executedCount = 0;
        
        spotlight.Add(action, args =>
        {
            if (1 > args.Length)
            {
                return HResultCode.CheatSpotlightInvalidOption;
            }

            if ("ExpectedArgument" != args[0])
            {
                return HResultCode.CheatSpotlightInvalidOption;
            }

            executedCount++;
            return HResultCode.Success;
        });
        
        // 인수 테스트
        {
            var errResult1 = spotlight.Execute(null!);
            var errResult2 = spotlight.Execute(string.Empty);
            var errResult3 = spotlight.Execute("ErrorCommand ExpectedArgument");
            var errResult4 = spotlight.Execute("ExpectedCommand");
            var errResult5 = spotlight.Execute("ExpectedCommand ErrorArgument");
            
            Assert.That(errResult1, Is.EqualTo(HResultCode.InvalidArgument));
            Assert.That(errResult2, Is.EqualTo(HResultCode.InvalidArgument));
            Assert.That(errResult3, Is.EqualTo(HResultCode.CheatSpotlightNotFoundCommand));
            Assert.That(errResult4, Is.EqualTo(HResultCode.CheatSpotlightInvalidOption));
            Assert.That(errResult5, Is.EqualTo(HResultCode.CheatSpotlightInvalidOption));
        }

        Assert.That(executedCount, Is.Zero);
    }

    [Test]
    public void TestSpotlight()
    {
        var spotlight = new CheatSpotlight();
        var action = new CheatAction("ExpectedCommand");
        var executedCount = 0;

        spotlight.Add(action, (ImmutableArray<string> args) =>
        {
            if (1 > args.Length)
            {
                return HResultCode.CheatSpotlightInvalidOption;
            }

            if ("ExpectedArgument" != args[0])
            {
                return HResultCode.CheatSpotlightInvalidOption;
            }

            executedCount++;
            return HResultCode.Success;
        });

        // act
        var testCount = Random.Shared.Next(3, 10);
        for (int i = 0; i < testCount; i++)
        {
            var result = spotlight.Execute("ExpectedCommand ExpectedArgument");
            Assert.That(result, Is.EqualTo(HResultCode.Success));
        }
        
        Assert.That(executedCount, Is.EqualTo(testCount));
    }
}