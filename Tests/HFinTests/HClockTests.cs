using Feature.Clock;

namespace FeatureTest;

public class HClockTests
{
    [Test]
    public void TestAdd()
    {
        var maxTickOfHalf = 10;
        var clock = new HClock(maxTickOfHalf, maxTickOfHalf);

        var tick1 = Random.Shared.Next(1, maxTickOfHalf);
        clock.AddTicks(tick1);
        Assert.That(clock.TotalTicks, Is.EqualTo(tick1));
        Assert.That(clock.TotalDays, Is.Zero);
        Assert.That(clock.TodayTicks, Is.EqualTo(tick1));
        Assert.That(clock.IsDaytime, Is.True);
        Assert.That(clock.DayTicks, Is.EqualTo(tick1));
        Assert.That(clock.NightTicks, Is.Zero);
        
        var tick2 = maxTickOfHalf - tick1;
        clock.AddTicks(tick2);
        Assert.That(clock.TotalTicks, Is.EqualTo(maxTickOfHalf));
        Assert.That(clock.TotalDays, Is.Zero);
        Assert.That(clock.TodayTicks, Is.EqualTo(maxTickOfHalf));
        Assert.That(clock.IsDaytime, Is.False);
        Assert.That(clock.DayTicks, Is.Zero);
        Assert.That(clock.NightTicks, Is.Zero);
        
        clock.AddTicks(tick2);
        Assert.That(clock.TotalTicks, Is.EqualTo(maxTickOfHalf + tick2));
        Assert.That(clock.TotalDays, Is.Zero);
        Assert.That(clock.TodayTicks, Is.EqualTo(maxTickOfHalf + tick2));
        Assert.That(clock.IsDaytime, Is.False);
        Assert.That(clock.DayTicks, Is.Zero);
        Assert.That(clock.NightTicks, Is.EqualTo(tick2));

        clock.AddTicks(maxTickOfHalf);
        Assert.That(clock.TotalTicks, Is.EqualTo(2 * maxTickOfHalf + tick2));
        Assert.That(clock.TotalDays, Is.EqualTo(1));
        Assert.That(clock.TodayTicks, Is.EqualTo(tick2));
        Assert.That(clock.IsDaytime, Is.True);
        Assert.That(clock.DayTicks, Is.EqualTo(tick2));
        Assert.That(clock.NightTicks, Is.EqualTo(0));

        clock.AddFullDays(1);
        Assert.That(clock.TotalTicks, Is.EqualTo(4 * maxTickOfHalf + tick2));
        Assert.That(clock.TotalDays, Is.EqualTo(2));
        Assert.That(clock.TodayTicks, Is.EqualTo(tick2));
        Assert.That(clock.IsDaytime, Is.True);
        Assert.That(clock.DayTicks, Is.EqualTo(tick2));
        Assert.That(clock.NightTicks, Is.EqualTo(0));

        clock.AddTicks(tick1);
        Assert.That(clock.TotalTicks, Is.EqualTo(5 * maxTickOfHalf));
        Assert.That(clock.TotalDays, Is.EqualTo(2));
        Assert.That(clock.TodayTicks, Is.EqualTo(maxTickOfHalf));
        Assert.That(clock.IsDaytime, Is.False);
        Assert.That(clock.DayTicks, Is.EqualTo(0));
        Assert.That(clock.NightTicks, Is.EqualTo(0));
        
        clock.AddTicks(tick1);
        Assert.That(clock.TotalTicks, Is.EqualTo(5 * maxTickOfHalf + tick1));
        Assert.That(clock.TotalDays, Is.EqualTo(2));
        Assert.That(clock.TodayTicks, Is.EqualTo(maxTickOfHalf + tick1));
        Assert.That(clock.IsDaytime, Is.False);
        Assert.That(clock.DayTicks, Is.EqualTo(0));
        Assert.That(clock.NightTicks, Is.EqualTo(tick1));
    }

    [Test]
    public void TestNext()
    {
        var maxTickOfHalf = 10;
        var clock = new HClock(maxTickOfHalf, maxTickOfHalf);

        clock.NextDayNightCycle();
        Assert.That(clock.IsDaytime, Is.False);
        Assert.That(clock.TotalTicks, Is.EqualTo(maxTickOfHalf));

        clock.NextDay();
        Assert.That(clock.IsDaytime, Is.True);
        Assert.That(clock.TotalTicks, Is.EqualTo(2 * maxTickOfHalf));

        clock.NextDay();
        Assert.That(clock.IsDaytime, Is.True);
        Assert.That(clock.TotalTicks, Is.EqualTo(4 * maxTickOfHalf));

        clock.AddTicks(1).NextDayNightCycle();
        Assert.That(clock.IsDaytime, Is.False);
        Assert.That(clock.TotalTicks, Is.EqualTo(5 * maxTickOfHalf));
    }
}