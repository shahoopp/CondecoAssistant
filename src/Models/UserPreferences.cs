namespace CondecoAssistant.Models;

public class UserPreferences
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public List<String> SelectedDays { get; set; } = new();
    public List<String> SelectedDesksInPriority { get; set; } = new();
}