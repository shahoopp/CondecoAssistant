using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CondecoAssistant.Pages.Home;


namespace CondecoAssistant.Pages.Desks;

public partial class DesksPage : Page
{
    private readonly List<ToggleButton> deskButtons = new();
    private readonly List<ToggleButton> selectedDesks = new();
    public DesksPage()
    {
        InitializeComponent();

        deskButtons = new List<ToggleButton>
        {
            DeskW100,
            DeskW101,
            DeskW102,
            DeskW103,
            DeskW104,
            DeskW105,
            DeskW106,
            DeskW107
        };

        // Load any saved selection here later
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new HomePage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Condeco Assistant";
    }

    private void DeskToggle_Click(object sender, RoutedEventArgs e)
    {
        var button = (ToggleButton)sender;

        if (button.IsChecked == true)
        {
            selectedDesks.Add(button);
        }
        else
        {
            selectedDesks.Remove(button);
        }

        UpdateDeskLabels();
    }
       
    private void UpdateDeskLabels()
    {
        foreach (var desk in deskButtons)
        {
            desk.Content = desk.Name.Replace("Desk", "16-");
        }

        for(int i = 0; i < selectedDesks.Count; i++)
        {
            var button = selectedDesks[i];
            string deskName = button.Name.Replace("Desk", "16-");
            button.Content = $"{deskName}\n#{i + 1}";
        }
    }
}