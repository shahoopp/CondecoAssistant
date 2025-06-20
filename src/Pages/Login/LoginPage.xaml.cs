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
using CondecoAssistant.Pages.Home;
using CondecoAssistant.Helpers;

namespace CondecoAssistant.Pages.Login;

public partial class LoginPage : Page
{
    public LoginPage()
    {
        InitializeComponent();

        // Load saved preferences
        var prefs = PreferencesStorage.Load();
        UsernameBox.Text = prefs.Username;
        PasswordBox.Password = prefs.Password;
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new HomePage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Condeco Assistant";
        ((MainWindow)Application.Current.MainWindow).AuthorText.Text = "by Shaheer Lone";

    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var prefs = PreferencesStorage.Load();
        prefs.Username = UsernameBox.Text;
        prefs.Password = PasswordBox.Password;
        PreferencesStorage.Save(prefs);
        MessageBox.Show("Login info saved.");
    }

    /*private void ShowPassWordButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        VisiblePasswordBox.Text = PasswordBox.Password;
        VisiblePasswordBox.Visibility = Visibility.Visible;
        PasswordBox.Visibility = Visibility.Collapsed;
    }

    private void ShowPassWordButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
        VisiblePasswordBox.Visibility = Visibility.Collapsed;
        PasswordBox.Visibility = Visibility.Visible;
    }*/
}