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
using CondecoAssistant.Pages.Information;
using System.Threading.Tasks;

namespace CondecoAssistant.Pages.Home;

public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();

        // Attach event handlers to InfoButtons
        //InfoButton1.Click += (s, e) => ToggleVisibility(LoginInfoText, DaysInfoText, DesksInfoText);
        //InfoButton2.Click += (s, e) => ToggleVisibility(DaysInfoText, LoginInfoText, DesksInfoText);
        //InfoButton3.Click += (s, e) => ToggleVisibility(DesksInfoText, LoginInfoText, DaysInfoText);
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
            //AnimateSlide(LoginPanelTransform, 0);
        }
        else
        {
            showText.Visibility = Visibility.Visible;
            //AnimateSlide(LoginPanelTransform, 0);
            //AnimateFadeIn(showText);
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
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new InformationPage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "App Information";
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

    /*
    private void RunAutomationButton_Click(object sender, RoutedEventArgs e)
    {
        //AutomationScheduler.StartRecurring();
        //MessageBox.Show("Automation will run next Wednesday at 1:00am.", "Automation Scheduled", MessageBoxButton.OK, MessageBoxImage.Information);

    }*/



}