using System;
using System.Collections.Generic;
using System.Globalization;
using CondecoAssistant.Models;
using CondecoAssistant.Helpers;

namespace CondecoAssistant.Helpers;

public static class DateHelper
{
    public static List<int> GetBookingDayNumbersFromPreferences()
    {
        var prefs = PreferencesStorage.Load();

        if (prefs.SelectedDays == null || prefs.SelectedDays.Count == 0)
        {
            return new List<int>();
        }

        DateTime today = DateTime.Today;

        int daysUntilNextWednesday = ((int)DayOfWeek.Wednesday - (int)today.DayOfWeek + 7) % 7;
        DateTime bookingReferenceWednesday = today.AddDays(daysUntilNextWednesday);

        DateTime bookingWeekStart = bookingReferenceWednesday.AddDays(6 - (int)DayOfWeek.Monday);

        var weekDates = new Dictionary<string, DateTime>
        {
            { "Monday", bookingWeekStart },
            { "Tuesday", bookingWeekStart.AddDays(1) },
            { "Wednesday", bookingWeekStart.AddDays(2) },
            { "Thursday", bookingWeekStart.AddDays(3) },
            { "Friday", bookingWeekStart.AddDays(4) }
        };

        var selectedDayNumbers = new List<int>();

        foreach (var day in prefs.SelectedDays)
        {
            if (weekDates.ContainsKey(day))
            {
                selectedDayNumbers.Add(weekDates[day].Day);
            }
        }

        return selectedDayNumbers;
    }
}
