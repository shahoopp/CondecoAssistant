using System;
using System.Collections.Generic;
using System.Globalization;
using CondecoAssistant.Models;
using CondecoAssistant.Helpers;

namespace CondecoAssistant.Helpers;

public static class DateHelper
{
    public static List<string> GetBookingDateStringFromPreferences()
    {
        var prefs = PreferencesStorage.Load();
        var selectedDays = prefs.SelectedDays;

        var twoWeeksFromToday = DateTime.Today.AddDays(7);
        int daysToMonday = ((int)DayOfWeek.Monday - (int)twoWeeksFromToday.DayOfWeek + 7) % 7;
        var bookingWeekStart = twoWeeksFromToday.AddDays(daysToMonday);

        var result = new List<string>();

        foreach (var day in selectedDays)
        {
            var bookingDate = bookingWeekStart;
            switch(day)
            {
                case "Monday":
                    bookingDate = bookingWeekStart;
                    break;
                case "Tuesday":
                    bookingDate = bookingWeekStart.AddDays(1);
                    break;
                case "Wednesday":
                    bookingDate = bookingWeekStart.AddDays(2);
                    break;
                case "Thursday":
                    bookingDate = bookingWeekStart.AddDays(3);
                    break;
                case "Friday":
                    bookingDate = bookingWeekStart.AddDays(4);
                    break;
                default:
                    continue; // Skip invalid days
            }
            string formatted = $"{bookingDate:dddd} {bookingDate.Day} {bookingDate:MMMM} {bookingDate.Year} is available for booking";
            result.Add(formatted);
        }
        return result;
    }
}
