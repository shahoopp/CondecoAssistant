using Microsoft.Win32.TaskScheduler;
using System.IO;


public static class TaskSchedulerHelper
{
    public static void CreateOrUpdateScheduledTask()
    {
        using (TaskService ts = new TaskService())
        {
            const string taskName = "CondecoDeskBooking";

            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "Runs CondecoAssistant automation";

            td.Triggers.Add(new WeeklyTrigger
            {
                StartBoundary = DateTime.Today.AddHours(23).AddMinutes(59),
                DaysOfWeek = DaysOfTheWeek.Tuesday,
                Enabled = true
            });

            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            td.Actions.Add(new ExecAction(exePath, null, Path.GetDirectoryName(exePath)));

            // This will override the existing task if it exists
            ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, null, null, TaskLogonType.InteractiveToken);
        }
    }

}
