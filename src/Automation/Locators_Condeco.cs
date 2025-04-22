using Microsoft.Playwright;
using CondecoAssistant.Helpers;
using CondecoAssistant.Models;

namespace CondecoAssistant.Automation;


public static class Locators_Condeco
{
    // Sign in page
    public static ILocator EmailField(IPage page) =>
        page.GetByRole(AriaRole.Textbox, new() { Name = "someone@example.com" });
    public static ILocator SignInPageNextButton(IPage page) =>
        page.GetByRole(AriaRole.Button, new() { Name = "Next" });
    public static ILocator PasswordField(IPage page)
    {
        var prefs = PreferencesStorage.Load();
        string email = prefs.Username ?? "";
        string namePart = email.Contains('@') ? email.Split('@')[0] : email;
        string fieldLabel = $"Enter the password for {namePart}@";
        return page.GetByRole(AriaRole.Textbox, new() { Name = fieldLabel });
    }
    public static ILocator SignInPage_SignInButton(IPage page) =>
    page.GetByRole(AriaRole.Button, new() { Name = "Sign in" });


    // Left Navigation Menu
    public static ILocator PersonalSpacesButton(IPage page) =>
        page.FrameLocator("iframe[name=\"leftNavigation\"]")
            .GetByRole(AriaRole.Button, new() { Name = "Personal Spaces" });

    public static ILocator BookAPersonalSpaceButton(IPage page) =>
        page.FrameLocator("iframe[name=\"leftNavigation\"]")
            .GetByRole(AriaRole.Link, new() { Name = "Book a personal space" });

    // Book a Personal Space page
    public static ILocator CountryDropdown(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByLabel("Country");

    public static ILocator LocationDropdown(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByLabel("Location");

    public static ILocator GroupDropdown(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByLabel("Group");

    public static ILocator FloorDropdown(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByLabel("Floor");

    public static ILocator WorkspaceTypeDropdown(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByText("Workspace type Desk");

    // Booking date(s)

    public static List<ILocator> BookingDates(IPage page)
    {
        var days = DateHelper.GetBookingDayNumbersFromPreferences();
        var locators = new List<ILocator>();
        foreach (var day in days)
        {
            locators.Add(page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
                .GetByText(day.ToString(), new() { Exact = true }));
        }
        return locators;
    }
    
    public static ILocator SearchButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .GetByRole(AriaRole.Button, new() { Name = "Search" });

}