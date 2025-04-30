using Microsoft.Win32.TaskScheduler;
using System;
using System.IO;
using System.Diagnostics;

namespace CondecoAssistant.Helpers;
public static class TaskSchedulerHelper
{
    public static void CreateOrUpdateTask()
    {
        using (TaskService ts = new TaskService())
        {
            string TaskName = "CondecoAssistantAutoBooking";
            string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            var existingTask = ts.FindTask(TaskName);
            if(existingTask != null)
            {
                ts.RootFolder.DeleteTask(TaskName);
            }

            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "Automated booking for Condeco Assistant every Wednesday at 12:00am.";

            WeeklyTrigger weeklyTrigger = new WeeklyTrigger
            {
                DaysOfWeek = DaysOfTheWeek.Wednesday,
                StartBoundary = DateTime.Today.AddDays(1).Date.AddHours(0).AddMinutes(10),
                WeeksInterval = 1
            };
            td.Triggers.Add(weeklyTrigger);

            td.Actions.Add(new ExecAction(exePath, "--auto", null));

            td.Principal.RunLevel = TaskRunLevel.Highest;

            ts.RootFolder.RegisterTaskDefinition(TaskName, td);
        }
    }
}