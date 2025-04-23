using Microsoft.Playwright;
using CondecoAssistant.Helpers;
using CondecoAssistant.Models;

namespace CondecoAssistant.Automation;
public static class Locators
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

    public static ILocator HomePageButton(IPage page) =>
        page.FrameLocator("iframe[name=\"leftNavigation\"]")
            .GetByRole(AriaRole.Link, new() { Name = " Today" });


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

    // Desk selectors
    public static List<ILocator> Desks(IPage page)
    {
        var prefs = PreferencesStorage.Load();
        var desks = prefs.SelectedDesksInPriority;
        var locators = new List<ILocator>();
        foreach (var desk in desks)
        {
            string deskNumber = desk.Substring(3);
            string deskName = $"-{deskNumber} }}Desk!!";

            var locator = page
                .FrameLocator("iframe[name=\"mainDisplayFrame\"]")
                .FrameLocator("iframe[title=\"Floor plan view\"]")
                .GetByRole(AriaRole.Button, new() { Name = deskName });

            locators.Add(locator);
        }
        return locators;
    }

    public static ILocator TestLocator(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "-W102 }}Desk!!" });

    // Book desk button
    public static ILocator BookDeskButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "Book" });

    // OK button
    public static ILocator OKButton(IPage page) =>
        page.FrameLocator("iframe[name=\"mainDisplayFrame\"]")
            .FrameLocator("iframe[title=\"Floor plan view\"]")
            .GetByRole(AriaRole.Button, new() { Name = "OK" });

}