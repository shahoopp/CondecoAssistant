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

namespace CondecoAssistant.Pages.Home;

public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();

        // Attach event handlers to InfoButtons
        InfoButton1.Click += (s, e) => ToggleVisibility(LoginInfoText, DaysInfoText, DesksInfoText);
        InfoButton2.Click += (s, e) => ToggleVisibility(DaysInfoText, LoginInfoText, DesksInfoText);
        InfoButton3.Click += (s, e) => ToggleVisibility(DesksInfoText, LoginInfoText, DaysInfoText);
    }

    private void ToggleVisibility(TextBlock textBlock1, TextBlock textBlock2, TextBlock textBlock3)
    {
        // Toggle textBlock1's visibility between Visible and Collapsed
        textBlock1.Visibility = textBlock1.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

        // Collapse the other TextBlocks
        if (textBlock1.Visibility == Visibility.Visible)
        {
            textBlock2.Visibility = Visibility.Collapsed;
            textBlock3.Visibility = Visibility.Collapsed;
        }
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new LoginPage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Login";
    }
    private void DaysButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new DaysPage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Days";
    }
    private void DesksButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new DesksPage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Desks";
    }
}