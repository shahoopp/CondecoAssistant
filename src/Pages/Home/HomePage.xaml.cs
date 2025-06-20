using System.Text;
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

namespace CondecoAssistant.Pages.Home;

public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new LoginPage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Login";
        ((MainWindow)Application.Current.MainWindow).AuthorText.Text = "Condeco Assistant";
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

    private async void RunAutomationLaterButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            MessageBox.Show("Automation will run at 11:59 PM Tuesday.", "Scheduled Automation", MessageBoxButton.OK, MessageBoxImage.Information);
            CondecoAssistant.Automation.AutomationScheduler.StartRecurring();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
