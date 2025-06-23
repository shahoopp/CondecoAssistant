using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CondecoAssistant.Pages.Login;
using CondecoAssistant.Automation;
using System.Threading.Tasks;
using CondecoAssistant.Helpers;

namespace CondecoAssistant.Pages.Home;

public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();

        var prefs = PreferencesStorage.Load();
        FormsLinkTextBox.Text = prefs.FormsLink;
    }

    private void SaveFormsLinkButton_Click(object sender, RoutedEventArgs e)
    {
        string link = FormsLinkTextBox.Text.Trim();
        if (!string.IsNullOrEmpty(link))
        {
            var prefs = PreferencesStorage.Load();
            prefs.FormsLink = link;
            PreferencesStorage.Save(prefs);

            MessageBox.Show("Forms link saved.", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            MessageBox.Show("Please enter a valid link.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private async void RunAutomationNowButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await CondecoAssistant.Automation.AutomationRunner.RunAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void RunAutomationLaterButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            MessageBox.Show("You have select a manual automation." +
                "\n\nAutomation will run at 11:58 PM today." +
                "\n\nPlease do not close the app, sleep, or shut down your laptop." +
                "\n\nEnsure that you have used the MS authenticator today to enable login to Engage.", "Scheduled Automation", MessageBoxButton.OK, MessageBoxImage.Information);
            CondecoAssistant.Automation.AutomationScheduler.StartRecurring();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new LoginPage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Login";
        ((MainWindow)Application.Current.MainWindow).AuthorText.Text = "Engage Assistant";
    }
}
