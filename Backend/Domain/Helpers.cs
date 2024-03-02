namespace domain;

public static class Helpers
{
    public static DateTime RoundToMinutes(this DateTime dateTime)
    {
        var timeSpanOneMinute = TimeSpan.FromMinutes(1);
        return new DateTime((dateTime.Ticks + timeSpanOneMinute.Ticks - 1) / timeSpanOneMinute.Ticks * timeSpanOneMinute.Ticks, dateTime.Kind);
    }
}