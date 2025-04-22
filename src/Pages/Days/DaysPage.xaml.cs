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


namespace CondecoAssistant.Pages.Days;

public partial class DaysPage : Page
{
    private readonly List<ToggleButton> dayButtons = new();
    public DaysPage()
    {
        InitializeComponent();

        var prefs = PreferencesStorage.Load();
        MondayButton.IsChecked = prefs.SelectedDays.Contains("Monday");
        TuesdayButton.IsChecked = prefs.SelectedDays.Contains("Tuesday");
        WednesdayButton.IsChecked = prefs.SelectedDays.Contains("Wednesday");
        ThursdayButton.IsChecked = prefs.SelectedDays.Contains("Thursday");
        FridayButton.IsChecked = prefs.SelectedDays.Contains("Friday");
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new HomePage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Condeco Assistant";
    }

    private void DayToggle_Click(object sender, RoutedEventArgs e)
    {
        var prefs = PreferencesStorage.Load();

        var selected = new List<String>();
        if (MondayButton.IsChecked == true)
        {
            selected.Add("Monday");
        }
        if (TuesdayButton.IsChecked == true)
        {
            selected.Add("Tuesday");
        }
        if (WednesdayButton.IsChecked == true)
        {
            selected.Add("Wednesday");
        }
        if (ThursdayButton.IsChecked == true)
        {
            selected.Add("Thursday");
        }
        if (FridayButton.IsChecked == true)
        {
            selected.Add("Friday");
        }

        if (selected.Count > 3)
        {
            ((ToggleButton)sender).IsChecked = false;
            MessageBox.Show("You can only select up to 3 days.");
            return;
        }

        prefs.SelectedDays = selected;
        PreferencesStorage.Save(prefs);
    }
}