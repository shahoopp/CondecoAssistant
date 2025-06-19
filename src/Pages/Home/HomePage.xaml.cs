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
using CondecoAssistant.Pages.Days;
using CondecoAssistant.Pages.Desks;
using CondecoAssistant.Automation;
using System.Threading.Tasks;

namespace CondecoAssistant.Pages.Home;

public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();
    }

    private void ToggleVisibility(TextBlock showText, TextBlock hideText1, TextBlock hideText2)
    {
        hideText1.Visibility = Visibility.Collapsed;
        hideText2.Visibility = Visibility.Collapsed;
        hideText1.Opacity = 0;
        hideText2.Opacity = 0;

        if (showText.Visibility == Visibility.Visible)
        {
            showText.Visibility = Visibility.Collapsed;
            showText.Opacity = 0;
        }
        else
        {
            showText.Visibility = Visibility.Visible;
        }
    }

    private void AnimateSlide(TranslateTransform transform, double to)
    {
        var animation = new System.Windows.Media.Animation.DoubleAnimation
        {
            To = to,
            Duration = TimeSpan.FromSeconds(2),
            EasingFunction = new System.Windows.Media.Animation.CubicEase { EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut }
        };
        transform.BeginAnimation(TranslateTransform.XProperty, animation);
    }

    private void AnimateFadeIn(UIElement element)
    {
        var fade = new System.Windows.Media.Animation.DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.5),
        };
        element.BeginAnimation(UIElement.OpacityProperty, fade);
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new LoginPage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Login";
        ((MainWindow)Application.Current.MainWindow).AuthorText.Text = "Condeco Assistant";
    }
    private void DaysButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new DaysPage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Days";
        ((MainWindow)Application.Current.MainWindow).AuthorText.Text = "Condeco Assistant";
    }
    private void DesksButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new DesksPage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Desks";
        ((MainWindow)Application.Current.MainWindow).AuthorText.Text = "Condeco Assistant";
    }

    private async void RunAutomationButton_Click(object sender, RoutedEventArgs e)
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

    // AddMonday_Click handler
    private void AddMonday_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement logic for adding Monday
        MessageBox.Show("Monday added!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    // AddTuesday_Click handler
    private void AddTuesday_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement logic for adding Tuesday
        MessageBox.Show("Tuesday added!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    // AddWednesday_Click handler
    private void AddWednesday_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement logic for adding Wednesday
        MessageBox.Show("Wednesday added!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    // AddThursday_Click handler
    private void AddThursday_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement logic for adding Thursday
        MessageBox.Show("Thursday added!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    // AddFriday_Click handler
    private void AddFriday_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement logic for adding Friday
        MessageBox.Show("Friday added!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
