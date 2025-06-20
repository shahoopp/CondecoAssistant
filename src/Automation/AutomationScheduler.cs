using System;
using System.Timers;

namespace CondecoAssistant.Automation;
public static class AutomationScheduler
{
    private static System.Timers.Timer? _timer;

    public static void StartRecurring()
    {
        _timer = new System.Timers.Timer(60_000);
        _timer.Elapsed += CheckTime;
        _timer.AutoReset = true;
        _timer.Start();
    }

    private static async void CheckTime(object? sender, ElapsedEventArgs e)
    {
        DateTime now = DateTime.Now;
        if (/*now.DayOfWeek == DayOfWeek.Tuesday && */now.Hour == 23 && now.Minute == 58)
        {
            _timer?.Stop(); // Stop the timer to prevent multiple executions
            await AutomationRunner.RunAsync();
            _timer?.Start(); // restart the timer after execution
        }
    }
}