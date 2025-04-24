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
using CondecoAssistant.Helpers;


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
            W100,
            W101,
            W102,
            W103,
            W104,
            W105,
            W106,
            W107
        };

        LoadSavedDesks();
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new HomePage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Condeco Assistant";
        ((MainWindow)Application.Current.MainWindow).AuthorText.Text = "by Shaheer Lone";

    }

    private void SaveDeskSelections()
    {
        var prefs = PreferencesStorage.Load();
        prefs.SelectedDesksInPriority = selectedDesks.Select(b => b.Name).ToList();
        PreferencesStorage.Save(prefs);

        UpdateSelectedDesksPanel();
    }

    private void DeskToggle_Click(object sender, RoutedEventArgs e)
    {
        var button = (ToggleButton)sender;

        if (button.IsChecked == true && !selectedDesks.Contains(button))
        {
            if(button != null)
                selectedDesks.Add(button);
        }
        else if (button.IsChecked == false)
        {
            selectedDesks.Remove(button);
        }

        if(selectedDesks.Count > 3)
        {
            ((ToggleButton)sender).IsChecked = false;
            MessageBox.Show("You can only select up to 3 days.");
            return;
        }

        SaveDeskSelections();
    }
    private void LoadSavedDesks()
    {
        var prefs = PreferencesStorage.Load();
        foreach (var desk in deskButtons)
        {
            if (prefs.SelectedDesksInPriority.Contains(desk.Name))
            {
                desk.IsChecked = true;
                selectedDesks.Add(desk);
            }
            else
            {
                desk.IsChecked = false;
            }
        }

        UpdateSelectedDesksPanel();
    }

    private void UpdateSelectedDesksPanel()
    {
        SelectedDesksPanel.Children.Clear();

        for (int i = 0; i < selectedDesks.Count; i++)
        {
            var label = new Label
            {
                Content = $"{selectedDesks[i].Content} (#{i + 1})",
                Background = Brushes.LightGray,
                Foreground = Brushes.Black,
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
        }
    }
}