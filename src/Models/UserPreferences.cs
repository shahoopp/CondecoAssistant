namespace CondecoAssistant.Models;

public class UserPreferences
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public List<String> RemainingDesks { get; set; } = new();
}