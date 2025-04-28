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
            W100, W101, W107, W108, W109,
            W110, W111, W112, W113, W114, W115, W116, W117, W118, W119,
            W200, W201, W202, W203, W204, W205, W206, W207, W208, W209, W210
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
    }

    private void DeskToggle_Click(object sender, RoutedEventArgs e)
    {
        var button = (ToggleButton)sender;

        if (button.IsChecked == true && !selectedDesks.Contains(button))
        {
            selectedDesks.Add(button);
        }
        else if (button.IsChecked == false)
        {
            selectedDesks.Remove(button);
        }

        if (selectedDesks.Count > 3)
        {
            button.IsChecked = false;
            MessageBox.Show("You can only select up to 3 desks.");
            return;
        }

        SaveDeskSelections();
        UpdateDeskPriorityDisplay(); // Update the priority display
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

        UpdateDeskPriorityDisplay(); // Update the priority display
    }

    private void UpdateDeskPriorityDisplay()
    {
        // Clear the priority display
        Priority1.Text = string.Empty;
        Priority2.Text = string.Empty;
        Priority3.Text = string.Empty;

        // Ensure the selectedDesks list is ordered by the order of selection
        var orderedSelectedDesks = PreferencesStorage.Load().SelectedDesksInPriority
            .Select(name => selectedDesks.FirstOrDefault(d => d.Name == name))
            .Where(d => d != null)
            .ToList();

        // Update the priority display based on the ordered selected desks
        if (orderedSelectedDesks.Count > 0)
            Priority1.Text = orderedSelectedDesks.ElementAtOrDefault(0)?.Name ?? string.Empty;
        if (orderedSelectedDesks.Count > 1)
            Priority2.Text = orderedSelectedDesks.ElementAtOrDefault(1)?.Name ?? string.Empty;
        if (orderedSelectedDesks.Count > 2)
            Priority3.Text = orderedSelectedDesks.ElementAtOrDefault(2)?.Name ?? string.Empty;
    }
}