using Microsoft.Playwright;
using CondecoAssistant.Helpers;
using CondecoAssistant.Models;
using System.Windows;

namespace CondecoAssistant.Automation;
public static class Locators
{
    // Sign in
    public static ILocator FormsEmailField(IPage page) =>
        page.GetByRole(AriaRole.Textbox, new() { Name = "Enter your email or phone" });

    public static ILocator FormsNextButton(IPage page) =>
        page.GetByRole(AriaRole.Button, new() { Name = "Next" });

    public static ILocator FormsPasswordField(IPage page)
    {
        var prefs = PreferencesStorage.Load();
        string email = prefs.Username ?? "";
        string namePart = email.Contains('@') ? email.Split('@')[0] : email;
        string fieldLabel = $"Enter the password for {namePart}@";
        return page.GetByRole(AriaRole.Textbox, new() { Name = fieldLabel });
    }

    public static ILocator FormsSignInPage_SignInButton(IPage page) =>
    page.GetByRole(AriaRole.Button, new() { Name = "Sign in" });

    // Forms data download
    public static ILocator DropdownButton(IPage page) =>
    page.GetByRole(AriaRole.Button, new() { Name = "More options", Exact = true });

    public static ILocator DownloadButton(IPage page) =>
        page.GetByRole(AriaRole.Menuitem, new() { Name = "Download a copy" });

    // Engage (Condeco)
    public static ILocator HomePageButton(IPage page) =>
        page.FrameLocator("iframe[name=\"leftNavigation\"]")
            .GetByRole(AriaRole.Link, new() { Name = " Today" });

    // Left Navigation Menu
    public static ILocator PersonalSpacesButton(IPage page) =>
        page.FrameLocator("iframe[name=\"leftNavigation\"]")
            .GetByRole(AriaRole.Button, new() { Name = "Personal Spaces" });

    public static ILocator YourTeamButton(IPage page) =>
        page.FrameLocator("iframe[name=\"leftNavigation\"]")
        .GetByRole(AriaRole.Button, new() { Name = "Your team" });

    public static ILocator FindYourTeamButton(IPage page) =>
        page.FrameLocator("iframe[name=\"leftNavigation\"]")
            .GetByRole(AriaRole.Link, new() { Name = "Find your team" });

    public static ILocator TeamDaysButton(IPage page) =>
        page.FrameLocator("iframe[name=\"leftNavigation\"]")
            .GetByRole(AriaRole.Link, new() { Name = "Team days" });

    public static ILocator CreateTeamDayButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByRole(AriaRole.Button, new() { Name = "Create team day" });

    public static ILocator CalendarButton(IPage page)
    {
        // Format: "Saturday 31 May 2025, Press"
        var tomorrow = DateTime.Today.AddDays(1);
        string formattedDate = tomorrow.ToString("dddd d MMMM yyyy");
        string buttonName = $"{formattedDate}, Press";
        return page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByRole(AriaRole.Button, new() { Name = buttonName });
    }

    public static ILocator SelectMonthButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByLabel("Press space or enter key to select a different month");

    public static ILocator SelectAllCheckbox(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByRole(AriaRole.Checkbox, new() { Name = "Select All" });

    public static ILocator BookingDate(IPage page, string dayName)
    {
        DateTime targetDate = DateHelper.GetBookingDateForDay(dayName);
        string formattedDate = targetDate.ToString("dddd, d MMMM yyyy").ToLower();
        string buttonName = $"{formattedDate}, press";

        //MessageBox.Show($"Booking date for {dayName} is set to {formattedDate}.", "Booking Date", MessageBoxButton.OK, MessageBoxImage.Information);

        return page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
        .GetByRole(AriaRole.Button, new() { Name = buttonName });
    }

    public static List<ILocator> PersonCheckBoxLocators(IPage page, List<string> names)
    {
        var locators = new List<ILocator>();

        foreach (var name in names)
        {
            var regex = new System.Text.RegularExpressions.Regex($"^{System.Text.RegularExpressions.Regex.Escape(name)}\\b");

            var locator = page
                .FrameLocator("iframe[name=\"mainDisplayFrame\"]")
                .GetByRole(AriaRole.Checkbox, new() { NameRegex = regex });

            locators.Add(locator);
        }

        return locators;
    }

    public static ILocator ContinueToBookingButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
        .GetByRole(AriaRole.Button, new() { Name = "Continue" });

    public static ILocator DeskButtonLocator(IPage page, string deskCode)
    {
        // Remove the "16" prefix if present
        var shortDeskCode = deskCode.StartsWith("16-") ? deskCode.Substring(2) : deskCode;

        var regex = new System.Text.RegularExpressions.Regex($"{System.Text.RegularExpressions.Regex.Escape(shortDeskCode)}\\b");

        return page
        .FrameLocator("iframe[name=\"mainDisplayFrame\"]")
        .GetByRole(AriaRole.Button, new() { NameRegex = regex });
    }

    public static ILocator CloseButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByRole(AriaRole.Button, new() { Name = "Close" });

    public static ILocator BookAndSendInvitesButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByRole(AriaRole.Button, new() { Name = "Book & send invites" });

    public static ILocator IHaveEnoughSpaceButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByRole(AriaRole.Button, new() { Name = "I have enough space" });

    public static ILocator DoneButton(IPage page) =>
                page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByRole(AriaRole.Button, new() { Name = "Done" });
    
    
}