using System;
using System.Threading.Tasks;
using System.Windows;
using CondecoAssistant.Automation;
using CondecoAssistant.Helpers;

namespace CondecoAssistant
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Optional: create a scheduled task if it doesn't exist
            TaskSchedulerHelper.CreateOrUpdateScheduledTask();

            // Wait 2 minutes before starting automation
            //await Task.Delay(TimeSpan.FromMinutes(2));

            // Run the automation
            //await AutomationRunner.RunAsync();

            // Optional: shut down the app after automation completes
            //Current.Shutdown();
        }
    }
}
