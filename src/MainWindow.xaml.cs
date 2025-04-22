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

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
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
}