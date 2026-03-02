namespace Feature.Clock;

public class HClock
{
    private readonly int _maxTickOfDaytime;
    private readonly int _maxTickOfNight;
    private readonly int _maxTickOfFullDay;

    private long _totalTicks;

    public HClock(int maxTickOfDaytime, int maxTickOfNight, long totalTicks = 0)
    {
        _maxTickOfDaytime = maxTickOfDaytime;
        _maxTickOfNight = maxTickOfNight;
        _maxTickOfFullDay = maxTickOfDaytime + maxTickOfNight;
        _totalTicks = totalTicks;
    }

    public long TotalTicks => 0;
    public long TotalDays => 0;
    public int TodayTicks => 0;

    public bool IsDaytime => false;
    public int DayTicks => 0;
    public int NightTicks => 0;

    public HClock AddTicks(int ticks)
    {
        return this;
    }

    public HClock AddFullDays(int numberOfFullDay)
    {
        return this;
    }

    public HClock NextDayNightCycle()
    {
        return this;
    }

    public HClock NextDay()
    {
        return this;
    }
}