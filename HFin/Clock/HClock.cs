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

    public long TotalTicks => _totalTicks;
    public long TotalDays => _totalTicks / _maxTickOfFullDay;
    public long TodayTicks => _totalTicks - TotalDays * _maxTickOfFullDay;

    public bool IsDaytime => TodayTicks < _maxTickOfDaytime;
    public long DayTicks => IsDaytime ? TodayTicks : 0;
    public long NightTicks => !IsDaytime ? TodayTicks - _maxTickOfDaytime : 0;

    public HClock AddTicks(int ticks)
    {
        _totalTicks += ticks;
        return this;
    }

    public HClock AddFullDays(int numberOfFullDay)
    {
        _totalTicks += numberOfFullDay * _maxTickOfFullDay;
        return this;
    }

    public HClock NextDayNightCycle()
    {
        if (IsDaytime)
        {
            _totalTicks += _maxTickOfDaytime - DayTicks;
        }
        else
        {
            _totalTicks += _maxTickOfNight - NightTicks;
        }
        return this;
    }

    public HClock NextDay()
    {
        _totalTicks += _maxTickOfFullDay - TodayTicks;
        return this;
    }
}