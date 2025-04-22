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
            DeskW100,
            DeskW101,
            DeskW102,
            DeskW103,
            DeskW104,
            DeskW105,
            DeskW106,
            DeskW107
        };

        LoadSavedDesks();
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new HomePage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Condeco Assistant";
    }

    private void SaveDeskSelections()
    {
        var prefs = PreferencesStorage.Load();
        prefs.SelectedDesksInPriority = selectedDesks
            .Select(b => b.Content.ToString()
            .Split('\n')[0])
            .ToList();
        PreferencesStorage.Save(prefs);
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

        SaveDeskSelections();
        UpdateDeskLabels();
    }



    private void LoadSavedDesks()
    {
        var prefs = PreferencesStorage.Load();
        foreach (var desk in deskButtons)
        {
            string deskName = desk.Content.ToString();
            if (prefs.SelectedDesksInPriority.Contains(deskName))
            {
                desk.IsChecked = true;
                selectedDesks.Add(desk);
            }
            else
            {
                desk.IsChecked = false;
            }
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