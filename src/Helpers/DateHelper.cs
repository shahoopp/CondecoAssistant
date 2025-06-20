using System;
using System.Collections.Generic;
using System.Globalization;
using CondecoAssistant.Models;
using CondecoAssistant.Helpers;

namespace CondecoAssistant.Helpers;

public static class DateHelper
{
    public static DateTime GetBookingDateForDay(string dayName)
    {
        var prefs = PreferencesStorage.Load();
        var twoWeeksFromToday = DateTime.Today.AddDays(7);
        int daysToMonday = ((int)DayOfWeek.Monday - (int)twoWeeksFromToday.DayOfWeek + 7) % 7;
        var bookingWeekStart = twoWeeksFromToday.AddDays(daysToMonday);

        return dayName switch
        {
            "Monday" => bookingWeekStart,
            "Tuesday" => bookingWeekStart.AddDays(1),
            "Wednesday" => bookingWeekStart.AddDays(2),
            "Thursday" => bookingWeekStart.AddDays(3),
            "Friday" => bookingWeekStart.AddDays(4),
            _ => throw new ArgumentException($"Invalid day name: {dayName}")
        };
    }

}
