using System;
using System.Threading.Tasks;
using System.Windows;
using CondecoAssistant.Automation;
using CondecoAssistant.Helpers;

namespace CondecoAssistant
{
    public partial class App : Application
    {
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            TaskSchedulerHelper.CreateOrUpdateScheduledTask();

            if (e.Args.Length > 0 && e.Args[0] == "--auto")
            {
                await AutomationRunner.RunAsync();
                Shutdown();
            }
            else
            {
                var mainWindow = new MainWindow();
                MainWindow = mainWindow;
                mainWindow.Show();
            }
        }
    }
}
