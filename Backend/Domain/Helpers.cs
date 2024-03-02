﻿namespace domain;

public static class Helpers
{
    public static DateTime RoundToMinutes(this DateTime dateTime)
    {
        var timeSpanOneMinute = TimeSpan.FromMinutes(1);
        return new DateTime((dateTime.Ticks + timeSpanOneMinute.Ticks - 1) / timeSpanOneMinute.Ticks * timeSpanOneMinute.Ticks, dateTime.Kind);
    }

    public static bool EqualsCaseInsensitive(this string stringA, string stringB)
    {
        return string.Equals(stringA, stringB, StringComparison.InvariantCultureIgnoreCase);
    }
}