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

namespace CondecoAssistant;

public partial class DaysPage : Page
{
    private readonly List<ToggleButton> dayButtons = new();
    public DaysPage()
    {
        InitializeComponent();

        dayButtons = new List<ToggleButton> 
        {   
            MondayButton, 
            TuesdayButton, 
            WednesdayButton, 
            ThursdayButton, 
            FridayButton
        };

        // Load any saved selection here later
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new HomePage());
        ((MainWindow)Application.Current.MainWindow).HeaderText.Text = "Condeco Assistant";
    }

    private void DayToggle_Click(object sender, RoutedEventArgs e)
    {
        var selected = dayButtons.Where(b => b.IsChecked == true).ToList();

        if (selected.Count > 3)
        {
            ((ToggleButton)sender).IsChecked = false;
            MessageBox.Show("You can only select up to 3 days.");
            return;
        }

        var selectedDays = selected.Select(b => b.Content.ToString()).ToList();
        System.Diagnostics.Debug.WriteLine("Selected days: " + string.Join(", ", selectedDays));
    }
}