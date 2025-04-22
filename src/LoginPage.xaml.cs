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

namespace CondecoAssistant;

public partial class LoginPage : Page
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new HomePage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Condeco Assistant";
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        // Save the login information
        string username = UsernameBox.Text;
        string password = PasswordBox.Password;
        // Here you would typically save the credentials securely
        // For demonstration, we'll just show a message box
        MessageBox.Show($"Username: {username}\nPassword: {password}", "Credentials Saved", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}