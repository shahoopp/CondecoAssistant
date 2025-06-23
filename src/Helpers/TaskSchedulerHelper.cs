using Microsoft.Win32.TaskScheduler;
using System.IO;

public static class TaskSchedulerHelper
{
    public static void CreateOrUpdateScheduledTask()
    {
        using (TaskService ts = new TaskService())
        {
            const string taskName = "EngageAssistant";

            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "Runs Engage desk booking automation";

            td.Triggers.Add(new WeeklyTrigger
            {
                StartBoundary = DateTime.Today.AddHours(23).AddMinutes(58),
                DaysOfWeek = DaysOfTheWeek.Tuesday,
                Enabled = true
            });

            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string arguments = "--auto"; // 👈 Add this line

            td.Actions.Add(new ExecAction(exePath, arguments, Path.GetDirectoryName(exePath)));

            ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, null, null, TaskLogonType.InteractiveToken);
        }
    }
}
